using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net;
using EasyEncryption;
using System.IO;
using System.Data.SqlTypes;
using System.Data;

namespace ftp_server
{
    //TODO: All the writes to the database should be atomic
    public static class Database
    {


        static readonly Mutex dbAccess = new Mutex();

        static readonly string connectionString = Program.envConnStr == null ? "Server=localhost\\SQLEXPRESS;Integrated Security=SSPI;database=FTP_Server" : Program.envConnStr;
        
        static readonly string filesSpace = Program.envFileStoragePath == null ? $"{Directory.GetCurrentDirectory()}\\Files-space" : Program.envFileStoragePath;

        
        public static bool InitTables(out string msg)
        {
            Dictionary<string, string> tablesCreationCommandMapping = new Dictionary<string, string>
            {
                {
                    "Users",
                $"create table Users (" +
                $"Id int NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                $"User_name nvarchar(50), " +
                $"User_email nvarchar(70)," +
                $"Password nvarchar(100), " +
                $"Directory nvarchar(100))"
                },
                {
                    "Sessions",
                $"create table Sessions (" +
                $"Id int NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                $"Logged_in SMALLDATETIME, " +
                $"User_Ip nvarchar(20), " +
                "User_name nvarchar(50), " +
                "User_id int )"
                },
                {
                "Files",
                $"create table Files (" +
                $"Id int NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                $"User_Id int," +
                $"File_name nvarchar(100)," +
                $"File_size bigint," +
                $"Access BIT)"

                }
            };
            try
            {
                List<string> tables = new List<string>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = new SqlCommand("", conn);
                    sqlCmd.CommandText = $"select name from sys.tables";
                    using(SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader["name"].ToString());
                        }
                    }
                    for (int i = 0; i < tablesCreationCommandMapping.Keys.Count; i++)
                    {
                        int j = 0;
                        for (j = 0; j < tables.Count; j++)
                        {
                            if (tables[j] == tablesCreationCommandMapping.Keys.ToList()[i])
                                break;
                        }
                        if (j == tables.Count)
                        {
                            try
                            {
                                sqlCmd.CommandText = tablesCreationCommandMapping[tablesCreationCommandMapping.Keys.ToList()[i]];
                                sqlCmd.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                msg = ex.Message;
                                Console.WriteLine($"{ex.Message}");
                                Console.WriteLine(ex.StackTrace);

                                return false;

                            }

                        }

                    }

                }

                


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;


            }
  
            msg = "";
            return true;
        }

        public static string IsSessionValid(out string msg, IPAddress clientIp)
        {
            msg = "";
            string userName = "";
            int userId = -1;
            dbAccess.WaitOne();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = $"select * from Sessions where User_Ip = \'{clientIp}\'";

                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //output.Add($"Id:{reader["Id"]}");
                            userName = reader["User_name"].ToString();
                            userId = int.Parse(reader["User_id"].ToString());


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                
                
                dbAccess.ReleaseMutex();

                msg = ex.Message;
                Console.WriteLine(msg + "{0}", ex.Source);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
            
            dbAccess.ReleaseMutex();
            if (userName == "" || userId == -1)
                return "Invalid session";

            return $"UserName:{userName}\r\nUserId:{userId}";
        }

        public static int GetUserIdByIp(out string msg,IPAddress clientIp, int initalCode)
        {
            msg = "";
            dbAccess.WaitOne();
            int userId = -1;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    if (initalCode == (int)Packet.Code.Session_Trying || initalCode == (int)Packet.Code.Sign_In)
                        sqlCmd.CommandText = $"select User_id from Sessions where User_Ip = \'{clientIp}\'";
                    else
                        sqlCmd.CommandText = $"select Id from Users where Id = SCOPE_IDENTITY()";

                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userId = int.Parse(reader[initalCode == (int)Packet.Code.Sign_Up ? "Id" : "User_id"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                dbAccess.ReleaseMutex();
                msg = ex.Message;
                Console.WriteLine(msg + "{0}", ex.Source);
                Console.WriteLine(ex.StackTrace);
                
                return -1;
            }
            
            dbAccess.ReleaseMutex();
            return userId;
        }
        public static string GetUserDirectoryById(out string msg, int userId)
        {
            msg = "";
            string output = "";
            dbAccess.WaitOne();
            try
            {

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand(); 
                    sqlCmd.CommandText = $"select Directory from Users where Id = {userId}";

                    using(SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output = reader["Directory"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                dbAccess.ReleaseMutex();
                msg = ex.Message;
                Console.WriteLine(msg + "{0}",ex.Source);
                Console.WriteLine(ex.StackTrace);
                
                return "";
                
            }
            
            dbAccess.ReleaseMutex();

            return output;
        }

        public static string GetAllPublicFiles(out string msg)
        {
            msg = "";
            dbAccess.WaitOne();
            string response = "";
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = $"select * from Files where Access = 1";
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fileName = reader["File_name"].ToString();
                            
                            response += $"{reader["Id"]}:{fileName.Remove(0, fileName.IndexOf('\\') + 1)}:{reader["File_size"]}|";
                        }
                        if (response != "")
                            response = response.Remove(response.Length - 1, 1);
                    }
                }   
            }
            catch (Exception ex)
            {
                dbAccess.ReleaseMutex();
                msg = ex.Message;
        
                Console.WriteLine(msg + "{0}", ex.Source);
                Console.WriteLine(ex.StackTrace);
                return "";

                
            }
            
            dbAccess.ReleaseMutex();
            return response;
        }
        

        public static FileInfo GetFileById(string fileId, out string msg)
        {
            msg = "";
            FileInfo response = null;
            dbAccess.WaitOne();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = $"select File_name from Files where Id = {fileId}";
                    using(SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            response = new FileInfo($"{filesSpace}\\{reader["File_name"]}");
                        }
                        if(response == null)
                        {
                            dbAccess.ReleaseMutex();
                            return null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                dbAccess.ReleaseMutex();
                msg = ex.Message;
                Console.WriteLine(ex.StackTrace);
                return null;
                
            }
            dbAccess.ReleaseMutex();
            return response;
        }

        public static string GetUserFilesById(int userId,out string msg)
        {
            msg = "";
            string response = "";
            dbAccess.WaitOne();
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = $"select * from Files where User_Id = {userId}";
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fileName = reader["File_name"].ToString();
                            response += $"{reader["Id"]}:{fileName.Remove(0, fileName.IndexOf('\\')+1)}:{reader["File_size"]}|";
                        }
                        if (response != "")
                            response = response.Remove(response.Length - 1, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                dbAccess.ReleaseMutex();
                msg = ex.Message;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
            dbAccess.ReleaseMutex();
            Console.WriteLine(response);
            return response;
        }
        public static Dictionary<string,string> WriteFile(FileInfo file, string userId, string access,out string msg)
        {
            msg = "";
            Dictionary<string,string> result= null;
            try
            {

                dbAccess.WaitOne();

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    string filePath = file.FullName.Remove(0, filesSpace.Length + 1);
                    Console.WriteLine("File length is {0}", file.Length);
                    sqlCmd.CommandText = $"insert Files output inserted.* values ({userId}, '{filePath}', {file.Length},{access})";
                    using(SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            result = new Dictionary<string, string>()
                            {
                                {"Id", reader["Id"].ToString() },
                                {"File_name", reader["File_name"].ToString() },
                                {"File_size", reader["File_size"].ToString() },
                                {"Access", reader["Access"].ToString() }

                            };
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                
                dbAccess.ReleaseMutex();
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                msg = e.Message;
                return null;
                
            }
            dbAccess.ReleaseMutex();
            return result;
        }
        
        public static void CreateSession(SqlCommand sqlCmd,string userName, IPAddress clIp, int userId)
        {
            
            sqlCmd.CommandText = $"insert into Sessions values (GETDATE(), \'{clIp}\', \'{userName} \', {userId})";
            sqlCmd.ExecuteNonQuery();
        }
        public static void DestroySession(SqlCommand sqlCmd,int id)
        {
            sqlCmd.CommandText = $"delete from Sessions where User_id={id}";
            sqlCmd.ExecuteNonQuery();
        }
        

        ///<summary>
        ///<param name="errMsg"> <b>errMsg</b> - Reference to a string object which will contain an error message.<br/>If no error occurs the string will be empty.</param><br/>
        /// Method does all the necessary integrity checks that a register functionality should do.<br/>
        /// If the new user's integrity checks out, the function returns an empty string and a relevant session will be created, all else will return a string with the msg to the user.
        /// </summary> 

        ///<returns><i>a message as to why the user couldn't register, else an empty string</i></returns>
        public static string RegisterUser(out string errMsg, Dictionary<string,string> fieldValueMapping, IPAddress clIp)
        {
            errMsg = "";
            dbAccess.WaitOne();
            string userName = "";
            int id = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = $"select Id from Users where User_email = \'{fieldValueMapping["UserEmail"]}\'";
                    using(SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["Id"].ToString());
                        }
                
                        if (id != -1)
                        {
                            dbAccess.ReleaseMutex();
                            return "User exists";
                        }
                    }
                    sqlCmd.CommandText = $"insert into Users values (\'{fieldValueMapping["UserName"]}\'" +
                    $",\'{fieldValueMapping["UserEmail"]}\', \'{fieldValueMapping["HashedPassword"]}\', \'\')";

                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.CommandText = $"select User_name, Id from Users where Id = SCOPE_IDENTITY()";
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["Id"].ToString());
                            userName = reader["User_name"].ToString();
                        }
                    }
                    string userFile = Directory.CreateDirectory($"Files-space/{id}_{userName}").Name;
                    sqlCmd.CommandText = $"update Users set Directory = \'{userFile}\' where Id = {id}";
                    sqlCmd.ExecuteNonQuery();

                }
                
               
               


            }
            catch (Exception ex)
            {
                
                
                dbAccess.ReleaseMutex();
                errMsg = ex.Message;
                Console.WriteLine($"{errMsg}\n{ex.Source}");
                Console.WriteLine(ex.StackTrace);
                return "db-error";
                
            }
            
            dbAccess.ReleaseMutex();
            return $"UserName:{userName}\r\nUserId:{id}";
        }

        

        /// <summary>
        /// The procedure checks 2 things: <br/>1) If the user exists at all.<br/>2) If the hashed passwords (hashed with EasyEncryption's SHA256) match.
        ///
        /// </summary>
        /// <param name="errMsg"></param>
        /// <param name="fieldValueMapping"></param>
        /// <param name="clIp">f</param>
        /// <returns>Returns a string representing the session created for the user</returns>
        public static string CheckUserLogin(out string errMsg, Dictionary<string, string> fieldValueMapping, IPAddress clIp)
        {
          
            errMsg = "";
            
            string userName = "";
            int id = -1;
            string hashedPassword = "";
            
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = $"select * from Users where User_email=\'{fieldValueMapping["UserEmail"]}\'";
                    using(SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["Id"].ToString());
                            userName = reader["User_name"].ToString();
                            hashedPassword = reader["Password"].ToString();
                        }
                    }
                    if (id == -1)
                    {

                        
                        return "Invalid credentials";
                    }
                    else if (!hashedPassword.Equals(fieldValueMapping["HashedPassword"]))
                    {

                        
                        return "Invalid credentials";
                    }
                    CreateSession(sqlCmd,userName, clIp, id);
                }
                
            }
            catch (Exception ex)
            {
                
                
                errMsg = ex.Message;
                Console.WriteLine($"{errMsg}\n{ex.Source}");
                Console.WriteLine(ex.StackTrace);
                return "db-error";
            }

            
            
            return $"UserName:{userName}\r\nUserId:{id}";

        }

        
        public static void UserLogout(out string errMsg, Dictionary<string, string> fieldValueMapping, IPAddress clIp)
        {
            errMsg = "";
            
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCmd = conn.CreateCommand();
                    DestroySession(sqlCmd,int.Parse(fieldValueMapping["UserId"]));

                }
                
                
            }
            catch (Exception ex)
            {
                
                

                errMsg = ex.Message;
                Console.WriteLine($"{errMsg}\n{ex.Source}");
                Console.WriteLine(ex.StackTrace);
            }
            
           

        }

        public static void DestroyFileEntry(string fileId, string userId, out string err)
        {
            err = "";
            dbAccess.WaitOne();
            try
            {
                string logicalPath = "";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = $"delete from Files output deleted.File_name where Id = {fileId} and User_Id = {userId}";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logicalPath = reader["File_name"].ToString();
                        }
                    }
                }
                DirectoryInfo dir = new DirectoryInfo($"{filesSpace}\\{logicalPath.Substring(0,logicalPath.LastIndexOf('\\'))}");
                Console.WriteLine(dir.FullName);
                List<FileSystemInfo> fEntries = dir.GetFileSystemInfos().ToList();
                fEntries.Find(x => x.FullName == $"{filesSpace}\\{logicalPath}").Delete();
                fEntries.RemoveAll(x => x.FullName == $"{filesSpace}\\{logicalPath}");
                if (fEntries.Count == 0)
                    dir.Delete();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                if (ex is IOException)
                    err = "Error occured with server";
                else
                    err = ex.Message;
            }
            finally
            {
                dbAccess.ReleaseMutex();
            }
        }
    }
}
