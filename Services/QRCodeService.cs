using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace Core.Services
{
    public class QRCodeService
    {
        public string GenerateBase64(string content)
        {
            QRCodeData _qrCodeData = new QRCodeGenerator().CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return string.Format($"data:image/png;base64,{Convert.ToBase64String(BitmapToBytesCode(qrCodeImage))}");
        }

        private static byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
