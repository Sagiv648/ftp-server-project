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
            this.finalizeUploadBtn = new System.Windows.Forms.Button();
            this.removeSelectedBtn = new System.Windows.Forms.Button();
            this.finalizeUploadedFiles = new System.Windows.Forms.CheckedListBox();
            this.uploadedFilesPanel = new System.Windows.Forms.Panel();
            this.searchUploadedTxtbox = new System.Windows.Forms.TextBox();
            this.publicFilesToUpload = new System.Windows.Forms.CheckedListBox();
            this.setPublic = new System.Windows.Forms.Button();
            this.removePublic = new System.Windows.Forms.Button();
            this.uploadedFilesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // publicFilesDisplayer
            // 
            this.publicFilesDisplayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.publicFilesDisplayer.FormattingEnabled = true;
            this.publicFilesDisplayer.Location = new System.Drawing.Point(12, 231);
            this.publicFilesDisplayer.Name = "publicFilesDisplayer";
            this.publicFilesDisplayer.Size = new System.Drawing.Size(439, 236);
            this.publicFilesDisplayer.TabIndex = 0;
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
            this.refreshListBtn.Click += new System.EventHandler(this.refreshListBtn_Click);
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
            // myFilesDisplayer
            // 
            this.myFilesDisplayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myFilesDisplayer.FormattingEnabled = true;
            this.myFilesDisplayer.Location = new System.Drawing.Point(808, 99);
            this.myFilesDisplayer.Name = "myFilesDisplayer";
            this.myFilesDisplayer.Size = new System.Drawing.Size(165, 262);
            this.myFilesDisplayer.TabIndex = 3;
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
            this.uploadFileBtn.Location = new System.Drawing.Point(808, 386);
            this.uploadFileBtn.Name = "uploadFileBtn";
            this.uploadFileBtn.Size = new System.Drawing.Size(118, 42);
            this.uploadFileBtn.TabIndex = 5;
            this.uploadFileBtn.Text = "Upload File";
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
            this.downloadFileBtn.Text = "Download File";
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
            this.deleteFileBtn.Location = new System.Drawing.Point(808, 444);
            this.deleteFileBtn.Name = "deleteFileBtn";
            this.deleteFileBtn.Size = new System.Drawing.Size(118, 42);
            this.deleteFileBtn.TabIndex = 10;
            this.deleteFileBtn.Text = "Delete File";
            this.deleteFileBtn.UseVisualStyleBackColor = true;
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
            // finalizeUploadBtn
            // 
            this.finalizeUploadBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.finalizeUploadBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.finalizeUploadBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.finalizeUploadBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalizeUploadBtn.Location = new System.Drawing.Point(381, 8);
            this.finalizeUploadBtn.Name = "finalizeUploadBtn";
            this.finalizeUploadBtn.Size = new System.Drawing.Size(118, 42);
            this.finalizeUploadBtn.TabIndex = 13;
            this.finalizeUploadBtn.Text = "Upload";
            this.finalizeUploadBtn.UseVisualStyleBackColor = true;
            this.finalizeUploadBtn.Click += new System.EventHandler(this.finalizeUploadBtn_Click);
            // 
            // removeSelectedBtn
            // 
            this.removeSelectedBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.removeSelectedBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeSelectedBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeSelectedBtn.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeSelectedBtn.Location = new System.Drawing.Point(36, 171);
            this.removeSelectedBtn.Name = "removeSelectedBtn";
            this.removeSelectedBtn.Size = new System.Drawing.Size(118, 42);
            this.removeSelectedBtn.TabIndex = 14;
            this.removeSelectedBtn.Text = "Remove";
            this.removeSelectedBtn.UseVisualStyleBackColor = true;
            this.removeSelectedBtn.Click += new System.EventHandler(this.removeSelectedBtn_Click);
            // 
            // finalizeUploadedFiles
            // 
            this.finalizeUploadedFiles.FormattingEnabled = true;
            this.finalizeUploadedFiles.Location = new System.Drawing.Point(3, 56);
            this.finalizeUploadedFiles.Name = "finalizeUploadedFiles";
            this.finalizeUploadedFiles.Size = new System.Drawing.Size(327, 109);
            this.finalizeUploadedFiles.TabIndex = 15;
            // 
            // uploadedFilesPanel
            // 
            this.uploadedFilesPanel.BackColor = System.Drawing.Color.Transparent;
            this.uploadedFilesPanel.Controls.Add(this.removePublic);
            this.uploadedFilesPanel.Controls.Add(this.setPublic);
            this.uploadedFilesPanel.Controls.Add(this.publicFilesToUpload);
            this.uploadedFilesPanel.Controls.Add(this.searchUploadedTxtbox);
            this.uploadedFilesPanel.Controls.Add(this.finalizeUploadedFiles);
            this.uploadedFilesPanel.Controls.Add(this.removeSelectedBtn);
            this.uploadedFilesPanel.Controls.Add(this.finalizeUploadBtn);
            this.uploadedFilesPanel.Location = new System.Drawing.Point(188, 12);
            this.uploadedFilesPanel.Name = "uploadedFilesPanel";
            this.uploadedFilesPanel.Size = new System.Drawing.Size(587, 216);
            this.uploadedFilesPanel.TabIndex = 16;
            this.uploadedFilesPanel.Visible = false;
            // 
            // searchUploadedTxtbox
            // 
            this.searchUploadedTxtbox.Location = new System.Drawing.Point(50, 29);
            this.searchUploadedTxtbox.Name = "searchUploadedTxtbox";
            this.searchUploadedTxtbox.Size = new System.Drawing.Size(280, 20);
            this.searchUploadedTxtbox.TabIndex = 16;
            this.searchUploadedTxtbox.TextChanged += new System.EventHandler(this.searchUploadedTxtbox_TextChanged);
            // 
            // publicFilesToUpload
            // 
            this.publicFilesToUpload.FormattingEnabled = true;
            this.publicFilesToUpload.Location = new System.Drawing.Point(335, 56);
            this.publicFilesToUpload.Name = "publicFilesToUpload";
            this.publicFilesToUpload.Size = new System.Drawing.Size(249, 109);
            this.publicFilesToUpload.TabIndex = 17;
            // 
            // setPublic
            // 
            this.setPublic.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.setPublic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.setPublic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setPublic.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setPublic.Location = new System.Drawing.Point(212, 171);
            this.setPublic.Name = "setPublic";
            this.setPublic.Size = new System.Drawing.Size(118, 42);
            this.setPublic.TabIndex = 18;
            this.setPublic.Text = "Set selected files to public";
            this.setPublic.UseVisualStyleBackColor = true;
            this.setPublic.Click += new System.EventHandler(this.setPublic_Click);
            // 
            // removePublic
            // 
            this.removePublic.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.removePublic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removePublic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removePublic.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removePublic.Location = new System.Drawing.Point(427, 171);
            this.removePublic.Name = "removePublic";
            this.removePublic.Size = new System.Drawing.Size(118, 42);
            this.removePublic.TabIndex = 19;
            this.removePublic.Text = "Set selected files to private";
            this.removePublic.UseVisualStyleBackColor = true;
            this.removePublic.Click += new System.EventHandler(this.removePublic_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(1010, 552);
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
        private System.Windows.Forms.Button finalizeUploadBtn;
        private System.Windows.Forms.Button removeSelectedBtn;
        private System.Windows.Forms.CheckedListBox finalizeUploadedFiles;
        private System.Windows.Forms.Panel uploadedFilesPanel;
        private System.Windows.Forms.TextBox searchUploadedTxtbox;
        private System.Windows.Forms.Button removePublic;
        private System.Windows.Forms.Button setPublic;
        private System.Windows.Forms.CheckedListBox publicFilesToUpload;
    }
}