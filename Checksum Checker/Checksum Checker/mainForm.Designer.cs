namespace Checksum_Checker
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.textboxFilePath = new System.Windows.Forms.TextBox();
			this.labelFilePath = new System.Windows.Forms.Label();
			this.textboxComputedHash = new System.Windows.Forms.TextBox();
			this.labelComputedHash = new System.Windows.Forms.Label();
			this.textboxHashToCompare = new System.Windows.Forms.TextBox();
			this.labelHashToCompare = new System.Windows.Forms.Label();
			this.buttonOpenFileDialog = new System.Windows.Forms.Button();
			this.buttonGetHash = new System.Windows.Forms.Button();
			this.buttonCompareHash = new System.Windows.Forms.Button();
			this.computeFileHash = new System.ComponentModel.BackgroundWorker();
			this.statusStripMain = new System.Windows.Forms.StatusStrip();
			this.toolstripprogressbarComputeProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.toolstripTimeElapsedTitle = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolstripTimeElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolstripDivider = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolstripProgressTitle = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolstripProgressCurrentByteCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolstripProgressVisualDivider = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolstripProgressTotalByteCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.pollCurrentBytesCompleted = new System.Windows.Forms.Timer(this.components);
			this.timeElapsed = new System.Windows.Forms.Timer(this.components);
			this.buttonCancelHash = new System.Windows.Forms.Button();
			this.comboboxSelectHashType = new System.Windows.Forms.ComboBox();
			this.labelSelectHashType = new System.Windows.Forms.Label();
			this.statusStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// textboxFilePath
			// 
			this.textboxFilePath.Location = new System.Drawing.Point(12, 25);
			this.textboxFilePath.Name = "textboxFilePath";
			this.textboxFilePath.ReadOnly = true;
			this.textboxFilePath.Size = new System.Drawing.Size(434, 20);
			this.textboxFilePath.TabIndex = 0;
			// 
			// labelFilePath
			// 
			this.labelFilePath.AutoSize = true;
			this.labelFilePath.Location = new System.Drawing.Point(12, 9);
			this.labelFilePath.Name = "labelFilePath";
			this.labelFilePath.Size = new System.Drawing.Size(26, 13);
			this.labelFilePath.TabIndex = 2;
			this.labelFilePath.Text = "File:";
			// 
			// textboxComputedHash
			// 
			this.textboxComputedHash.Location = new System.Drawing.Point(12, 122);
			this.textboxComputedHash.Name = "textboxComputedHash";
			this.textboxComputedHash.ReadOnly = true;
			this.textboxComputedHash.Size = new System.Drawing.Size(434, 20);
			this.textboxComputedHash.TabIndex = 0;
			// 
			// labelComputedHash
			// 
			this.labelComputedHash.AutoSize = true;
			this.labelComputedHash.Location = new System.Drawing.Point(12, 106);
			this.labelComputedHash.Name = "labelComputedHash";
			this.labelComputedHash.Size = new System.Drawing.Size(86, 13);
			this.labelComputedHash.TabIndex = 2;
			this.labelComputedHash.Text = "Computed Hash:";
			// 
			// textboxHashToCompare
			// 
			this.textboxHashToCompare.Location = new System.Drawing.Point(12, 171);
			this.textboxHashToCompare.Name = "textboxHashToCompare";
			this.textboxHashToCompare.Size = new System.Drawing.Size(434, 20);
			this.textboxHashToCompare.TabIndex = 0;
			// 
			// labelHashToCompare
			// 
			this.labelHashToCompare.AutoSize = true;
			this.labelHashToCompare.Location = new System.Drawing.Point(12, 155);
			this.labelHashToCompare.Name = "labelHashToCompare";
			this.labelHashToCompare.Size = new System.Drawing.Size(92, 13);
			this.labelHashToCompare.TabIndex = 2;
			this.labelHashToCompare.Text = "Hash to Compare:";
			// 
			// buttonOpenFileDialog
			// 
			this.buttonOpenFileDialog.Location = new System.Drawing.Point(452, 23);
			this.buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			this.buttonOpenFileDialog.Size = new System.Drawing.Size(95, 23);
			this.buttonOpenFileDialog.TabIndex = 3;
			this.buttonOpenFileDialog.Text = "Open File";
			this.buttonOpenFileDialog.UseVisualStyleBackColor = true;
			this.buttonOpenFileDialog.Click += new System.EventHandler(this.buttonOpenFileDialog_Click);
			// 
			// buttonGetHash
			// 
			this.buttonGetHash.Location = new System.Drawing.Point(452, 120);
			this.buttonGetHash.Name = "buttonGetHash";
			this.buttonGetHash.Size = new System.Drawing.Size(95, 23);
			this.buttonGetHash.TabIndex = 3;
			this.buttonGetHash.Text = "Get Hash";
			this.buttonGetHash.UseVisualStyleBackColor = true;
			this.buttonGetHash.Click += new System.EventHandler(this.buttonGetHash_Click);
			// 
			// buttonCompareHash
			// 
			this.buttonCompareHash.Location = new System.Drawing.Point(452, 169);
			this.buttonCompareHash.Name = "buttonCompareHash";
			this.buttonCompareHash.Size = new System.Drawing.Size(95, 23);
			this.buttonCompareHash.TabIndex = 3;
			this.buttonCompareHash.Text = "Compare Hash";
			this.buttonCompareHash.UseVisualStyleBackColor = true;
			this.buttonCompareHash.Click += new System.EventHandler(this.buttonCompareHash_Click);
			// 
			// computeFileHash
			// 
			this.computeFileHash.WorkerReportsProgress = true;
			this.computeFileHash.WorkerSupportsCancellation = true;
			this.computeFileHash.DoWork += new System.ComponentModel.DoWorkEventHandler(this.computeFileHash_DoWork);
			this.computeFileHash.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.computeFileHash_ProgressChanged);
			this.computeFileHash.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.computeFileHash_RunWorkerCompleted);
			// 
			// statusStripMain
			// 
			this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripprogressbarComputeProgress,
            this.toolstripTimeElapsedTitle,
            this.toolstripTimeElapsedTime,
            this.toolstripDivider,
            this.toolstripProgressTitle,
            this.toolstripProgressCurrentByteCount,
            this.toolstripProgressVisualDivider,
            this.toolstripProgressTotalByteCount});
			this.statusStripMain.Location = new System.Drawing.Point(0, 205);
			this.statusStripMain.Name = "statusStripMain";
			this.statusStripMain.Size = new System.Drawing.Size(559, 22);
			this.statusStripMain.SizingGrip = false;
			this.statusStripMain.TabIndex = 4;
			this.statusStripMain.Text = "statusStripMain";
			// 
			// toolstripprogressbarComputeProgress
			// 
			this.toolstripprogressbarComputeProgress.Name = "toolstripprogressbarComputeProgress";
			this.toolstripprogressbarComputeProgress.Size = new System.Drawing.Size(150, 16);
			this.toolstripprogressbarComputeProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// toolstripTimeElapsedTitle
			// 
			this.toolstripTimeElapsedTitle.Name = "toolstripTimeElapsedTitle";
			this.toolstripTimeElapsedTitle.Size = new System.Drawing.Size(80, 17);
			this.toolstripTimeElapsedTitle.Text = "Time Elapsed:";
			// 
			// toolstripTimeElapsedTime
			// 
			this.toolstripTimeElapsedTime.Name = "toolstripTimeElapsedTime";
			this.toolstripTimeElapsedTime.Size = new System.Drawing.Size(49, 17);
			this.toolstripTimeElapsedTime.Text = "00:00:00";
			// 
			// toolstripDivider
			// 
			this.toolstripDivider.Name = "toolstripDivider";
			this.toolstripDivider.Size = new System.Drawing.Size(10, 17);
			this.toolstripDivider.Text = "|";
			// 
			// toolstripProgressTitle
			// 
			this.toolstripProgressTitle.Name = "toolstripProgressTitle";
			this.toolstripProgressTitle.Size = new System.Drawing.Size(98, 17);
			this.toolstripProgressTitle.Text = "Bytes Computed:";
			// 
			// toolstripProgressCurrentByteCount
			// 
			this.toolstripProgressCurrentByteCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolstripProgressCurrentByteCount.Name = "toolstripProgressCurrentByteCount";
			this.toolstripProgressCurrentByteCount.Size = new System.Drawing.Size(44, 17);
			this.toolstripProgressCurrentByteCount.Text = "0 Bytes";
			// 
			// toolstripProgressVisualDivider
			// 
			this.toolstripProgressVisualDivider.Name = "toolstripProgressVisualDivider";
			this.toolstripProgressVisualDivider.Size = new System.Drawing.Size(12, 17);
			this.toolstripProgressVisualDivider.Text = "/";
			// 
			// toolstripProgressTotalByteCount
			// 
			this.toolstripProgressTotalByteCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolstripProgressTotalByteCount.Name = "toolstripProgressTotalByteCount";
			this.toolstripProgressTotalByteCount.Size = new System.Drawing.Size(44, 17);
			this.toolstripProgressTotalByteCount.Text = "0 Bytes";
			// 
			// pollCurrentBytesCompleted
			// 
			this.pollCurrentBytesCompleted.Interval = 250;
			this.pollCurrentBytesCompleted.Tick += new System.EventHandler(this.pollCurrentBytesCompleted_Tick);
			// 
			// timeElapsed
			// 
			this.timeElapsed.Interval = 1000;
			this.timeElapsed.Tick += new System.EventHandler(this.timeElapsed_Tick);
			// 
			// buttonCancelHash
			// 
			this.buttonCancelHash.Location = new System.Drawing.Point(452, 120);
			this.buttonCancelHash.Name = "buttonCancelHash";
			this.buttonCancelHash.Size = new System.Drawing.Size(95, 23);
			this.buttonCancelHash.TabIndex = 5;
			this.buttonCancelHash.Text = "Cancel";
			this.buttonCancelHash.UseVisualStyleBackColor = true;
			this.buttonCancelHash.Visible = false;
			this.buttonCancelHash.Click += new System.EventHandler(this.buttonCancelHash_Click);
			// 
			// comboboxSelectHashType
			// 
			this.comboboxSelectHashType.FormattingEnabled = true;
			this.comboboxSelectHashType.Location = new System.Drawing.Point(12, 73);
			this.comboboxSelectHashType.Name = "comboboxSelectHashType";
			this.comboboxSelectHashType.Size = new System.Drawing.Size(535, 21);
			this.comboboxSelectHashType.TabIndex = 6;
			this.comboboxSelectHashType.SelectionChangeCommitted += new System.EventHandler(this.comboboxSelectHashType_SelectionChangeCommitted);
			// 
			// labelSelectHashType
			// 
			this.labelSelectHashType.AutoSize = true;
			this.labelSelectHashType.Location = new System.Drawing.Point(12, 57);
			this.labelSelectHashType.Name = "labelSelectHashType";
			this.labelSelectHashType.Size = new System.Drawing.Size(95, 13);
			this.labelSelectHashType.TabIndex = 2;
			this.labelSelectHashType.Text = "Select Hash Type:";
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(559, 227);
			this.Controls.Add(this.comboboxSelectHashType);
			this.Controls.Add(this.statusStripMain);
			this.Controls.Add(this.buttonCompareHash);
			this.Controls.Add(this.buttonOpenFileDialog);
			this.Controls.Add(this.labelHashToCompare);
			this.Controls.Add(this.labelComputedHash);
			this.Controls.Add(this.labelSelectHashType);
			this.Controls.Add(this.labelFilePath);
			this.Controls.Add(this.textboxHashToCompare);
			this.Controls.Add(this.textboxComputedHash);
			this.Controls.Add(this.textboxFilePath);
			this.Controls.Add(this.buttonGetHash);
			this.Controls.Add(this.buttonCancelHash);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "mainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Checksum Checker";
			this.statusStripMain.ResumeLayout(false);
			this.statusStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox textboxFilePath;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.TextBox textboxComputedHash;
        private System.Windows.Forms.Label labelComputedHash;
        private System.Windows.Forms.TextBox textboxHashToCompare;
        private System.Windows.Forms.Label labelHashToCompare;
        private System.Windows.Forms.Button buttonOpenFileDialog;
        private System.Windows.Forms.Button buttonGetHash;
        private System.Windows.Forms.Button buttonCompareHash;
		private System.ComponentModel.BackgroundWorker computeFileHash;
		private System.Windows.Forms.StatusStrip statusStripMain;
		private System.Windows.Forms.ToolStripStatusLabel toolstripProgressTotalByteCount;
		private System.Windows.Forms.ToolStripProgressBar toolstripprogressbarComputeProgress;
		private System.Windows.Forms.ToolStripStatusLabel toolstripProgressTitle;
		private System.Windows.Forms.ToolStripStatusLabel toolstripProgressCurrentByteCount;
		private System.Windows.Forms.ToolStripStatusLabel toolstripProgressVisualDivider;
		private System.Windows.Forms.Timer pollCurrentBytesCompleted;
		private System.Windows.Forms.ToolStripStatusLabel toolstripTimeElapsedTitle;
		private System.Windows.Forms.ToolStripStatusLabel toolstripDivider;
		private System.Windows.Forms.ToolStripStatusLabel toolstripTimeElapsedTime;
		private System.Windows.Forms.Timer timeElapsed;
		private System.Windows.Forms.Button buttonCancelHash;
		private System.Windows.Forms.ComboBox comboboxSelectHashType;
		private System.Windows.Forms.Label labelSelectHashType;
    }
}

