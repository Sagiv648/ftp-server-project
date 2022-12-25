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
        OpenFileDialog fileDloag;
        string userFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public MainMenuForm(List<object> opaqueObjects)
        {
            InitializeComponent();
            Dictionary<string, string> response = null;
            if(opaqueObjects.Count != 0)
            {
                response = (Dictionary<string,string>)opaqueObjects.Find(x => x is Dictionary<string, string>);
            }

            FormClosing += Program.CloseForm;
            fileDloag = new OpenFileDialog();
            fileDloag.Filter = "All files (*.*)|*.*";
            fileDloag.InitialDirectory = userFile != "" ? userFile : Environment.GetLogicalDrives()[0];

            userDataUserNameLbl.Text = userDataUserNameLbl.Text.Replace("%", $"{(response == null ? "" : response["UserName"])}");
            

            
        }

        private void refreshListBtn_Click(object sender, EventArgs e)
        {
            
            
        }

        private void downloadFileBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void uploadFileBtn_Click(object sender, EventArgs e)
        {
            if(fileDloag.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fileDloag.FileName);
            }
        }
    }
}
