using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ftp_client
{
    public partial class LoginForm : Form
    {

        Dictionary<string, string> response = null;

        public LoginForm()
        {
            InitializeComponent();
            FormClosing += Program.CloseForm;

            if (Program.formsParams.FindIndex(x => x is Dictionary<Form, Dictionary<string, string>>) != -1)
                registerPromptLbl.Text = "You can log in now.";


        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "RegisterForm";
            
            response = null;
            Dispose();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            
            
            int i = 0;
            if(userEmailTextBox.Text.Length == 0)
            {
                
                for (i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name.Equals("emptyUser"))
                        break;
                }

                Controls.Add(new Label()
                {
                    Text = "*",
                    Name = "emptyUser",
                    BackColor = Color.Transparent,
                    Location = new Point(userEmailTextBox.Location.X + 260, userEmailTextBox.Location.Y),
                    Size = new Size(welcomeLbl.Size.Width + 10, welcomeLbl.Height + 20),
                    Font = new Font(registerPromptLbl.Font, registerPromptLbl.Font.Style),
                    ForeColor = Color.Red,
                    Visible = true,
                }); 
                
                
            }
            if(userPasswordTextbox.Text.Length == 0)
            {
                
                for(i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name.Equals("emptyPassword"))
                        break;
                }

                if(i == Controls.Count)
                Controls.Add(new Label()
                {
                    Text = "*",
                    Name = "emptyPassword",
                    BackColor = Color.Transparent,
                    Location = new Point(userEmailTextBox.Location.X + 260, userPasswordTextbox.Location.Y),
                    Size = new Size(welcomeLbl.Size.Width + 10, welcomeLbl.Height + 20),
                    Font = new Font(registerPromptLbl.Font, registerPromptLbl.Font.Style),
                    ForeColor = Color.Red,
                    Visible = true,
                }) ;


            }

            if(userEmailTextBox.Text.Length != 0 && userPasswordTextbox.Text.Length != 0)
            {

               List<Control> allCtrls = new List<Control>();

                
                for (i = 0; i < Controls.Count; i++)
                {
                    allCtrls.Add(Controls[i]);
                }

                foreach (var item in allCtrls.FindAll(x => x.Name.Contains("empty")))
                {
                    Controls.Remove(item);
                } 


                Cursor = Cursors.WaitCursor;
                
                response = Connection.SendLoginRequest(userEmailTextBox.Text, userPasswordTextbox.Text);
                userEmailTextBox.Text = null;
                userPasswordTextbox.Text = null;
                Cursor = Cursors.Default;
                if(response != null && int.Parse(response["Code"]) != (int)Connection.Code.Action_Confirm)
                {
                    Controls.Add(new Label()
                    {
                        Text = "Invalid Credentials",
                        Name = "emptyInvalidCredentials",
                        BackColor = Color.Transparent,
                        Location = new Point(loginBtn.Location.X, loginBtn.Location.Y - 20),
                        Size = new Size(welcomeLbl.Size.Width + 10, welcomeLbl.Height + 20),
                        Font = new Font(registerPromptLbl.Font, registerPromptLbl.Font.Style),
                        ForeColor = Color.Red,
                        Visible = true,
                    });
                }
                else if(response == null)
                {
                    Controls.Add(new Label()
                    {
                        Text = "Potentially network problems.",
                        Name = "emptyInvalidCredentials",
                        BackColor = Color.Transparent,
                        Location = new Point(loginBtn.Location.X, loginBtn.Location.Y - 20),
                        Size = new Size(welcomeLbl.Size.Width + 10, welcomeLbl.Height + 20),
                        Font = new Font(registerPromptLbl.Font, registerPromptLbl.Font.Style),
                        ForeColor = Color.Red,
                        Visible = true,
                    });
                }
                else
                {
                    Program.formsParams.Add(response);
                    Program.navigatedForm = "MainMenuForm";
                    
                    Dispose();
                }
            }
            
        }
    }
}
