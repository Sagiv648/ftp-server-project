using EasyEncryption;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ftp_client
{
    public static class Connection
    {
        static TcpClient client = null;
        public static SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);


        
         static readonly string serverIP = "192.168.1.18";
         static readonly int port = 20;
        static IPEndPoint server = new IPEndPoint(IPAddress.Parse(serverIP), port);
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
        
        static readonly string headerRequest = "Code:1%\r\n" +
            "UserId:2%\r\n" +
            "UserName:3%\r\n" +
            "FileName:4%\r\n" +
            "FileSize:5%\r\n" +
            "Access:6%\r\n" +
            "END\r\n";


        static readonly string userInfoRequest = "Code:1%\r\n" +
            "UserName:2%\r\n" +
            "UserEmail:3%\r\n" +
            "HashedPassword:4%\r\n" +
            "END\r\n";

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


        static StringBuilder packetBuilder = new StringBuilder();
        
        
        public static bool InitEmailClient()
        {
            try
            {
                mailClient.Credentials = new NetworkCredential("lsrpacc9@gmail.com", "rcabxcabfdxervev");
                mailClient.EnableSsl = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.Source}");

                return false;
            }

            return true;
        }


        /// <summary>
        /// Will send a userInfo request to the server to try for a session.
        /// </summary>
        /// <returns><b>Dictionary with fields and their values</b> if the session is valid and <b>null</b> if the session is invalid.</returns>
        public static Dictionary<string,string> TrySession()
        {

            packetBuilder.Append(userInfoRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.Session_Trying).ToString());
            
            //Console.WriteLine(packetBuilder);
            StreamReader reader;
            StreamWriter writer;
            Dictionary<string, string> output = null;
            
            try
            {
                client = new TcpClient();
                client.Connect(server);
                string temp = "";

                reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                output = new Dictionary<string, string>();
                while( (temp = reader.ReadLine()) != "END")
                {
                    string[] tempArr = temp.Split(':');
                    output.Add(tempArr[0], tempArr[1]);
                }
            }
            catch (Exception exception)
            {
                client.Close();
                if(exception is SocketException)
                {
                    Console.WriteLine("Network error");
                }
                else if(exception is ObjectDisposedException)
                {
                    Console.WriteLine("Connection closed");
                }
                Console.WriteLine($"{exception.Message}\n{exception.Source}");
                packetBuilder = packetBuilder.Clear();
                return null;
            }
            foreach (var item in output)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            packetBuilder = packetBuilder.Clear();
            
            return output;
        }
        
        /// <summary>
        /// Will send a userInfo request to the server to login to the server.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns><b>Dictionary with the fields and their values</b> if the user managed to login successfully and <b>null</b> if the login didn't manage to login successfully.</returns>
        public static Dictionary<string,string> SendLoginRequest(string userEmail, string userPassword)
        {
            string hashedPassword = SHA.ComputeSHA256Hash(userPassword);
            Console.WriteLine(userEmail);
            Console.WriteLine(userPassword);
            packetBuilder.Append(userInfoRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.Sign_In).ToString());
            packetBuilder = packetBuilder.Replace("3%", userEmail);
            packetBuilder = packetBuilder.Replace("4%", hashedPassword);

            Console.WriteLine("The packet I sent is \n{0}", packetBuilder);
            StreamReader reader;
            StreamWriter writer;
            Dictionary<string, string> output = null;
            string temp = "";
            try
            {
                client = new TcpClient();
                client.Connect(server);
                reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                output = new Dictionary<string, string>();
                while( (temp = reader.ReadLine()) != "END")
                {
                    string[] tempArr = temp.Split(':');
                    output.Add(tempArr[0], tempArr[1]);
                    
                }

            }
            catch (Exception exception)
            {

                client.Close();
                if (exception is SocketException)
                {
                    Console.WriteLine("Network error");
                }
                else if (exception is ObjectDisposedException)
                {
                    Console.WriteLine("Connection closed");
                }
                Console.WriteLine($"{exception.Message}\n{exception.Source}");
                packetBuilder = packetBuilder.Clear();
                return null;
            }
            foreach (var item in output)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            packetBuilder = packetBuilder.Clear();
            return output;
        }

        /// <summary>
        /// Will send a userInfo request to the server to register an account to the server.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns><b>Dictionary with the fields and their values</b> if the user managed to login successfully and <b>null</b> if the login didn't manage to login successfully.</returns>
        public static Dictionary<string,string> SendRegisterRequest(string userName, string userEmail, string userPassword)
        {

            return null;
        }

        /// <summary>
        /// Will send a userInfo request to the server to destroy the current session.
        /// </summary>
        public static void SendLogoutRequest()
        {

        }
    }
}
