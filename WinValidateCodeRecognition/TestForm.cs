using System;
using System.Drawing;
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
            string path = @"C:\Users\Michael_AS\Desktop\passCodeAction.jpg";
            Bitmap bmp = new Bitmap(path);
            Bitmap bmpT = ValidateCodeHelper.Resize(bmp, 200, 200, ValidateCodeHelper.Mode.High);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = ValidateCodeHelper.ToBinaryzation(bmpT, Convert.ToInt32(textBox1.Text));
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            string path = @"C:\Users\Michael_AS\Desktop\passCodeAction.jpg";
            Bitmap bmp = new Bitmap(path);
            Bitmap bmpT = ValidateCodeHelper.Resize(bmp, 200, 200, ValidateCodeHelper.Mode.High);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox1.Text = trackBar1.Value.ToString();
            pictureBox1.Image = ValidateCodeHelper.ToBinaryzation(bmpT, trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"F:\1.jpg";
            Bitmap bmp = new Bitmap(path);
            pictureBox2.Image = bmp;
            bmp = ValidateCodeHelper.Resize(bmp, 410, 500, ValidateCodeHelper.Mode.High);
            int grayavg = ValidateCodeHelper.GrayAvg(bmp);
            pictureBox2.Image = bmp;
            bmp = ValidateCodeHelper.ToBinaryzation(bmp, grayavg);
            string result = ValidateCodeHelper.GetStringByBitmap(bmp, 41, 50, 50);
            //0：□，1：■
            result = result.Replace('1', '■').Replace('0', '□');
            textBox2.Text = ValidateCodeHelper.OutputMatrixString(result, 41, 50);
        }
    }
}
