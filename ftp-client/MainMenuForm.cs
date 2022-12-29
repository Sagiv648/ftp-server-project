using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_client
{
    public partial class MainMenuForm : Form
    {
        
        string userFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        Dictionary<string, string> response = null;

        CheckedListBox uploadedContainer = null;
        List<string> checkedVals = new List<string>();
        List<string> checkedPublicVals = new List<string>();
        public MainMenuForm()
        {
            InitializeComponent();
            uploadedContainer = new CheckedListBox();
            uploadedContainer.Items.AddRange(finalizeUploadedFiles.Items);
            finalizeUploadedFiles.ItemCheck += Entry_Checked;
            publicFilesToUpload.ItemCheck += Entry_Checked;

            //publicFilesToUpload.MouseDoubleClick += Open_Folder;
            //finalizeUploadedFiles.MouseDoubleClick+= Open_Folder;
            
            if(Program.formsParams.Count != 0)
            {
                response = ((Dictionary<string,string>)Program.formsParams.Find(x => x is Dictionary<string, string>));
            }

            FormClosing += Program.CloseForm;
            

         

            if (response == null)
                MessageBox.Show("Network issues occured, try again later", "Response not recieved", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                userDataUserNameLbl.Text = userDataUserNameLbl.Text.Replace("%", $"{response["UserName"]}");
                string[] files = response["Your_Files"].Split('|');
                if(files.Length > 0 && files[0] != "")
                    myFilesDisplayer.DataSource = response["Your_Files"].Split('|');

                //foreach (var item in response)
                //{
                //    Program.DisplayTestWindow($"{item.Key}:{item.Value}", ToString(), 2);
                //}
            }

            

            
        }


        private void Open_Folder(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                CheckedListBox list = (CheckedListBox)sender;
                MessageBox.Show($"{list.Text.Substring(list.Text.IndexOf("-")+2, list.Text.LastIndexOf('\\')+1)}");
                Process.Start("explorer.exe", list.Text.Substring(list.Text.IndexOf("-") + 2, list.Text.LastIndexOf('\\')+1));
                

            }
            

        }

        private void Entry_Checked(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox list = (CheckedListBox)sender;

            if (list.Name == finalizeUploadedFiles.Name)
            {
                if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
                {

                    checkedVals.Add(list.Items[e.Index].ToString());
                }
                else if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
                {
                    checkedVals.Remove(list.Items[e.Index].ToString());
                } 
            }
            else
            {
                if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
                {

                    checkedPublicVals.Add(list.Items[e.Index].ToString());
                }
                else if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
                {
                    checkedPublicVals.Remove(list.Items[e.Index].ToString());
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

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files (*.*)|*.*";
            fileDialog.InitialDirectory = userFile != "" ? userFile : Environment.GetLogicalDrives()[0];

            FolderBrowserDialog fb = new FolderBrowserDialog();
            
            if (fb.ShowDialog() == DialogResult.OK)
            {

                //string[] path = fileDialog.FileName.Split('\\');

                //finalizeUploadedFiles.Items.AddRange(fileDialog.FileNames);
                string[] allEntries = Directory.GetFileSystemEntries(fb.SelectedPath);
                foreach (var item in allEntries)
                {
                    if(File.Exists(item))
                        finalizeUploadedFiles.Items.Add($"File - {item}");
                    else
                        finalizeUploadedFiles.Items.Add($"Folder - {item}");
                    
                }
                
                
                uploadedFilesPanel.Visible = true;
                
            }
            uploadedContainer.Items.Clear();
            uploadedContainer.Items.AddRange(finalizeUploadedFiles.Items);
            
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
                
                
                response = null;
                Dispose();
                
            }

        }

        private void removeSelectedBtn_Click(object sender, EventArgs e)
        {
            if(finalizeUploadedFiles.SelectedIndex != -1)
            {
                finalizeUploadedFiles.Items.RemoveAt(finalizeUploadedFiles.SelectedIndex);
                uploadedContainer.Items.Clear();
                uploadedContainer.Items.AddRange(finalizeUploadedFiles.Items);
                

            }
        }

        private void finalizeUploadBtn_Click(object sender, EventArgs e)
        {

        }

        private void searchUploadedTxtbox_TextChanged(object sender, EventArgs e)
        {
            finalizeUploadedFiles.Items.Clear();
            
            foreach (var item in uploadedContainer.Items)
            {
                if (item.ToString().Contains(((TextBox)sender).Text))
                {
                    finalizeUploadedFiles.Items.Add(item);
                    if(checkedVals.FindIndex(x => x == item.ToString()) != -1)
                        finalizeUploadedFiles.SetItemCheckState(finalizeUploadedFiles.Items.Count-1,CheckState.Checked);
                   

                }
                    
            }
            
            
        }

        private void setPublic_Click(object sender, EventArgs e)
        {
            if(checkedVals.Count > 0)
            {
                foreach (var item in checkedVals)
                {
                    publicFilesToUpload.Items.Add(item);
                    finalizeUploadedFiles.Items.Remove(item);
                }
                checkedVals.Clear();
            }
        }

        private void removePublic_Click(object sender, EventArgs e)
        {
            if(checkedPublicVals.Count > 0)
            {
                foreach (var item in checkedPublicVals)
                {
                    finalizeUploadedFiles.Items.Add(item);
                    publicFilesToUpload.Items.Remove(item);
                }
                checkedPublicVals.Clear();
            }
        }
    }
}
