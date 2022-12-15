using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace ftp_server
{
    public partial class ServerMainForm : Form
    {

        //List<Worker> workers = new List<Worker>();

        public static Thread manager = new Thread(new ThreadStart(ManagerMethod));
        public static Thread listenerThread = new Thread(new ThreadStart(ListenerMethod));
        public int workersNum = 10;
        int backlog = 50;
        public static Queue<TcpClient> clientsQueue = new Queue<TcpClient>();
        TcpListener serverListener;

        ServerManagementForm mangForm = null;
        Dictionary<IntPtr, Control> controls = new Dictionary<IntPtr,Control>();
        public ServerMainForm()
        {
            InitializeComponent();
            
            foreach (Control item in Controls)
            {
                controls.Add(item.Handle, item);
            }

            for (int i = 0; i < workersNum; i++)
            {
                Worker.workers.Add(new Worker(Worker.WorkerMethod, new WorkerInput()));
                //workers[workers.Count - 1].GetThread().Start(workers[workers.Count - 1].GetWorkerInput());
            }
            
                

        }

         // Management threads methods
         public static void ListenerMethod()
         {
            Console.WriteLine($"This is the listener {Thread.CurrentThread.ManagedThreadId}");
         }

         public static void ManagerMethod()
         {
            Console.WriteLine($"This is the manager {Thread.CurrentThread.ManagedThreadId}");
         }
 

         bool InitServer()
        {
            int portTest;
            IPAddress addrTest;

            IPEndPoint endPoint;
            if( !int.TryParse(portTextBox.Text, out portTest))
            {
                MessageBox.Show($"Invalid port number {portTextBox.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(ipAddrTextBox.Text.Length == 0)
            {
                addrTest = IPAddress.Any;

            }
            else if(!IPAddress.TryParse(ipAddrTextBox.Text, out addrTest))
            {
                MessageBox.Show($"Invalid IP address {ipAddrTextBox.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            endPoint = new IPEndPoint(addrTest, portTest);

            MessageBox.Show($"Server is running on socket: {endPoint}", "Running", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                serverListener = new TcpListener(endPoint);
                serverListener.Start(backlog);
                
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error occured\n{e.Message}");
                return false;
                
            }
            return true;
            
        }

        private void enterPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && InitServer())
            {
                mangForm = new ServerManagementForm(Worker.workers);
                List<Control> ctrls = controls.Values.ToList();
                ctrls.FindAll(x => x.Visible == true).ForEach(x => x.Visible = false);
                controls[runningLoopLbl.Handle].Visible = true;
                serverRunnerBtn.Text = "Manage";
                serverRunnerBtn.Visible = true;
                mangForm.ShowDialog();
                
            }
            

            
            
        }


        private void serverRunnerBtn_Click(object sender, EventArgs e)
        {
            mangForm = new ServerManagementForm(Worker.workers);
            if (((Control)sender).Text == "Run" && InitServer())
            {
                List<Control> ctrls = controls.Values.ToList();
                ctrls.FindAll(x => x.Visible == true).ForEach(x => x.Visible = false);
                controls[runningLoopLbl.Handle].Visible = true;
                
                serverRunnerBtn.Text = "Manage";
                serverRunnerBtn.Visible = true;
                mangForm.ShowDialog();
            }
            else if( ((Control)sender).Text == "Manage")
            {
                if (!mangForm.Visible)
                {

                    mangForm.ShowDialog();
                }
            }
        }
        
        private void controlAddedEvent(object sender, ControlEventArgs e)
        {
            controls.Add(((Control)sender).Handle, (Control)sender);
        }
        private void controlRemovedEvent(object sender, ControlEventArgs e)
        {

            controls.Remove(((Control)sender).Handle);

           
        }
    }
}
