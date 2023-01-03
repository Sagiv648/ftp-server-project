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

        public static readonly string compName = Environment.MachineName;
        
         static readonly string serverIP = "192.168.1.18";
         static readonly int port = 20;
        static IPEndPoint server = compName == "DESKTOP-DM0U3G8" ? new IPEndPoint(IPAddress.Parse("127.0.0.1"),port) : new IPEndPoint(IPAddress.Parse(serverIP), port);
        
        
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



        //Backup
        //public static Dictionary<string, string> SendUploadRequest(List<string> privatePaths, List<string> publicPaths, string rootPath, string newRootPath, string userId, string userName)
        //{
        //    //static readonly string headerRequest = "Code:1%\r\n" +
        //    //"UserId:2%\r\n" +
        //    //"UserName:3%\r\n" +
        //    //"RootDirectoryName:4%\r\n" +
        //    //"%path:%sz:%access\r\n" +
        //    //"END\r\n";
        //    packetBuilder = packetBuilder.Append(headerRequest);
        //    packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Upload).ToString());
        //    packetBuilder = packetBuilder.Replace("2%", userId);
        //    packetBuilder = packetBuilder.Replace("3%", userName);
        //    packetBuilder = packetBuilder.Replace("4%", newRootPath);
        //    string files = "";
        //    foreach (string path in privatePaths)
        //    {
        //        FileInfo f = new FileInfo(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
        //        files += $"{path}:{f.Length}:{0}\r\n";

        //        Console.WriteLine(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
        //    }
        //    Console.WriteLine("Public files");
        //    foreach (string path in publicPaths)
        //    {
        //        FileInfo f = new FileInfo(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
        //        files += $"{path}:{f.Length}:{1}\r\n";
        //        Console.WriteLine(path.Remove(0, newRootPath.Length).Insert(0, rootPath));
        //    }
        //    packetBuilder = packetBuilder.Replace("%", files);
        //    Console.WriteLine("output packet is\n");
        //    Console.WriteLine(packetBuilder.ToString());
        //    Dictionary<string, string> response = null;
        //    try
        //    {
        //        client = new TcpClient();
        //        client.Connect(server);
        //        StreamWriter writer = new StreamWriter(client.GetStream());
        //        StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
        //        NetworkStream stream = client.GetStream();
        //        writer.Write(packetBuilder.ToString());
        //        writer.Flush();
        //        //MemoryStream mem = new MemoryStream();


        //        // TODO: Logging system - START batch
        //        for (int i = 0; i < privatePaths.Count; i++)
        //        {
        //            MemoryStream mem = new MemoryStream();
        //            FileInfo f = new FileInfo(privatePaths[i].Remove(0, newRootPath.Length).Insert(0, rootPath));
        //            FileStream fs = f.Open(FileMode.Open, FileAccess.Read);
        //            long fSz = f.Length;
        //            int read = 0;
        //            long totalRead = 0;
        //            char[] buffer = new char[4096];
        //            byte[] buf = new byte[4096];
        //            FileInfo log = new FileInfo(".log");
        //            StreamWriter logStream = new StreamWriter(log.OpenWrite());
        //            Console.WriteLine($"START {f.Name}");

        //            string tmp = "";
        //            while ((tmp = reader.ReadLine()) != "END")
        //            {
        //                if (tmp.Equals("START"))
        //                {
        //                    logStream.WriteLine($"START COMMIT{f.Name}-{DateTime.Now}");
        //                    break;
        //                }

        //            }

        //            fs.CopyTo(stream);
        //            fs.Close();
        //            logStream.WriteLine($"END COMMIT{f.Name}-{DateTime.Now}");
        //            //TODO: IMPORTANT! File transfering sender
        //            //while (fs.Position < fs.Length) 
        //            //{
        //            //    fs.CopyTo(stream);
        //            //    while((tmp = reader.ReadLine()) != "END")
        //            //    {
        //            //        Console.WriteLine(tmp);
        //            //        if (tmp.Contains("Recv"))
        //            //        {


        //            //        }
        //            //    }
        //            //    Console.WriteLine("fs Position = {0}\\{1}", fs.Position,fs.Length);
        //            //}

        //            //while((tmp = reader.ReadLine()) != "END")
        //            //{
        //            //    if(tmp.Equals("All"))
        //            //    {
        //            //        client.Close();
        //            //        break;
        //            //    }
        //            //}

        //            //client.GetStream().Close();
        //            Console.WriteLine("server disconnected");


        //            //while(totalRead < fSz)
        //            //{


        //            //    read = fs.Read(buf, 0, buf.Length);
        //            //    totalRead += read;

        //            //    if (!client.Connected)
        //            //    {
        //            //        Console.WriteLine("disconnected");
        //            //        break;
        //            //    }

        //            //    while((tmp = reader.ReadLine()) != "END")
        //            //    {
        //            //        if (tmp.Contains("Recv"))
        //            //        {
        //            //            Console.WriteLine(tmp);
        //            //        }
        //            //    }
        //            //    //Array.Clear(buf, 0, buf.Length - read);
        //            //    stream.Write(buf, 0, read);
        //            //    fs.Seek(totalRead, SeekOrigin.Begin);
        //            //}


        //            //IMPORTANT! File transfering

        //            Console.WriteLine($"END {f.Name}");

        //        }

        //        // TODO: Logging system - END Batch

        //        response = new Dictionary<string, string>();
        //        //string tmp = "";
        //        //while((tmp = reader.ReadLine()) != "END")
        //        //{
        //        //    string[] tempArr = tmp.Split(':');
        //        //    response.Add(tempArr[0], tempArr[1]);
        //        //}
        //        //client.Close();
        //    }
        //    catch (Exception exception)
        //    {

        //        client.Close();
        //        if (exception is SocketException)
        //        {
        //            Console.WriteLine("Network error");
        //        }
        //        else if (exception is ObjectDisposedException)
        //        {
        //            Console.WriteLine("Connection closed");
        //        }
        //        Console.WriteLine($"{exception.Message}\n{exception.Source}");
        //        packetBuilder = packetBuilder.Clear();
        //        return null;
        //    }
        //    return response;
        //    //packetBuilder.Append(headerRequest);
        //    //packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Upload).ToString());

        //    //FileInfo file = new FileInfo(localPath);
        //    //long fSz = file.Length;
        //    //packetBuilder = packetBuilder.Replace("4%", file.Name);
        //    //packetBuilder = packetBuilder.Replace("5%", file.Length.ToString());
        //    //packetBuilder = packetBuilder.Replace("6%", Access);

        //    //Queue<List<byte>> buffer = new Queue<List<byte>>();

        //    //FileStream f = file.Open(FileMode.Open, FileAccess.Read);
        //    //byte[] localBuffer = new byte[4096];

        //    //long totalLenRead = 0;
        //    //    int read = 0;
        //    //FileStream s = File.Create($"C:\\Users\\sagiv\\Projects\\1.College-work-directory\\{file.Name}");
        //    //Console.WriteLine($"START {s.Name}");
        //    //while( true)
        //    //{
        //    //    if (totalLenRead >= fSz)
        //    //        break;
        //    //    read = f.Read(localBuffer,0, localBuffer.Length);
        //    //    if (read < localBuffer.Length)
        //    //        Array.Resize(ref localBuffer, read);
        //    //    totalLenRead += read;
        //    //    s.Write(localBuffer, 0, localBuffer.Length);
        //    //    s.Seek(totalLenRead, SeekOrigin.Begin);
        //    //    Console.WriteLine($"COMMIT {totalLenRead}");


        //    //}

        //    //Console.WriteLine($"END {s.Name}");

        //    //f.Close();
        //    //s.Close();

        //    //char[] block = new char[1024];
        //    ////int index = 0;
        //    //long totalIndex = 0;
        //    //int currentRead = 0;
        //    //byte[] all = new byte[fz];
        //    //s
        //    //int index = 0;
        //    //int t = fRead.Read(all, index, all.Length);
        //    //byte[] arr = new byte[fz];
        //    //for (int i = 0; i < all.Length; i++)
        //    //{
        //    //    arr[i] = (byte)all[i];
        //    //    //Console.Write(arr[i]);
        //    //}

        //    //StreamWriter writer = new StreamWriter(output, Encoding.Unicode);
        //    //output.Write(arr,0,arr.Length);
        //    //output.Close();
        //    //writer.Close();




        //    //Console.WriteLine(t);
        //    //while( true)
        //    //{
        //    //    
        //    //    Console.WriteLine("NEW BLOCK");
        //    //    Console.WriteLine(new string(block));
        //    //    totalIndex += currentRead;
        //    //    index = 0;
        //    //    if (totalIndex >= fz)
        //    //        break;
        //    //}

        //}



        public static Dictionary<string,string> SendUploadRequest(string physicalPath, string virtualPath,string virtualRootPath,string access,string userId, string userName )
        {
            //static readonly string headerRequest = "Code:1%\r\n" +
            //"UserId:2%\r\n" +
            //"UserName:3%\r\n" +
            //"RootDirectoryName:4%\r\n" +
            //"%path:%sz:%access\r\n" +
            //"END\r\n";
            Dictionary<string, string> response = null;
            packetBuilder = packetBuilder.Append(headerRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Upload).ToString());
            packetBuilder = packetBuilder.Replace("2%", userId);
            packetBuilder = packetBuilder.Replace("3%", userName);
            packetBuilder = packetBuilder.Replace("4%", virtualRootPath);
            
            FileInfo physicalFile = new FileInfo(physicalPath);
            FileStream physicalFileStream = physicalFile.Open(FileMode.Open, FileAccess.ReadWrite);
            packetBuilder = packetBuilder.Replace("%", $"{virtualPath}:{physicalFile.Length}:{access}\r\n");
            Console.WriteLine("output packet is\n");
            Console.WriteLine(packetBuilder.ToString());
            FileInfo log = new FileInfo(".log");
            FileStream logFileStream = log.OpenWrite();
            StreamWriter logStream = new StreamWriter(logFileStream);
            try
            {
                client = new TcpClient();
                client.Connect(server);
                StreamWriter writer = new StreamWriter(client.GetStream());
                StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                NetworkStream stream = client.GetStream();
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                
                
                
                Console.WriteLine($"START {physicalFile.Name}");
                client.SendTimeout = 600000;
                string tmp = "";
                
                while ((tmp = reader.ReadLine()) != "END")
                {
                    if (tmp.Equals("START"))
                    {
                        logStream.BaseStream.Seek(logStream.BaseStream.Length, SeekOrigin.Begin);
                        logStream.WriteLine($"START COMMIT {physicalFile.Name} - {DateTime.Now}");
                        break;
                    }

                }
                //TODO: IMPORTANT! File transfering sender
                
                physicalFileStream.CopyTo(stream);
                response = new Dictionary<string, string>();
                Dictionary<string, string> test = new Dictionary<string, string>();
                
                string t = reader.ReadLine();
                Console.WriteLine("t is {0}", t);
                while((tmp = reader.ReadLine()) != null)
                {
                    Console.WriteLine(tmp);
                    string[] tmpArr = tmp.Split(':');
                    if(tmpArr.Length == 2)
                    {
                        Console.WriteLine(tmpArr[0] + ":" + tmpArr[1]);
                        response.Add(tmpArr[0], tmpArr[1]);
                    }
                }

                

               

                if (!response.ContainsKey("Code") || !response.ContainsKey("File"))
                {
                    Console.WriteLine("Doesn't contain one");
                    logStream.Close();
                    logFileStream.Dispose();
                    return null;

                }
                  

                logStream.BaseStream.Seek(logStream.BaseStream.Length, SeekOrigin.Begin);
                logStream.WriteLine($"END COMMIT {physicalFile.Name} - {DateTime.Now}");


                logStream.Close();
                physicalFileStream.Close();
               
                
                
            }
            catch (Exception exception)
            {

                client.Close();
                logStream.Close();
                physicalFileStream.Close();
                Console.WriteLine($"{exception.Message}\n{exception.Source}");
                packetBuilder = packetBuilder.Clear();
                return null;
            }
            packetBuilder = packetBuilder.Clear();
            return response;
  
        }
    }
}
