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

       


        public static string ByteArrToString(byte[] arr)
        {
            return new string(arr.ToList().Select(x => (char)x).ToArray());
            
        }

        public static Queue<string> BuildHeaderPacket()
        {
            Queue<string> output = new Queue<string>();

            return output;
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

        
        public static Queue<string> BuildUserInfoPacket(TcpClient cl)
        {
            IPAddress clIp = IPAddress.Parse(((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());

            string packetOut = $"Code:{Code.Action_Confirm}\r\nAccessGranted:Yes\r\nYour_Files:%\r\nPublicFiles:%";
            //Perform a Sql query to get user files based on the session, since if the client successfully registered, a session will be created for him.
            //If the client successfully logged in, a session will be created for him.
            //If the client's session still stands, he will have access.
            
            string errMsg = "";
            int userId = Database.GetUserIdByIp(out errMsg,clIp);
            if (userId == -1)
                return null;

            string directory = Database.GetUserDirectoryById(out errMsg, userId);
            DirectoryInfo clientDirectory = new DirectoryInfo(directory);
            List<FileInfo> clientFiles = clientDirectory.GetFiles().ToList();
            int i = 0;
            string fileNames = "";
            for(i = 0; i < clientFiles.Count; i++)
            {
                
                if(i == clientFiles.Count - 1)
                {
                    fileNames += clientFiles[i].Name;
                }
                else
                {
                    fileNames += clientFiles[i].Name + '|';
                }
            }
            packetOut = packetOut.Substring(0, packetOut.IndexOf('%')) + fileNames + packetOut.Substring(packetOut.IndexOf('%')+1) ;

            packetOut = packetOut.Substring(0, packetOut.Length - 1) + Database.GetAllPublicFiles(out errMsg);
            //Once I have the User's directory name, I can find out what files he has on the server's disk.


            Queue<string> output = new Queue<string>();
            

            return output;
        }

        public static bool RecieveHeaderPacket(Queue<byte[]> buffer)
        {
            


            return true;
        }
        public static bool RecieveDataPacket(Queue<byte[]> buffer)
        {
            return true;
        }
        public static int RecieveUserInfo(Queue<string> bufferInput, TcpClient cl)
        {

            Queue<string> buffer = new Queue<string>(bufferInput);
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

            //Console.WriteLine(Convert.ToString(((IPEndPoint)cl.Client.RemoteEndPoint).Address));



            IPAddress clIp = IPAddress.Parse(((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString());
            while (buffer.Count > 0)
            {
                string str = buffer.Dequeue();
                if (str[str.Length - 1] != 0)
                {
                    while (buffer.Peek()[buffer.Peek().Length-1] != 0)
                    {
                        str += buffer.Dequeue();
                    }
                    str += buffer.Dequeue();
                }
                Console.WriteLine(str);
                List<string> fields = str.TrimEnd('\0').Split(':').ToList();
                if (fields[0] == "Code")
                {
                    int codeTest = 0;
                    if (!int.TryParse(fields[1], out codeTest))
                    {
                        buffer.Clear();
                        fields.Clear();
                        return (int)Code.Action_Denied;
                    }
                    switch (codeTest)
                    {
                        case (int)Code.Sign_Up:

                            //Handle DB registering with UserName, UserEmail, HashedPassword
                            //Return true if user successfully logged in with the specificed credentials, all else will return false
                            return (int)Code.Action_Denied;

                        case (int)Code.Sign_In:

                            //Handle DB logging UserName, UserEmail, HashedPassword
                            //Return true if user exists and password is correct, all else will return false
                            return (int)Code.Action_Denied;

                        case (int)Code.Sign_Out:

                            //Handle DB session breaking using the Client's IP
                            return (int)Code.Action_Denied;

                        case (int)Code.Session_Trying:

                            string msg = "";
                            if (!Database.IsSessionValid(out msg, clIp))
                            {
                                Console.WriteLine(msg);
                                return (int)Code.Action_Denied;
                            }
                            return (int)Code.Session_Trying;

                        default:
                            
                            fields.Clear();
                            
                            return -1;

                    }
                }


                
            }

            
            return -1;
        }


    }
}
