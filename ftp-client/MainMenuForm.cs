using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
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
        List<string> publicFilesContainer = new List<string>();
        List<string> privateFilesContainer = new List<string>();
        FolderBrowserDialog fb = new FolderBrowserDialog();
        string selectedPath = "";
        List<string> paths = new List<string>();
        FilesExplorer explorer = null;
        public MainMenuForm()
        {
            InitializeComponent();
            uploadedContainer = new CheckedListBox();
            

            //publicFilesToUpload.MouseDoubleClick += Open_Folder;
            //finalizeUploadedFiles.MouseDoubleClick+= Open_Folder;
            
            if(Program.formsParams.Count != 0)
            {
                response = (Dictionary<string, string>)Program.formsParams.Last();
            }

            FormClosing += Program.CloseForm;
            

         

            if (response == null)
                MessageBox.Show("Network issues occured, try again later", "Response not recieved", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                userDataUserNameLbl.Text = userDataUserNameLbl.Text.Replace("%", $"{response["UserName"]}");
                string[] files = response["Your_Files"].Split('|');
                if(files.Length > 0 && files[0] != "")
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        myFilesDisplayer.Items.Add(files[i]);
                    }
                }
                string[] publicFiles = response["PublicFiles"].Split('|');
                for(int i = 0;i < publicFiles.Length; i++)
                {
                    publicFilesDisplayer.Items.Add(publicFiles[i]);
                }
               
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
            
        }

        private void refreshListBtn_Click(object sender, EventArgs e)
        {
            
            
        }

        private void downloadFileBtn_Click(object sender, EventArgs e)
        {
            if(publicFilesDisplayer.SelectedItems.Count > 0)
            {
               
                
                FolderBrowserDialog fb = new FolderBrowserDialog();
                if(fb.ShowDialog() != DialogResult.OK)
                {
                    return; 
                }
                
                foreach (var item in publicFilesDisplayer.SelectedItems)
                {
                   Connection.SendDownloadRequest(response["UserName"], response["UserId"], item.ToString(), fb.SelectedPath);

                }
                Process.Start("explorer.exe", fb.SelectedPath);
            }
        }

        private void uploadFileBtn_Click(object sender, EventArgs e)
        {
            


            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files (*.*)|*.*";
            fileDialog.InitialDirectory = userFile != "" ? userFile : Environment.GetLogicalDrives()[0];

            
            
            if (fb.ShowDialog() == DialogResult.OK)
            {

                selectedFiles.Items.Clear();
                publicFiles.Items.Clear();
                searchUploadedTxtbox.Text = "";
                publicFilesContainer.Clear();
                paths.Clear();

                selectedPath = fb.SelectedPath;
                explorer = new FilesExplorer(fb.SelectedPath, paths);
                explorer.ShowDialog();
                if (paths.Count == 0)
                    return;
                selectedFiles.Items.Clear();
                for (int i = 0; i < paths.Count; i++)
                {
                    if (paths[i].Split('\\')[0] != searchUploadedTxtbox.Text)
                        paths[i] = paths[i].Remove(0, selectedPath.Length).Insert(0, searchUploadedTxtbox.Text);
                }
                selectedPath = searchUploadedTxtbox.Text;
                selectedFiles.Items.AddRange(paths.ToArray());
                uploadedFilesPanel.Visible = true;
                
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
                
                Program.formsParams.Clear();
                response = null;
                Dispose();
                
            }

        }

        private void removeSelectedBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void finalizeUploadBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void searchUploadedTxtbox_TextChanged(object sender, EventArgs e)
        {

            if (searchUploadedTxtbox.Text == "")
            {
                beginUploadBtn.Visible = false;
                removeSelectedBtn.Visible = false;

            }

            else
            {
                beginUploadBtn.Visible = true;
                removeSelectedBtn.Visible = true;
            }
                


            selectedFiles.Items.Clear();
            publicFiles.Items.Clear();
            for(int i = 0; i < paths.Count;i++)
            {
                
                    paths[i] = paths[i].Remove(0, selectedPath.Length).Insert(0, searchUploadedTxtbox.Text);
            }
            
            for(int i = 0; i < publicFilesContainer.Count; i++)
            {
                publicFilesContainer[i] = publicFilesContainer[i].Remove(0, selectedPath.Length).Insert(0, searchUploadedTxtbox.Text);
            }
            

            selectedPath = searchUploadedTxtbox.Text;
            selectedFiles.Items.AddRange(paths.ToArray());
            if(publicFilesContainer.Count > 0)
                publicFiles.Items.AddRange(publicFilesContainer.ToArray());

            
            
        }

        private void setPrivateBtn_Click(object sender, EventArgs e)
        {
            


            if (publicFiles.SelectedIndex != -1 && publicFiles.Items.Count > 0)
            {
                paths.Add(publicFiles.Text);
                selectedFiles.Items.Add(publicFiles.Text);
                publicFilesContainer.Remove(publicFiles.Text);
                publicFiles.Items.Remove(publicFiles.Text);
                
                
            }
            
        }

        private void setPublicBtn_Click(object sender, EventArgs e)
        {
            


            if(selectedFiles.SelectedIndex != -1 && selectedFiles.Items.Count > 0)
            {
                paths.Remove(selectedFiles.Text);
                publicFiles.Items.Add(selectedFiles.Text);
                publicFilesContainer.Add(selectedFiles.Text);
                selectedFiles.Items.Remove(selectedFiles.Text);
                
            }
        }

        private void beginUploadBtn_Click(object sender, EventArgs e)
        {
            string[] invalidNames = {"CON", "PRN", "AUX", "NUL",
              "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
              "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"};
            string[] invalidChars = {"<",
                                      ">",
                                      ":",
                                      "\"",
                                      "\\",
                                      "|",
                                      "?",
                                      "*"};
            char[] nonPrintableChars = new char[32];
            for (int i = 0; i < nonPrintableChars.Length; i++)
            {
                nonPrintableChars[i] = (char)i;
            }
            if(searchUploadedTxtbox.Text == "")
            {
                MessageBox.Show("Directory name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (searchUploadedTxtbox.Text[searchUploadedTxtbox.Text.Length-1] == ' ' || searchUploadedTxtbox.Text[searchUploadedTxtbox.Text.Length - 1] == '.')
            {
                MessageBox.Show("Directory names cannot end with space( ) or dot(.)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach(string name in invalidNames)
            {
                if(searchUploadedTxtbox.Text.Equals(name))
                {
                    MessageBox.Show($"Invalid directory name {name}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (string ch in invalidChars)
            {
                if (searchUploadedTxtbox.Text.Contains(ch))
                {
                    MessageBox.Show($"Invalid character {ch}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (var item in nonPrintableChars)
            {
                if (searchUploadedTxtbox.Text.Contains(item))
                {
                    MessageBox.Show($"Non-printable characters are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }



            List<string> allFiles = new List<string>();
            allFiles.AddRange(paths);
            allFiles.AddRange(publicFilesContainer);
            Dictionary<string,string> uploadResponse = null;



            foreach (var path in allFiles)
            {
                int access = Convert.ToInt32(publicFilesContainer.Contains(path));
                string physicalPath = path.Remove(0, searchUploadedTxtbox.Text.Length).Insert(0, fb.SelectedPath);
                uploadResponse = Connection.SendUploadRequest(physicalPath, path, searchUploadedTxtbox.Text, 
                    access.ToString(),
                    response["UserId"], response["UserName"]);
                int codeTest = 0;

                try
                {
                    foreach (var item in uploadResponse)
                    {
                        Console.WriteLine(item.Key + ":" + item.Value);
                    }


                    
                    if (!int.TryParse(uploadResponse["Code"], out codeTest))
                    {
                        Console.WriteLine("Error occured.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                if(codeTest == (int)Connection.Code.Action_Confirm)
                {
                    if(access == 1)
                        publicFilesDisplayer.Items.Add(uploadResponse["File_name"]);
                    else
                        myFilesDisplayer.Items.Add(uploadResponse["File_name"]);

                    Console.WriteLine($"{physicalPath} transmitted successfully.");
                    
                    
                    
                }
                else
                {
                    Console.WriteLine($"{physicalPath} DID NOT transmit successfully.");
                }
            }
            uploadedFilesPanel.Visible = false;
            paths.Clear();
            publicFilesContainer.Clear();
            publicFiles.Items.Clear();
            selectedFiles.Items.Clear();
            searchUploadedTxtbox.Text = "";
            MessageBox.Show("All uploaded.");


            //Dictionary<string, string> uploadResponse = Connection.SendUploadRequest(paths, publicFilesContainer, fb.SelectedPath, searchUploadedTxtbox.Text, response["UserId"], response["UserName"]);
            //if (uploadResponse != null)
            //{
            //    
            //}
            //else
            //{
            //    MessageBox.Show("Error occured.");
            //}


        }

        private void removeSelectedBtn_Click_1(object sender, EventArgs e)
        {
            if(publicFiles.SelectedIndex != -1)
            {
                publicFilesContainer.Remove(publicFiles.Text);
                publicFiles.Items.RemoveAt(publicFiles.SelectedIndex);
                
            } 
            else if(selectedFiles.SelectedIndex != -1)
            {
                paths.Remove(selectedFiles.Text);
                selectedFiles.Items.RemoveAt(selectedFiles.SelectedIndex);
            }
            if(selectedFiles.Items.Count == 0 && publicFiles.Items.Count == 0)
            {
                uploadedFilesPanel.Visible = false;
            }
                
        }
    }
}
