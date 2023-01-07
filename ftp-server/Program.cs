using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ftp_server
{
    static class Program
    {

        public static readonly string envConnStr = Environment.GetEnvironmentVariable("sql-connection-string", EnvironmentVariableTarget.User);
        public static readonly string envDbName = Environment.GetEnvironmentVariable("ftp-server-db", EnvironmentVariableTarget.User);
        public static readonly string envFileStoragePath = Environment.GetEnvironmentVariable("Server-files-storage", EnvironmentVariableTarget.User);


        static readonly Thread manager = new Thread(new ThreadStart(ManagerMethod));
        static readonly Thread listenerThread = new Thread(new ParameterizedThreadStart(ListenerMethod));

        static readonly AutoResetEvent listenerManagerCommunication = new AutoResetEvent(false);


        static readonly int workersNum = 50;
        static readonly int backlog = 50;
        static readonly Queue<TcpClient> clientsQueue = new Queue<TcpClient>();
        static TcpListener serverListener;

        
       
       
        static void Main()
        {
            string message;
            if (!Database.InitTables(out message))
            {
                
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
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
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
