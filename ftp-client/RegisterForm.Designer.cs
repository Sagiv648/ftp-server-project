namespace ftp_client
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.userNameLbl = new System.Windows.Forms.Label();
            this.userNameTextbox = new System.Windows.Forms.TextBox();
            this.emailTextbox = new System.Windows.Forms.TextBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.registerBtn = new System.Windows.Forms.Button();
            this.emailLbl = new System.Windows.Forms.Label();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.confirmPasswordLbl = new System.Windows.Forms.Label();
            this.confirmPasswordTextbox = new System.Windows.Forms.TextBox();
            this.registerLbl = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.emailVerificationNav = new System.Windows.Forms.Button();
            this.errorLbl = new System.Windows.Forms.Label();
            this.emptyUsername = new System.Windows.Forms.Label();
            this.emptyEmail = new System.Windows.Forms.Label();
            this.emptyPassword = new System.Windows.Forms.Label();
            this.emptyConfirmPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userNameLbl
            // 
            this.userNameLbl.AutoSize = true;
            this.userNameLbl.BackColor = System.Drawing.Color.Transparent;
            this.userNameLbl.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.userNameLbl.Location = new System.Drawing.Point(11, 109);
            this.userNameLbl.Name = "userNameLbl";
            this.userNameLbl.Size = new System.Drawing.Size(100, 23);
            this.userNameLbl.TabIndex = 0;
            this.userNameLbl.Text = "Username:";
            // 
            // userNameTextbox
            // 
            this.userNameTextbox.Location = new System.Drawing.Point(118, 112);
            this.userNameTextbox.MaxLength = 31;
            this.userNameTextbox.Name = "userNameTextbox";
            this.userNameTextbox.Size = new System.Drawing.Size(188, 20);
            this.userNameTextbox.TabIndex = 1;
            // 
            // emailTextbox
            // 
            this.emailTextbox.Location = new System.Drawing.Point(118, 155);
            this.emailTextbox.MaxLength = 31;
            this.emailTextbox.Name = "emailTextbox";
            this.emailTextbox.Size = new System.Drawing.Size(188, 20);
            this.emailTextbox.TabIndex = 3;
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(118, 203);
            this.passwordTextbox.MaxLength = 31;
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(188, 20);
            this.passwordTextbox.TabIndex = 5;
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // registerBtn
            // 
            this.registerBtn.BackColor = System.Drawing.Color.Transparent;
            this.registerBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.registerBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.registerBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.registerBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.registerBtn.FlatAppearance.BorderSize = 0;
            this.registerBtn.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerBtn.Location = new System.Drawing.Point(118, 317);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(118, 45);
            this.registerBtn.TabIndex = 6;
            this.registerBtn.Text = "Register";
            this.registerBtn.UseVisualStyleBackColor = false;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // emailLbl
            // 
            this.emailLbl.AutoSize = true;
            this.emailLbl.BackColor = System.Drawing.Color.Transparent;
            this.emailLbl.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLbl.Location = new System.Drawing.Point(45, 152);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(66, 23);
            this.emailLbl.TabIndex = 7;
            this.emailLbl.Text = "Email:";
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.BackColor = System.Drawing.Color.Transparent;
            this.passwordLbl.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLbl.Location = new System.Drawing.Point(14, 199);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(97, 23);
            this.passwordLbl.TabIndex = 8;
            this.passwordLbl.Text = "Password:";
            // 
            // confirmPasswordLbl
            // 
            this.confirmPasswordLbl.AutoSize = true;
            this.confirmPasswordLbl.BackColor = System.Drawing.Color.Transparent;
            this.confirmPasswordLbl.Font = new System.Drawing.Font("Mongolian Baiti", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPasswordLbl.Location = new System.Drawing.Point(14, 249);
            this.confirmPasswordLbl.Name = "confirmPasswordLbl";
            this.confirmPasswordLbl.Size = new System.Drawing.Size(174, 23);
            this.confirmPasswordLbl.TabIndex = 10;
            this.confirmPasswordLbl.Text = "Confirm Password:";
            // 
            // confirmPasswordTextbox
            // 
            this.confirmPasswordTextbox.Location = new System.Drawing.Point(194, 252);
            this.confirmPasswordTextbox.MaxLength = 31;
            this.confirmPasswordTextbox.Name = "confirmPasswordTextbox";
            this.confirmPasswordTextbox.Size = new System.Drawing.Size(188, 20);
            this.confirmPasswordTextbox.TabIndex = 9;
            this.confirmPasswordTextbox.UseSystemPasswordChar = true;
            // 
            // registerLbl
            // 
            this.registerLbl.AutoSize = true;
            this.registerLbl.BackColor = System.Drawing.Color.Transparent;
            this.registerLbl.Font = new System.Drawing.Font("Miriam CLM", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.registerLbl.Location = new System.Drawing.Point(112, 38);
            this.registerLbl.Name = "registerLbl";
            this.registerLbl.Size = new System.Drawing.Size(124, 35);
            this.registerLbl.TabIndex = 11;
            this.registerLbl.Text = "Register";
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.Transparent;
            this.backBtn.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.backBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.backBtn.FlatAppearance.BorderSize = 0;
            this.backBtn.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(308, 374);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(118, 45);
            this.backBtn.TabIndex = 12;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = false;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // emailVerificationNav
            // 
            this.emailVerificationNav.BackColor = System.Drawing.Color.Transparent;
            this.emailVerificationNav.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.emailVerificationNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.emailVerificationNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.emailVerificationNav.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.emailVerificationNav.FlatAppearance.BorderSize = 0;
            this.emailVerificationNav.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailVerificationNav.Location = new System.Drawing.Point(12, 374);
            this.emailVerificationNav.Name = "emailVerificationNav";
            this.emailVerificationNav.Size = new System.Drawing.Size(118, 53);
            this.emailVerificationNav.TabIndex = 13;
            this.emailVerificationNav.Text = "Email Verification";
            this.emailVerificationNav.UseVisualStyleBackColor = false;
            this.emailVerificationNav.Visible = false;
            this.emailVerificationNav.Click += new System.EventHandler(this.emailVerificationNav_Click);
            // 
            // errorLbl
            // 
            this.errorLbl.AutoSize = true;
            this.errorLbl.BackColor = System.Drawing.Color.Transparent;
            this.errorLbl.Font = new System.Drawing.Font("Miriam CLM", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.errorLbl.ForeColor = System.Drawing.Color.Red;
            this.errorLbl.Location = new System.Drawing.Point(88, 291);
            this.errorLbl.Name = "errorLbl";
            this.errorLbl.Size = new System.Drawing.Size(188, 23);
            this.errorLbl.TabIndex = 14;
            this.errorLbl.Text = "All fields are required";
            this.errorLbl.Visible = false;
            // 
            // emptyUsername
            // 
            this.emptyUsername.AutoSize = true;
            this.emptyUsername.BackColor = System.Drawing.Color.Transparent;
            this.emptyUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyUsername.ForeColor = System.Drawing.Color.Red;
            this.emptyUsername.Location = new System.Drawing.Point(312, 112);
            this.emptyUsername.Name = "emptyUsername";
            this.emptyUsername.Size = new System.Drawing.Size(25, 31);
            this.emptyUsername.TabIndex = 15;
            this.emptyUsername.Text = "*";
            this.emptyUsername.Visible = false;
            // 
            // emptyEmail
            // 
            this.emptyEmail.AutoSize = true;
            this.emptyEmail.BackColor = System.Drawing.Color.Transparent;
            this.emptyEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyEmail.ForeColor = System.Drawing.Color.Red;
            this.emptyEmail.Location = new System.Drawing.Point(312, 155);
            this.emptyEmail.Name = "emptyEmail";
            this.emptyEmail.Size = new System.Drawing.Size(25, 31);
            this.emptyEmail.TabIndex = 16;
            this.emptyEmail.Text = "*";
            this.emptyEmail.Visible = false;
            // 
            // emptyPassword
            // 
            this.emptyPassword.AutoSize = true;
            this.emptyPassword.BackColor = System.Drawing.Color.Transparent;
            this.emptyPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyPassword.ForeColor = System.Drawing.Color.Red;
            this.emptyPassword.Location = new System.Drawing.Point(312, 203);
            this.emptyPassword.Name = "emptyPassword";
            this.emptyPassword.Size = new System.Drawing.Size(25, 31);
            this.emptyPassword.TabIndex = 17;
            this.emptyPassword.Text = "*";
            this.emptyPassword.Visible = false;
            // 
            // emptyConfirmPassword
            // 
            this.emptyConfirmPassword.AutoSize = true;
            this.emptyConfirmPassword.BackColor = System.Drawing.Color.Transparent;
            this.emptyConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyConfirmPassword.ForeColor = System.Drawing.Color.Red;
            this.emptyConfirmPassword.Location = new System.Drawing.Point(388, 252);
            this.emptyConfirmPassword.Name = "emptyConfirmPassword";
            this.emptyConfirmPassword.Size = new System.Drawing.Size(25, 31);
            this.emptyConfirmPassword.TabIndex = 18;
            this.emptyConfirmPassword.Text = "*";
            this.emptyConfirmPassword.Visible = false;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::ftp_client.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(455, 450);
            this.Controls.Add(this.emptyConfirmPassword);
            this.Controls.Add(this.emptyPassword);
            this.Controls.Add(this.emptyEmail);
            this.Controls.Add(this.emptyUsername);
            this.Controls.Add(this.errorLbl);
            this.Controls.Add(this.emailVerificationNav);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.registerLbl);
            this.Controls.Add(this.confirmPasswordLbl);
            this.Controls.Add(this.confirmPasswordTextbox);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.passwordTextbox);
            this.Controls.Add(this.emailTextbox);
            this.Controls.Add(this.userNameTextbox);
            this.Controls.Add(this.userNameLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterForm";
            this.Text = "FTP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userNameLbl;
        private System.Windows.Forms.TextBox userNameTextbox;
        private System.Windows.Forms.TextBox emailTextbox;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Label emailLbl;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.Label confirmPasswordLbl;
        private System.Windows.Forms.TextBox confirmPasswordTextbox;
        private System.Windows.Forms.Label registerLbl;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button emailVerificationNav;
        private System.Windows.Forms.Label errorLbl;
        private System.Windows.Forms.Label emptyUsername;
        private System.Windows.Forms.Label emptyEmail;
        private System.Windows.Forms.Label emptyPassword;
        private System.Windows.Forms.Label emptyConfirmPassword;
    }
}