using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ToolsDocument
{
    public partial class ucPictureImage : UserControl
    {
        string XPath;
        public string _XPath
        {
            set { XPath = value; }
            get { return XPath; }
        }

        public PictureBoxSizeMode _SizeMode
        {
            set { pictureBox1.SizeMode = value; }
            get { return pictureBox1.SizeMode; }
        }
        public BorderStyle _BorderStyle
        {
            set { pictureBox1.BorderStyle = value; }
            get { return pictureBox1.BorderStyle; }
        }

        public string _imgBinary
        {
            set 
            {
                try
                {
                    cImage CI = new cImage();
                    string sValue = value;
                    if (sValue != null && sValue != "")
                        pictureBox1.Image = CI.StringToImage(sValue);
                }
                catch { }
            }
            get
            {
                cImage CI = new cImage();                
                return  CI.ImageToString(pictureBox1.Image);;
            }
        }

        public ucPictureImage()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string filePath = null;
            openFileDialog1.Filter = "Image|*.png;*.gif;";
            string strCuu = Directory.GetCurrentDirectory();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filePath);
            }
            Directory.SetCurrentDirectory(strCuu);
        }
    }
}
