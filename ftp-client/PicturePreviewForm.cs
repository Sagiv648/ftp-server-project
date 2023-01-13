using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftp_client
{
    public partial class PicturePreviewForm : Form
    {
        MemoryStream picStream;
        string location;
        string fileName;
        
        public PicturePreviewForm(MemoryStream memStream, string physicalPath, string fileName)
        {
            
            InitializeComponent();
            pictureBoxDisplayPreview.Image = null;
            picStream = memStream;
            location = physicalPath;
            this.fileName= fileName;
            Console.WriteLine(memStream.Length);
            
        }

        private void PicturePreviewForm_Load(object sender, EventArgs e)
        {
            pictureBoxDisplayPreview.Image = Image.FromStream(picStream);
            
            pictureBoxDisplayPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void picSaveBtn_Click(object sender, EventArgs e)
        {
            pictureBoxDisplayPreview.Image.Save($"{location}\\{fileName}");
            DialogResult = DialogResult.Yes;
            pictureBoxDisplayPreview.Image.Dispose();
            Close();
        }

        

        private void picDropBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            pictureBoxDisplayPreview.Image.Dispose();
            Close();
        }
    }
}
