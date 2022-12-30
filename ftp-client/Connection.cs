using EasyEncryption;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_client
{
    public static class Connection
    {
        static TcpClient client = null;
        public static SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);


        
         static readonly string serverIP = "192.168.1.18";
         static readonly int port = 20;
        static IPEndPoint server = new IPEndPoint(IPAddress.Parse(serverIP), port);
        
        
        static readonly string headerRequest = "Code:1%\r\n" +
            "UserId:2%\r\n" +
            "UserName:3%\r\n" +
            "RootDirectoryName:4%\r\n" +
            "%" +
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
            packetBuilder = packetBuilder.Append(userInfoRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.Sign_Up).ToString());
            packetBuilder = packetBuilder.Replace("2%", userName);
            packetBuilder = packetBuilder.Replace("3%", userEmail);
            packetBuilder = packetBuilder.Replace("4%", SHA.ComputeSHA256Hash(userPassword));
            Dictionary<string, string> response = null;
            StreamReader reader = null;
            StreamWriter writer = null;
            string temp = "";
            try
            {
                client = new TcpClient();
                client.Connect(server);
                reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                response = new Dictionary<string, string>();
                while((temp = reader.ReadLine()) != "END") 
                {
                    string[] tempArr = temp.Split(':');
                    response.Add(tempArr[0], tempArr[1]);
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

            foreach (var item in response)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            packetBuilder = packetBuilder.Clear();
            return response;

        }

        /// <summary>
        /// Will send a userInfo request to the server to destroy the current session.
        /// </summary>
        public static Dictionary<string,string> SendLogoutRequest(string userId)
        {
            packetBuilder = packetBuilder.Append(userInfoRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.Sign_Out).ToString());
            packetBuilder = packetBuilder.Replace("2%", userId);
            packetBuilder = packetBuilder.Replace("UserName", "UserId");
            Dictionary<string, string> response = null;
            try
            {
                client = new TcpClient();
                client.Connect(server);
                StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                response = new Dictionary<string, string>();
                string temp = "";
                while((temp = reader.ReadLine()) != "END") 
                {
                    string[] tempArr = temp.Split(':');
                    response.Add(tempArr[0], tempArr[1]);
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

            foreach (var item in response)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            packetBuilder = packetBuilder.Clear();
            return response;
        }

        public static Dictionary<string,string> SendUploadRequest(List<string> privatePaths, List<string> publicPaths ,string rootPath, string newRootPath,string userId, string userName )
        {
            //static readonly string headerRequest = "Code:1%\r\n" +
            //"UserId:2%\r\n" +
            //"UserName:3%\r\n" +
            //"RootDirectoryName:4%\r\n" +
            //"%path:%sz:%access\r\n" +
            //"END\r\n";
            packetBuilder = packetBuilder.Append(headerRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Upload).ToString());
            packetBuilder = packetBuilder.Replace("2%", userId);
            packetBuilder = packetBuilder.Replace("3%", userName);
            packetBuilder = packetBuilder.Replace("4%", newRootPath);
            string files = "";
            foreach (string path in privatePaths)
            {
                FileInfo f = new FileInfo(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
                files += $"{path}:{f.Length}:{0}\r\n";
                
                Console.WriteLine(path.Remove(0,newRootPath.Length).Insert(0,rootPath));
            }
            Console.WriteLine("Public files");
            foreach (string path in publicPaths)
            {
                FileInfo f = new FileInfo(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
                files += $"{path}:{f.Length}:{1}\r\n";
                Console.WriteLine(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
            }
            packetBuilder = packetBuilder.Replace("%", files);
            Console.WriteLine("output packet is\n");
            Console.WriteLine(packetBuilder.ToString());
            Dictionary<string, string> response = null;
            try
            {
                client = new TcpClient();
                client.Connect(server);
                StreamWriter writer = new StreamWriter(client.GetStream());
                StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                NetworkStream stream = client.GetStream();
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                MemoryStream mem = new MemoryStream();


                // TODO: Logging system - START batch
                for(int i = 0; i < privatePaths.Count; i++)
                {
                    FileInfo f = new FileInfo(privatePaths[i].Remove(0, newRootPath.Length).Insert(0, rootPath));
                    FileStream fs = f.Open(FileMode.Open,FileAccess.Read);
                    long fSz = f.Length;
                    int read = 0;
                    long totalRead = 0;
                    char[] buffer = new char[4096];
                    byte[] buf = new byte[4096];
                    // TODO: Logging system - START filename

                    Console.WriteLine($"START {f.Name}");

                    //TODO: IMPORTANT! File transfering
                    //while (true)
                    //{
                    //    if (totalRead >= fSz)
                    //        break; 
                    //    read = fs.Read(buf, 0, buf.Length);
                    //    for(int k = 0; k < buffer.Length; k++)
                    //    {
                    //        buffer[k] = (char)buf[k];
                    //    }
                    //    if(read < buf.Length)
                    //    {
                    //        Array.Resize(ref buf, read);
                    //        Array.Resize(ref buffer, read);
                    //    }
                    //    totalRead += read;

                    //    writer.Write(buffer, 0, buffer.Length);
                    //    writer.Flush();
                    //    //mem.Write(buffer, 0, buffer.Length);
                    //    //mem.CopyTo(stream);
                    //    //mem.Seek(totalRead, SeekOrigin.Begin);
                    //    //stream.Write(buffer, 0, buffer.Length);
                        
                    //    //stream.Seek(totalRead, SeekOrigin.Begin);

                    //    // TODO: Logging system - COMMIT filename - {uploaded}\{totalSize}
                    //    Console.WriteLine($"COMMIT {f.Name} - {totalRead}\\{fSz}");
                        
                    //}
                    //mem.Seek(0,SeekOrigin.Begin);
                    //mem.Position = 0;
                    //mem.CopyTo(writer);
                    //writer.Flush();
                    //string str = reader.ReadLine();
                    //if(str != "Next")
                    //{
                    //    Console.WriteLine("It's not next, smth going on");
                    //}
                    // TODO: Logging system - END filename
                    Console.WriteLine($"END {f.Name}");
                }
                // TODO: Logging system - END Batch

                response = new Dictionary<string, string>();
                //string tmp = "";
                //while((tmp = reader.ReadLine()) != "END")
                //{
                //    string[] tempArr = tmp.Split(':');
                //    response.Add(tempArr[0], tempArr[1]);
                //}
                //client.Close();
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
            return response;
            //packetBuilder.Append(headerRequest);
            //packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Upload).ToString());

            //FileInfo file = new FileInfo(localPath);
            //long fSz = file.Length;
            //packetBuilder = packetBuilder.Replace("4%", file.Name);
            //packetBuilder = packetBuilder.Replace("5%", file.Length.ToString());
            //packetBuilder = packetBuilder.Replace("6%", Access);

            //Queue<List<byte>> buffer = new Queue<List<byte>>();

            //FileStream f = file.Open(FileMode.Open, FileAccess.Read);
            //byte[] localBuffer = new byte[4096];

            //long totalLenRead = 0;
            //    int read = 0;
            //FileStream s = File.Create($"C:\\Users\\sagiv\\Projects\\1.College-work-directory\\{file.Name}");
            //Console.WriteLine($"START {s.Name}");
            //while( true)
            //{
            //    if (totalLenRead >= fSz)
            //        break;
            //    read = f.Read(localBuffer,0, localBuffer.Length);
            //    if (read < localBuffer.Length)
            //        Array.Resize(ref localBuffer, read);
            //    totalLenRead += read;
            //    s.Write(localBuffer, 0, localBuffer.Length);
            //    s.Seek(totalLenRead, SeekOrigin.Begin);
            //    Console.WriteLine($"COMMIT {totalLenRead}");


            //}

            //Console.WriteLine($"END {s.Name}");

            //f.Close();
            //s.Close();

            //char[] block = new char[1024];
            ////int index = 0;
            //long totalIndex = 0;
            //int currentRead = 0;
            //byte[] all = new byte[fz];
            //s
            //int index = 0;
            //int t = fRead.Read(all, index, all.Length);
            //byte[] arr = new byte[fz];
            //for (int i = 0; i < all.Length; i++)
            //{
            //    arr[i] = (byte)all[i];
            //    //Console.Write(arr[i]);
            //}

            //StreamWriter writer = new StreamWriter(output, Encoding.Unicode);
            //output.Write(arr,0,arr.Length);
            //output.Close();
            //writer.Close();




            //Console.WriteLine(t);
            //while( true)
            //{
            //    
            //    Console.WriteLine("NEW BLOCK");
            //    Console.WriteLine(new string(block));
            //    totalIndex += currentRead;
            //    index = 0;
            //    if (totalIndex >= fz)
            //        break;
            //}
            
        }
    }
}
