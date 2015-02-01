using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;						// For use in using the Stream class to read the users selected file
using System.Security.Cryptography;		// For use to gain access to the Hash, MD5 and SHA1 classes
using System.Text.RegularExpressions;	// For use in checking the validity of hashes to be compared

namespace Checksum_Checker {

	// -------------------------
	// Project extended on from the YouTube tutorial:
	//	"C# How To MD5 Hash a File And Show Progress" by BetterCoder; See https://www.youtube.com/watch?v=9MJAUL7G49w
	// -----
	// I have not used the example he provided, I worked from scratch and just implemented the code where applicable
	//	making sure to understand WHY as I go, there is no point otherwise. Heavily commented for that exact reason. :D
	//	That and I have explored and extended the project a lot further than in his original video. :)
	// -------------------------

	// TODO -----
	//	- Compare MD5 hash to computed hash and return Yes/No
	//	- Add SHA1 support
	//	- Enable / Disable "Get Hash" and "Compare Hash" buttons dynamically on file loaded and hash calculated
	//	- Add checkbox for "Get and Compare Hash" or just "Get Hash"
	//	- Save user preference on default loading hash alorithm
	// -----

    public partial class mainForm : Form {
		// Declaring the class mainForm, that inherits from the base class Form
		//	The partial keyword means that this class is only partly defined in this source file, and the compiler must look elsewhere
		//	for the remainder of the definition (in this case it will be some automatically generated code which defines amongst
		//	others all the controls you placed on your form in the designer)
		//	See http://stackoverflow.com/a/4438334

        public mainForm() {
			// Form object is created

            InitializeComponent();
			// Call for initialising the form
			//	It is a required call in all subclasses of Form and it instructs all the components on the form to
			//	initialise, position, and display themselves as appropriate.

            this.Text = "Checksum Checker V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // Sets the Form Title Text plus the current version of the program
		}

		string filePath;
		// Global String variable to place the file path into (in this case the entire file path also)

		string computedMD5Hash;
		string computedSHA1Hash;
		// Global Strings to store the relevant computed hash values

		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
		// A string array of byte size suffixes for use in the SizeSuffix function

		int secondsPassed = 0;
		// Global variable for the amount of seconds passed for the time elapsed tool strip label, toolstripTimeElapsedTime

		long totalBytesRead = 0;
		// Global long variable to store the total count of the bytes read in the computeFileHash_DoWork functions do-while loop

		OpenFileDialog openFD = new OpenFileDialog();
		// Creates a global new OpenFileDialog instance openFD

		private bool isMD5Hash(string hash, bool result = false) {
			// Checks via regex if the given MD5 Hexadecimal hash is valid

			Regex regex = new Regex("[0-9a-fA-F]{32}");
			// Create a new Regex object with the regex pattern
			// See http://stackoverflow.com/questions/1715434/is-there-a-way-to-test-if-a-string-is-an-md5-hash#comment1592955_1715468 for regex pattern

			Match match = regex.Match(hash);
			// Create a new instance of match and check the regex pattern against the submitted Hexadecimal MD5 hash

			if (match.Success) result = true;
			// If the match is a success change the result to true

			return result;
			// Return the boolean state
		}

		private static string SizeSuffix(Int64 value) {
			// A function that provides easy string conversion for byte sizes, eg. bytes to KB, etc...
			//	See http://stackoverflow.com/a/14488941

			if (value < 0) { return "-" + SizeSuffix(-value); }
			if (value == 0) { return "0.0 bytes"; }

			int mag = (int)Math.Log(value, 1024);
			decimal adjustedSize = (decimal)value / (1L << (mag * 10));

			return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
		}

		private static string makeHashString(byte[] hashBytes) {
			// Takes the created hash, as a byte array, and returns the hash, converted into a string

			StringBuilder hash = new StringBuilder(32);
			// Creates a new StringBuilder, hash, with the initial capactity of 32, which is the length of an MD5 hash
			//	Lengths:
			//	MD5  = 32
			//	SHA1 = 40

			foreach (byte b in hashBytes) {
				// Foreach of the bytes in hashBytes as new variable b
				hash.Append(b.ToString("x2"));
				// Convert the byte into a lowercase hexidecimal and append that to the StringBuilder, hash
			}

			return hash.ToString();
			// Return the converted hash as a string
		}

		private void resetForm() {
			// Place all things that need to be reset when the user calculates a new hash
			
			totalBytesRead = 0;
			// Reset the total bytes read amount - crucial that this is done

			toolstripprogressbarComputeProgress.Value = 0;
			// Reset the progressbar to zero

			secondsPassed = 0;
			// Reset the seconds passed to zero

			computedMD5Hash = "";
			// Reset the computed hash variable

			textboxComputedHash.Text = "";
			// Reset the computed hash textbox

			toolstripProgressCurrentByteCount.Text = "0 Bytes";
			// Reset the current byte count in the tool strip

			toolstripTimeElapsedTime.Text = "00:00:00";
			// Reset the current elapsed time in the tool strip

			return;
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e) {
			// Actions for when the button, buttonOpenFileDialog, is clicked

            if (openFD.ShowDialog() == DialogResult.OK) {
				// Runs this code if the user has selected and hit the OK button in the OpenFileDialog

                textboxFilePath.Text = filePath = openFD.FileName;
				// Set the text of the textboxFilePath to the user selected file path
				//	Also set the (string) variable filePath to that of the file path
            }

			return;
        }

		private void buttonGetHash_Click(object sender, EventArgs e) {
			// Actions for when the button, buttonGetHash, is clicked

			if (filePath != null) {
				// If filePath is set, meaning the user has selected a file to hash, then continue

				if (!computeFileHash.IsBusy) {
					// If the worker isn't already processing a computeFileHash task, then continue

					resetForm();
					// Resets form fields that, well, need to be reset

					buttonGetHash.Enabled = false;
					buttonGetHash.Visible = false;
					// Disable and hide the get hash button

					buttonCancelHash.Enabled = true;
					buttonCancelHash.Visible = true;
					// Enable and show the cancel hash button

					computeFileHash.RunWorkerAsync();
					// Proceed to the computeFileHash_DoWork function below to start computing the relevant hash on a new Thread

					pollCurrentBytesCompleted.Start();
					//	Start a timer in which the totalBytesRead is polled and the tool strip label,
					//	toolstripProgressCurrentByteCount, text is updated to reflect the new value

					timeElapsed.Start();
					// Start a timer in which counts up the total time the hash task has been running, it also updates
					//	the tool strip label, toolstripTimeElapsedTime, to reflect the new time
				}
				else {
					MessageBox.Show("Please wait until the current hash has finished calculating.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					// If the worker is already processing a task, show the user an error
				}

			}
			else {
				MessageBox.Show("Please select a file first!","Warning!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				// If filePath is not set then show the user an error, as they have not selected a file to hash
			}

			return;
		}

		private void buttonCancelHash_Click(object sender, EventArgs e) {
			// Actions for when the button, buttonCancelHash, is clicked

			if (computeFileHash.IsBusy) {
				// If there is a running operation for the background worker computeFileHash...

				computeFileHash.CancelAsync();
				// ... Cancel the hash calculation operation
			}
			else {
				MessageBox.Show("No hash calculation is currently running to terminate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				// Otherwise return an error message stating that the user cannot terminate an operation that does not exist!
			}

			return;
		}

		private void buttonCompareHash_Click(object sender, EventArgs e) {
			// Actions for when the button, buttonCompareHash, is clicked

			if (isMD5Hash(textboxHashToCompare.Text)) {
				// Send the content of the text box, textboxHashToCompare, to the isMD5Hash function to check if the user
				//	has entered a valid MD5 hash

				if (textboxHashToCompare.Text == computedMD5Hash) {
					MessageBox.Show("Success!\n\nThey are a match, it seems you have the correct file!\n\n:D", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					// Computed and Compared MD5 Hashes are the same
				}
				else {
					MessageBox.Show("Warning!\n\nThese hashes do not match, you may have the wrong file or it has been sabotaged!\n\nI'd suggest re-downloading it directly from the source and trying again.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					// Computed and Compared MD5 Hashes are NOT the same
				}
			}
			else {
				MessageBox.Show("Not a valid Hexadecimal MD5 Hash. Please try again.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				// If they don't match, tell the user that
			}
		}

		private void computeFileHash_DoWork(object sender, DoWorkEventArgs e) {
			// Start the process of computing the hash in the background

			byte[] buffer;
			// New byte array for the buffer to read parts of the user selected file into and hash those parts

			int bytesRead;
			// The amount of bytes read as the loop iterates below

			long size;
			// The size of the users file to be hashed in bytes

			using (Stream file = File.OpenRead(filePath)) {
				// Create a new Stream, file, from the users selected file from the global filePath variable

				size = file.Length;
				// Set size to the user selected file length (size)

				toolstripProgressTotalByteCount.Text = SizeSuffix(size);
				// Sets the tool strip label, toolstripProgressTotalByteCount, to the value to size
				//	Used for showing the user progression of the computation alongside the progress bar

				using (HashAlgorithm hasher = MD5.Create()) {
					// Create a new HashAlgorithm, hasher, of the type MD5 to do the hashing of the users selected file

					do {
						// See explanation below while... below

						if (computeFileHash.CancellationPending) {
							// Checks if the background worker, computeFileHash, has been told to cancel

							e.Cancel = true;
							// Cancel the background worker, computeFileHash

							return;
						}

						buffer = new byte[4096];
						// Initialise the buffer to a new byte array, to the size of 4096 bytes

						bytesRead = file.Read(buffer, 0, buffer.Length);
						// Read from the file - Read will return the amount of bytes read, so bytesRead gets updated with that amount
						//	Arg1 - the buffer to read the file into - in this case the buffer setup just above
						//	Arg2 - the buffer byte offset - where to start putting the file into the buffer, in this case the beginning, so start at zero
						//	Arg3 - how many bytes to read - get the buffers length that we setup just above

						totalBytesRead += bytesRead;
						// Update the total for the amount of bytes read so far

						hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
						// Creates the MD5 hash value for the bytes just read into the buffer, buffer, above
						//	Arg1 - the input buffer to hash - in this case the buffer, buffer, in which the file was read into above
						//	Arg2 - the input buffer byte offset - the offset to start reading the file from, in this case the beginning, so start at zero
						//	Arg3 - the number of bytes to hash - we calculated this into the variable bytesRead above, so pass that as the argument
						//	Arg4 - place the hash into an output buffer - dont need that, so pass null
						//	Arg5 - the byte offset for the output buffer - set to zero as we dont need the offset, in both aspects

						computeFileHash.ReportProgress((int)((double)totalBytesRead / size * 100));
						// ReportProgress calls the background workers ProgressChanged function, in this case the
						//	computeFileHash_ProgressChanged function below with the supplied argument being
						//	an integer of the percentage of the file currently being hashed
					}
					while (bytesRead != 0);
					// Keep looping over the files buffer until the bytesRead count equals zero, meaning there are no more bytes to be read
					//	and the file has been processed and can move on to the full hash generation next

					hasher.TransformFinalBlock(buffer, 0, 0);
					// Transform the final block, put all the hashes together to make the full hash
					//	Arg1 - the input buffer - so just pass the buffer, buffer, again
					//	Arg2 - the input buffer byte offset - again, do not need the offset, start at the beginning, so set to zero
					//	Arg3 - the byte count - the amount of bytes from buffer to be read, in this case zero, as we didn't read any new bytes

					e.Result = makeHashString(hasher.Hash);
					// Set the Result argument of the DoWorkEventArgs to the computed hash value
					//	It is run through the custom makeHashString function to first convert the computed byte
					//	array into a string before sending it on its merry way
				}
			}
		}

		private void computeFileHash_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			// Called in the do-while loop in the computeFileHash_DoWork function by ReportProgress

			toolstripprogressbarComputeProgress.Value = e.ProgressPercentage;
			// Set the progress bars value, toolstripprogressbarComputeProgress, in the statusStrip of mainForm to the current
			//	compute progress of the hash. Getting the current progress percentage from the argument sent to the function
			//	in the do-while loop in the computeFileHash_DoWork function
		}

		private void computeFileHash_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			// Called when the backgroundWorker has finished the given task; be it successfull, cancelled or errored

			if (e.Error != null) {
				// Operation errored

				MessageBox.Show("Error:\n\n" + e.Error.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				// Display the received error to the user
			}
			else if (e.Cancelled) {
				// Operation was cancelled

				textboxComputedHash.Text = "Hash calculation cancelled by user.";
				// Set the text of the textbox, textboxComputedHash, to the cancellation message
			}
			else {
				// Operation completed successfully

				textboxComputedHash.Text = computedMD5Hash = e.Result.ToString();
				// Set the text of the textbox, textboxComputedHash, to the computed MD5 hash value
			}
			// Found out this state reporting at the link below
			//	See http://stackoverflow.com/a/5921494

			pollCurrentBytesCompleted.Stop();
			// Stop the polling timer of the totalBytesRead variable

			timeElapsed.Stop();
			// Stop the time elapsed timer

			buttonCancelHash.Enabled = false;
			buttonCancelHash.Visible = false;
			// Disable & hide buttonCancelHash

			buttonGetHash.Enabled = true;
			buttonGetHash.Visible = true;
			// Enable & show buttonGetHash

			toolstripProgressCurrentByteCount.Text = SizeSuffix(totalBytesRead);
			// Update the total for the amount of bytes read for the user to see on the tool strip label, toolstripProgressCurrentByteCount
		}

		private void pollCurrentBytesCompleted_Tick(object sender, EventArgs e) {
			// Polls the totalBytesRead variable and updates the tool strip label, toolstripProgressCurrentByteCount, to reflect the new value

			toolstripProgressCurrentByteCount.Text = SizeSuffix(totalBytesRead);
			// Update the total for the amount of bytes read for the user to see on the tool strip label, toolstripProgressCurrentByteCount
		}

		private void timeElapsed_Tick(object sender, EventArgs e) {
			// Updates the Elapsed Time tool strip label, toolstripTimeElapsedTime, with the new time elapsed every second

			secondsPassed += 1;
			// Increment the elapsed seconds by 1

			TimeSpan t = TimeSpan.FromSeconds(secondsPassed);
			// Create a new TimeSpan object, t, from the seconds variable, secondsPassed

			toolstripTimeElapsedTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
			// Set the tool strip label, toolstripTimeElapsedTime, to the formatted time string from the new TimeSpan object, t
			//	See http://stackoverflow.com/a/463668
			//	Also see https://msdn.microsoft.com/en-us/library/dwhawy9k.aspx for format strings
		}

    }
}
