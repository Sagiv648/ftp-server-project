﻿using System;
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

       


        public static string ByteArrToString(byte[] arr)
        {
            return new string(arr.ToList().Select(x => (char)x).ToArray());
            
        }

        public static string BuildHeaderPacket()
        {
            

            return null;
        }
        public static Queue<string> BuildDataPacket()
        {
            Queue<string> output = new Queue<string>();

            return output;
        }


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
         * AccessGranted:[]\r\n
         * Your_Files:[]\r\n
         * PublicFiles:[]|[]|[]|[]...|[]\r\n
         * 
         * 
         * 
         * 
         */


        //UserInfo Packet response should look like this
        /* Code:[]\r\n
         * Your_Files:[]\r\n
         * PublicFiles:[]|[]|[]|[]...|[]\r\n
         * 
         * 
         * 
         * 
         */
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
            //Console.WriteLine(packetOut);
            //Perform a Sql query to get user files based on the session, since if the client successfully registered, a session will be created for him.
            //If the client successfully logged in, a session will be created for him.
            //If the client's session still stands, he will have access.
            


            string errMsg = "";

            int userId = -1;
            if (initialCode == (int)Code.Session_Trying)
                userId = Database.GetUserIdByIp(out errMsg, clIp, initialCode);
            else
                userId = int.Parse(response.Split('\r', '\n').ToList().Find(x => x.Contains("UserId")).TrimEnd(new char[] { '\r', '\n' }).Split(':')[1]);
            Console.WriteLine("userId is {0} and code was {1}", userId, initialCode);
            if (userId == -1)
                return null;
            //Console.WriteLine("The problem is probably here right?");
            string directory = Database.GetUserDirectoryById(out errMsg, userId);
            string fileNames = "";

            DirectoryInfo clientDirectory = new DirectoryInfo($"{Database.diskPath}/{directory}");
            List<FileInfo> clientFiles = clientDirectory.GetFiles().ToList();
            //Console.WriteLine("count of client files is {0}",clientFiles.Count);
            int i = 0;
            fileNames = "";
            for (i = 0; i < clientFiles.Count; i++)
            {

                if (i == clientFiles.Count - 1)
                {
                    fileNames += clientFiles[i].Name;
                }
                else
                {
                    fileNames += clientFiles[i].Name + '|';
                }
            }
            //Console.WriteLine("is the mslib after or before this");
            packetOut = packetOut.Replace("1%", fileNames); 
            packetOut = packetOut.Replace("2%", Database.GetAllPublicFiles(out errMsg));
            Console.WriteLine("Packet out is\n{0}", packetOut);


            //Once I have the User's directory name, I can find out what files he has on the server's disk.


            //Console.WriteLine(packetOut);

            return packetOut;
        }

        public static int RecieveHeaderPacket(Dictionary<string,string> buffer,out string responsePacket)
        {
            responsePacket = "";


            return -1;
        }
        public static bool RecieveDataPacket(Queue<byte[]> buffer)
        {
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







            //while (buffer.Count > 0)
            //{
            //    string str = buffer.Dequeue();
            //    if (str[str.Length - 1] != 0)
            //    {
            //        while (buffer.Peek()[buffer.Peek().Length-1] != 0)
            //        {
            //            str += buffer.Dequeue();
            //        }
            //        str += buffer.Dequeue();
            //    }
                
            //    List<string> fields = str.TrimEnd('\0').Split(':').ToList();
            //    string errMsg = "";
            //    if (fields[0] == "Code")
            //    {
            //        int codeTest = 0;
            //        if (!int.TryParse(fields[1], out codeTest))
            //        {
            //            buffer.Clear();
            //            fields.Clear();
            //            return (int)Code.Action_Denied;
            //        }
            //        switch (codeTest)
            //        {
            //            case (int)Code.Sign_Up:

            //                string response = Database.RegisterUser(out errMsg,buffer);
            //                if(response == "")
            //                {
            //                    Console.WriteLine(errMsg);
            //                    return (int)Code.Action_Denied;
            //                }
            //                //Handle DB registering with UserName, UserEmail, HashedPassword
            //                //Return true if user successfully logged in with the specificed credentials, all else will return false
            //                responsePacket = response;
            //                return (int)Code.Sign_Up;

            //            case (int)Code.Sign_In:

            //                //Handle DB logging UserName, UserEmail, HashedPassword
            //                //Return true if user exists and password is correct, all else will return false
            //                return (int)Code.Action_Denied;

            //            case (int)Code.Sign_Out:

            //                //Handle DB session breaking using the Client's IP
            //                return (int)Code.Action_Denied;

            //            case (int)Code.Session_Trying:

                            
            //                string packet = Database.IsSessionValid(out errMsg, clIp);
            //                if (packet.Length == 0)
            //                {
            //                    Console.WriteLine(errMsg);
            //                    return (int)Code.Action_Denied;
            //                }
            //                responsePacket = packet;
            //                return (int)Code.Session_Trying;

            //            default:
                            
            //                fields.Clear();
                            
            //                return -1;

            //        }
            //    }


                
            //}

            
            
        }


    }
}
