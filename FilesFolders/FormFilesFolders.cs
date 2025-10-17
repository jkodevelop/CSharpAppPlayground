using CSharpAppPlayground.UIClasses;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CSharpAppPlayground.FilesFolders
{
    public partial class FormFilesFolders : FormWithRichText
    {
        private CancellationTokenSource cts;
        private bool cancelTokenIsDisposed = true;

        private bool somethingRunning = false;
        public FormFilesFolders()
        {
            InitializeComponent();
            this.FormClosing += FormFilesFolders_FormClosing;
        }

        public void EnableButtons()
        {
            btnFileCount.Enabled = true;
            btnStorage.Enabled = true;
            btnCountAndStorage.Enabled = true;
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
                EnableButtons();
            }
        }

        private CountFilesFolders countFilesFolders = new CountFilesFolders();
        private async void btnFileCount_Click(object sender, EventArgs e)
        {
            if (somethingRunning)
            {
                Debug.Print("btnFileCount_Click: Something is already running.");
                return;
            }
            somethingRunning = true;
            CancellationToken cancelToken = GenCancelToken();

            await countFilesFolders.TestPerformanceAsync(txtFolderPath.Text, cancelToken);

            CancelAndDisposeToken();
            somethingRunning = false;
        }

        private CountStorage countStorage = new CountStorage();
        private async void btnStorage_Click(object sender, EventArgs e)
        {
            if (somethingRunning)
            {
                Debug.Print("btnStorage_Click: Something is already running.");
                return;
            }
            somethingRunning = true;
            CancellationToken cancelToken = GenCancelToken();

            await countStorage.TestPerformanceAsync(txtFolderPath.Text, cancelToken);

            CancelAndDisposeToken();
            somethingRunning = false;
        }

        private void btnCountAndStorage_Click(object sender, EventArgs e)
        {

        }

        private void btnDriveInfo_Click(object sender, EventArgs e)
        {
            if(txtFolderPath.Text == string.Empty || txtFolderPath.Text.Length < 2 || txtFolderPath.Text[1] != ':')
            {
                Debug.Print("Please select a proper folder path first.");
                DriveCheck.CheckAllDriveInfo();
                return;
            } 

            string driveName = txtFolderPath.Text.Substring(0, 2); // Extract drive letter, e.g., "C:"
           
            //DriveCheck.isHDD(driveName);
            //DriveCheck.PrintAllDriveDetails();

            if (DriveHelper.IsDriveSSD(driveName))                 
                Debug.Print($"Drive {driveName} is SSD");
            else
                Debug.Print($"Drive {driveName} is HDD");
        }
    }
}
