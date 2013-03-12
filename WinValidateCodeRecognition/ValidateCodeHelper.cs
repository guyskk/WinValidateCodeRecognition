using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinValidateCodeRecognition
{
    public static class ValidateCodeHelper
    {
        /// <summary>
        /// 图像灰度平均值
        /// </summary>
        /// <param name="img">图像</param>
        /// <returns></returns>
        public static int GrayAvg(Bitmap bmp)
        {
            int gray = 0;
            int sum = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    //灰度算法
                    gray = (bmp.GetPixel(x, y).R * 299 + bmp.GetPixel(x, y).G * 587 + bmp.GetPixel(x, y).B * 114 + 500) / 1000;
                    sum += gray;
                }
            }
            return sum / (bmp.Width * bmp.Height);
        }

        /// <summary>
        /// 二值化图像
        /// </summary>
        /// <param name="bmp">图像</param>
        /// <param name="threshold">阀值(0-255)</param>
        /// <returns></returns>
        public static Bitmap ToBinaryzation(Bitmap bmp, int threshold)
        {
            int gray = 0;
            Bitmap bmpTmp = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(bmpTmp);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    gray = (bmp.GetPixel(x, y).R * 299 + bmp.GetPixel(x, y).G * 587 + bmp.GetPixel(x, y).B * 114 + 500) / 1000;
                    Color color = new Color();
                    if (gray > threshold)
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
            return bmpTmp;
        }
        public static Bitmap ToBinaryzation(Bitmap bmp)
        {
            Bitmap bmpTmp = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(bmpTmp);
            g.DrawImage(bmp, 0, 0);
            return bmpTmp;
        }

        #region 获取主体矩形边界坐标
        /// <summary>
        /// 获取主体矩形边界坐标，顺时针
        /// </summary>
        /// <param name="bmpTg">目标图片</param>
        /// <param name="bmpMT">模板图片</param>
        /// <param name="redress">容错率(0-100)</param>
        /// <returns></returns>
        public static Point[] GetTargetPoints(Bitmap bmpTg, Bitmap bmpMT, int redress)
        {
            //主体矩形边界坐标,顺时针
            Point[] points ={
                 new Point(0,0),
                 new Point(0,0),
                 new Point(0,0),
                 new Point(0,0),
                            };

            int mtMainPxSum = 0;//模板主体像素数
            int matchPxNum = 0;//目标图片与模板主体匹配成功的像素数

            int tmpRGB = 0;
            int mtxDiff = 0;//模板图片主体x起始坐标距离0的距离
            int mtyDiff = 0;//模板图片主体y起始坐标距离0的距离
            int mtMainW = 0;//模板图片主体宽
            int mtMainH = 0;//模板图片主体高
            List<int> mtxs = new List<int>();//模板图片x坐标集合
            List<int> mtys = new List<int>();//模板图片y坐标集合

            for (int x = 0; x < bmpMT.Width; x++)
            {
                for (int y = 0; y < bmpMT.Height; y++)
                {
                    if (bmpMT.GetPixel(x, y).B < 150)
                    {
                        mtxs.Add(x);
                        mtys.Add(y);
                    }
                }
            }

            mtxDiff = mtxs.Min() - 0;
            mtyDiff = mtys.Min() - 0;
            mtMainW = mtxs.Max() - mtxs.Min() + 1;
            mtMainH = mtys.Max() - mtys.Min() + 1;

            for (int tgx = 0; tgx < bmpTg.Width - mtMainW; tgx++)
            {
                for (int tgy = 0; tgy < bmpTg.Height - mtMainH; tgy++)
                {
                    mtMainPxSum = 0;
                    matchPxNum = 0;
                    for (int mtx = 0; mtx < mtMainW; mtx++)
                    {
                        for (int mty = 0; mty < mtMainH; mty++)
                        {
                            if (bmpMT.GetPixel(mtx + mtxDiff, mty + mtyDiff).B > 100)
                                continue;
                            mtMainPxSum++;//统计模板主体像素数
                            tmpRGB = bmpTg.GetPixel(tgx + mtx, tgy + mty).B - bmpMT.GetPixel(mtx + mtxDiff, mty + mtyDiff).B;
                            tmpRGB = tmpRGB > 0 ? tmpRGB : tmpRGB * (-1);
                            if (tmpRGB < 150)//如果匹配
                            {
                                matchPxNum++;//统计匹配的像素数
                            }
                        }
                    }
                    //模板匹配结束一轮后
                    int accuracy = (int)((matchPxNum * 1.0f / mtMainPxSum) * 100);//正确率
                    if (accuracy + redress < 100)//如果正确率加上容错率没有大于百分百，说明没有匹配上，进行下一轮匹配
                    {
                        continue;
                    }
                    points[0].X = tgx;
                    points[0].Y = tgy;
                    points[1].X = tgx + mtMainW;
                    points[1].Y = tgy;
                    points[2].X = tgx + mtMainW;
                    points[2].Y = tgy + mtMainH;
                    points[3].X = tgx;
                    points[3].Y = tgy + mtMainH;
                    return points;
                }
            }
            return points;
        }
        #endregion


        /// <summary>
        /// 匹配第一个数字
        /// </summary>
        /// <param name="bmpTg"></param>
        /// <param name="bmpMT"></param>
        /// <param name="redress"></param>
        /// <returns></returns>
        public static bool MatchFirstNum(Bitmap bmpTg, Bitmap bmpMT, int redress)
        {

            int mtMainPxSum = 0;//模板主体像素数
            int matchPxNum = 0;//目标图片与模板主体匹配成功的像素数

            int tmpRGB = 0;
            int mtxDiff = 0;//模板图片主体x起始坐标距离0的距离
            int mtyDiff = 0;//模板图片主体y起始坐标距离0的距离
            int mtMainW = 0;//模板图片主体宽
            int mtMainH = 0;//模板图片主体高
            List<int> mtxs = new List<int>();//模板图片x坐标集合
            List<int> mtys = new List<int>();//模板图片y坐标集合

            for (int x = 0; x < bmpMT.Width; x++)
            {
                for (int y = 0; y < bmpMT.Height; y++)
                {
                    if (bmpMT.GetPixel(x, y).B < 150)
                    {
                        mtxs.Add(x);
                        mtys.Add(y);
                    }
                }
            }

            mtxDiff = mtxs.Min() - 0;
            mtyDiff = mtys.Min() - 0;
            mtMainW = mtxs.Max() - mtxs.Min() + 1;
            mtMainH = mtys.Max() - mtys.Min() + 1;

            for (int tgx = 0; tgx < (bmpTg.Width / 2) - mtMainW; tgx++)
            {
                for (int tgy = 0; tgy < bmpTg.Height - mtMainH; tgy++)
                {
                    mtMainPxSum = 0;
                    matchPxNum = 0;
                    for (int mtx = 0; mtx < mtMainW; mtx++)
                    {
                        for (int mty = 0; mty < mtMainH; mty++)
                        {
                            if (bmpMT.GetPixel(mtx + mtxDiff, mty + mtyDiff).B > 100)
                                continue;
                            mtMainPxSum++;//统计模板主体像素数
                            tmpRGB = bmpTg.GetPixel(tgx + mtx, tgy + mty).B - bmpMT.GetPixel(mtx + mtxDiff, mty + mtyDiff).B;
                            tmpRGB = tmpRGB > 0 ? tmpRGB : tmpRGB * (-1);
                            if (tmpRGB < 150)//如果匹配
                            {
                                matchPxNum++;//统计匹配的像素数
                            }
                        }
                    }
                    //模板匹配结束一轮后
                    int accuracy = (int)((matchPxNum * 1.0f / mtMainPxSum) * 100);//正确率
                    if (accuracy + redress < 100)//如果正确率加上容错率没有大于百分百，说明没有匹配上，进行下一轮匹配
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 匹配运算结果数字
        /// </summary>
        /// <param name="bmpTg"></param>
        /// <param name="bmpMT"></param>
        /// <param name="redress"></param>
        /// <returns></returns>
        public static bool MatchResultNum(Bitmap bmpTg, Bitmap bmpMT, int redress)
        {

            int mtMainPxSum = 0;//模板主体像素数
            int matchPxNum = 0;//目标图片与模板主体匹配成功的像素数

            int tmpRGB = 0;
            int mtxDiff = 0;//模板图片主体x起始坐标距离0的距离
            int mtyDiff = 0;//模板图片主体y起始坐标距离0的距离
            int mtMainW = 0;//模板图片主体宽
            int mtMainH = 0;//模板图片主体高
            List<int> mtxs = new List<int>();//模板图片x坐标集合
            List<int> mtys = new List<int>();//模板图片y坐标集合

            for (int x = 0; x < bmpMT.Width; x++)
            {
                for (int y = 0; y < bmpMT.Height; y++)
                {
                    if (bmpMT.GetPixel(x, y).B < 150)
                    {
                        mtxs.Add(x);
                        mtys.Add(y);
                    }
                }
            }

            mtxDiff = mtxs.Min() - 0;
            mtyDiff = mtys.Min() - 0;
            mtMainW = mtxs.Max() - mtxs.Min() + 1;
            mtMainH = mtys.Max() - mtys.Min() + 1;

            for (int tgx = (bmpTg.Width / 2); tgx < (bmpTg.Width / 2) - mtMainW; tgx++)
            {
                for (int tgy = 0; tgy < bmpTg.Height - mtMainH; tgy++)
                {
                    mtMainPxSum = 0;
                    matchPxNum = 0;
                    for (int mtx = 0; mtx < mtMainW; mtx++)
                    {
                        for (int mty = 0; mty < mtMainH; mty++)
                        {
                            if (bmpMT.GetPixel(mtx + mtxDiff, mty + mtyDiff).B > 100)
                                continue;
                            mtMainPxSum++;//统计模板主体像素数
                            tmpRGB = bmpTg.GetPixel(tgx + mtx, tgy + mty).B - bmpMT.GetPixel(mtx + mtxDiff, mty + mtyDiff).B;
                            tmpRGB = tmpRGB > 0 ? tmpRGB : tmpRGB * (-1);
                            if (tmpRGB < 150)//如果匹配
                            {
                                matchPxNum++;//统计匹配的像素数
                            }
                        }
                    }
                    //模板匹配结束一轮后
                    int accuracy = (int)((matchPxNum * 1.0f / mtMainPxSum) * 100);//正确率
                    if (accuracy + redress < 100)//如果正确率加上容错率没有大于百分百，说明没有匹配上，进行下一轮匹配
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 图片是否匹配
        /// </summary>
        /// <param name="bmpTg"></param>
        /// <param name="bmpMT"></param>
        /// <param name="redress"></param>
        /// <returns></returns>
        public static bool IsMatch(Bitmap bmpTg, Bitmap bmpMT, int redress)
        {
            if (GetTargetPoints(bmpTg, bmpMT, redress)[2].X > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 顺时针旋转90度
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap Rotate90(Bitmap bmp)
        {
            Point[] destinationPoints = {
                new Point(bmp.Width-1, 0), // 左上角
                new Point(bmp.Height-1, bmp.Width-1),// 右上角
                new Point(0, 0) // 左下角
                                        };

            Bitmap bmpTmp = new Bitmap(bmp.Height, bmp.Width);
            Graphics g = Graphics.FromImage(bmpTmp);
            g.FillRectangle(Brushes.White, 0, 0, bmpTmp.Width, bmpTmp.Height);//填充背景为白色

            g.DrawImage(bmp, destinationPoints);
            g.Dispose();
            bmp.Dispose();
            return bmpTmp;
        }

        public enum Mode
        {
            High = 0,
            Low = 1
        }

        /// <summary>
        /// 改变图片尺寸
        /// </summary>
        /// <param name="bmp">图像</param>
        /// <param name="width">新的宽</param>
        /// <param name="height">新的高</param>
        /// <param name="mode">H：高质量，L：低质量</param>
        /// <returns></returns>
        public static Bitmap Resize(Bitmap bmp, int width, int height, Mode mode)
        {
            Bitmap bmpTmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmpTmp);
            g.FillRectangle(Brushes.White, 0, 0, bmpTmp.Width, bmpTmp.Height);

            if (mode == Mode.Low)
            {
                // 改变图像大小使用低质量的模式
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, new Rectangle(0, 0, width, height), // source rectangle
                new Rectangle(0, 0, bmp.Width, bmp.Height), // destination rectangle
                GraphicsUnit.Pixel);
            }
            else if (mode == Mode.High)
            {
                //使用高质量模式
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(
                bmp,
                new Rectangle(0, 0, width, height),
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                GraphicsUnit.Pixel);
            }
            g.Dispose();
            bmp.Dispose();
            return bmpTmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <param name="isDeleteOrg"></param>
        /// <returns></returns>
        public static Bitmap Resize(Bitmap bmp, int width, int height, string mode)
        {

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = bmp.Width;
            int oh = bmp.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = bmp.Height * width / bmp.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = bmp.Width * height / bmp.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)bmp.Width / (double)bmp.Height > (double)towidth / (double)toheight)
                    {
                        oh = bmp.Height;
                        ow = bmp.Height * towidth / toheight;
                        y = 0;
                        x = (bmp.Width - ow) / 2;
                    }
                    else
                    {
                        ow = bmp.Width;
                        oh = bmp.Width * height / towidth;
                        x = 0;
                        y = (bmp.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                bmp.Dispose();
                return bitmap;

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                bmp.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }
    }
}
