using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Collections;
using ThoughtWorks.QRCode.Codec;

namespace SW
{
    /// <summary>
    /// 二维码生成
    /// </summary>
    public class QR
    {
        /// <summary>
        /// 生成二维码 //Image img = QR.barCode("Byte", 10, 7, "Q", "http://www.baidu.com");
        /// QR.barCode("Byte", 2, 7, "L", data);
        /// </summary>
        /// <param name="encodType">编码方法 Byte AlphaNumeric Numeric</param>
        /// <param name="size">生成尺寸 [4]</param>
        /// <param name="version">生成版本 [0-40]</param>
        /// <param name="errorCorrert">纠错[L|M|Q|H]</param>
        /// <param name="data">数据</param>
        public static Image barCode(string encodType, int size, int version, string errorCorrert, string data)
        {
            //https://chart.googleapis.com/chart?
            //cht=qr&chs=500x500&choe=UTF-8&chld=L|4&chl=http://www.wotui.net
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeScale = size;
            qrCodeEncoder.QRCodeVersion = version;

            string encoding = encodType;
            if (encoding == "Byte")
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            else if (encoding == "AlphaNumeric")
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            else if (encoding == "Numeric")
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;

            if (errorCorrert == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrert == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrert == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrert == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            Image image = null;
            try
            {
                qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
                //image = qrCodeEncoder.Encode(data, Encoding.Default);    
                image = qrCodeEncoder.Encode(data, Encoding.UTF8);
                //Object oMissing = System.Reflection.Missing.Value;
            }
            catch (Exception el)
            {
                //MessageBox.Show("编译错误：" + el.Message.ToString()); return null;
            }
            return image;

            //解析二维码信息
            // QRCodeDecoder decoder = new QRCodeDecoder();
            //  String decodedString = decoder.decode(new QRCodeBitmapImage(new Bitmap(pictureBox1.Image)));
            //this.label3.Text = decodedString; 
        }
    }


    public class BarCode
    {
        /// <summary>
        /// 条形码生成函数
        /// </summary>
        /// <param name="text">条型码字串</param>
        /// <returns></returns>
        public static Bitmap CreateOneBarCode(string text, int height = 30)
        {
            //查检是否合条件TEXT
            bool ck = CheckErrerCode(text);
            if (!ck)
            {
                //MessageBox.Show("条形码字符不合要求，不能是存在汉字或全角字符");
                return null;
            }
            string barstring = BuildBarString(text);
            return KiCode128C(barstring, height);

        }

        /// <summary>
        /// 建立条码字符串
        /// </summary>
        /// <param name="tex">条码内容</param>
        /// <returns></returns>
        private static string BuildBarString(string tex)
        {
            //string barstart = "ytnystart";    //码头
            //string barbody = "";                //码身
            //string barcheck = "";               //码检
            //string barend = "ytnyend";    //码尾
            string barstart = "bbsbssbssss";    //码头
            string barbody = "";                //码身
            string barcheck = "";               //码检
            string barend = "bbsssbbbsbsbb";    //码尾

            int checkNum = 104;
            //循环添加码身,计算码检
            for (int i = 1; i <= tex.Length; i++)
            {
                int index = (int)tex[i - 1] - 32;
                checkNum += (index * i);

                barbody += AddSimpleTag(index);//加入字符值的条码标记
            }
            //码检值计算

            barcheck = AddSimpleTag(int.Parse(Convert.ToDouble(checkNum % 103).ToString("0")));


            string barstring = barstart + barbody + barcheck + barend;
            return barstring;
        }

        //增加一个条码标记
        private static string AddSimpleTag(int CodeIndex)
        {
            string res = "";

            /// <summary>1-4的条的字符标识 </summary>
            string[] TagB = { "", "b", "bb", "bbb", "bbbb" };
            /// <summary>1-4的空的字符标识 </summary>
            string[] TagS = { "", "s", "ss", "sss", "ssss" };
            string[] Code128List = new string[] {
                "212222","222122","222221","121223","121322","131222","122213","122312","132212","221213",
                "221312","231212","112232","122132","122231","113222","123122","123221","223211","221132",
                "221231","213212","223112","312131","311222","321122","321221","312212","322112","322211",
                "212123","212321","232121","111323","131123","131321","112313","132113","132311","211313",
                "231113","231311","112133","112331","132131","113123","113321","133121","313121","211331",
                "231131","213113","213311","213131","311123","311321","331121","312113","312311","332111",
                "314111","221411","431111","111224","111422","121124","121421","141122","141221","112214",
                "112412","122114","122411","142112","142211","241211","221114","413111","241112","134111",
                "111242","121142","121241","114212","124112","124211","411212","421112","421211","212141",
                "214121","412121","111143","111341","131141","114113","114311","411113","411311","113141",
                "114131","311141","411131","211412","211214","211232" };

            string tag = Code128List[CodeIndex];

            for (int i = 0; i < tag.Length; i++)
            {
                string temp = "";
                int num = int.Parse(tag[i].ToString());
                if (i % 2 == 0)
                {
                    temp = TagB[num];
                }
                else
                {
                    temp = TagS[num];
                }
                res += temp;
            }
            return res;
        }

        /// <summary>
        /// 检查条形码文字是否合条件(不能是汉字或全角字符)
        /// </summary>
        /// <param name="cktext"></param>
        /// <returns></returns>
        private static bool CheckErrerCode(string cktext)
        {
            foreach (char c in cktext)
            {
                byte[] tmp = System.Text.UnicodeEncoding.Default.GetBytes(c.ToString());
                if (tmp.Length > 1)
                    return false;
            }
            return true;
        }

        /// <summary>生成条码 </summary>
        /// <param name="BarString">条码模式字符串</param>  //Format32bppArgb
        /// <param name="Height">生成的条码高度</param>
        /// <returns>条码图形</returns>
        private static Bitmap KiCode128C(string BarString, int _Height)
        {

            Bitmap b = new Bitmap(BarString.Length, _Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //using (Graphics grp = Graphics.FromImage(b))
            //{
            try
            {
                char[] cs = BarString.ToCharArray();


                for (int i = 0; i < cs.Length; i++)
                {
                    for (int j = 0; j < _Height; j++)
                    {
                        if (cs[i] == 'b')
                        {
                            b.SetPixel(i, j, System.Drawing.Color.Black);
                        }
                        else
                        {
                            b.SetPixel(i, j, System.Drawing.Color.White);
                        }
                    }
                }

                //grp.DrawString(text, SystemFonts.CaptionFont, Brushes.Black, new PointF(leftEmpty, b.Height - botEmpty));

                return b;
            }
            catch
            {
                return null;
            }
            //}
        }

    }

    /// <summary>
    /// 压缩保存图片
    /// </summary>
    public class ImageJpg
    {

        /// <summary>
        /// 使用压缩保存图像
        /// </summary>
        /// <param name="saveFileName">保存绝对路径</param>
        /// <param name="Quality">质量[90|80]</param>
        /// <param name="image">图片对像</param>
        public static void ImageSave(string saveFileName, int Quality, Image image)
        {
            ImageSave(saveFileName, null, Quality, image);
        }
        /// <summary>
        /// 使用压缩保存图像
        /// </summary>
        /// <param name="saveFileName">保存绝对路径</param>
        /// <param name="mimeType">类型</param>
        /// <param name="Quality">质量[90|80]</param>
        /// <param name="image">图片对像</param>
        public static void ImageSave(string saveFileName, string mimeType, int Quality, Image image)
        {
            EncoderParameter parameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (Quality > 0) ? Convert.ToInt64(Quality) : 0x55L);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = parameter;
            if (File.Exists(saveFileName))
            {
                File.Delete(saveFileName);
            }
            ImageCodecInfo _ici = GetEncoderInfo(string.IsNullOrEmpty(mimeType) ? MimeTypeGet(saveFileName) : mimeType);
            image.Save(saveFileName, _ici, encoderParams);
        }

        /// <summary>
        /// 根据类型返回编码方法
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType.ToLower() == mimeType.ToLower())
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 根据图像文件名，读出类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string MimeTypeGet(string fileName)
        {
            Hashtable hashtable = new Hashtable();
            hashtable[".jpeg"] = "image/jpeg";
            hashtable[".jpg"] = "image/jpeg";
            hashtable[".png"] = "image/png";
            hashtable[".tif"] = "image/tiff";
            hashtable[".tiff"] = "image/tiff";
            hashtable[".bmp"] = "image/bmp";
            hashtable[".gif"] = "image/gif";
            string str = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
            return (hashtable[str] as string);
        }

    }
}
