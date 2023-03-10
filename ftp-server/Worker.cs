using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Collections.ObjectModel;

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
                Dictionary<string, string> filesMapping = new Dictionary<string, string>()
                    {
                        {"Path", "" },
                        {"Size", "" },
                        {"Access", "" }
                    };
                try
                {
                    reader = new StreamReader(input.GetStream(), Encoding.ASCII);

                    writer = new StreamWriter(input.GetStream(), Encoding.ASCII, bufferSize);



                    string localStr;
                    
                    while ((localStr = reader.ReadLine()) != "END")
                    {
                        string[] tempArr = localStr.Split(':');
                        if(tempArr.Length == 3)
                        {
                            filesMapping["Path"] =tempArr[0];
                            filesMapping["Size"] =tempArr[1];
                            filesMapping["Access"] =tempArr[2];
                        }
                        else
                        fields.Add(tempArr[0], tempArr[1].Trim('\0'));
                    }
                    foreach (var item in fields)
                    {
                        Console.WriteLine(item.Key+":"+item.Value);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}\n{ex.Source}");
                    Console.WriteLine(ex.StackTrace);
                    continue;
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
                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket, (int)Packet.Code.Session_Trying);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("session valid");
                            input.GetClient().Dispose();
                            continue;

                        case (int)Packet.Code.Sign_Up:
                            //Handle the case where the user successfully signed up

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket, (int)Packet.Code.Sign_Up);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("sign up good");
                            input.GetClient().Dispose();
                            continue;

                        case (int)Packet.Code.Sign_In:
                            //Handle the case where the user successfully signed in

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket, (int)Packet.Code.Sign_In);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("user exists");
                            input.GetClient().Dispose();
                            continue;

                        case (int)Packet.Code.Sign_Out:
                            //Handle the case where the user successfully signed out

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket, (int)Packet.Code.Sign_Out);
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("sign out successful");
                            input.GetClient().Dispose();
                            continue;

                        case (int)Packet.Code.Action_Denied:
                            //Handle the case where the packet is valid but the action is denied

                            response = Packet.BuildUserInfoPacket(input.GetClient(), responsePacket, (int)Packet.Code.Action_Denied);
                            Console.WriteLine(response);
                            //responsePacket = $"Code:{(int)Packet.Code.Action_Denied}";
                            writer.Write(response);
                            writer.Flush();
                            Console.WriteLine("denied");
                            input.GetClient().Dispose();
                            continue;

                        default:
                            Console.WriteLine("not a userInfo packet");
                            //Handle the case where the packet is invalid as a userInfo packet AND may be a header packet
                            //Packets are sent in Header->Data, never the opposite
                            break;
                    } 
                    
                    
                    
                    //If it reaches here, the packet is not a userInfo packet, therefore here there would be a header packet
                    switch(Packet.RecieveHeaderPacket(fields, filesMapping, input.GetClient(),out responsePacket))
                    {
                        case (int)Packet.Code.File_Upload:
                            Console.WriteLine("upload");
                            //Collect the relevant data, like the uploaded file size, name and access modifier and use them to listen for the client's data packet
                            //writer.Write(responsePacket);
                            //writer.Flush();
                            
                            break;


                        case (int)Packet.Code.File_Download:
                            Console.WriteLine("download");
                            //Collect the relevant data, like the file name.
                            //Transmit a header packet to the client and then straight after transmit the file's data packet.
                            writer.Write(responsePacket);
                            writer.Flush();
                            break;


                        case (int)Packet.Code.File_Delete:
                            Console.WriteLine("delete");
                            //Collect the relevant data, like the file name
                            //Perform the operation and send a header packet with the renewed files' list
                            writer.Write(responsePacket);
                            writer.Flush();
                            break;


                        case (int)Packet.Code.File_Rename:
                            Console.WriteLine("rename");
                            //Collect the relevant data, like the file name
                            //Perform the rename, both on disk and db and then send a header packet with the renewed files list
                            writer.Write(responsePacket);
                            writer.Flush();
                            break;

                        case (int)Packet.Code.Public_Files_Refresh:

                            break;
                        default:
                            Console.WriteLine("wild one");
                            writer.Write(responsePacket);
                            writer.Flush();
                            //Not a userInfo packet and not a header packet, therefore a wild packet, will not be accepted and will be discarded
                            break;
                    }


                }
                catch (Exception ex)
                {
                    input.GetClient().Dispose();
                    Console.WriteLine($"{ex.Message}\n{ex.Source}");
                    Console.WriteLine(ex.StackTrace);
                    continue;
                }





                //1. Read from the stream.
                //2. Parse the bytes.

                input.GetClient().Dispose();
                //Read the bytes from the client's stream and parse the packets accordingly
                filesMapping.Clear();

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
