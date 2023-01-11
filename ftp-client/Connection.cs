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
        static readonly string serverIP = "127.0.0.1";
        static readonly int port = 20;
        static IPEndPoint server = new IPEndPoint(IPAddress.Parse(serverIP), port);
        
        
        static readonly string headerRequest = "Code:1%\r\n" +
            "UserId:2%\r\n" +
            "UserName:3%\r\n" +
            "RootDirectoryName:4%\r\n" +
            "%" +
            "END\r\n";

        static readonly string headerDownloadRequest = "Code:1%\r\n" +
            "UserId:2%\r\n" +
            "UserName:3%\r\n" +
            "%\r\n" +
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
            Public_Files_Refresh = 7,
            Session_Trying = 10,
            Action_Confirm = 200,
            Action_Denied = 400,
            User_Already_Exists = 500


        }

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

        public static Dictionary<string,string> SendRefreshRequest(string userName, string userId)
        {
            
            string packet = $"Code:{((int)Code.Public_Files_Refresh)}\r\n" +
                $"UserName:{userName}\r\nUserId:{userId}\r\nEND\r\n";
            Dictionary<string, string> result = null;
            try
            {
                using (client = new TcpClient())
                {
                    client.Connect(server);
                    StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                    
                    
                    writer.Write(packet);
                    writer.Flush();

                    StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                    result = new Dictionary<string, string>();
                    string tmp = "";
                    while ((tmp = reader.ReadLine()) != null)
                    {
                        string[] tempArr = tmp.Split(':');
                        if (tempArr[0] == "Your_Files" || tempArr[0] == "PublicFiles")
                        {
                            string files = "";
                            for (int i = 1; i < tempArr.Length; i++)
                                files += $"{tempArr[i]}:";
                            files = files.Remove(files.Length - 1, 1);

                            result.Add(tempArr[0], files);
                        }
                        else if(tempArr.Length == 2)
                            result.Add(tempArr[0], tempArr[1]);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
            return result;
        }

        /// <summary>
        /// Will send a userInfo request to the server to try for a session.
        /// </summary>
        /// <returns><b>Dictionary with fields and their values</b> if the session is valid and <b>null</b> if the session is invalid.</returns>
        public static Dictionary<string,string> TrySession()
        {
            StringBuilder packetBuilder = new StringBuilder();

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
                    if (tempArr[0] == "Your_Files" || tempArr[0] == "PublicFiles")
                    {
                        string files = "";
                        for (int i = 1; i < tempArr.Length; i++)
                            files += $"{tempArr[i]}:";
                        files = files.Remove(files.Length - 1, 1);
                        
                        output.Add(tempArr[0], files);
                    }
                    else
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
                
                return null;
            }
            client.Close();
            
            
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
            StringBuilder packetBuilder = new StringBuilder();

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
                
                return null;
            }
            foreach (var item in output)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            
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
            StringBuilder packetBuilder = new StringBuilder();

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
                
                return null;

            }

            foreach (var item in response)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            
            return response;

        }

        /// <summary>
        /// Will send a userInfo request to the server to destroy the current session.
        /// </summary>
        public static Dictionary<string,string> SendLogoutRequest(string userId)
        {
            StringBuilder packetBuilder = new StringBuilder();

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
                Console.WriteLine(exception.StackTrace);
                return null;

            }

            foreach (var item in response)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            client.Close();
            
            return response;
        }



        public static bool SendPicPreviewRequest(string userName, string userId, string fileId, string physicalPathDestination)
        {
            StringBuilder packetBuilder = new StringBuilder();

            packetBuilder = packetBuilder.Append(headerDownloadRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Download).ToString());
            packetBuilder = packetBuilder.Replace("2%", userId);
            packetBuilder = packetBuilder.Replace("3%", userName);
            packetBuilder = packetBuilder.Replace("%", $"File:{fileId}");
            //Console.WriteLine(packetBuilder.ToString());
            Console.WriteLine(packetBuilder);

            try
            {
                client = new TcpClient();
                client.Connect(server);
                StreamWriter writer = new StreamWriter(client.GetStream());
                StreamReader reader = new StreamReader(client.GetStream());
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                string tmp = "";

                //Console.WriteLine(reader.ReadLine() + " here one");
                string[] tmpArr = null;
                while ((tmp = reader.ReadLine()) != null)
                {
                    Console.WriteLine(tmp);
                    //tmp = reader.ReadLine();
                    tmpArr = tmp.Split(':');
                    if (tmpArr.Length == 2)
                        break;
                }
                if (tmpArr.Length != 2)
                {
                    Console.WriteLine("Here");
                    client.Close();

                    return false;
                }
                string fileName = "";
                long fileSz = 0;
                Console.WriteLine(tmpArr[0] + ":" + tmpArr[1] + " here sec");
                for (int i = 0; i < tmpArr.Length; i++)
                {
                    if (!long.TryParse(tmpArr[i], out fileSz))
                    {
                        fileName = tmpArr[i];
                    }
                }
                Console.WriteLine("sz is {0}", fileSz);
                if (fileSz == 0)
                {
                    Console.WriteLine("what here?");
                    client.Close();

                    return false;
                }

                writer.Write("START\r\nEND\r\n");
                writer.Flush();

                FileInfo downloadLogger = new FileInfo("download.log");



                MemoryStream mem = new MemoryStream();
                
               
                FileStream loggerStream = downloadLogger.OpenWrite();
                writer = new StreamWriter(loggerStream);
                byte[] buffer = new byte[64000];



                writer.WriteLine($"START DOWNLOAD {fileName} - 0/{fileSz} - {DateTime.Now}");
                writer.BaseStream.Seek(downloadLogger.Length, SeekOrigin.Begin);
                long totalRead = 0;
                int read = 0;

                Console.WriteLine("Reading now");

                while (totalRead < fileSz)
                {
                    read = client.GetStream().Read(buffer, 0, buffer.Length);
                    totalRead += read;
                    mem.Write(buffer, 0, read);

                    mem.Seek(totalRead, SeekOrigin.Begin);
                    writer.WriteLine($"COMMIT DOWNLOAD {fileName} - {totalRead}/{fileSz} - {DateTime.Now}");
                    writer.BaseStream.Seek(downloadLogger.Length, SeekOrigin.Begin);

                }
                client.Close();
                
                


                writer.WriteLine($"END DOWNLOAD {fileName} - {totalRead}/{fileSz} - {DateTime.Now}");
                writer.Close();
                Form picView = new PicturePreviewForm(mem);
               
                picView.ShowDialog();
                picView.Dispose();
                mem.Close();


                

            }
            catch (Exception exception)
            {

                Console.WriteLine($"ERROR AT {exception.Source} - {exception.Message}");
                Console.WriteLine(exception.StackTrace);
                return false;
            }



            return true;
        }

        public static bool SendDownloadRequest(string userName, string userId, string fileId, string physicalPathDestination)
        {
            StringBuilder packetBuilder = new StringBuilder();

            packetBuilder = packetBuilder.Append(headerDownloadRequest);
            packetBuilder = packetBuilder.Replace("1%", ((int)Code.File_Download).ToString());
            packetBuilder = packetBuilder.Replace("2%", userId);
            packetBuilder = packetBuilder.Replace("3%", userName);
            packetBuilder = packetBuilder.Replace("%", $"File:{fileId}");
            //Console.WriteLine(packetBuilder.ToString());
            Console.WriteLine(packetBuilder);
            
            try
            {
                client = new TcpClient();
                client.Connect(server);
                StreamWriter writer = new StreamWriter(client.GetStream());
                StreamReader reader = new StreamReader(client.GetStream());
                writer.Write(packetBuilder.ToString());
                writer.Flush();
                string tmp = "";

                //Console.WriteLine(reader.ReadLine() + " here one");
                string[] tmpArr = null;
                while ((tmp = reader.ReadLine()) != null)
                {
                    Console.WriteLine(tmp);
                    //tmp = reader.ReadLine();
                    tmpArr = tmp.Split(':');
                    if (tmpArr.Length == 2)
                        break;
                }
                if(tmpArr.Length != 2)
                {
                    Console.WriteLine("Here");
                    client.Close();
                    
                    return false;
                }
                string fileName = "";
                long fileSz = 0;
                Console.WriteLine(tmpArr[0] + ":" + tmpArr[1]+" here sec");
                for(int i = 0; i < tmpArr.Length; i++)
                {
                    if (!long.TryParse(tmpArr[i], out fileSz))
                    {
                        fileName = tmpArr[i];
                    }
                }
                Console.WriteLine("sz is {0}", fileSz);
                if(fileSz == 0)
                {
                    Console.WriteLine("what here?");
                    client.Close();
                    
                    return false;
                }

                writer.Write("START\r\nEND\r\n");
                writer.Flush();

                FileInfo downloadLogger = new FileInfo("download.log");
                FileInfo newFile = new FileInfo($"{physicalPathDestination}\\{fileName}");

                

                FileStream fStream = newFile.Create();
                fStream.Close();
                fStream = newFile.OpenWrite();
                FileStream loggerStream = downloadLogger.OpenWrite();
                writer = new StreamWriter(loggerStream);
                byte[] buffer = new byte[64000];

               

                writer.WriteLine($"START DOWNLOAD {fileName} - 0/{fileSz} - {DateTime.Now}");
                writer.BaseStream.Seek(downloadLogger.Length, SeekOrigin.Begin);
                long totalRead = 0;
                int read = 0;

                Console.WriteLine("Reading now");

                while(totalRead < fileSz)
                {
                    read = client.GetStream().Read(buffer, 0, buffer.Length);
                    totalRead += read;
                    fStream.Write(buffer, 0, read);
                    
                    fStream.Seek(totalRead, SeekOrigin.Begin);
                    writer.WriteLine($"COMMIT DOWNLOAD {fileName} - {totalRead}/{fileSz} - {DateTime.Now}");
                    writer.BaseStream.Seek(downloadLogger.Length, SeekOrigin.Begin);

                }
                client.Close();
                fStream.Close();
                writer.WriteLine($"END DOWNLOAD {fileName} - {totalRead}/{fileSz} - {DateTime.Now}");
                writer.Close();

            }
            catch (Exception exception)
            {

                Console.WriteLine($"ERROR AT {exception.Source} - {exception.Message}");
                Console.WriteLine(exception.StackTrace);
                return false;
            }


            
            return true;

        }

        public static Dictionary<string,string> SendUploadRequest(string physicalPath, string virtualPath,string virtualRootPath,string access,string userId, string userName )
        {
            StringBuilder packetBuilder = new StringBuilder();

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
            FileInfo log = new FileInfo("upload.log");
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
                
                //string t = reader.ReadLine();
                //Console.WriteLine("t is {0}", t);
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
                
                

               

                if (!response.ContainsKey("Code"))
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
                Console.WriteLine(exception.StackTrace);
                return null;
            }
            
            return response;
  
        }
    }
}
