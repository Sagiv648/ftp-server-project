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

      

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        
   
        [STAThread]
        static void Main()
        {

            if (!File.Exists("upload.log"))
            {
                File.Create("upload.log");
                FileInfo logFile = new FileInfo("upload.log");
                logFile.Attributes = FileAttributes.Hidden;
                
            }
            else
            {
               //Implement logging functionality

            }

            if (!File.Exists("download.log"))
            {
                File.Create("download.log");
                FileInfo logFile = new FileInfo("download.log");
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

                    DisplayTestWindow(ex.Message, ex.Source, 1);
                    Console.WriteLine(ex.StackTrace);
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

        public static void DisplayTestWindow(string msg, string source, int numTrack)
        {
            MessageBox.Show($"Tracking: {numTrack}\nMessage: {msg}", $"Source: {source}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
