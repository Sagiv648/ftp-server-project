using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ftp_server
{
    public class WorkerInput
    {
        TcpClient cl;
        NetworkStream clientStream;
        bool workFinished;
        

        public TcpClient GetClient() => cl;

        public NetworkStream GetStream() => clientStream;

        public void SetClient(TcpClient client)
        {
            cl = client;
            clientStream = cl.GetStream();
        }
        public bool IsWorkFinished() => workFinished;

        public void SetWorkFinishedStatus(bool status) => workFinished = true;
        
        
    }
}
