namespace ftp_client
{
    partial class FilesExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesExplorer));
            this.button1 = new System.Windows.Forms.Button();
            this.pickedListBox = new System.Windows.Forms.ListBox();
            this.filesExplorerListBox = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.finishBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(12, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pickedListBox
            // 
            this.pickedListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pickedListBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.pickedListBox.FormattingEnabled = true;
            this.pickedListBox.HorizontalScrollbar = true;
            this.pickedListBox.Location = new System.Drawing.Point(473, 29);
            this.pickedListBox.Name = "pickedListBox";
            this.pickedListBox.Size = new System.Drawing.Size(278, 342);
            this.pickedListBox.TabIndex = 2;
            // 
            // filesExplorerListBox
            // 
            this.filesExplorerListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.filesExplorerListBox.FormattingEnabled = true;
            this.filesExplorerListBox.HorizontalScrollbar = true;
            this.filesExplorerListBox.Location = new System.Drawing.Point(12, 29);
            this.filesExplorerListBox.Name = "filesExplorerListBox";
            this.filesExplorerListBox.Size = new System.Drawing.Size(384, 342);
            this.filesExplorerListBox.TabIndex = 3;
            // 
            // addBtn
            // 
            this.addBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.addBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addBtn.Location = new System.Drawing.Point(321, 377);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 40);
            this.addBtn.TabIndex = 4;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.removeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeBtn.Location = new System.Drawing.Point(572, 377);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(102, 40);
            this.removeBtn.TabIndex = 5;
            this.removeBtn.Text = "Remove";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // finishBtn
            // 
            this.finishBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.finishBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.finishBtn.Location = new System.Drawing.Point(409, 377);
            this.finishBtn.Name = "finishBtn";
            this.finishBtn.Size = new System.Drawing.Size(101, 56);
            this.finishBtn.TabIndex = 6;
            this.finishBtn.Text = "Finish";
            this.finishBtn.UseVisualStyleBackColor = true;
            this.finishBtn.Click += new System.EventHandler(this.finishBtn_Click);
            // 
            // FilesExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(810, 445);
            this.Controls.Add(this.finishBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.filesExplorerListBox);
            this.Controls.Add(this.pickedListBox);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilesExplorer";
            this.Text = "FTP_Client Files Explorer";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox pickedListBox;
        private System.Windows.Forms.ListBox filesExplorerListBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button finishBtn;
    }
}