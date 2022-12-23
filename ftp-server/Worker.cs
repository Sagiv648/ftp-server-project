using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace ftp_server
{
    public class Worker
    {
        Thread t;
        WorkerInput input;
       
        
        public static Mutex ManagerOverseerMutex = new Mutex();

        public static List<Worker> workers = new List<Worker>();


        public Worker(ParameterizedThreadStart workingMethod, WorkerInput input)
        {
            t = new Thread(workingMethod);
            this.input = input;
            
            

        }

        public Thread GetThread() => t;

        public void SetWorkerInput(WorkerInput input) => this.input = input;

        public WorkerInput GetWorkerInput() => input;

        
        
        public static void WorkerMethod(object obj)
        {
            WorkerInput input = (WorkerInput)obj;
            //DirectoryInfo directoy = null;
            Queue<string> localBuffer = new Queue<string>();
            ;
            int bufferSize = (int)Math.Pow(2, 10) * 64;

            StreamReader reader = null;
            StreamWriter writer = null;

            while (true)
            {
                reader = null;
                writer = null;
                //directoy = null;
                
                localBuffer.Clear();

                input.SetWorkFinishedStatus(true);
                input.SetWorkFinishedStatus(!input.GetSignal().WaitOne());

                reader = new StreamReader(input.GetStream(),Encoding.ASCII);
       
                writer = new StreamWriter(input.GetStream(), Encoding.ASCII ,bufferSize);
                
                
                Console.WriteLine("Am I here?");
                string localStr;
                
                while( (localStr = reader.ReadLine()).Length != 0)
                   localBuffer.Enqueue(localStr);
                // At this point, I have the header/userInfo packet

                switch (Packet.RecieveUserInfo(localBuffer,input.GetClient()))
                {
                    case (int)Packet.Code.Session_Trying:
                        Console.WriteLine("session valid");
                        //Handle the case where the session is valid
                        break;
                    case (int)Packet.Code.Sign_Up:
                        Console.WriteLine("sign up good");
                        //Handle the case where the user successfully signed up
                        break;

                    case (int)Packet.Code.Sign_In:
                        Console.WriteLine("user exists");
                        //Handle the case where the user successfully signed in
                        break;

                    case (int)Packet.Code.Sign_Out:
                        Console.WriteLine("sign out successful");
                        //Handle the case where the user successfully signed out
                        break;

                    case (int)Packet.Code.Action_Denied:
                        Console.WriteLine("denied");
                        //Handle the case where the packet is valid but the action is denied
                        break;

                    default:
                        Console.WriteLine("not a userInfo packet");
                        //Handle the case where the packet is invalid as a userInfo packet AND may be a header packet
                        //Packets are sent in Header->Data, never the opposite
                        break;
                }


                
                
                //1. Read from the stream.
                //2. Parse the bytes.


                //Read the bytes from the client's stream and parse the packets accordingly


            }



        }

        public static void UpdateWorkers(List<Worker> oldWorkers, List<Worker> newWorkers)
        {
            foreach (var item in oldWorkers)
            {
                while (!item.GetWorkerInput().IsWorkFinished()) ; //Spinning lock
            }
            
            for(int i = 0; i < oldWorkers.Count; i++)
            {

                oldWorkers[i].GetWorkerInput().GetSignal().Dispose();
                oldWorkers[i].GetThread().Abort();
                
            }
            oldWorkers.Clear();
            int newWorkersLen = newWorkers.Capacity;
            for(int i = 0; i < newWorkersLen; i++)
            {
                newWorkers.Add(new Worker(WorkerMethod, new WorkerInput()));
                newWorkers[i].t.Start(newWorkers[i].GetWorkerInput());
                
                
            }
            workers = newWorkers;

        }

        public override string ToString()
        {
            return $"Worker - {t.ManagedThreadId}, " + string.Format("{0}", input.IsWorkFinished() ? "Offline" : "Online");
        }

    }
}
