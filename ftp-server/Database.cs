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

namespace ftp_server
{
    //TODO: All the read/writes to the database should be atomic
    public static class Database
    {


        private static Mutex dbAccess = new Mutex();

        private static string connectionString = Environment.GetEnvironmentVariable("sql-connection-string", EnvironmentVariableTarget.User);
        private static string dbName = Environment.GetEnvironmentVariable("ftp-server-db", EnvironmentVariableTarget.User);
        private static SqlConnection conn = new SqlConnection(connectionString);
        private static SqlCommand sqlCmd = new SqlCommand("", conn);
        private static SqlDataReader reader = null;
        public static bool InitTables(out string msg)
        {
            Dictionary<string, string> tablesCreationCommandMapping = new Dictionary<string, string>
            {
                {
                    "Users",
                    $"use {dbName} " +
                $"create table Users (" +
                $"Id int NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                $"User_name nvarchar(50), " +
                $"User_email nvarchar(70)," +
                $"Password nvarchar(100), " +
                $"Directory nvarchar(100))"
                },
                {
                    "Sessions",
                    $"use {dbName} " +
                $"create table Sessions (" +
                $"Id int NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                $"Logged_in SMALLDATETIME, " +
                $"User_Ip nvarchar(20), " +
                "User_name nvarchar(50), " +
                "User_id int )"
                },
                {
                    "Files",
                    $"use {dbName} " +
                    $"create table Files (" +
                    $"Id int NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                    $"File_name nvarchar(100)," +
                    $"Access BIT)"

                }
            };


            try
            {
                conn.Open();
                sqlCmd.CommandText = $"use {dbName} select name from sys.tables";
                reader = sqlCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                conn.Close();

                return false;


            }
            List<string> tables = new List<string>();
            while (reader.Read())
            {

                tables.Add(reader["name"].ToString());

            }

            reader.Close();
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
                        Console.WriteLine(msg);
                        conn.Close();

                        return false;

                    }

                }

            }

            conn.Close();
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
                conn.Open();

                sqlCmd.CommandText = $"use {dbName} select * from Sessions where User_Ip = \'{clientIp}\'";
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    //output.Add($"Id:{reader["Id"]}");
                    userName = $"UserName:{reader["User_name"]}";
                    userId = int.Parse(reader["User_id"].ToString());


                }


                reader.Close();

            }
            catch (Exception ex)
            {
                
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();

                msg = ex.Message;
                Console.WriteLine(msg + "{0}", ex.Source);
                return "";
            }
            conn.Close();
            dbAccess.ReleaseMutex();
            if (userName == "" || userId == -1)
                return "Invalid session";

            
            
           
            return $"UserName:{userName}\r\nUserId:{userId}";
        }

        public static int GetUserIdByIp(out string msg,IPAddress clientIp)
        {
            msg = "";
            dbAccess.WaitOne();
            int userId = -1;
            try
            {
                conn.Open();
                Console.WriteLine(clientIp);
                sqlCmd.CommandText = $"use {dbName} select User_id from Sessions where User_Ip = \'{clientIp}\'";
                //Console.WriteLine(sqlCmd.CommandText);
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    userId = int.Parse(reader["User_id"].ToString());
                }
                Console.WriteLine(userId);
                reader.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Console.WriteLine(msg + "{0}", ex.Source);
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();
                return -1;
            }
            conn.Close();
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
                conn.Open();
                sqlCmd.CommandText = $"use {dbName} select Directory from Users where Id = {userId}";
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    output = reader["Directory"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Console.WriteLine(msg + "{0}",ex.Source);
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();
                return "";
                
            }
            conn.Close();
            dbAccess.ReleaseMutex();

            return output;
        }

        public static string GetAllPublicFiles(out string msg)
        {
            msg = "";
            dbAccess.WaitOne();
            string output = "";
            try
            {
                conn.Open();
                sqlCmd.CommandText = $"use {dbName} select File_name from Files where Access = 1";
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    output += reader["File_name"].ToString() + '|';
                }
                output = output.Substring(0,output.Length - 1);
                reader.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Console.WriteLine(msg + "{0}", ex.Source);
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();

                msg = ex.Message;
                return "";

                
            }
            conn.Close();
            dbAccess.ReleaseMutex();
            return output;
        }

        public static void CreateSession(string userName, IPAddress clIp, int userId)
        {
            sqlCmd.CommandText = $"use {dbName} insert into Sessions values (GETDATE(), \'{clIp}\', \'{userName} \', {userId})";
            sqlCmd.ExecuteNonQuery();
        }
        public static void DestroySession(int id)
        {
            sqlCmd.CommandText = $"use {dbName} delete from Sessions where User_id={id}";
            sqlCmd.ExecuteNonQuery();
        }
        //TODO: Write RegisterUser procedure - Needs testing

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
                
                conn.Open();
                sqlCmd.CommandText = $"use {dbName} select Id from Users where User_email = \'{fieldValueMapping["UserEmail"]}\'";
                reader = sqlCmd.ExecuteReader();
                while(reader.Read())
                {
                    id = int.Parse(reader["Id"].ToString());
                }
                reader.Close();
                if(id != -1)
                {
                    
                    conn.Close();
                    dbAccess.ReleaseMutex();
                    return "User exists";
                }
                
                sqlCmd.CommandText = $"use {dbName} insert into Users (\'{fieldValueMapping["User_name"]}\'" +
                    $",\'{fieldValueMapping["User_email"]}\', \'{fieldValueMapping["Password"]}\', \'\' ')";

                sqlCmd.ExecuteNonQuery();
                sqlCmd.CommandText = $"use {dbName} select User_name, Id from Users where Id = SCOPE_IDENTITY()";
                reader = sqlCmd.ExecuteReader();
                
                
                while(reader.Read()) 
                {
                    id = int.Parse(reader["Id"].ToString());
                    userName = reader["User_name"].ToString();
                }
                reader.Close();
                string userFile = Directory.CreateDirectory($"Files-space/{id}_{userName}").Name;
                sqlCmd.CommandText = $"use {dbName} update Users set Directory = \'{userFile}\' where Id = {id}";
                sqlCmd.ExecuteNonQuery();

                CreateSession(userName, clIp,id);



            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                Console.WriteLine($"{errMsg}\n{ex.Source}");
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();
                return "db-error";
                
            }
            conn.Close();
            dbAccess.ReleaseMutex();
            return $"UserName:{userName}\r\nUserId:{id}";
        }

        //TODO: Write CheckUserLogin procedure

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
            dbAccess.WaitOne();
            string userName = "";
            int id = -1;
            string hashedPassword = "";
            try
            {
                conn.Open();
                sqlCmd.CommandText = $"use {dbName} select * from Users where User_email=\'{fieldValueMapping["UserEmail"]}\'";
                reader = sqlCmd.ExecuteReader();
                while(reader.Read())
                {
                    id = int.Parse(reader["Id"].ToString());
                    userName = reader["User_name"].ToString();
                    hashedPassword = reader["Password"].ToString();
                }
                reader.Close();

                if(id == -1)
                {
                    conn.Close();
                    dbAccess.ReleaseMutex();
                    return "Invalid credentials";
                }
                else if (!hashedPassword.Equals(fieldValueMapping["Password"]))
                {
                    conn.Close();
                    dbAccess.ReleaseMutex();
                    return "Invalid credentials";
                }

                CreateSession(userName, clIp, id);
                
            }
            catch (Exception ex)
            {
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();
                errMsg = ex.Message;
                Console.WriteLine($"{errMsg}\n{ex.Source}");
                return "db-error";
            }

            conn.Close();
            dbAccess.ReleaseMutex();
            return $"UserName:{userName}\r\nUserId:{id}";

        }

        //TODO: Write UserLogout procedure
        public static void UserLogout(out string errMsg, Dictionary<string, string> fieldValueMapping, IPAddress clIp)
        {
            errMsg = "";
            dbAccess.WaitOne();
            try
            {
                conn.Open();
                DestroySession(int.Parse(fieldValueMapping["UserId"]));
            }
            catch (Exception ex)
            {
                conn.Close();
                dbAccess.ReleaseMutex();

                errMsg = ex.Message;
                Console.WriteLine($"{errMsg}\n{ex.Source}");
                
            }

        }

    }
}
