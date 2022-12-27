namespace ftp_client
{
    partial class EmailVerificationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailVerificationForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resendEmailBtn = new System.Windows.Forms.Button();
            this.emailVerificationBackBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 130);
            this.textBox1.MaxLength = 20;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(389, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "We have sent you an email with a code.\r\nEnter the code here in order to proceed.\r" +
    "\n";
            // 
            // resendEmailBtn
            // 
            this.resendEmailBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.resendEmailBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resendEmailBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resendEmailBtn.Location = new System.Drawing.Point(100, 156);
            this.resendEmailBtn.Name = "resendEmailBtn";
            this.resendEmailBtn.Size = new System.Drawing.Size(141, 42);
            this.resendEmailBtn.TabIndex = 2;
            this.resendEmailBtn.Text = "Resend email.\r\n";
            this.resendEmailBtn.UseVisualStyleBackColor = true;
            // 
            // emailVerificationBackBtn
            // 
            this.emailVerificationBackBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.emailVerificationBackBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.emailVerificationBackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailVerificationBackBtn.Location = new System.Drawing.Point(348, 156);
            this.emailVerificationBackBtn.Name = "emailVerificationBackBtn";
            this.emailVerificationBackBtn.Size = new System.Drawing.Size(141, 42);
            this.emailVerificationBackBtn.TabIndex = 3;
            this.emailVerificationBackBtn.Text = "Back";
            this.emailVerificationBackBtn.UseVisualStyleBackColor = true;
            this.emailVerificationBackBtn.Click += new System.EventHandler(this.emailVerificationBackBtn_Click);
            // 
            // EmailVerificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(576, 221);
            this.Controls.Add(this.emailVerificationBackBtn);
            this.Controls.Add(this.resendEmailBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmailVerificationForm";
            this.Text = "TCP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button resendEmailBtn;
        private System.Windows.Forms.Button emailVerificationBackBtn;
    }
}