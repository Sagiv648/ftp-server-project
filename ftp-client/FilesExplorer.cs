using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_client
{
    public partial class FilesExplorer : Form
    {
        string selectedPath;
        List<string> paths;
        
        string currentPath;
        public FilesExplorer(string selectedPath, List<string> paths)
        {
            InitializeComponent();
           
            this.selectedPath = selectedPath;
            currentPath = selectedPath;
            this.paths = paths;
            filesExplorerListBox.Items.AddRange(Directory.EnumerateFileSystemEntries(selectedPath).ToArray());
            
            filesExplorerListBox.MouseDoubleClick += Enter_Directory;
        }

        private void Enter_Directory(object sender, MouseEventArgs e) 
        {
            
            ListBox ls = (ListBox)sender;
            
            if(e.Button == MouseButtons.Left && Directory.Exists(ls.Text))
            {
                
                
                string selected = ls.Text;
                ls.Items.Clear();
                currentPath = selected;
                ls.Items.AddRange(Directory.EnumerateFileSystemEntries($"{selected}").ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(currentPath != selectedPath)
            {

                
                filesExplorerListBox.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(currentPath);
                
                filesExplorerListBox.Items.AddRange(Directory.EnumerateFileSystemEntries(dir.Parent.FullName).ToArray());
                
                currentPath = dir.Parent.FullName;
            }
           
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if(filesExplorerListBox.SelectedIndex != -1 && 
                paths.FindIndex(x => x == filesExplorerListBox.SelectedItem.ToString()) == -1 && 
                File.Exists(filesExplorerListBox.SelectedItem.ToString()))
            {
                
                pickedListBox.Items.Add(filesExplorerListBox.SelectedItem.ToString());
                paths.Add(filesExplorerListBox.SelectedItem.ToString());
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if(filesExplorerListBox.SelectedIndex != -1)
            {
                paths.Remove(filesExplorerListBox.SelectedItem.ToString());
                pickedListBox.Items.Remove(filesExplorerListBox.SelectedItem.ToString());
            }

        }

        private void finishBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
