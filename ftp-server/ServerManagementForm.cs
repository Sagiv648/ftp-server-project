using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;

namespace ftp_server
{
    public partial class ServerManagementForm : Form
    {

        List<Worker> workers;
       
        public ServerManagementForm(List<Worker> workers)
        {
            InitializeComponent();
            this.workers = workers;
            
            workerAmountTxtBox.Text = workers.Count.ToString();

            workersLst.DataSource = workers;
            
        }
        
        private void workersAmountEditBtn_Click(object sender, EventArgs e)
        {
            
            
            int test;
            if(!int.TryParse(workerAmountTxtBox.Text, out test))
            {
                MessageBox.Show("Invalid value for workers amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Worker> newList = new List<Worker>(test);
            
            
            Worker.ManagerOverseerMutex.WaitOne(); //Stops the manager thread from working
            processinglastreqlbl.Visible = true;
            Worker.UpdateWorkers(workers, newList);
            workers = newList;
            Worker.ManagerOverseerMutex.ReleaseMutex(); // At this point the manager thread will take the lead and assign the jobs to the workers himself
            workersLst.DataSource = workers;
            processinglastreqlbl.Visible = false;
            
        }

        private void ServerManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
