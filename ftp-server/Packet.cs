using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_server
{
    public static class Packet
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
         * AccessModifier:[]\r\n
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

       
        static string filesSpace = Program.envFileStoragePath == null ? $"{Directory.GetCurrentDirectory()}\\Files-space" : Program.envFileStoragePath;

        public static string BuildUserInfoPacket(TcpClient cl, string response, int initialCode)
        {
            IPAddress clIp = IPAddress.Parse(((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());
            bool isActionConfirm = true;

            if(initialCode != (int)Code.Sign_Out)
                isActionConfirm = response.Contains("UserName") && response.Contains("UserId");
            else
                response = "Action:Signout";

            string packetOut = $"Code:{(isActionConfirm ?  (int)Code.Action_Confirm :(int)Code.Action_Denied )}" +
                $"\r\n{(isActionConfirm ? response + "\r\nYour_Files:1%\r\nPublicFiles:2%" : "Error:" + response) }\r\nEND\r\n";
            Console.WriteLine(packetOut);
            if (!isActionConfirm || initialCode == (int)Code.Sign_Out)
                return packetOut;
 
            string errMsg = "";

            int userId = -1;
            if (initialCode == (int)Code.Session_Trying)
                userId = Database.GetUserIdByIp(out errMsg, clIp, initialCode);
            else
                userId = int.Parse(response.Split('\r', '\n').ToList().Find(x => x.Contains("UserId")).TrimEnd(new char[] { '\r', '\n' }).Split(':')[1]);
            Console.WriteLine("userId is {0} and code was {1}", userId, initialCode);
            if (userId == -1)
                return null;
            
            string directory = Database.GetUserDirectoryById(out errMsg, userId);
            string fileNames = "";
            string clientRootPath = filesSpace + $"\\{directory}";
            
            DirectoryInfo clientDirectory = new DirectoryInfo($"{clientRootPath}");
            List<FileInfo> clientFiles = clientDirectory.GetFiles("*",SearchOption.AllDirectories).ToList();
            
            int i = 0;
            fileNames = "";
            for (i = 0; i < clientFiles.Count; i++)
            {

                if (i == clientFiles.Count - 1)
                {
                    fileNames += clientFiles[i].FullName.Remove(0,clientRootPath.Length+1);
                }
                else
                {
                    fileNames += clientFiles[i].FullName.Remove(0,clientRootPath.Length+1) + '|';
                }
            }
            
            packetOut = packetOut.Replace("1%", fileNames); 
            packetOut = packetOut.Replace("2%", Database.GetAllPublicFiles(out errMsg));
            //Console.WriteLine("Packet out is\n{0}", packetOut);
            return packetOut;
        }

        public static int RecieveHeaderPacket(Dictionary<string,string> buffer, Dictionary<string, string> fileMapping, TcpClient client,out string responsePacket)
        {
            responsePacket = "";

            if (buffer.ContainsKey("Code"))
            {
                int codeTest = 0;
                if (!int.TryParse(buffer["Code"], out codeTest))
                    return -1;

                switch (codeTest)
                {
                    case (int)Code.File_Upload:
                        responsePacket = WriteFiles(fileMapping, buffer, client).ToString();

                        if (responsePacket == false.ToString())
                            return (int)Code.Action_Denied;

                        return (int)Code.File_Upload;

                    case (int)Code.File_Download:

                        if(!SendFiles(buffer,client))
                            return (int)Code.Action_Denied;

                        return (int)Code.File_Download;

                    case (int)Code.File_Rename:


                        return (int)Code.File_Rename;

                    case (int)Code.File_Delete:

                        return (int)Code.File_Delete;
                        



                    default:
                        return -1;
                        
                }
            }


            return -1;
        }
        //TODO: Write the logic of writing it on the server's disk AND appending it to the DB
        public static bool WriteFiles(Dictionary<string, string> filesMapping, Dictionary<string,string> bufferInput ,TcpClient client)
        {
            
            string filePath = filesSpace + $"\\{bufferInput["UserId"]}_{bufferInput["UserName"]}".Trim();
            string response = "";
            
            try
            {
                Console.WriteLine("The input path is: {0}", filesMapping["Path"]);
                if (!filesMapping.ContainsKey("Path") || !filesMapping.ContainsKey("Size") || !filesMapping.ContainsKey("Access"))
                    return false;
                
                string[] pathComps = filesMapping["Path"].Split('\\');

                
                string fileName = pathComps[pathComps.Length - 1];
                if(pathComps.Length >= 2)
                {
                    DirectoryInfo location = new DirectoryInfo(filePath);
                    
                    for(int i = 0; i < pathComps.Length-1; i++)
                        location = location.CreateSubdirectory($"./{pathComps[i]}");

                    filePath = location.FullName;
                    
                }
                
                long size = long.Parse(filesMapping["Size"]);
                Console.WriteLine($"{filePath + "\\" + filesMapping["Path"]}: {filesMapping["Size"]}: {filesMapping["Access"]}");
                byte[] buffer = new byte[64000];
                int read = 0;
                long totalRead = 0;
                FileInfo outputFile = new FileInfo($"{filePath + "\\" + fileName}");
                
                StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                StreamReader reader = new StreamReader(client.GetStream());
                
                FileStream outputFileStream = outputFile.Create();
                if (filesMapping["Access"] == "1")
                    outputFile.Attributes = FileAttributes.Hidden;
               

                writer.Write("START\r\nEND\r\n");
                writer.Flush();
                //TODO: IMPORTANT! File transfering reciever
                response = $"Code:{(int)Code.Action_Confirm}\r\n" +
                $"File:{filesMapping["Path"]}\r\n" +
                $"END\r\n";

                Console.WriteLine("totalread = {0}\\{1}", totalRead, size);
                while (totalRead < size)
                {

                    read = client.GetStream().Read(buffer, 0, buffer.Length);
                    outputFileStream.Write(buffer, 0, read);
                    totalRead += read;
                    Console.WriteLine("totalread = {0}\\{1}", totalRead, size);
                    if (totalRead >= size)
                    {
                        Console.WriteLine(response);
                        writer.Write(response);
                        writer.Flush();
                        
                        break;
                    }
                    //writer.Write($"Recv-{read}\r\nEND\r\n");
                    //writer.Flush();
                    
                }
                
               
                Console.WriteLine("All passed?");
                string msg = "";
                Database.WriteFile(filesMapping["Path"], bufferInput["UserId"], filesMapping["Access"], out msg);
                if(msg != "")
                    Console.WriteLine(msg);
                outputFileStream.Close();

            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}\n{exception.Source}");
                Console.WriteLine(exception.StackTrace);
                return false;
            }
            return true;
        }
        
        public static bool SendFiles(Dictionary<string, string> bufferInput, TcpClient client)
        {
            try
            {
                string storagePath = filesSpace;
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                //Console.WriteLine("Here?");
                DirectoryInfo dir = new DirectoryInfo(storagePath);
                //Console.WriteLine(dir.FullName);
                
                FileInfo[] allFiles = dir.GetFiles($"*", SearchOption.AllDirectories);
                FileInfo file = null;
                foreach (var item in allFiles)
                {
                    
                    if (item.FullName.Contains(bufferInput["File"]) && item.Attributes != FileAttributes.Hidden)
                    {
                        file = item;
                        break;
                    }
                }
                if (file == null)
                {
                    client.Close();
                    return false;

                }
                //Console.WriteLine(file.FullName);
                writer.Write($"\r\n{file.Name}:{file.Length}\r\nEND\r\n");
                writer.Flush();
                string tmp = "";
                
                while((tmp = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"tmp is {tmp}");
                    if (tmp == "START")
                        break;
                }
                if (tmp != "START")
                {
                    Console.WriteLine("tmp is not START it's {0}", tmp);
                    return false;
                }
                    
                FileStream downloadedFile = file.OpenRead();
                long fSize = file.Length;
                //int read = 0;
                //long totalRead = 0;
                byte[] buffer = new byte[64000];

                downloadedFile.CopyTo(client.GetStream());
                downloadedFile.Close();
                Console.WriteLine("All transfered");

            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"ERROR AT: {ex.Source} - {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            

            return true;
        }

        public static int RecieveUserInfo(Dictionary<string,string> bufferInput, TcpClient cl, out string responsePacket)
        {
            responsePacket = "";
            Dictionary<string,string> buffer = new Dictionary<string, string>(bufferInput);
            //UserInfo Packet request should look like this
            /* Code:[]\r\n
             * UserName:[]\r\n
             * UserEmail:[]\r\n
             * HashedPassword:[]\r\n
             * 
             * 
             * 
             */
            //UserInfo Packet response should look like this
            /* Code:[]\r\n
             * UserName:[]\r\n
             * Your_Files:[]\r\n
             * PublicFiles:[]|[]|[]|[]...|[]\r\n
             * 
             * 
             * 
             * 
             */
            IPAddress clIp = IPAddress.Parse(((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());

            if (!buffer.ContainsKey("Code"))
                return -1;

            int codeTest = 0;
            if (!int.TryParse(buffer["Code"], out codeTest))
            {
                buffer.Clear();
                
                return (int)Code.Action_Denied;
            }
            string errMsg = "";
            string response = "";
            switch (codeTest)
            {
                case (int)Code.Sign_Up:
                    //Handle DB registering with UserName, UserEmail, HashedPassword
                    //Return true if user successfully logged in with the specificed credentials, all else will return false

                    response = Database.RegisterUser(out errMsg, buffer, clIp);
                    responsePacket = response;
                    if (response == "User exists")
                    {
                        
                        Console.WriteLine(errMsg);
                        return (int)Code.Action_Denied;
                    }
                    
                    
                    return (int)Code.Sign_Up;

                case (int)Code.Sign_In:
                    //Handle DB logging UserName, UserEmail, HashedPassword
                    //Return true if user exists and password is correct, all else will return false

                    response = Database.CheckUserLogin(out errMsg, buffer, clIp);
                    responsePacket = response;
                    if(response == "Invalid credentials")
                    {
                        Console.WriteLine(errMsg);
                        return (int)Code.Action_Denied;
                    }
                    return (int)Code.Sign_In;

                case (int)Code.Sign_Out:
                    //Handle DB session breaking using the Client's IP
                    Database.UserLogout(out errMsg, buffer, clIp);
                    if(errMsg != "")
                        return (int)Code.Action_Denied;
                    return (int)Code.Sign_Out;

                case (int)Code.Session_Trying:


                    response = Database.IsSessionValid(out errMsg, clIp);
                    responsePacket = response;
                    if (response == "Invalid session")
                    {
                        Console.WriteLine(errMsg);
                        return (int)Code.Action_Denied;
                    }
                    
                    return (int)Code.Session_Trying;

                default:

                    buffer.Clear();

                    return -1;

            }

        }


    }
}
