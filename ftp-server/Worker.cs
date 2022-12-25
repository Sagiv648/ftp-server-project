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
            Dictionary<string, string> fields = new Dictionary<string, string>();
            int bufferSize = (int)Math.Pow(2, 10) * 64;

            StreamReader reader = null;
            StreamWriter writer = null;

            while (true)
            {
                reader = null;
                writer = null;
                //directoy = null;
                
                fields.Clear();

                input.SetWorkFinishedStatus(true);
                input.SetWorkFinishedStatus(!input.GetSignal().WaitOne());
                try
                {
                    reader = new StreamReader(input.GetStream(), Encoding.ASCII);

                    writer = new StreamWriter(input.GetStream(), Encoding.ASCII, bufferSize);



                    string localStr;

                    while ((localStr = reader.ReadLine()) != "END")
                    {
                        string[] tempArr = localStr.Split(':');
                        fields.Add(tempArr[0], tempArr[1].Trim('\0'));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}\n{ex.Source}");
                    continue;
                }
                

                foreach (var item in fields)
                {
                    Console.WriteLine(item.Key + " " + item.Value);
                }
                   
                // At this point, I have the header/userInfo packet
                string responsePacket;
                string response;

                try
                {
                    switch (Packet.RecieveUserInfo(fields, input.GetClient(), out responsePacket))
                    {
                        case (int)Packet.Code.Session_Trying:
                            //Handle the case where the session is valid
                            Console.WriteLine(responsePacket);
                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("session valid");

                            continue;

                        case (int)Packet.Code.Sign_Up:
                            //Handle the case where the user successfully signed up

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("sign up good");

                            continue;

                        case (int)Packet.Code.Sign_In:
                            //Handle the case where the user successfully signed in

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("user exists");

                            continue;

                        case (int)Packet.Code.Sign_Out:
                            //Handle the case where the user successfully signed out

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("sign out successful");

                            continue;

                        case (int)Packet.Code.Action_Denied:
                            //Handle the case where the packet is valid but the action is denied

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket);
                            Console.WriteLine(response);
                            //responsePacket = $"Code:{(int)Packet.Code.Action_Denied}";
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("denied");

                            continue;

                        default:
                            Console.WriteLine("not a userInfo packet");
                            //Handle the case where the packet is invalid as a userInfo packet AND may be a header packet
                            //Packets are sent in Header->Data, never the opposite
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}\n{ex.Source}");

                    continue;
                }
                //If it reaches here, the packet is not a userInfo packet, therefore here there would be a header packet
                
                
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
