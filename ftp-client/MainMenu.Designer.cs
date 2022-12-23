namespace ftp_client
{
    partial class MainMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.userEmailLbl = new System.Windows.Forms.Label();
            this.userPasswordLbl = new System.Windows.Forms.Label();
            this.userEmailTextBox = new System.Windows.Forms.TextBox();
            this.userPasswordTextbox = new System.Windows.Forms.TextBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Miriam", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 46);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(280, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to the client.";
            // 
            // userEmailLbl
            // 
            this.userEmailLbl.AutoSize = true;
            this.userEmailLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userEmailLbl.Location = new System.Drawing.Point(35, 128);
            this.userEmailLbl.Name = "userEmailLbl";
            this.userEmailLbl.Size = new System.Drawing.Size(52, 20);
            this.userEmailLbl.TabIndex = 1;
            this.userEmailLbl.Text = "Email:";
            // 
            // userPasswordLbl
            // 
            this.userPasswordLbl.AutoSize = true;
            this.userPasswordLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPasswordLbl.Location = new System.Drawing.Point(35, 182);
            this.userPasswordLbl.Name = "userPasswordLbl";
            this.userPasswordLbl.Size = new System.Drawing.Size(82, 20);
            this.userPasswordLbl.TabIndex = 2;
            this.userPasswordLbl.Text = "Password:";
            // 
            // userEmailTextBox
            // 
            this.userEmailTextBox.Location = new System.Drawing.Point(92, 128);
            this.userEmailTextBox.Name = "userEmailTextBox";
            this.userEmailTextBox.Size = new System.Drawing.Size(232, 20);
            this.userEmailTextBox.TabIndex = 3;
            // 
            // userPasswordTextbox
            // 
            this.userPasswordTextbox.Location = new System.Drawing.Point(119, 184);
            this.userPasswordTextbox.Name = "userPasswordTextbox";
            this.userPasswordTextbox.Size = new System.Drawing.Size(232, 20);
            this.userPasswordTextbox.TabIndex = 4;
            // 
            // loginBtn
            // 
            this.loginBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBtn.Location = new System.Drawing.Point(119, 235);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(120, 42);
            this.loginBtn.TabIndex = 5;
            this.loginBtn.Text = "Sign in";
            this.loginBtn.UseVisualStyleBackColor = true;
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterBtn.Location = new System.Drawing.Point(92, 344);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(162, 42);
            this.RegisterBtn.TabIndex = 6;
            this.RegisterBtn.Text = "Sign up";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 450);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.userPasswordTextbox);
            this.Controls.Add(this.userEmailTextBox);
            this.Controls.Add(this.userPasswordLbl);
            this.Controls.Add(this.userEmailLbl);
            this.Controls.Add(this.label1);
            this.Name = "MainMenu";
            this.Text = "FTP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label userEmailLbl;
        private System.Windows.Forms.Label userPasswordLbl;
        private System.Windows.Forms.TextBox userEmailTextBox;
        private System.Windows.Forms.TextBox userPasswordTextbox;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button RegisterBtn;
    }
}

