using CSharpAppPlayground.UIClasses;

namespace CSharpAppPlayground.MediaParsers
{
    public partial class FormMediaParser : FormWithRichText
    {
        public FormMediaParser()
        {
            InitializeComponent();
        }

        public void EnableFolderButtons()
        {
            btnParseMediaFolder.Enabled = true;
            DisableFileButtons();
        }
        public void DisableFolderButtons()
        {
            btnParseMediaFolder.Enabled = false;
        }
        public void EnableFileButtons()
        {
            btnParseMediaFile.Enabled = true;
            DisableFolderButtons();
        }
        public void DisableFileButtons()
        {
            btnParseMediaFile.Enabled = false;
        }

        private void btnFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
                EnableFolderButtons();
            }
        }

        private void btnFileBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // For single selection: get full file path
                txtFolderPath.Text = openFileDialog1.FileName;
                // multi-select, you can use openFileDialog1.FileNames (string[])
                // Example: txtFolderPath.Text = string.Join(";", openFileDialog1.FileNames);
                EnableFileButtons();
            }
        }

        private void btnParseMediaFolder_Click(object sender, EventArgs e)
        {

        }

        private void btnParseMediaFile_Click(object sender, EventArgs e)
        {

        }
    }
}
