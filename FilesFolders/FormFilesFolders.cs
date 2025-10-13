using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.FilesFolders
{
    public partial class FormFilesFolders : FormWithRichText
    {
        public FormFilesFolders()
        {
            InitializeComponent();
        }

        public void EnableButtons()
        {
            btnFileCount.Enabled = true;
        }

        private void btnFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
                EnableButtons();
            }
        }

        private CountFilesFolders countFilesFolders = new CountFilesFolders();
        private void btnFileCount_Click(object sender, EventArgs e)
        {
            int fileCount = 0;
            int folderCount = 0;
            // countFilesFolders.CountFilesAndFolders(txtFolderPath.Text, out fileCount, out folderCount);

            countFilesFolders.TestPerformance(txtFolderPath.Text);

            Debug.Print(txtFolderPath.Text);
            Debug.Print($"Files: {fileCount}, Folders: {folderCount}");
        }
    }
}
