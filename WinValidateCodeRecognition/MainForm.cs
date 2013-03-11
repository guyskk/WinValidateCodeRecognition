using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.IO;

namespace WinValidateCodeRecognition
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string imgName = @"006";
        private void button1_Click(object sender, EventArgs e)
        {
            Image image = Image.FromFile(@"F:\validateCodeImg\" + imgName + @".jpg");
            Image img = (Image)image.Clone();
            Bitmap bmp = new Bitmap((Image)image.Clone());
            int gray = 0;
            Graphics g = Graphics.FromImage(image);
            int sum = 0;
            int[] zf = new int[256];//灰度数组

            #region 灰度平均值
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    //灰度算法
                    gray = (bmp.GetPixel(x, y).R * 299 + bmp.GetPixel(x, y).G * 587 + bmp.GetPixel(x, y).B * 114 + 500) / 1000;
                    zf[gray]++;
                    sum += gray;
                }
            }
            int avg = sum / (bmp.Width * bmp.Height);
            #endregion

            #region 以获得的灰度平均值为阀值，对图像进行二值化处理
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    gray = (bmp.GetPixel(x, y).R * 299 + bmp.GetPixel(x, y).G * 587 + bmp.GetPixel(x, y).B * 114 + 500) / 1000;
                    zf[gray]++;
                    sum += gray;
                    Color color = new Color();
                    if (gray > avg)
                    {
                        color = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        color = Color.FromArgb(0, 0, 0);
                    }
                    g.DrawLine(new Pen(color, 1), x, y, x + 1, y + 1);
                }
            }
            #endregion

            #region 直方图绘制
            //Graphics gg = Graphics.FromImage(img);
            ////string k = ((int)(bmp.Height * 0.5) / zf.Max()).ToString();
            //for (int i = 0; i < zf.Length; i++)
            //{
            //    Pen p = new Pen(Color.Red, 1);
            //    gg.DrawLine(p, i, 0, i, zf[i]);
            //}
            #endregion

            image.Save(@"F:\validateCodeImg\" + imgName + @"_二值化图.jpg");
            image.Dispose();
            g.Dispose();
            //img.Save(@"F:\validateCodeImg\" + imgName + @"_直方图.jpg");
            //gg.Dispose();
            MessageBox.Show("OK!\nGray_AVG:" + avg);//灰度平均值
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string imgNameTmp = imgName + @"_二值化图";
            Image imgTg = Image.FromFile(@"F:\validateCodeImg\" + imgNameTmp + @".jpg");
            Image imgMT = Image.FromFile(@"F:\validateCodeImg\三.jpg");
            Bitmap bmpTg = new Bitmap(imgTg);
            Bitmap bmpMT = new Bitmap(imgMT);

            Point[] p = ValidateCodeHelper.GetTargetPoints(bmpTg, bmpMT, 50);
            Image imgTmp = (Image)imgTg.Clone();
            Graphics g = Graphics.FromImage(imgTmp);
            Pen pen = new Pen(Color.Red, 1);
            g.DrawLine(pen, p[0], p[1]);
            g.DrawLine(pen, p[1], p[2]);
            g.DrawLine(pen, p[2], p[3]);
            g.DrawLine(pen, p[3], p[0]);

            imgTmp.Save(@"F:\validateCodeImg\" + imgNameTmp + @"_主体匹配.jpg");
            g.Dispose();
            imgTg.Dispose();
            imgMT.Dispose();

            MessageBox.Show("OK");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://dynamic.12306.cn/TrainQuery/ticketPriceByStation.jsp");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            HtmlDocument doc = webBrowser1.Document;

            doc.GetElementById("startStation_ticketPrice1").Children[1].Children[0].SetAttribute("value", "上海");
            doc.GetElementById("arriveStation_ticketPrice1").Children[1].Children[0].SetAttribute("value", "北京");


            #region 获取验证码图片
            Image valiCodeImg = GetValidateCode("img_rrand_code");
            string path = @"D:\My Documents\GitHub\WinValidateCodeRecognition\WinValidateCodeRecognition\TemplatePic\";
            for (int i = 0; i < 10; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(path + i);
                for (int n = 0; n < dir.GetFiles().Count(); n++)
                {
                    Image imgTg = Image.FromFile(path + i + n + ".jpg");
                    Bitmap bmpMT = new Bitmap(imgTg);
                }
            }
            //ValidateCodeHelper.MatchFirstNum(new Bitmap(valiCodeImg),

            pictureBox1.Image = valiCodeImg;
            #endregion

            doc.GetElementById("randCode").SetAttribute("value", "3");

            doc.InvokeScript("cccxsubmit", new object[] { "stationDIV", "stationDIV2" });
            StreamWriter sw = new StreamWriter(@"F:\1.txt");
            sw.Write(doc.GetElementById("gridbox").InnerHtml);
            sw.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            doc.InvokeScript("refreshImg");
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            #region 获取验证码图片
            pictureBox1.Image = GetValidateCode("img_rrand_code");
            #endregion

        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <param name="imgId"></param>
        /// <returns></returns>
        private Image GetValidateCode(string imgId)
        {
            Image myImage = new Bitmap(60, 20);
            HtmlDocument doc = webBrowser1.Document;
            IHTMLControlElement img = (IHTMLControlElement)doc.Images[imgId].DomElement;
            IHTMLDocument2 tmpDoc = webBrowser1.Document.DomDocument as IHTMLDocument2;
            HTMLBody body = (HTMLBody)tmpDoc.body;
            IHTMLControlRange rang = (IHTMLControlRange)body.createControlRange();
            rang.add(img);
            rang.execCommand("Copy", false, null);
            myImage = Clipboard.GetImage();
            return myImage;
        }

    }
}
