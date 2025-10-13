namespace CSharpAppPlayground.FilesFolders
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
            SuspendLayout();
            // 
            // btnFileCount
            // 
            btnFileCount.Enabled = false;
            btnFileCount.Location = new Point(12, 62);
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
            btnFolderBrowse.Size = new Size(75, 23);
            btnFolderBrowse.TabIndex = 3;
            btnFolderBrowse.Text = "Browse";
            btnFolderBrowse.UseVisualStyleBackColor = true;
            btnFolderBrowse.Click += btnFolderBrowse_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(93, 12);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.ScrollBars = ScrollBars.Horizontal;
            txtFolderPath.Size = new Size(396, 23);
            txtFolderPath.TabIndex = 4;
            // 
            // btnStorage
            // 
            btnStorage.Enabled = false;
            btnStorage.Location = new Point(135, 62);
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
            btnCountAndStorage.Location = new Point(12, 108);
            btnCountAndStorage.Name = "btnCountAndStorage";
            btnCountAndStorage.Size = new Size(240, 30);
            btnCountAndStorage.TabIndex = 6;
            btnCountAndStorage.Text = "File Count + Storage";
            btnCountAndStorage.UseVisualStyleBackColor = true;
            btnCountAndStorage.Click += btnCountAndStorage_Click;
            // 
            // FormFilesFolders
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}