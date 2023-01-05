using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Windows.Forms;

namespace ftp_server
{
    static class Program
    {

        public static string envConnStr = Environment.GetEnvironmentVariable("sql-connection-string", EnvironmentVariableTarget.User);
        public static string envDbName = Environment.GetEnvironmentVariable("ftp-server-db", EnvironmentVariableTarget.User);
        public static string envFileStoragePath = Environment.GetEnvironmentVariable("Server-files-storage", EnvironmentVariableTarget.User);


        public static Thread manager = new Thread(new ThreadStart(ManagerMethod));
        public static Thread listenerThread = new Thread(new ParameterizedThreadStart(ListenerMethod));

        public static AutoResetEvent listenerManagerCommunication = new AutoResetEvent(false);


        static int workersNum = 50;
        static int backlog = 50;
        public static Queue<TcpClient> clientsQueue = new Queue<TcpClient>();
        static TcpListener serverListener;

        
       
       
        static void Main()
        {
            string message;
            if (!Database.InitTables(out message))
            {
                //TODO: Display error message in the admin panel
                Console.WriteLine(message);
            }
            

            for (int i = 0; i < workersNum; i++)
            {
                Worker.workers.Add(new Worker(Worker.WorkerMethod, new WorkerInput()));
                Worker.workers[i].GetWorkerInput().SetWorkFinishedStatus(true);
                Worker.workers[i].GetThread().Start(Worker.workers[i].GetWorkerInput());

            }

            if (InitServer())
            {
                listenerThread.Start(serverListener);
                manager.Start();
            }



            //Changing worker amount
            //int test;
            //if (!int.TryParse(workerAmountTxtBox.Text, out test))
            //{
            //    MessageBox.Show("Invalid value for workers amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //List<Worker> newList = new List<Worker>(test);


            //Worker.ManagerOverseerMutex.WaitOne(); //Stops the manager thread from working
            //processinglastreqlbl.Visible = true;
            //Worker.UpdateWorkers(workers, newList);
            //workers = newList;
            //Worker.ManagerOverseerMutex.ReleaseMutex(); // At this point the manager thread will take the lead and assign the jobs to the workers himself
            //workersLst.DataSource = workers;
            //processinglastreqlbl.Visible = false;


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ServerMainForm());


        }

        static bool InitServer()
        {

            IPEndPoint endPoint;
            
            endPoint = new IPEndPoint(IPAddress.Loopback, 20);
            Console.WriteLine($"Server is running on socket: {endPoint}");
            
            try
            {
                serverListener = new TcpListener(endPoint);
                serverListener.Server.ReceiveBufferSize = (int)Math.Pow(2, 10) * 64;
                serverListener.Server.SendBufferSize = (int)Math.Pow(2, 10) * 64;
                
                serverListener.Start(backlog);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error occured\n{e.Message}");
                return false;

            }
            return true;

        }

        static void ListenerMethod(object obj)
        {
            while (true)
            {
                TcpListener listener = (TcpListener)obj;
                TcpClient cl = listener.AcceptTcpClient();
                clientsQueue.Enqueue(cl);
            }


        }

        static void ManagerMethod()
        {

            while (true)
            {
                while (clientsQueue.Count == 0)
                {
                    Thread.Sleep(5000);
                }

                Worker.ManagerOverseerMutex.WaitOne();
                int i = 0;
                for (i = 0; i < Worker.workers.Count; i++)
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

    }
}
