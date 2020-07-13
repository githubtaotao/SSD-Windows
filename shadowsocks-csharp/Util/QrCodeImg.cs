using Shadowsocks.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Shadowsocks.Util
{
    class QrCodeImg
    {
        public Tuple<Bitmap, string> GenerateQrCode(string ssconfig, int width, int height)
        {
            string qrText = ssconfig;
            QRCode code = ZXing.QrCode.Internal.Encoder.encode(qrText, ErrorCorrectionLevel.M);
            ByteMatrix m = code.Matrix;
            int blockSize = Math.Max(height / m.Height, 1);

            var qrWidth = m.Width * blockSize;
            var qrHeight = m.Height * blockSize;
            var dWidth = width - qrWidth;
            var dHeight = height - qrHeight;
            var maxD = Math.Max(dWidth, dHeight);
            string rmaxD = maxD >= 7 * blockSize ? "Zoom" : "CenterImage";

            Bitmap drawArea = new Bitmap((m.Width * blockSize), (m.Height * blockSize));
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
                using (Brush b = new SolidBrush(Color.Black))
                {
                    for (int row = 0; row < m.Width; row++)
                    {
                        for (int col = 0; col < m.Height; col++)
                        {
                            if (m[row, col] != 0)
                            {
                                g.FillRectangle(b, blockSize * row, blockSize * col, blockSize, blockSize);
                            }
                        }
                    }
                }
            }
            Tuple<Bitmap, string> result = new Tuple<Bitmap, string>(drawArea, rmaxD);
            return result;
        }
    }
}
