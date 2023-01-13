namespace ftp_client
{
    partial class MainMenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.refreshListBtn = new System.Windows.Forms.Button();
            this.publicFilesLbl = new System.Windows.Forms.Label();
            this.myFilesLbl = new System.Windows.Forms.Label();
            this.uploadFileBtn = new System.Windows.Forms.Button();
            this.downloadFileBtn = new System.Windows.Forms.Button();
            this.userDataUserNameLbl = new System.Windows.Forms.Label();
            this.deleteFileBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.uploadedFilesPanel = new System.Windows.Forms.Panel();
            this.setPrivateBtn = new System.Windows.Forms.Button();
            this.publicFiles = new System.Windows.Forms.ListBox();
            this.setPublicBtn = new System.Windows.Forms.Button();
            this.directoyNameLbl = new System.Windows.Forms.Label();
            this.selectedFiles = new System.Windows.Forms.ListBox();
            this.searchUploadedTxtbox = new System.Windows.Forms.TextBox();
            this.beginUploadBtn = new System.Windows.Forms.Button();
            this.removeSelectedBtn = new System.Windows.Forms.Button();
            this.publicFilesListView = new System.Windows.Forms.ListView();
            this.fileIdHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.privateFilesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uploadedFilesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshListBtn
            // 
            this.refreshListBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.refreshListBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refreshListBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshListBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshListBtn.Location = new System.Drawing.Point(12, 483);
            this.refreshListBtn.Name = "refreshListBtn";
            this.refreshListBtn.Size = new System.Drawing.Size(118, 42);
            this.refreshListBtn.TabIndex = 1;
            this.refreshListBtn.Text = "Refresh List";
            this.refreshListBtn.UseVisualStyleBackColor = true;
            // 
            // publicFilesLbl
            // 
            this.publicFilesLbl.AutoSize = true;
            this.publicFilesLbl.BackColor = System.Drawing.Color.Transparent;
            this.publicFilesLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.publicFilesLbl.Location = new System.Drawing.Point(7, 200);
            this.publicFilesLbl.Name = "publicFilesLbl";
            this.publicFilesLbl.Size = new System.Drawing.Size(132, 28);
            this.publicFilesLbl.TabIndex = 2;
            this.publicFilesLbl.Text = "Public files:";
            // 
            // myFilesLbl
            // 
            this.myFilesLbl.AutoSize = true;
            this.myFilesLbl.BackColor = System.Drawing.Color.Transparent;
            this.myFilesLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myFilesLbl.Location = new System.Drawing.Point(803, 68);
            this.myFilesLbl.Name = "myFilesLbl";
            this.myFilesLbl.Size = new System.Drawing.Size(101, 28);
            this.myFilesLbl.TabIndex = 4;
            this.myFilesLbl.Text = "My files:";
            // 
            // uploadFileBtn
            // 
            this.uploadFileBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.uploadFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uploadFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadFileBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadFileBtn.Location = new System.Drawing.Point(808, 348);
            this.uploadFileBtn.Name = "uploadFileBtn";
            this.uploadFileBtn.Size = new System.Drawing.Size(118, 42);
            this.uploadFileBtn.TabIndex = 5;
            this.uploadFileBtn.Text = "Upload Files";
            this.uploadFileBtn.UseVisualStyleBackColor = true;
            this.uploadFileBtn.Click += new System.EventHandler(this.uploadFileBtn_Click);
            // 
            // downloadFileBtn
            // 
            this.downloadFileBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.downloadFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.downloadFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadFileBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadFileBtn.Location = new System.Drawing.Point(333, 483);
            this.downloadFileBtn.Name = "downloadFileBtn";
            this.downloadFileBtn.Size = new System.Drawing.Size(118, 57);
            this.downloadFileBtn.TabIndex = 6;
            this.downloadFileBtn.Text = "Download Files";
            this.downloadFileBtn.UseVisualStyleBackColor = true;
            this.downloadFileBtn.Click += new System.EventHandler(this.downloadFileBtn_Click);
            // 
            // userDataUserNameLbl
            // 
            this.userDataUserNameLbl.AutoSize = true;
            this.userDataUserNameLbl.BackColor = System.Drawing.Color.Transparent;
            this.userDataUserNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userDataUserNameLbl.Location = new System.Drawing.Point(8, 72);
            this.userDataUserNameLbl.Name = "userDataUserNameLbl";
            this.userDataUserNameLbl.Size = new System.Drawing.Size(122, 24);
            this.userDataUserNameLbl.TabIndex = 7;
            this.userDataUserNameLbl.Text = "Username: %";
            // 
            // deleteFileBtn
            // 
            this.deleteFileBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.deleteFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteFileBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteFileBtn.Location = new System.Drawing.Point(1140, 348);
            this.deleteFileBtn.Name = "deleteFileBtn";
            this.deleteFileBtn.Size = new System.Drawing.Size(118, 42);
            this.deleteFileBtn.TabIndex = 10;
            this.deleteFileBtn.Text = "Delete Files";
            this.deleteFileBtn.UseVisualStyleBackColor = true;
            this.deleteFileBtn.Click += new System.EventHandler(this.deleteFileBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.logoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logoutBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutBtn.Location = new System.Drawing.Point(615, 490);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(118, 42);
            this.logoutBtn.TabIndex = 11;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // uploadedFilesPanel
            // 
            this.uploadedFilesPanel.BackColor = System.Drawing.Color.Transparent;
            this.uploadedFilesPanel.Controls.Add(this.setPrivateBtn);
            this.uploadedFilesPanel.Controls.Add(this.publicFiles);
            this.uploadedFilesPanel.Controls.Add(this.setPublicBtn);
            this.uploadedFilesPanel.Controls.Add(this.directoyNameLbl);
            this.uploadedFilesPanel.Controls.Add(this.selectedFiles);
            this.uploadedFilesPanel.Controls.Add(this.searchUploadedTxtbox);
            this.uploadedFilesPanel.Location = new System.Drawing.Point(188, 12);
            this.uploadedFilesPanel.Name = "uploadedFilesPanel";
            this.uploadedFilesPanel.Size = new System.Drawing.Size(587, 216);
            this.uploadedFilesPanel.TabIndex = 16;
            this.uploadedFilesPanel.Visible = false;
            // 
            // setPrivateBtn
            // 
            this.setPrivateBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.setPrivateBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.setPrivateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setPrivateBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setPrivateBtn.Location = new System.Drawing.Point(417, 184);
            this.setPrivateBtn.Name = "setPrivateBtn";
            this.setPrivateBtn.Size = new System.Drawing.Size(118, 29);
            this.setPrivateBtn.TabIndex = 21;
            this.setPrivateBtn.Text = "Set private";
            this.setPrivateBtn.UseVisualStyleBackColor = true;
            this.setPrivateBtn.Click += new System.EventHandler(this.setPrivateBtn_Click);
            // 
            // publicFiles
            // 
            this.publicFiles.FormattingEnabled = true;
            this.publicFiles.HorizontalScrollbar = true;
            this.publicFiles.Location = new System.Drawing.Point(370, 8);
            this.publicFiles.Name = "publicFiles";
            this.publicFiles.Size = new System.Drawing.Size(214, 173);
            this.publicFiles.TabIndex = 20;
            // 
            // setPublicBtn
            // 
            this.setPublicBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.setPublicBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.setPublicBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setPublicBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setPublicBtn.Location = new System.Drawing.Point(112, 185);
            this.setPublicBtn.Name = "setPublicBtn";
            this.setPublicBtn.Size = new System.Drawing.Size(118, 29);
            this.setPublicBtn.TabIndex = 19;
            this.setPublicBtn.Text = "Set public";
            this.setPublicBtn.UseVisualStyleBackColor = true;
            this.setPublicBtn.Click += new System.EventHandler(this.setPublicBtn_Click);
            // 
            // directoyNameLbl
            // 
            this.directoyNameLbl.AutoSize = true;
            this.directoyNameLbl.BackColor = System.Drawing.Color.Transparent;
            this.directoyNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoyNameLbl.Location = new System.Drawing.Point(3, 43);
            this.directoyNameLbl.Name = "directoyNameLbl";
            this.directoyNameLbl.Size = new System.Drawing.Size(161, 20);
            this.directoyNameLbl.TabIndex = 18;
            this.directoyNameLbl.Text = "Root Directory Name:";
            // 
            // selectedFiles
            // 
            this.selectedFiles.FormattingEnabled = true;
            this.selectedFiles.HorizontalScrollbar = true;
            this.selectedFiles.Location = new System.Drawing.Point(3, 73);
            this.selectedFiles.Name = "selectedFiles";
            this.selectedFiles.Size = new System.Drawing.Size(327, 108);
            this.selectedFiles.TabIndex = 17;
            // 
            // searchUploadedTxtbox
            // 
            this.searchUploadedTxtbox.Location = new System.Drawing.Point(173, 45);
            this.searchUploadedTxtbox.Name = "searchUploadedTxtbox";
            this.searchUploadedTxtbox.Size = new System.Drawing.Size(157, 20);
            this.searchUploadedTxtbox.TabIndex = 16;
            this.searchUploadedTxtbox.TextChanged += new System.EventHandler(this.searchUploadedTxtbox_TextChanged);
            // 
            // beginUploadBtn
            // 
            this.beginUploadBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.beginUploadBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.beginUploadBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.beginUploadBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beginUploadBtn.Location = new System.Drawing.Point(468, 231);
            this.beginUploadBtn.Name = "beginUploadBtn";
            this.beginUploadBtn.Size = new System.Drawing.Size(118, 42);
            this.beginUploadBtn.TabIndex = 17;
            this.beginUploadBtn.Text = "Begin upload";
            this.beginUploadBtn.UseVisualStyleBackColor = true;
            this.beginUploadBtn.Visible = false;
            this.beginUploadBtn.Click += new System.EventHandler(this.beginUploadBtn_Click);
            // 
            // removeSelectedBtn
            // 
            this.removeSelectedBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.removeSelectedBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeSelectedBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeSelectedBtn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeSelectedBtn.Location = new System.Drawing.Point(622, 231);
            this.removeSelectedBtn.Name = "removeSelectedBtn";
            this.removeSelectedBtn.Size = new System.Drawing.Size(118, 42);
            this.removeSelectedBtn.TabIndex = 18;
            this.removeSelectedBtn.Text = "Remove Selected";
            this.removeSelectedBtn.UseVisualStyleBackColor = true;
            this.removeSelectedBtn.Visible = false;
            this.removeSelectedBtn.Click += new System.EventHandler(this.removeSelectedBtn_Click_1);
            // 
            // publicFilesListView
            // 
            this.publicFilesListView.AllowColumnReorder = true;
            this.publicFilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileIdHeader,
            this.fileNameHeader,
            this.fileSizeHeader});
            this.publicFilesListView.FullRowSelect = true;
            this.publicFilesListView.GridLines = true;
            this.publicFilesListView.HideSelection = false;
            this.publicFilesListView.Location = new System.Drawing.Point(12, 234);
            this.publicFilesListView.Name = "publicFilesListView";
            this.publicFilesListView.Size = new System.Drawing.Size(450, 243);
            this.publicFilesListView.TabIndex = 19;
            this.publicFilesListView.UseCompatibleStateImageBehavior = false;
            this.publicFilesListView.View = System.Windows.Forms.View.Details;
            // 
            // fileIdHeader
            // 
            this.fileIdHeader.Text = "File Id";
            this.fileIdHeader.Width = 150;
            // 
            // fileNameHeader
            // 
            this.fileNameHeader.Text = "File Name";
            this.fileNameHeader.Width = 150;
            // 
            // fileSizeHeader
            // 
            this.fileSizeHeader.Text = "File Size";
            this.fileSizeHeader.Width = 146;
            // 
            // privateFilesListView
            // 
            this.privateFilesListView.AllowColumnReorder = true;
            this.privateFilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.privateFilesListView.FullRowSelect = true;
            this.privateFilesListView.GridLines = true;
            this.privateFilesListView.HideSelection = false;
            this.privateFilesListView.Location = new System.Drawing.Point(808, 99);
            this.privateFilesListView.Name = "privateFilesListView";
            this.privateFilesListView.Size = new System.Drawing.Size(450, 243);
            this.privateFilesListView.TabIndex = 20;
            this.privateFilesListView.UseCompatibleStateImageBehavior = false;
            this.privateFilesListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Id";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Name";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File Size";
            this.columnHeader3.Width = 146;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1270, 565);
            this.Controls.Add(this.privateFilesListView);
            this.Controls.Add(this.publicFilesListView);
            this.Controls.Add(this.removeSelectedBtn);
            this.Controls.Add(this.beginUploadBtn);
            this.Controls.Add(this.uploadedFilesPanel);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.deleteFileBtn);
            this.Controls.Add(this.userDataUserNameLbl);
            this.Controls.Add(this.downloadFileBtn);
            this.Controls.Add(this.uploadFileBtn);
            this.Controls.Add(this.myFilesLbl);
            this.Controls.Add(this.publicFilesLbl);
            this.Controls.Add(this.refreshListBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainMenuForm";
            this.Text = "FTP Client";
            this.uploadedFilesPanel.ResumeLayout(false);
            this.uploadedFilesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button refreshListBtn;
        private System.Windows.Forms.Label publicFilesLbl;
        private System.Windows.Forms.Label myFilesLbl;
        private System.Windows.Forms.Button uploadFileBtn;
        private System.Windows.Forms.Button downloadFileBtn;
        private System.Windows.Forms.Label userDataUserNameLbl;
        private System.Windows.Forms.Button deleteFileBtn;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Panel uploadedFilesPanel;
        private System.Windows.Forms.TextBox searchUploadedTxtbox;
        private System.Windows.Forms.ListBox selectedFiles;
        private System.Windows.Forms.Label directoyNameLbl;
        private System.Windows.Forms.Button setPrivateBtn;
        private System.Windows.Forms.ListBox publicFiles;
        private System.Windows.Forms.Button setPublicBtn;
        private System.Windows.Forms.Button beginUploadBtn;
        private System.Windows.Forms.Button removeSelectedBtn;
        private System.Windows.Forms.ListView publicFilesListView;
        private System.Windows.Forms.ColumnHeader fileIdHeader;
        private System.Windows.Forms.ColumnHeader fileNameHeader;
        private System.Windows.Forms.ColumnHeader fileSizeHeader;
        private System.Windows.Forms.ListView privateFilesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}