namespace ftp_client
{
    partial class PicturePreviewForm
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
            this.pictureBoxDisplayPreview = new System.Windows.Forms.PictureBox();
            this.picSaveBtn = new System.Windows.Forms.Button();
            this.picDropBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplayPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDisplayPreview
            // 
            this.pictureBoxDisplayPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDisplayPreview.Location = new System.Drawing.Point(61, 46);
            this.pictureBoxDisplayPreview.Name = "pictureBoxDisplayPreview";
            this.pictureBoxDisplayPreview.Size = new System.Drawing.Size(598, 303);
            this.pictureBoxDisplayPreview.TabIndex = 0;
            this.pictureBoxDisplayPreview.TabStop = false;
            // 
            // picSaveBtn
            // 
            this.picSaveBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.picSaveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSaveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.picSaveBtn.Location = new System.Drawing.Point(556, 355);
            this.picSaveBtn.Name = "picSaveBtn";
            this.picSaveBtn.Size = new System.Drawing.Size(103, 83);
            this.picSaveBtn.TabIndex = 1;
            this.picSaveBtn.Text = "Save";
            this.picSaveBtn.UseVisualStyleBackColor = true;
            // 
            // picDropBtn
            // 
            this.picDropBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.picDropBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picDropBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.picDropBtn.Location = new System.Drawing.Point(61, 355);
            this.picDropBtn.Name = "picDropBtn";
            this.picDropBtn.Size = new System.Drawing.Size(103, 83);
            this.picDropBtn.TabIndex = 2;
            this.picDropBtn.Text = "Drop";
            this.picDropBtn.UseVisualStyleBackColor = true;
            // 
            // PicturePreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(787, 450);
            this.Controls.Add(this.picDropBtn);
            this.Controls.Add(this.picSaveBtn);
            this.Controls.Add(this.pictureBoxDisplayPreview);
            this.Name = "PicturePreviewForm";
            this.Text = "PicturePreviewForm";
            this.Load += new System.EventHandler(this.PicturePreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplayPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxDisplayPreview;
        private System.Windows.Forms.Button picSaveBtn;
        private System.Windows.Forms.Button picDropBtn;
    }
}