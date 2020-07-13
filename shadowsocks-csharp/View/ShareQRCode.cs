using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shadowsocks.View
{
    public partial class ShareQRCode : Form
    {
        public ShareQRCode(Image qrCode)
        {
            InitializeComponent();
            this.pictureBox2.Image = qrCode;
        }
    }
}
