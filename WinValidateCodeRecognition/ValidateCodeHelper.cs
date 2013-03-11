using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WinValidateCodeRecognition
{
    public static class ValidateCodeHelper
    {
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

    }
}
