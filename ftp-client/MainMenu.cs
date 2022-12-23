using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ftp_client
{
    public partial class MainMenu : Form
    {
        TcpClient cl = new TcpClient();
        static readonly string serverIP = "192.168.1.18";
        static readonly int port = 7777;

        public MainMenu()
        {
            InitializeComponent();
            
            cl.Connect(new IPEndPoint(IPAddress.Parse(serverIP), port));
            StreamWriter r = new StreamWriter(cl.GetStream(), Encoding.ASCII);
            string msg = "Code:10\0\r\nUserName:[]\0\r\nUserEmail:[]\0\r\nHashedPassword:[]\0\r\n";
            //Console.WriteLine(msg);
            r.WriteLine(msg);
            r.Flush();
        }
    }
}
