using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using EasyEncryption;
using System.Security.Cryptography;

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
        [STAThread]
        static void Main()
        {
            string hashedPass = EasyEncryption.MD5.ComputeMD5Hash("12345");
            Console.WriteLine("MD5: {0}", EasyEncryption.MD5.ComputeMD5Hash("12345"));
            Console.WriteLine("SHA1: {0}", EasyEncryption.SHA.ComputeSHA1Hash("12345"));
            Console.WriteLine("SHA256: {0}", EasyEncryption.SHA.ComputeSHA256Hash("vxhnvakhvhtfzueav123456bfui1234567890-+13919mmm"));
            
            //TODO: Conditionally run the inital form depends on the response the client get from the server about whether the session is valid or not.
            //Session valid ? => straight to the application.
            //Session invalid ? => straight to the login screen.


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            Application.Run(new MainMenu());
            
        }
    }
}
