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
    public partial class EmailVerificationForm : Form
    {
        public EmailVerificationForm(List<object> opaqueObjects)
        {
            InitializeComponent();
            FormClosing += Program.CloseForm;
        }

        private void emailVerificationBackBtn_Click(object sender, EventArgs e)
        {
            Program.navigatedForm = "RegisterForm";
            Hide();
            Dispose();
        }

    }
}
