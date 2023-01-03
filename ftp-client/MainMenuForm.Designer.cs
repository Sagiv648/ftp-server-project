﻿namespace ftp_client
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
            this.publicFilesDisplayer = new System.Windows.Forms.ListBox();
            this.refreshListBtn = new System.Windows.Forms.Button();
            this.publicFilesLbl = new System.Windows.Forms.Label();
            this.myFilesDisplayer = new System.Windows.Forms.ListBox();
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
            this.uploadedFilesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // publicFilesDisplayer
            // 
            this.publicFilesDisplayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.publicFilesDisplayer.FormattingEnabled = true;
            this.publicFilesDisplayer.ItemHeight = 25;
            this.publicFilesDisplayer.Location = new System.Drawing.Point(24, 444);
            this.publicFilesDisplayer.Margin = new System.Windows.Forms.Padding(6);
            this.publicFilesDisplayer.Name = "publicFilesDisplayer";
            this.publicFilesDisplayer.Size = new System.Drawing.Size(876, 452);
            this.publicFilesDisplayer.TabIndex = 0;
            // 
            // refreshListBtn
            // 
            this.refreshListBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.refreshListBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refreshListBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshListBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshListBtn.Location = new System.Drawing.Point(24, 929);
            this.refreshListBtn.Margin = new System.Windows.Forms.Padding(6);
            this.refreshListBtn.Name = "refreshListBtn";
            this.refreshListBtn.Size = new System.Drawing.Size(236, 81);
            this.refreshListBtn.TabIndex = 1;
            this.refreshListBtn.Text = "Refresh List";
            this.refreshListBtn.UseVisualStyleBackColor = true;
            // 
            // publicFilesLbl
            // 
            this.publicFilesLbl.AutoSize = true;
            this.publicFilesLbl.BackColor = System.Drawing.Color.Transparent;
            this.publicFilesLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.publicFilesLbl.Location = new System.Drawing.Point(14, 385);
            this.publicFilesLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.publicFilesLbl.Name = "publicFilesLbl";
            this.publicFilesLbl.Size = new System.Drawing.Size(264, 56);
            this.publicFilesLbl.TabIndex = 2;
            this.publicFilesLbl.Text = "Public files:";
            // 
            // myFilesDisplayer
            // 
            this.myFilesDisplayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myFilesDisplayer.FormattingEnabled = true;
            this.myFilesDisplayer.HorizontalScrollbar = true;
            this.myFilesDisplayer.ItemHeight = 25;
            this.myFilesDisplayer.Location = new System.Drawing.Point(1616, 190);
            this.myFilesDisplayer.Margin = new System.Windows.Forms.Padding(6);
            this.myFilesDisplayer.Name = "myFilesDisplayer";
            this.myFilesDisplayer.Size = new System.Drawing.Size(389, 502);
            this.myFilesDisplayer.TabIndex = 3;
            // 
            // myFilesLbl
            // 
            this.myFilesLbl.AutoSize = true;
            this.myFilesLbl.BackColor = System.Drawing.Color.Transparent;
            this.myFilesLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myFilesLbl.Location = new System.Drawing.Point(1606, 131);
            this.myFilesLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.myFilesLbl.Name = "myFilesLbl";
            this.myFilesLbl.Size = new System.Drawing.Size(202, 56);
            this.myFilesLbl.TabIndex = 4;
            this.myFilesLbl.Text = "My files:";
            // 
            // uploadFileBtn
            // 
            this.uploadFileBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.uploadFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uploadFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadFileBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadFileBtn.Location = new System.Drawing.Point(1616, 742);
            this.uploadFileBtn.Margin = new System.Windows.Forms.Padding(6);
            this.uploadFileBtn.Name = "uploadFileBtn";
            this.uploadFileBtn.Size = new System.Drawing.Size(236, 81);
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
            this.downloadFileBtn.Location = new System.Drawing.Point(666, 929);
            this.downloadFileBtn.Margin = new System.Windows.Forms.Padding(6);
            this.downloadFileBtn.Name = "downloadFileBtn";
            this.downloadFileBtn.Size = new System.Drawing.Size(236, 110);
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
            this.userDataUserNameLbl.Location = new System.Drawing.Point(16, 138);
            this.userDataUserNameLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.userDataUserNameLbl.Name = "userDataUserNameLbl";
            this.userDataUserNameLbl.Size = new System.Drawing.Size(251, 44);
            this.userDataUserNameLbl.TabIndex = 7;
            this.userDataUserNameLbl.Text = "Username: %";
            // 
            // deleteFileBtn
            // 
            this.deleteFileBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.deleteFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteFileBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteFileBtn.Location = new System.Drawing.Point(1616, 854);
            this.deleteFileBtn.Margin = new System.Windows.Forms.Padding(6);
            this.deleteFileBtn.Name = "deleteFileBtn";
            this.deleteFileBtn.Size = new System.Drawing.Size(236, 81);
            this.deleteFileBtn.TabIndex = 10;
            this.deleteFileBtn.Text = "Delete Files";
            this.deleteFileBtn.UseVisualStyleBackColor = true;
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.logoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logoutBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutBtn.Location = new System.Drawing.Point(1230, 942);
            this.logoutBtn.Margin = new System.Windows.Forms.Padding(6);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(236, 81);
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
            this.uploadedFilesPanel.Location = new System.Drawing.Point(376, 23);
            this.uploadedFilesPanel.Margin = new System.Windows.Forms.Padding(6);
            this.uploadedFilesPanel.Name = "uploadedFilesPanel";
            this.uploadedFilesPanel.Size = new System.Drawing.Size(1174, 415);
            this.uploadedFilesPanel.TabIndex = 16;
            this.uploadedFilesPanel.Visible = false;
            // 
            // setPrivateBtn
            // 
            this.setPrivateBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.setPrivateBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.setPrivateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setPrivateBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setPrivateBtn.Location = new System.Drawing.Point(834, 354);
            this.setPrivateBtn.Margin = new System.Windows.Forms.Padding(6);
            this.setPrivateBtn.Name = "setPrivateBtn";
            this.setPrivateBtn.Size = new System.Drawing.Size(236, 56);
            this.setPrivateBtn.TabIndex = 21;
            this.setPrivateBtn.Text = "Set private";
            this.setPrivateBtn.UseVisualStyleBackColor = true;
            this.setPrivateBtn.Click += new System.EventHandler(this.setPrivateBtn_Click);
            // 
            // publicFiles
            // 
            this.publicFiles.FormattingEnabled = true;
            this.publicFiles.HorizontalScrollbar = true;
            this.publicFiles.ItemHeight = 25;
            this.publicFiles.Location = new System.Drawing.Point(740, 15);
            this.publicFiles.Margin = new System.Windows.Forms.Padding(6);
            this.publicFiles.Name = "publicFiles";
            this.publicFiles.Size = new System.Drawing.Size(424, 329);
            this.publicFiles.TabIndex = 20;
            // 
            // setPublicBtn
            // 
            this.setPublicBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.setPublicBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.setPublicBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setPublicBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setPublicBtn.Location = new System.Drawing.Point(225, 356);
            this.setPublicBtn.Margin = new System.Windows.Forms.Padding(6);
            this.setPublicBtn.Name = "setPublicBtn";
            this.setPublicBtn.Size = new System.Drawing.Size(236, 56);
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
            this.directoyNameLbl.Location = new System.Drawing.Point(6, 83);
            this.directoyNameLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.directoyNameLbl.Name = "directoyNameLbl";
            this.directoyNameLbl.Size = new System.Drawing.Size(324, 37);
            this.directoyNameLbl.TabIndex = 18;
            this.directoyNameLbl.Text = "Root Directory Name:";
            // 
            // selectedFiles
            // 
            this.selectedFiles.FormattingEnabled = true;
            this.selectedFiles.HorizontalScrollbar = true;
            this.selectedFiles.ItemHeight = 25;
            this.selectedFiles.Location = new System.Drawing.Point(6, 140);
            this.selectedFiles.Margin = new System.Windows.Forms.Padding(6);
            this.selectedFiles.Name = "selectedFiles";
            this.selectedFiles.Size = new System.Drawing.Size(650, 204);
            this.selectedFiles.TabIndex = 17;
            // 
            // searchUploadedTxtbox
            // 
            this.searchUploadedTxtbox.Location = new System.Drawing.Point(346, 87);
            this.searchUploadedTxtbox.Margin = new System.Windows.Forms.Padding(6);
            this.searchUploadedTxtbox.Name = "searchUploadedTxtbox";
            this.searchUploadedTxtbox.Size = new System.Drawing.Size(310, 31);
            this.searchUploadedTxtbox.TabIndex = 16;
            this.searchUploadedTxtbox.TextChanged += new System.EventHandler(this.searchUploadedTxtbox_TextChanged);
            // 
            // beginUploadBtn
            // 
            this.beginUploadBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.beginUploadBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.beginUploadBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.beginUploadBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beginUploadBtn.Location = new System.Drawing.Point(936, 444);
            this.beginUploadBtn.Margin = new System.Windows.Forms.Padding(6);
            this.beginUploadBtn.Name = "beginUploadBtn";
            this.beginUploadBtn.Size = new System.Drawing.Size(236, 81);
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
            this.removeSelectedBtn.Location = new System.Drawing.Point(1244, 445);
            this.removeSelectedBtn.Margin = new System.Windows.Forms.Padding(6);
            this.removeSelectedBtn.Name = "removeSelectedBtn";
            this.removeSelectedBtn.Size = new System.Drawing.Size(236, 81);
            this.removeSelectedBtn.TabIndex = 18;
            this.removeSelectedBtn.Text = "Remove Selected";
            this.removeSelectedBtn.UseVisualStyleBackColor = true;
            this.removeSelectedBtn.Visible = false;
            this.removeSelectedBtn.Click += new System.EventHandler(this.removeSelectedBtn_Click_1);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2020, 1062);
            this.Controls.Add(this.removeSelectedBtn);
            this.Controls.Add(this.beginUploadBtn);
            this.Controls.Add(this.uploadedFilesPanel);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.deleteFileBtn);
            this.Controls.Add(this.userDataUserNameLbl);
            this.Controls.Add(this.downloadFileBtn);
            this.Controls.Add(this.uploadFileBtn);
            this.Controls.Add(this.myFilesLbl);
            this.Controls.Add(this.myFilesDisplayer);
            this.Controls.Add(this.publicFilesLbl);
            this.Controls.Add(this.refreshListBtn);
            this.Controls.Add(this.publicFilesDisplayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainMenuForm";
            this.Text = "FTP Client";
            this.uploadedFilesPanel.ResumeLayout(false);
            this.uploadedFilesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox publicFilesDisplayer;
        private System.Windows.Forms.Button refreshListBtn;
        private System.Windows.Forms.Label publicFilesLbl;
        private System.Windows.Forms.ListBox myFilesDisplayer;
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
    }
}