using CSharpAppPlayground.UIClasses;
using System.Diagnostics;

namespace CSharpAppPlayground.MediaParsers
{
    public partial class FormMediaParser : FormWithRichText
    {
        protected MediaLibChecker checker = new MediaLibChecker();

        private CancellationTokenSource cts;
        private bool cancelTokenIsDisposed = true;

        private bool isProcessing = false;
        private bool isFolderMode = true;

        public FormMediaParser()
        {
            InitializeComponent();
            this.FormClosing += FormMediaParser_FormClosing;
        }

        public void EnableFolderButtons()
        {
            btnParseMediaFolder.Enabled = true;
            DisableFileButtons();
            isFolderMode = true;
        }
        public void DisableFolderButtons()
        {
            btnParseMediaFolder.Enabled = false;
        }
        public void EnableFileButtons()
        {
            btnParseMediaFile.Enabled = true;
            DisableFolderButtons();
            isFolderMode = false;
        }
        public void DisableFileButtons()
        {
            btnParseMediaFile.Enabled = false;
        }

        private CancellationToken GenCancelToken()
        {
            cts = new CancellationTokenSource();
            cancelTokenIsDisposed = false;
            return cts.Token;
        }

        private void CancelAndDisposeToken()
        {
            if (cts != null && !cancelTokenIsDisposed)
            {
                //Debug.Print("CancelAndDisposeToken: Cancelling token.");
                cts.Cancel();
                cts.Dispose();
                cancelTokenIsDisposed = true;
                Debug.Print("CancelAndDisposeToken: Token disposed.\n");
            }
        }

        private void FormMediaParser_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelAndDisposeToken();
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

        MediaFileParser fileParser = new MediaFileParser();
        private void btnParseMediaFile_Click(object sender, EventArgs e)
        {
            MediaMeta metaData = fileParser.GetFileMetaData(txtFolderPath.Text);
            Debug.Print($"{metaData.ToString()}");
        }

        private async void btnLibCheck_Click(object sender, EventArgs e)
        {
            if(txtFolderPath.Text == string.Empty)
            {
                Debug.Print("Set a file or folder with media in there");
                return;
            }
            if (isProcessing)
            {
                Debug.Print("btnLibCheck: soemthing is running already");
                return;
            }
            isProcessing = true;

            if (isFolderMode)
            {
                CancellationToken cancelToken = GenCancelToken();
                // await checker.TestPerformanceAsync_Folder(txtFolderPath.Text, cancelToken);
            }
            else
            {
                CancellationToken cancelToken = GenCancelToken();
                await checker.TestPerformanceAsync_File(txtFolderPath.Text, cancelToken);
            }
            CancelAndDisposeToken();
            isProcessing = false;
        }
    }
}
