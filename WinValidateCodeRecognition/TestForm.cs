using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinValidateCodeRecognition
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"D:\My Documents\GitHub\WinValidateCodeRecognition\WinValidateCodeRecognition\TemplatePic\16\16.jpg";
            Bitmap bmp = new Bitmap(path);
            Bitmap bmpT = ValidateCodeHelper.Resize(bmp, 200, 200, ValidateCodeHelper.Mode.High);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = ValidateCodeHelper.ToBinaryzation(bmpT, Convert.ToInt32(textBox1.Text));
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            string path = @"D:\My Documents\GitHub\WinValidateCodeRecognition\WinValidateCodeRecognition\TemplatePic\16\16.jpg";
            Bitmap bmp = new Bitmap(path);
            Bitmap bmpT = ValidateCodeHelper.Resize(bmp, 200, 200, ValidateCodeHelper.Mode.High);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox1.Text = trackBar1.Value.ToString();
            pictureBox1.Image = ValidateCodeHelper.ToBinaryzation(bmpT, trackBar1.Value);
        }
    }
}
