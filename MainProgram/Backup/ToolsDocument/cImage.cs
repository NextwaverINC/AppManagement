using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ToolsDocument
{
    public class cImage
    {
        public Image ByteToImage(byte[] bin)
        {
            try
            {
                System.Drawing.Image newImage;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bin, 0, bin.Length))
                {
                    ms.Write(bin, 0, bin.Length);
                    newImage = Image.FromStream(ms, true);
                }
                return newImage;
            }
            catch { return null; }
        }
        public byte[] GetImageByte(Image img)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, img.RawFormat);
                ms.Flush();
                byte[] bin = ms.ToArray();
                ms.Write(bin, 0, bin.Length);
                ms.Close();
                ms.Dispose();
                return bin;
            }
            catch { return null; }
        }
        public string ByteToString(byte[] bin)
        {
            try
            {
                return Convert.ToBase64String(bin);
            }
            catch { return ""; }
        }
        public byte[] StringToByte(string b64encodedString)
        {
            try
            {
                return Convert.FromBase64String(b64encodedString);
            }
            catch { return null; }
        }
        public Image StringToImage(string b64encodedString)
        {
            if (b64encodedString != "")
            {
                byte[] bin = StringToByte(b64encodedString);
                return ByteToImage(bin);
            }
            return null;
        }
        public string ImageToString(Image img)
        {
            byte[] bin = GetImageByte(img);
            return ByteToString(bin);
        }
    }
}
