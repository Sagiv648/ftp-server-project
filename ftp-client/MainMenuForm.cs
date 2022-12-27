using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_client
{
    public partial class MainMenuForm : Form
    {
        OpenFileDialog fileDialog;
        string userFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        Dictionary<string, string> response = null;
        public MainMenuForm()
        {
            InitializeComponent();
            
           
            if(Program.formsParams.Count != 0)
            {
                response = ((Dictionary<string,string>)Program.formsParams.Find(x => x is Dictionary<string, string>));
            }

            FormClosing += Program.CloseForm;
            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files (*.*)|*.*";
            fileDialog.InitialDirectory = userFile != "" ? userFile : Environment.GetLogicalDrives()[0];

            

            if (response == null)
                MessageBox.Show("Network issues occured, try again later", "Response not recieved", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                userDataUserNameLbl.Text = userDataUserNameLbl.Text.Replace("%", $"{response["UserName"]}");
                string[] files = response["Your_Files"].Split('|');
                if(files.Length > 0 && files[0] != "")
                    myFilesDisplayer.DataSource = response["Your_Files"].Split('|');

                foreach (var item in response)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }

            

            
        }

        private void refreshListBtn_Click(object sender, EventArgs e)
        {
            
            
        }

        private void downloadFileBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void uploadFileBtn_Click(object sender, EventArgs e)
        {
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                int access = 0;
                string[] path = fileDialog.FileName.Split('\\');
                if (MessageBox.Show($"Would you like to make {path[path.Length-1]} public?", "Access modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    access = 1;
                }
                
                if(!Connection.SendFileUpload(fileDialog.FileName, response["UserName"], response["UserId"], access.ToString(), downloadUploadProgressBar) )
                Console.WriteLine(fileDialog.FileName);
            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            response = Connection.SendLogoutRequest(response["UserId"]);
            if(response == null)
            {
                MessageBox.Show("Couldn't get the response from the server.", "Network error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            else if(int.Parse(response["Code"]) == (int)Connection.Code.Action_Confirm)
            {
                Program.navigatedForm = "LoginForm";
                Hide();
                Dispose();
                
            }

        }
    }
}
