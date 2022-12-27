using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using EasyEncryption;
using System.IO;
using System.Text;
using System.Reflection;
using System.Net.Mail;

namespace ftp_client
{

    

    public static class Program
    {


        public static List<object> formsParams= new List<object>();

        
       


        public static string navigatedForm = "LoginForm";

        public static Dictionary<string,Form> allForms= null;

       

        

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
         * AccessModifier:[]
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

        //UserInfo Packet request should look like this
        /* Code:[]\r\n
         * UserName:[]\r\n
         * UserEmail:[]\r\n
         * HashedPassword:[]
         * 
         * 
         * 
         */
        //UserInfo Packet response should look like this
        /* Code:[]\r\n
         * AccessGranted:[]\r\n
         * Your_Files:[]\r\n
         * PublicFiles:[]|[]|[]|[]...|[]
         * 
         * 
         * 
         * 
         */


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        
   
        [STAThread]
        static void Main()
        {



            //Console.WriteLine(SHA.ComputeSHA256Hash("12345"));

            //Connection.SendFileUpload($@"C:\Users\sagiv\Projects\1.College-work-directory\win-forms-project\Untitled\ftp-client\bin\Debug\t.exe", "", "", "");



            if (!File.Exists(".log"))
            {
                File.Create(".log");
                FileInfo logFile = new FileInfo(".log");
                logFile.Attributes = FileAttributes.Hidden;
                
            }
            else
            {
               //Implement logging functionality

            }
            Dictionary<string,string> response = Connection.TrySession();
            if(response == null)
            {
                MessageBox.Show("Couldn't get response from the server.", "Network error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else if (int.Parse(response["Code"]) == (int)Connection.Code.Action_Confirm)
            {
                navigatedForm = "MainMenuForm";
                formsParams.Add(response);
            }
                

            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            allForms = new Dictionary<string, Form> {
                { "LoginForm", null},
                {"RegisterForm", null },
                { "EmailVerificationForm", null},
                { "MainMenuForm", null}
            };

            while (navigatedForm != "quitting")
            {
                switch (navigatedForm)
                {
                    case "LoginForm":
                        allForms[navigatedForm] = new LoginForm();
                        break;
                    case "RegisterForm":
                        allForms[navigatedForm] = new RegisterForm();
                        break;
                    //case "EmailVerificationForm":
                    //    allForms[navigatedForm] = new EmailVerificationForm();
                    //    break;
                    case "MainMenuForm":
                        allForms[navigatedForm] = new MainMenuForm();
                        break;


                    default:
                        break;
                }

                try
                {
                    Application.Run(allForms[navigatedForm]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}\n{ex.Source}");
                    
                }
                Application.Exit();
                
            }

                

        }

        public static void CloseForm(object sender, FormClosingEventArgs e)
        {

            if (sender is EmailVerificationForm)
                navigatedForm = "RegisterForm";
            else
                navigatedForm = "quitting";

        }
    }
}
