using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using EasyEncryption;
using System.IO;
using System.Text;

namespace ftp_client
{
    public static class Program
    {
        public enum Code
        {

            Sign_Up = 0,
            Sign_In = 1,
            Sign_Out = 2,
            File_Upload = 3,
            File_Download = 4,
            File_Delete = 5,
            File_Rename = 6,
            Session_Trying = 10,
            Action_Confirm = 200,
            Action_Denied = 400,
            User_Already_Exists = 500


        }

        //Header packets that should always be followed with data packets are (from client to server and the opposite):
        //1. Header packets with File_Upload code
        //2. Header packets with File_Download code



        //Header Packet should look like this
        /* UserName:[]\r\n
         * UserEmail:[]\r\n
         * Code:[]\r\n
         * Directory:[][Directory]\r\n
         * FileName:[]\r\n
         * FileSize:[]\r\n
         * AccessModifier:[]
         * 
         * 
         * 
         * 
         */

        //------------------------------------------

        //Data Packet should look like this
        /*
         * File bytes...
         */

        //UserInfo Packet request should look like this
        /* Code:[]\r\n
         * UserName:[]\r\n
         * UserEmail:[]\r\n
         * HashedPassword:[]
         * 
         * 
         * 
         */
        //UserInfo Packet response should look like this
        /* Code:[]\r\n
         * AccessGranted:[]\r\n
         * Your_Files:[]\r\n
         * PublicFiles:[]|[]|[]|[]...|[]
         * 
         * 
         * 
         * 
         */


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static TcpClient cl = new TcpClient();
        public static readonly string serverIP = "192.168.1.18";
        public static readonly int port = 20;
        [STAThread]
        static void Main()
        {

            cl.Connect(new IPEndPoint(IPAddress.Parse(serverIP), port));
            StreamWriter r = new StreamWriter(cl.GetStream(), Encoding.ASCII);
            string msg = "Code:10\0\r\nUserName:[]\0\r\nUserEmail:[]\0\r\nHashedPassword:[]\0\r\n";
            
            r.WriteLine(msg);
            r.Flush();

            
            //TODO: Conditionally run the inital form depends on the response the client get from the server about whether the session is valid or not.
            //Session valid ? => straight to the application.
            //Session invalid ? => straight to the login screen.


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            Application.Run(new MainMenuForm());
            
        }
    }
}
