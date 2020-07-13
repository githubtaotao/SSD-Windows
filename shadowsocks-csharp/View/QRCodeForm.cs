using ZXing.QrCode.Internal;
using Shadowsocks.Controller;
using Shadowsocks.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shadowsocks.Model;
using Shadowsocks.Util;

namespace Shadowsocks.View
{
    public partial class QRCodeForm : Form
    {
        private string code;

        public QRCodeForm(string code)
        {
            this.code = code;
            InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.ssw128.GetHicon());
            this.Text = I18N.GetString("QRCode and URL");
        }

        private void GenQR(string ssconfig)
        {
            QrCodeImg generateImg = new QrCodeImg();
            Tuple<Bitmap, string> rQrCode = generateImg.GenerateQrCode(ssconfig, this.pictureBox1.Width, this.pictureBox1.Height);
            string sizeMode = rQrCode.Item2;
            if(sizeMode == "Zoom")
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            pictureBox1.Image = rQrCode.Item1;
        }

        private void QRCodeForm_Load(object sender, EventArgs e)
        {
            var servers = Configuration.Load();
            var serverDatas = servers.configs.Select(
                server =>
                    new KeyValuePair<string, string>(ShadowsocksController.GetServerURL(server), server.FriendlyName())
                ).ToList();
            listBox1.DataSource = serverDatas;

            var selectIndex = serverDatas.FindIndex(serverData => serverData.Key.StartsWith(code));
            if (selectIndex >= 0) listBox1.SetSelected(selectIndex, true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var url = (sender as ListBox)?.SelectedValue.ToString();
            GenQR(url);
            textBoxURL.Text = url;
        }

        private void textBoxURL_Click(object sender, EventArgs e)
        {
            textBoxURL.SelectAll();
        }
    }
}
