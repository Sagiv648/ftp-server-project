using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net;

namespace ftp_server
{
    //TODO: All the read/writes to the database should be atomic
    public static class Database
    {

        
        private static Mutex dbAccess = new Mutex();
        
        private static string connectionString = Environment.GetEnvironmentVariable("sql-connection-string", EnvironmentVariableTarget.User);
        private static string dbName = Environment.GetEnvironmentVariable("ftp-server-db", EnvironmentVariableTarget.User);
        private static SqlConnection conn = new SqlConnection(connectionString);
        private static SqlCommand sqlCmd = new SqlCommand("",conn);
        private static SqlDataReader reader = null;
        public static bool InitTables(out string msg)
        {
            Dictionary<string, string> tablesCreationCommandMapping = new Dictionary<string, string>
            {
                {
                    "Users",
                    $"use {dbName} " +
                $"create table Users (" +
                $"Id int NOT NULL PRIMARY KEY, " +
                $"User_name nvarchar(50), " +
                $"User_email nvarchar(70)," +
                $"Password nvarchar(100), " +
                $"Directory nvarchar(100))"
                },
                {
                    "Sessions",
                    $"use {dbName} " +
                $"create table Sessions (" +
                $"Id int NOT NULL PRIMARY KEY, " +
                $"Logged_in SMALLDATETIME, " +
                $"User_Ip nvarchar(20), " +
                "User_name nvarchar(50), " +
                "User_id int )"
                },
                {
                    "Files",
                    $"use {dbName} " +
                    $"create table Files (" +
                    $"Id int NOT NULL PRIMARY KEY, " +
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
            while(reader.Read())
            {

                tables.Add(reader["name"].ToString());

            }
            
            reader.Close();
            for(int i = 0; i < tablesCreationCommandMapping.Keys.Count; i++)
            {
                int j = 0;
                for(j = 0; j < tables.Count; j++)
                {
                    if (tables[j] == tablesCreationCommandMapping.Keys.ToList()[i])
                        break;
                }
                if(j == tables.Count)
                {
                    try
                    {
                        sqlCmd.CommandText = tablesCreationCommandMapping[tablesCreationCommandMapping.Keys.ToList()[i]];
                        sqlCmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        conn.Close();
                        
                        return false;
                        
                    }
                    
                }
                
            }

            conn.Close();
            msg = "";
            return true;
        }

        public static bool IsSessionValid(out string msg, IPAddress clientIp)
        {
            msg = "";
            List<string> output = new List<string>();
            dbAccess.WaitOne();
            try
            {
                conn.Open();
                
                sqlCmd.CommandText = $"use {dbName} select * from Sessions where User_Ip = \'{clientIp}\'";
                reader = sqlCmd.ExecuteReader();
                
                while (reader.Read())
                {
                    output.Add(reader["Id"].ToString());
                    output.Add(reader["Logged_in"].ToString());
                    output.Add(reader["User_Ip"].ToString());
                    output.Add(reader["User_name"].ToString());
                    output.Add(reader["User_id"].ToString());

                }
                
                
                reader.Close();

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                if(!reader.IsClosed)
                    reader.Close();
                conn.Close();
                
                dbAccess.ReleaseMutex();
                return false;
            }
            dbAccess.ReleaseMutex();
            if (output.Count == 0)
                return false;


            
            return true;
        }

        public static int GetUserIdByIp(out string msg,IPAddress clientIp)
        {
            msg = "";
            dbAccess.WaitOne();
            int userId = -1;
            try
            {
                conn.Open();
                sqlCmd.CommandText = $"use {dbName} select User_id from Sessions where User_Ip = \"{clientIp}\"";
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    userId = int.Parse(reader["User_id"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();
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
                if (!reader.IsClosed)
                    reader.Close();
                conn.Close();
                dbAccess.ReleaseMutex();

                msg = ex.Message;
                return "";

                
            }

            dbAccess.ReleaseMutex();
            return output;
        }

        //TODO: Write RegisterUser procedure
        
        public static string RegisterUser(out string errMsg)
        {
            //Description: Method does all the necessary integrity checks that a register functionality should do...
            // if the new user's integrity checks out, the function returns an empty string and a relevant session will be created, all else will return a string with the msg to the user.
            errMsg = "";

            return "";
        }

        //TODO: Write CheckUserLogin procedure
        public static string CheckUserLogin(out string errMsg)
        {
            //Description: Method does all the necessary integrity checks that a login functionality should do...
            // if the user's integrity checks out, the function returns an empty string and the relevant session will be renewed, all else will return a string with the msg to the user.
            errMsg = "";

            return "";

        }

        //TODO: Write UserLogout procedure
        public static void UserLogout(out string errMsg)
        {
            //Description: Method deletes the relevant entry from the Sessions table, since a logout functionality will only be available if the user's session stands OR he logged in...
            // No checks of the user's integrity will be necessary, the function returns nothing and simply deletes the relevant session of the user.
            errMsg = "";

        }

    }
}
