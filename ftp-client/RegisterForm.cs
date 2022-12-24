using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_client
{
    public partial class RegisterForm : Form
    {
        public RegisterForm(List<object> opaqueObjects)
        {
            InitializeComponent();
            FormClosing += Program.CloseForm;
            
        }

        
        private void backBtn_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "LoginForm";
            Hide();
            Dispose();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "EmailVerificationForm";
            Hide();
            Dispose();
        }

        private void emailVerificationNav_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "EmailVerificationForm";
            Hide();
            Dispose();
        }
    }
}
