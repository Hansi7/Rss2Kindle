using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PicturesToMobi
{
    public class ImageTool
    {
        public static Image Resize(Image image, int newWidth, int maxHeight, bool notAllowEnlarge)
        {
            if (notAllowEnlarge && (image.Width <= newWidth))
            {
                newWidth = image.Width;
            }
            int height = (image.Height * newWidth) / image.Width;
            if (height > maxHeight)
            {
                newWidth = (image.Width * maxHeight) / image.Height;
                height = maxHeight;
            }
            Bitmap bitmap = new Bitmap(newWidth, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, height);
            }
            return bitmap;
        }
        public static void SaveJpeg(string path, Image img)
        {
            EncoderParameter parameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 85L);
            ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = parameter;
            img.Save(path, encoderInfo, encoderParams);
        }
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            return Enumerable.FirstOrDefault<ImageCodecInfo>(ImageCodecInfo.GetImageEncoders(), (Func<ImageCodecInfo, bool>)(t => (t.MimeType == mimeType)));
        }


    }
}
