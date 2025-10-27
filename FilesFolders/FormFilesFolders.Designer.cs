﻿namespace CSharpAppPlayground.FilesFolders
{
    partial class FormFilesFolders
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
            btnFileCount = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnFolderBrowse = new Button();
            txtFolderPath = new TextBox();
            btnStorage = new Button();
            btnCountAndStorage = new Button();
            btnDriveInfo = new Button();
            btnReadFile = new Button();
            btnFileBrowse = new Button();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnFileCount
            // 
            btnFileCount.Enabled = false;
            btnFileCount.Location = new Point(12, 81);
            btnFileCount.Name = "btnFileCount";
            btnFileCount.Size = new Size(117, 30);
            btnFileCount.TabIndex = 0;
            btnFileCount.Text = "File Count";
            btnFileCount.UseVisualStyleBackColor = true;
            btnFileCount.Click += btnFileCount_Click;
            // 
            // btnFolderBrowse
            // 
            btnFolderBrowse.Location = new Point(12, 12);
            btnFolderBrowse.Name = "btnFolderBrowse";
            btnFolderBrowse.Size = new Size(64, 23);
            btnFolderBrowse.TabIndex = 3;
            btnFolderBrowse.Text = "Folder";
            btnFolderBrowse.UseVisualStyleBackColor = true;
            btnFolderBrowse.Click += btnFolderBrowse_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(82, 12);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.ScrollBars = ScrollBars.Horizontal;
            txtFolderPath.Size = new Size(341, 23);
            txtFolderPath.TabIndex = 4;
            // 
            // btnStorage
            // 
            btnStorage.Enabled = false;
            btnStorage.Location = new Point(135, 81);
            btnStorage.Name = "btnStorage";
            btnStorage.Size = new Size(117, 30);
            btnStorage.TabIndex = 5;
            btnStorage.Text = "Storage";
            btnStorage.UseVisualStyleBackColor = true;
            btnStorage.Click += btnStorage_Click;
            // 
            // btnCountAndStorage
            // 
            btnCountAndStorage.Enabled = false;
            btnCountAndStorage.Location = new Point(12, 127);
            btnCountAndStorage.Name = "btnCountAndStorage";
            btnCountAndStorage.Size = new Size(240, 37);
            btnCountAndStorage.TabIndex = 6;
            btnCountAndStorage.Text = "File Count + Storage";
            btnCountAndStorage.UseVisualStyleBackColor = true;
            btnCountAndStorage.Click += btnCountAndStorage_Click;
            // 
            // btnDriveInfo
            // 
            btnDriveInfo.Location = new Point(12, 403);
            btnDriveInfo.Name = "btnDriveInfo";
            btnDriveInfo.Size = new Size(128, 35);
            btnDriveInfo.TabIndex = 7;
            btnDriveInfo.Text = "Drive Info";
            btnDriveInfo.UseVisualStyleBackColor = true;
            btnDriveInfo.Click += btnDriveInfo_Click;
            // 
            // btnReadFile
            // 
            btnReadFile.Enabled = false;
            btnReadFile.Location = new Point(285, 81);
            btnReadFile.Name = "btnReadFile";
            btnReadFile.Size = new Size(208, 30);
            btnReadFile.TabIndex = 8;
            btnReadFile.Text = "Read File (todo)";
            btnReadFile.UseVisualStyleBackColor = true;
            btnReadFile.Click += btnReadFile_Click;
            // 
            // btnFileBrowse
            // 
            btnFileBrowse.Location = new Point(429, 11);
            btnFileBrowse.Name = "btnFileBrowse";
            btnFileBrowse.Size = new Size(64, 23);
            btnFileBrowse.TabIndex = 9;
            btnFileBrowse.Text = "File";
            btnFileBrowse.UseVisualStyleBackColor = true;
            btnFileBrowse.Click += btnFileBrowse_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 64);
            label1.Name = "label1";
            label1.Padding = new Padding(100, 0, 100, 0);
            label1.Size = new Size(240, 15);
            label1.TabIndex = 10;
            label1.Text = "Folder";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(285, 62);
            label2.Name = "label2";
            label2.Padding = new Padding(90, 0, 90, 0);
            label2.Size = new Size(205, 15);
            label2.TabIndex = 11;
            label2.Text = "File";
            // 
            // FormFilesFolders
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnFileBrowse);
            Controls.Add(btnReadFile);
            Controls.Add(btnDriveInfo);
            Controls.Add(btnCountAndStorage);
            Controls.Add(btnStorage);
            Controls.Add(txtFolderPath);
            Controls.Add(btnFolderBrowse);
            Controls.Add(btnFileCount);
            Name = "FormFilesFolders";
            Text = "FormFilesFolders";
            Controls.SetChildIndex(btnFileCount, 0);
            Controls.SetChildIndex(btnFolderBrowse, 0);
            Controls.SetChildIndex(txtFolderPath, 0);
            Controls.SetChildIndex(btnStorage, 0);
            Controls.SetChildIndex(btnCountAndStorage, 0);
            Controls.SetChildIndex(btnDriveInfo, 0);
            Controls.SetChildIndex(btnReadFile, 0);
            Controls.SetChildIndex(btnFileBrowse, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnFileCount;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnFolderBrowse;
        private TextBox txtFolderPath;
        private Button btnStorage;
        private Button btnCountAndStorage;
        private Button btnDriveInfo;
        private Button btnReadFile;
        private Button btnFileBrowse;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Label label2;
    }
}