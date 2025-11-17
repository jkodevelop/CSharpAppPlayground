using CSharpAppPlayground.FilesFolders.Files;
using CSharpAppPlayground.MediaParsers;
using CSharpAppPlayground.UIClasses;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CSharpAppPlayground.FilesFolders
{
    public partial class FormFilesFolders : FormWithRichText
    {
        private CancellationTokenSource cts;
        private bool cancelTokenIsDisposed = true;

        private bool isProcessing = false;
        private bool isFolderMode = true;

        BookmarkParsersBenchmark bookmarkBenchmark;

        public FormFilesFolders()
        {
            InitializeComponent();
            this.FormClosing += FormFilesFolders_FormClosing;
            bookmarkBenchmark = new BookmarkParsersBenchmark(this);
        }

        public void EnableFolderButtons()
        {
            btnFileCount.Enabled = true;
            btnStorage.Enabled = true;
            btnCountAndStorage.Enabled = true;
            DisableFileButtons();
            isFolderMode = true;
        }
        public void DisableFolderButtons()
        {
            btnFileCount.Enabled = false;
            btnStorage.Enabled = false;
            btnCountAndStorage.Enabled = false;
        }
        public void EnableFileButtons()
        {
            btnParseBookmark.Enabled = true;
            DisableFolderButtons();
            isFolderMode = false;
        }
        public void DisableFileButtons()
        {
            btnParseBookmark.Enabled = false;
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

        private void FormFilesFolders_FormClosing(object sender, FormClosingEventArgs e)
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
                txtFolderPath.Text = openFileDialog1.FileName;
                EnableFileButtons();
            }
        }

        private CountFilesFolders countFilesFolders = new CountFilesFolders();
        private async void btnFileCount_Click(object sender, EventArgs e)
        {
            if (isProcessing)
            {
                Debug.Print("btnFileCount_Click: Something is already running.");
                return;
            }
            isProcessing = true;
            CancellationToken cancelToken = GenCancelToken();

            await countFilesFolders.TestPerformanceAsync(txtFolderPath.Text, cancelToken);

            CancelAndDisposeToken();
            isProcessing = false;
        }

        private CountStorage countStorage = new CountStorage();
        private async void btnStorage_Click(object sender, EventArgs e)
        {
            if (isProcessing)
            {
                Debug.Print("btnStorage_Click: Something is already running.");
                return;
            }
            isProcessing = true;
            CancellationToken cancelToken = GenCancelToken();

            await countStorage.TestPerformanceAsync(txtFolderPath.Text, cancelToken);

            CancelAndDisposeToken();
            isProcessing = false;
        }

        private CountFilesFoldersStorage countAll = new CountFilesFoldersStorage();
        private async void btnCountAndStorage_Click(object sender, EventArgs e)
        {
            if (isProcessing)
            {
                Debug.Print("btnStorage_Click: Something is already running.");
                return;
            }
            isProcessing = true;
            CancellationToken cancelToken = GenCancelToken();

            await countAll.TestPerformanceAsync(txtFolderPath.Text, cancelToken);

            CancelAndDisposeToken();
            isProcessing = false;
        }

        private void btnDriveInfo_Click(object sender, EventArgs e)
        {
            if (txtFolderPath.Text == string.Empty || txtFolderPath.Text.Length < 2 || txtFolderPath.Text[1] != ':')
            {
                Debug.Print("Please select a proper folder path first.");
                DriveCheck.CheckAllDriveInfo();
                // DriveCheck.PrintAllDriveDetails();
                return;
            }

            string driveName = txtFolderPath.Text.Substring(0, 2); // Extract drive letter, e.g., "C:"

            DriveCheck.PrintDriveInfo(new DriveInfo(driveName));

            if (DriveHelper.IsDriveSSD(driveName))
                Debug.Print($"Drive {driveName} is SSD");
            else
                Debug.Print($"Drive {driveName} is HDD");
        }

        private void btnParseBookmark_Click(object sender, EventArgs e)
        {
            string bookmarkFilePath = txtFolderPath.Text;
            bookmarkBenchmark.RunBenchmarks(bookmarkFilePath);
        }

        private void btnTestQuery_Click(object sender, EventArgs e)
        {
            if (!isFolderMode && tbQuery.Text != string.Empty)
            {
                string bookmarkFilePath = txtFolderPath.Text;
                bookmarkBenchmark.QueryTest(bookmarkFilePath, tbQuery.Text);
            }
            else 
            {
                Debug.Print("Query test skipped. File is not specified | Query: is empty");
            }
                
        }
    }
}
