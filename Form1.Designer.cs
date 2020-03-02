namespace GoogleFileTran
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnUpLoad = new System.Windows.Forms.Button();
            this.btnGetFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnUpLoadFolder = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnGoUrl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(156, 163);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(317, 292);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Location = new System.Drawing.Point(156, 32);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(116, 23);
            this.btnUpLoad.TabIndex = 2;
            this.btnUpLoad.Text = "上傳單一檔案";
            this.btnUpLoad.UseVisualStyleBackColor = true;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // btnGetFolder
            // 
            this.btnGetFolder.Location = new System.Drawing.Point(19, 163);
            this.btnGetFolder.Name = "btnGetFolder";
            this.btnGetFolder.Size = new System.Drawing.Size(121, 23);
            this.btnGetFolder.TabIndex = 6;
            this.btnGetFolder.Text = "取得現有檔案清單";
            this.btnGetFolder.UseVisualStyleBackColor = true;
            this.btnGetFolder.Click += new System.EventHandler(this.btnGetFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "資料Url";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(72, 77);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(478, 22);
            this.txtUrl.TabIndex = 8;
            // 
            // btnUpLoadFolder
            // 
            this.btnUpLoadFolder.Location = new System.Drawing.Point(24, 32);
            this.btnUpLoadFolder.Name = "btnUpLoadFolder";
            this.btnUpLoadFolder.Size = new System.Drawing.Size(116, 23);
            this.btnUpLoadFolder.TabIndex = 9;
            this.btnUpLoadFolder.Text = "上傳資料夾壓縮檔";
            this.btnUpLoadFolder.UseVisualStyleBackColor = true;
            this.btnUpLoadFolder.Click += new System.EventHandler(this.btnUpLoadFolder_Click);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.ForeColor = System.Drawing.SystemColors.Desktop;
            this.labStatus.Location = new System.Drawing.Point(84, 112);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(32, 12);
            this.labStatus.TabIndex = 10;
            this.labStatus.Text = "Status";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(24, 301);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(116, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "刪除檔案";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnGoUrl
            // 
            this.btnGoUrl.Location = new System.Drawing.Point(24, 227);
            this.btnGoUrl.Name = "btnGoUrl";
            this.btnGoUrl.Size = new System.Drawing.Size(116, 23);
            this.btnGoUrl.TabIndex = 13;
            this.btnGoUrl.Text = "網頁查看檔案";
            this.btnGoUrl.UseVisualStyleBackColor = true;
            this.btnGoUrl.Click += new System.EventHandler(this.btnGoUrl_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 503);
            this.Controls.Add(this.btnGoUrl);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.btnUpLoadFolder);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetFolder);
            this.Controls.Add(this.btnUpLoad);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnUpLoad;
        private System.Windows.Forms.Button btnGetFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnUpLoadFolder;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnGoUrl;
    }
}

