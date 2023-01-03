using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ftp_client
{
    public partial class RegisterForm : Form
    {
        
        public RegisterForm()
        {
            InitializeComponent();
            FormClosing += Program.CloseForm;
            
            
            
        }

        
        private void backBtn_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "LoginForm"; 
            Dispose();
            //Hide();
           
        }


        bool TestEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (Exception)
            {
                

                return false;
            }
            
            return true;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            int i;
            if (passwordTextbox.Text != confirmPasswordTextbox.Text)
            {
                for (i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name.Contains("empty") && Controls[i].Visible)
                        Controls[i].Visible = false;
                }


                errorLbl.Text = "Passwords doesn't match";
                errorLbl.Visible = true;
                return;
            }
            if (!TestEmail(emailTextbox.Text))
            {
                for (i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name.Contains("empty") && Controls[i].Visible)
                        Controls[i].Visible = false;
                }
                errorLbl.Text = "Incorrect email format";
                errorLbl.Visible = true;
                return;
            }

            
            if(userNameTextbox.Text.Length == 0)
            {
                if (!emptyUsername.Visible)
                    emptyUsername.Visible = true;
                errorLbl.Text = "All fields are required";
                errorLbl.Visible = true;
                return;
            }
            else
            {
                emptyUsername.Visible = false;
            }
            if(emailTextbox.Text.Length == 0)
            {
                if (!emptyEmail.Visible)
                    emptyEmail.Visible = true;
                errorLbl.Text = "All fields are required";
                errorLbl.Visible = true;
                return;
            }
            else
            {
                emptyEmail.Visible = false;
            }
            if(passwordTextbox.Text.Length == 0)
            {
                if (!emptyPassword.Visible)
                    emptyPassword.Visible = true;
                errorLbl.Text = "All fields are required";
                errorLbl.Visible = true;
                return;
            }
            else
            {
                emptyPassword.Visible = false;
            }
            if(confirmPasswordTextbox.Text.Length == 0)
            {
                if (!emptyConfirmPassword.Visible)
                    emptyConfirmPassword.Visible = true;
                errorLbl.Text = "All fields are required";
                errorLbl.Visible = true;
                return;
            }
            else
            {
                emptyConfirmPassword.Visible = false;
            }
            List<Control> allCtrls = new List<Control>();
            foreach (Control item in Controls)
            {
                allCtrls.Add(item);
            }
            
            Dictionary<string, string> response = null;
            for (i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Name.Contains("empty") && Controls[i].Visible)
                    Controls[i].Visible = false;
            }


            Cursor.Current = Cursors.WaitCursor;
            response = Connection.SendRegisterRequest(userNameTextbox.Text, emailTextbox.Text, passwordTextbox.Text);
            Cursor.Current = Cursors.Default;
            userNameTextbox.Text = null;
            emailTextbox.Text = null;
            passwordTextbox.Text = null;
            confirmPasswordTextbox.Text = null;

            int codeTest = 0;
            if (!int.TryParse(response["Code"], out codeTest))
            {
                MessageBox.Show("Network issues, please try again later.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if(codeTest == 400)
                {
                    errorLbl.Text = response["Error"];
                    errorLbl.Visible = true;
                    return;
                }
            }

            Program.formsParams.Add(new Dictionary<Form, Dictionary<string, string>>()
            {
                {this, response }
            });
            Program.navigatedForm = "LoginForm";
            //Hide();
            Dispose();
         
            
        }

        private void emailVerificationNav_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "EmailVerificationForm";
            //Hide();
            Dispose();
        }
    }
}
