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
        public static Thread listenerThread = new Thread(new ParameterizedThreadStart(ListenerMethod));

        public static AutoResetEvent listenerManagerCommunication = new AutoResetEvent(false);
        

        public int workersNum = 10;
        int backlog = 50;
        public static Queue<TcpClient> clientsQueue = new Queue<TcpClient>();
        TcpListener serverListener;

        

        ServerManagementForm mangForm = null;
        Dictionary<IntPtr, Control> controls = new Dictionary<IntPtr,Control>();
        public ServerMainForm()
        {
            InitializeComponent();
            string message;
            if(!Database.InitTables(out message))
            {
                //TODO: Display error message in the admin panel
                Console.WriteLine(message);
            }
            

            foreach (Control item in Controls)
            {
                controls.Add(item.Handle, item);
            }

            for (int i = 0; i < workersNum; i++)
            {
                Worker.workers.Add(new Worker(Worker.WorkerMethod, new WorkerInput()));
                Worker.workers[i].GetWorkerInput().SetWorkFinishedStatus(true);
                Worker.workers[i].GetThread().Start(Worker.workers[i].GetWorkerInput());
                
            }
                
                
            
            
                

        }

         // Management threads methods
         public static void ListenerMethod(object obj)
         {
            while (true)
            {
                TcpListener listener = (TcpListener)obj;
                TcpClient cl = listener.AcceptTcpClient();
                clientsQueue.Enqueue(cl);
            }
            
            
         }

         public static void ManagerMethod()
         {
            


            
            while (true)
            {
                while (clientsQueue.Count == 0)
                {
                    Thread.Sleep(5000);
                }
                
                Worker.ManagerOverseerMutex.WaitOne();
                int i = 0;
                for(i = 0; i < Worker.workers.Count; i++)
                {
                    if (Worker.workers[i].GetWorkerInput().IsWorkFinished())
                    {
                        Worker.workers[i].GetWorkerInput().SetClient(clientsQueue.Dequeue());
                        //Worker.workers[i].GetWorkerInput().SetWorkFinishedStatus(false);

                        Worker.workers[i].GetWorkerInput().GetSignal().Set();

                       
                        break;
                    }
                }
                Worker.ManagerOverseerMutex.ReleaseMutex();
                if (i == Worker.workers.Count)
                    Thread.Sleep(1000);


                
                

            }
            

            
                
            


            
         }
 

         bool InitServer()
         {
           
            IPEndPoint endPoint;
            //endPoint = new IPEndPoint(IPAddress.Any, 20);
            endPoint = new IPEndPoint(IPAddress.Loopback, 20);
            MessageBox.Show($"Server is running on socket: {endPoint}", "Running", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                serverListener = new TcpListener(endPoint);
                serverListener.Server.ReceiveBufferSize = (int)Math.Pow(2, 10) * 64;
                serverListener.Server.SendBufferSize = (int)Math.Pow(2, 10) * 64;
                //serverListener.Server.ReceiveBufferSize = 50;
                //serverListener.Server.SendBufferSize = 50;
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
                listenerThread.Start(serverListener);
                manager.Start();
                mangForm = new ServerManagementForm(Worker.workers);
                
                List<Control> ctrls = controls.Values.ToList();
                ctrls.FindAll(x => x.Visible == true).ForEach(x => x.Visible = false);
                controls[runningLoopLbl.Handle].Visible = true;
                serverRunnerBtn.Text = "Manage";
                serverRunnerBtn.Visible = true;
                mangForm.ShowDialog();
                //mangForm.Show();
                
            }
            

            
            
        }


        private void serverRunnerBtn_Click(object sender, EventArgs e)
        {
            mangForm = new ServerManagementForm(Worker.workers);
            if (((Control)sender).Text == "Run" && InitServer())
            {
                listenerThread.Start(serverListener);
                manager.Start();
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
