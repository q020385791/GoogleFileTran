using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GoogleFileTran
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //取得檔案名稱時候，背景工作用來將相關資訊也加入
        public List<ListNameAndID> InfoFolder = new List<ListNameAndID>();

        //Google drive的Server權限
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive File Save and Load";
        DriveService service = new DriveService();

        private void Form1_Load(object sender, EventArgs e)
        {
            UserCredential credential;

            //取得憑證
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            //建立Server溝通
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        //取得目前在硬碟上的檔案清單
        private void btnGetFolder_Click(object sender, EventArgs e)
        {
            labStatus.Text = "查詢中 請稍後";
            listBox1.Items.Clear();
            ThreadStart t = new ThreadStart(GetFolder);
            Thread BackThread = new Thread(t);
            BackThread.ApartmentState = System.Threading.ApartmentState.STA;
            BackThread.Start();

        }
        //上傳資料夾檔案，為壓縮檔
        private void btnUpLoadFolder_Click(object sender, EventArgs e)
        {
            ThreadStart t = new ThreadStart(UpLoadFolder);
            Thread BackThread = new Thread(t);
            BackThread.ApartmentState = System.Threading.ApartmentState.STA;
            BackThread.Start();
        }
        //刪除在雲端硬碟上的檔案
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {
                int Selecteditem = listBox1.SelectedIndex;
                //string SelectedName = listBox1.GetItemText(listBox1.SelectedItem);

                ListNameAndID SelectedObj = InfoFolder[listBox1.SelectedIndex];
                listBox1.SelectedIndex = Selecteditem - 1;
                InfoFolder.RemoveAt(Selecteditem);
                listBox1.Items.RemoveAt(Selecteditem);

                service.Files.Delete(SelectedObj.FileID).Execute();
                btnGetFolder_Click(sender, e);
            }
            else
            {
                labStatus.Text = "未選取檔案";
            }

        }
        //開啟瀏覽器瀏覽
        private void btnGoUrl_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            proc.EnableRaisingEvents = false;

            //Here you can also specify a html page on local machine

            //such as C:\Test\default.html

            proc.StartInfo.FileName = txtUrl.Text;

            proc.Start();
        }
        //上傳單一檔案
        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            ThreadStart t = new ThreadStart(uploadFile);
            Thread BackThread = new Thread(t);
            BackThread.ApartmentState = System.Threading.ApartmentState.STA;
            BackThread.Start();
        }
        //上傳單一檔案
        public void uploadFile()
        {
            string FileName = "";
            string FilePath = "";
            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = OpenDialog.FileName;
                FileName = Path.GetFileName(OpenDialog.FileName);
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = FileName
            };
            try
            {
                FilesResource.CreateMediaUpload request;
                UpdateUI("上傳中 請稍後", labStatus);
                using (var stream = new System.IO.FileStream(FilePath,
                                        System.IO.FileMode.Open))
                {
                    string contentType = MimeMapping.GetMimeMapping(FilePath);
                    request = service.Files.Create(
                        fileMetadata, stream, contentType);
                    request.Fields = "id,webViewLink,createdTime";
                    request.Upload();
                }
                var file = request.ResponseBody;
                UpdateUI("上傳完成   時間：" + file.CreatedTime.ToString(), labStatus);
                UpdateUI(file.WebViewLink, txtUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("錯誤："+ex.Message);
            }
            btnGetFolderClickThread();

        }

        #region 跨執行緒更新UI的Text
        private delegate void UpdateUICallBack(string value, Control ctl);
        private void UpdateUI(string value, Control ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateUICallBack uu = new UpdateUICallBack(UpdateUI);
                this.Invoke(uu, value, ctl);
            }
            else
            {
                ctl.Text = value;
            }
        }
        #endregion

        #region 跨執行緒增加ListBox的項目
        private delegate void AddListBoxCallBack(string value, ListBox ctl);
        private void AddListBox(string value, ListBox ctl)
        {
            if (InvokeRequired)
            {
                AddListBoxCallBack uu = new AddListBoxCallBack(AddListBox);
                this.Invoke(uu, value, ctl);
            }
            else
            {
                ctl.Items.Add(value);
            }
        }
        #endregion
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                try
                {
                    string SelectedName = listBox1.GetItemText(listBox1.SelectedItem);
                    ListNameAndID SelectedObj = InfoFolder[listBox1.SelectedIndex];
                    txtUrl.Text = SelectedObj.WebViewLink;
                    labStatus.Text = "上傳時間" + SelectedObj.Time;
                }
                catch (Exception r)
                {
                    Console.WriteLine(r.Message);
                }
            }
        }

        //上傳資料夾
        public void UpLoadFolder()
        {
            string FileName = "";
            string FilePath = "";
            FolderBrowserDialog BrowserDialog = new FolderBrowserDialog();
            if (BrowserDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = BrowserDialog.SelectedPath;
                FileName = Path.GetFileName(FilePath);
            }
            try
            {
                UpdateUI("壓檔中 請稍後", labStatus);
                ZipFile.CreateFromDirectory(FilePath, FilePath + ".zip");

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = FileName,
                    MimeType = "application/zip"
                };
                UpdateUI("上傳中 請稍後", labStatus);
                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(FilePath + ".zip",
                                        System.IO.FileMode.Open))
                {
                    string contentType = MimeMapping.GetMimeMapping(FilePath + ".zip");
                    request = service.Files.Create(
                        fileMetadata, stream, contentType);
                    request.Fields = "id,webViewLink,createdTime";
                    request.Upload();
                }

                var file = request.ResponseBody;
                UpdateUI("上傳完成   時間：" + file.CreatedTime.ToString(), labStatus);
                Console.WriteLine("File ID: " + file.Id + file.WebViewLink + file.CreatedTime);
                Google.Apis.Drive.v3.Data.Permission permission = new Google.Apis.Drive.v3.Data.Permission();
                permission.Type = "anyone";
                permission.Role = "commenter";

                service.Permissions.Create(permission, file.Id).Execute();
                Console.WriteLine(FilePath + ".zip upLoad Finish");
                File.Delete(FilePath + ".zip");
                UpdateUI(file.WebViewLink, txtUrl);
                btnGetFolderClickThread();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }

        //背景工作相關格式
        public class ListNameAndID
        {
            public string FileName;
            public string FileID;
            public string WebViewLink;
            public string Time;
        }

        #region 跨執行緒點按鈕
        private void btnGetFolderClickThread()
        {
            System.Threading.Thread th = new System.Threading.Thread(ThreadExecution);
            th.Start();
        }

        private void ThreadExecution()
        {
            //Some process
            this.Invoke(new Action(() => { btnGetFolder.PerformClick(); }));
        }

        #endregion

        //取得Google硬碟上的資料夾資訊
        public void GetFolder() 
        {

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            //這邊篩選要的檔案contenttype
            //listRequest.Q = "mimeType = 'application/zip'";
            listRequest.Fields = "nextPageToken, files(id, name,webViewLink,createdTime)";
            //listRequest.Fields = "mimeType = 'application/vnd.google-apps.folder'";
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            //ComboBox CB = new ComboBox();
            //CB.Location = new Point(50, 50);
            //this.Controls.Add(CB);
            List<ListNameAndID> ListFile = new List<ListNameAndID>();
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                InfoFolder = new List<ListNameAndID>();
                foreach (var file in files)
                {
                    ListNameAndID SingleFile = new ListNameAndID();
                    SingleFile.FileName = file.Name;
                    SingleFile.FileID = file.Id;
                    SingleFile.WebViewLink = file.WebViewLink;
                    SingleFile.Time = file.CreatedTime.ToString();
                    file.GetType().ToString();
                    Console.WriteLine("{0} ({1}){2}{3}", file.Name, file.Id, file.MimeType, file.WebViewLink);


                    AddListBox(file.Name,listBox1);
                    InfoFolder.Add(SingleFile);
                }


                UpdateUI("資料數："+ files.Count, labStatus);
                //Console.WriteLine("Files:");
            }
            else
            {
                UpdateUI("無檔案", labStatus);
            }
        }
        //用ID取得檔名(目前不用)
        public void GetFileNameByID(string ID)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";
            listRequest.Q = "'" + ID + "' in parents";
            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
        }
    }
}
