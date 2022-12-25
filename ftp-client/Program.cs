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
        {Console.WriteLine(SHA.ComputeSHA256Hash("12345"));

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
            
            if (int.Parse(response["Code"]) == (int)Connection.Code.Action_Confirm)
            {
                navigatedForm = "MainMenuForm";
                formsParams.Add(response);
            }
                



            
            Connection.InitEmailClient();
            // Very basic debugging network functionality

            //string pass = "12345";
            //string hashed = SHA.ComputeSHA256Hash(pass);
            //Console.WriteLine(hashed);
            //string enteredPass = SHA.ComputeSHA256Hash("12345");
            //Console.WriteLine(enteredPass);
            //Console.WriteLine($"{hashed.Equals(enteredPass)}");

            //Connection.mailClient.Send("lsrpacc9@gmail.com", "sagivalia11@gmail.com", "My email", "Test test test");

            MailMessage m = new MailMessage("lsrpacc9@gmail.com", "sagivalia11@gmail.com");
            m.IsBodyHtml = true;
            m.Subject = "THIS IS A TEST EMAIL WITH HTML!";
            m.Body = "<h1>Hello!!!!<h1>";
            //Connection.mailClient.Send(m);
            //cl.Connect(new IPEndPoint(IPAddress.Parse(serverIP), port));
            //StreamWriter r = new StreamWriter(cl.GetStream(), Encoding.ASCII);
            //string msg = "Code:10\r\nUserName:%1\r\nUserEmail:%2\r\nHashedPassword:%3\r\nEND\r\n";
            //r.WriteLine(msg);
            //r.Flush();
            //StreamReader reader = new StreamReader(cl.GetStream(), Encoding.ASCII);
            //string response = "";
           

            //int codeTest;
            //Dictionary<string,string> fieldValueMapping = new Dictionary<string,string>();
            
            //while ( (response = reader.ReadLine()) != "END")
            //{
            //    string[] tempArr = response.Split(':');
            //    fieldValueMapping.Add(tempArr[0], tempArr[1]);
                
            //}
            //codeTest = int.Parse(fieldValueMapping["Code"]);
            
            //TODO: Conditionally run the inital form depends on the response the client get from the server about whether the session is valid or not.
            //Session valid ? => straight to the application.
            //Session invalid ? => straight to the login screen.


            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            allForms = new Dictionary<string, Form> {
                { "LoginForm", null},
                {"RegisterForm", null },
                { "EmailVerificationForm", null},
                { "MainMenuForm", null}
            };

            //registerForm = new RegisterForm();
            //loginForm= new LoginForm();



            //Console.WriteLine("Response packet is:");
            //foreach (var item in fieldValueMapping)
            //{
            //        Console.WriteLine($"{item.Key}:{item.Value}");
            //}
            //if (codeTest == (int)Code.Action_Confirm)
            //{
            //    Application.Run(new MainMenuForm());

            //}

            //else
            //{
            //    while (true)
            //    {
            //        Application.Run(new LoginForm());
            //        if (noLoopBetweenRegisterLogin)
            //            break;
            //        Application.Run(new RegisterForm());
            //        if (noLoopBetweenRegisterLogin)
            //            break;
            //    }
            //    if (entryGranted)
            //        Application.Run(new MainMenuForm());
                

            //}


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
