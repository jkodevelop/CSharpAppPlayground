namespace CSharpAppPlayground.MediaParsers
{
    partial class FormMediaParser
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
            txtFolderPath = new TextBox();
            btnFolderBrowse = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnParseMediaFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            btnFileBrowse = new Button();
            btnParseMediaFolder = new Button();
            btnLibCheck = new Button();
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            SuspendLayout();
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(12, 41);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.ScrollBars = ScrollBars.Horizontal;
            txtFolderPath.Size = new Size(473, 23);
            txtFolderPath.TabIndex = 6;
            // 
            // btnFolderBrowse
            // 
            btnFolderBrowse.Location = new Point(12, 12);
            btnFolderBrowse.Name = "btnFolderBrowse";
            btnFolderBrowse.Size = new Size(130, 23);
            btnFolderBrowse.TabIndex = 5;
            btnFolderBrowse.Text = "Folder Browse";
            btnFolderBrowse.UseVisualStyleBackColor = true;
            btnFolderBrowse.Click += btnFolderBrowse_Click;
            // 
            // btnParseMediaFile
            // 
            btnParseMediaFile.Enabled = false;
            btnParseMediaFile.Location = new Point(148, 88);
            btnParseMediaFile.Name = "btnParseMediaFile";
            btnParseMediaFile.Size = new Size(130, 27);
            btnParseMediaFile.TabIndex = 7;
            btnParseMediaFile.Text = "Parse Media File";
            btnParseMediaFile.UseVisualStyleBackColor = true;
            btnParseMediaFile.Click += btnParseMediaFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFileBrowse
            // 
            btnFileBrowse.Location = new Point(148, 12);
            btnFileBrowse.Name = "btnFileBrowse";
            btnFileBrowse.Size = new Size(130, 23);
            btnFileBrowse.TabIndex = 8;
            btnFileBrowse.Text = "File Browse";
            btnFileBrowse.UseVisualStyleBackColor = true;
            btnFileBrowse.Click += btnFileBrowse_Click;
            // 
            // btnParseMediaFolder
            // 
            btnParseMediaFolder.Enabled = false;
            btnParseMediaFolder.Location = new Point(12, 88);
            btnParseMediaFolder.Name = "btnParseMediaFolder";
            btnParseMediaFolder.Size = new Size(130, 27);
            btnParseMediaFolder.TabIndex = 9;
            btnParseMediaFolder.Text = "Parse Media Folder";
            btnParseMediaFolder.UseVisualStyleBackColor = true;
            btnParseMediaFolder.Click += btnParseMediaFolder_Click;
            // 
            // btnLibCheck
            // 
            btnLibCheck.Location = new Point(12, 403);
            btnLibCheck.Name = "btnLibCheck";
            btnLibCheck.Size = new Size(141, 35);
            btnLibCheck.TabIndex = 10;
            btnLibCheck.Text = "Lib Check";
            btnLibCheck.UseVisualStyleBackColor = true;
            btnLibCheck.Click += btnLibCheck_Click;
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Location = new Point(168, 403);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(75, 23);
            videoView1.TabIndex = 11;
            videoView1.Text = "videoView1";
            // 
            // FormMediaParser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(videoView1);
            Controls.Add(btnLibCheck);
            Controls.Add(btnParseMediaFolder);
            Controls.Add(btnFileBrowse);
            Controls.Add(btnParseMediaFile);
            Controls.Add(txtFolderPath);
            Controls.Add(btnFolderBrowse);
            Name = "FormMediaParser";
            Text = "FormMediaParser";
            Controls.SetChildIndex(btnFolderBrowse, 0);
            Controls.SetChildIndex(txtFolderPath, 0);
            Controls.SetChildIndex(btnParseMediaFile, 0);
            Controls.SetChildIndex(btnFileBrowse, 0);
            Controls.SetChildIndex(btnParseMediaFolder, 0);
            Controls.SetChildIndex(btnLibCheck, 0);
            Controls.SetChildIndex(videoView1, 0);
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFolderPath;
        private Button btnFolderBrowse;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnParseMediaFile;
        private OpenFileDialog openFileDialog1;
        private Button btnFileBrowse;
        private Button btnParseMediaFolder;
        private Button btnLibCheck;
        private LibVLCSharp.WinForms.VideoView videoView1;
    }
}