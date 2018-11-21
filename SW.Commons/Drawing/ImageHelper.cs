using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SW.Drawing
{
    public class ImageHelper
    {
        /// <summary>
        /// 取出边框
        /// </summary>
        /// <param name="img"></param>
        /// <param name="border"></param>
        /// <returns></returns>
        public static Bitmap RemoveBorder(Bitmap bmp, int border)
        {
            int newwidth = bmp.Width - border * 2;
            int newheight = bmp.Height - border * 2;
            //目标图像
            Bitmap dest = new Bitmap(newwidth, newheight);
            Graphics g = Graphics.FromImage(dest);
            Rectangle sr = new Rectangle(border, border, newwidth, newheight);//要截取的矩形区域
            Rectangle dr = new Rectangle(0, 0, newwidth, newheight);//要显示到Form的矩形区域
            g.DrawImage(bmp, dr, sr, GraphicsUnit.Pixel);
            return dest;
        }
    }
}
