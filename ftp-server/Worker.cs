using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

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
            
            Console.WriteLine($"Hello from {Thread.CurrentThread.ManagedThreadId}");
            input.SetWorkFinishedStatus(true);
        }

        public static void UpdateWorkers(List<Worker> oldWorkers, List<Worker> newWorkers)
        {
            foreach (var item in oldWorkers)
            {
                while (!item.GetWorkerInput().IsWorkFinished()) ; //Spinning lock
            }
            
            oldWorkers.ForEach(x => x.GetThread().Abort());
            int newWorkersLen = newWorkers.Capacity;
            for(int i = 0; i < newWorkersLen; i++)
            {
                newWorkers.Add(new Worker(WorkerMethod, new WorkerInput()));
                
                
            }
            workers = newWorkers;

        }

    }
}
