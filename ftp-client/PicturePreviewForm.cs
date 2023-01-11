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
        public PicturePreviewForm(MemoryStream memStream)
        {
            InitializeComponent();
            pictureBoxDisplayPreview.Image = null;
            picStream = memStream;
            //pictureBoxDisplayPreview.Image. = pictureBoxDisplayPreview.Size;
            //pictureBoxDisplayPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PicturePreviewForm_Load(object sender, EventArgs e)
        {
            pictureBoxDisplayPreview.Image = Image.FromStream(picStream);
            pictureBoxDisplayPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
