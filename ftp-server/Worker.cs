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

            while (true)
            {
                input.SetWorkFinishedStatus(true);
                input.SetWorkFinishedStatus(!input.GetSignal().WaitOne());

                StreamReader r = new StreamReader(input.GetStream());

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
