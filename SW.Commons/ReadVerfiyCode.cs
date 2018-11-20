using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Drawing;
using System.Diagnostics;
namespace SW.Commons
{
    /// <summary>
    /// 验证码识别
    /// </summary>
    public class ReadVerfiyCode
    {
        /// <summary>
        /// 验证码图片URI，如：http://js.189.cn/rand.action
        /// </summary>
        public string VerfiyCodeURI { get; set; }


        /// <summary>
        /// 验证码图片存放目录，默认为：D:\ekaocr\Tesseract-OCR\verfiyCode，需要可写权限
        /// </summary>
        public string VerfiyCodePath { get; set; }

        /// <summary>
        /// tesseract.exe地址，默认为：D:\ekaocr\Tesseract-OCR\tesseract.exe
        /// </summary>
        public string TesseractPath { get; set; }

        /// <summary>
        /// 设置外部程序工作目录为  默认为：d:\
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// 设置验证码图片URI的Referer
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// 设置验证码图片URI传入的Cookies
        /// </summary>
        public string Cookies { get; set; }

        /// <summary>
        /// 设置验证码图片URI传入的Cookie集合容器
        /// </summary>
        public CookieCollection Cookie { get; set; }

        /// <summary>
        /// 是否去掉验证码图片的杂点，缺省值为false
        /// </summary>
        public bool IsClearNoise { get; set; }

        /// <summary>
        /// 清除文件标识，指示验证码识别完成后是否清除文件
        /// </summary>
        public bool IsClearFile { get; set; }

        /// <summary>
        /// 识别网站类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 去边框的像素
        /// </summary>
        public int BianKuanPixel { get; set; }
        /// <summary>
        /// 是否去边框
        /// </summary>
        public bool IsDeleteBianKuan { get; set; }

        public string VerifyCodeImage
        {
            set { verfiycode_Img = value; }
        }

        string cookie, fileName, verfiycode_Img, verifyCode;


        public ReadVerfiyCode()
        {
            VerfiyCodePath = @"D:\ekaocr\Tesseract-OCR\verfiyCode";
            TesseractPath = @"D:\ekaocr\Tesseract-OCR\tesseract.exe";
            WorkingDirectory = @"D:\";
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public string GetVerfiyCode()
        {
            string cookies = string.Empty;
            return GetVerfiyCode(out cookies);
        }

        

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public string GetVerfiyCode(out string cookies)
        {
            if (string.IsNullOrEmpty(VerfiyCodeURI))
                throw new ArgumentNullException("VerfiyCodeURI不能为空！");
            if (string.IsNullOrEmpty(VerfiyCodePath))
                throw new ArgumentNullException("VerfiyCodePath不能为空！");
            if (string.IsNullOrEmpty(TesseractPath))
                throw new ArgumentNullException("TesseractPath不能为空！");
            if (string.IsNullOrEmpty(TesseractPath))
                throw new ArgumentNullException("WorkingDirectory不能为空！");

            //下载验证码图片存放本地目录
            DownLoadVerfiyCode();

            string outputImg = VerfiyCodePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\txt";
            if (!Directory.Exists(outputImg))
            {
                Directory.CreateDirectory(outputImg);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmsshhh")
                    + new Random(DateTime.Now.Millisecond).Next(1000, 9999);
            }

            outputImg = outputImg + "\\" + fileName;

            //识别验证码
            ExcuteCMD(verfiycode_Img, outputImg);

            //清空文件
            if (IsClearFile)
            {
                ClearFiles(verfiycode_Img, outputImg + ".txt");
            }

            cookies = cookie;
            return verifyCode;
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public string GetVerfiyCode(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                return "";
            }
            verfiycode_Img = imagePath;
            string outputImg = VerfiyCodePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\txt";
            if (!Directory.Exists(outputImg))
            {
                Directory.CreateDirectory(outputImg);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmsshhh")
                    + new Random(DateTime.Now.Millisecond).Next(1000, 9999);
            }

            outputImg = outputImg + "\\" + fileName;

            //识别验证码
            ExcuteCMD(verfiycode_Img, outputImg);

            //清空文件
            if (IsClearFile)
            {
                ClearFiles(verfiycode_Img, outputImg + ".txt");
            }

            return verifyCode;
        }



        /// <summary>
        /// 下载验证码图片存放本地目录
        /// </summary>
        void DownLoadVerfiyCode()
        {
            if (VerfiyCodeURI.Contains("?"))
            {
                VerfiyCodeURI += "&t=" + DateTime.Now.GetHashCode().ToString();
            }
            else
            {
                VerfiyCodeURI += "?t=" + DateTime.Now.GetHashCode().ToString();
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(VerfiyCodeURI);
            if (!string.IsNullOrEmpty(Referer))
                httpWebRequest.Referer = Referer;
            if (!string.IsNullOrEmpty(Cookies))
                httpWebRequest.Headers.Add("Cookie", Cookies);
            if (Cookie != null)
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(Cookie);
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();


            try
            {
                cookie = httpWebResponse.GetResponseHeader("Set-Cookie");
            }
            catch { }

            Stream stream = httpWebResponse.GetResponseStream();//得到回写的字节流
            using (Bitmap bp = new Bitmap(stream))
            {

                //保存目录名字
                string img_path = VerfiyCodePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\img";
                if (!Directory.Exists(img_path))
                {
                    Directory.CreateDirectory(img_path);
                }

                fileName = DateTime.Now.ToString("yyyyMMddHHmmsshhh") + new Random(DateTime.Now.Millisecond).Next(1000, 9999);

                verfiycode_Img = img_path + "\\" + fileName + ".jpeg";

                bp.Save(img_path + "\\" + fileName + "_old.png");

                if (IsDeleteBianKuan)
                {
                    /*
                    ImageDeal.DeleteBianKuang(bp, BianKuanPixel);
                    */
                    using (Bitmap bp2 = SW.Commons.Drawing.ImageHelper.RemoveBorder(bp, BianKuanPixel))
                    {
                        bp2.Save(verfiycode_Img);
                    }                    
                }
                else
                {
                    //去除图片杂点
                    if (IsClearNoise)
                    {
                        using (Bitmap bNew = new Bitmap(bp.Width, bp.Height))
                        {
                            for (int w = 0; w < bp.Width; w++)
                            {
                                for (int h = 0; h < bp.Height; h++)
                                {
                                    Color color = bp.GetPixel(w, h);
                                    bNew.SetPixel(w, h, color);
                                }
                            }
                            Graphics graphics = Graphics.FromImage(bNew);
                            if (!string.IsNullOrEmpty(Type))
                            {
                                if (Type.Equals("UPAY"))
                                {
                                    CheckCode_LTWap.MainTest(bp, verfiycode_Img);
                                }
                            }
                            else
                            {
                                ClearNoise(205, 2, bNew);
                                bNew.Save(verfiycode_Img);
                            }
                        }
                    }
                    else
                    {
                        bp.Save(verfiycode_Img);
                    }
                }
            }

            stream.Close();
            httpWebResponse.Close();

        }


        /// <summary>
        /// 执行命令行，识别验证码
        /// </summary>
        /// <param name="sourceImgPath"></param>
        /// <param name="outputPath"></param>
        void ExcuteCMD(string sourceImgPath, string outputPath)
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();


            //设置外部程序名
            Info.FileName = TesseractPath;

            //设置外部程序的启动参数（命令行参数）为test.txt
            Info.Arguments = sourceImgPath + " " + outputPath + " -l eng";

            //设置外部程序工作目录为  C:\
            Info.WorkingDirectory = WorkingDirectory;

            //声明一个程序类
            System.Diagnostics.Process Proc;

            try
            {
                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                return;
            }
            //等待3秒钟
            Proc.WaitForExit(3000);

            //如果这个外部程序没有结束运行则对其强行终止
            if (Proc.HasExited == false)
            {
                Proc.Kill();
            }

            FileStream fs = null;
            if (File.Exists(outputPath + ".txt"))
            {
                using (fs = new FileStream(outputPath + ".txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    verifyCode = sr.ReadToEnd().Replace("\n\n", "").Replace(" ", "");
                    sr.Close();
                    fs.Close();
                }
            }

        }

        /// <summary>
        /// 清除文件
        /// </summary>
        /// <param name="sourceImgPath"></param>
        /// <param name="outputPath"></param>
        void ClearFiles(string sourceImgPath, string outputPath)
        {
            if (File.Exists(sourceImgPath))
            {
                File.Delete(sourceImgPath);
            }

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }


        #region [图片杂点去除]
        /// <summary>
        ///  去掉杂点（适合杂点/杂线粗为1）
        /// </summary>
        /// <param name="dgGrayValue">背前景灰色界限</param>
        /// <returns></returns>
        void ClearNoise(int dgGrayValue, int MaxNearPoints, Bitmap argBUpdate)
        {
            Color piexl;
            int nearDots = 0;
            int nearDotsH = 0;
            int XSpan, YSpan, tmpX, tmpY;
            //逐点判断
            for (int i = 0; i < argBUpdate.Width; i++)
                for (int j = 0; j < argBUpdate.Height; j++)
                {
                    piexl = argBUpdate.GetPixel(i, j);
                    if (piexl.R < dgGrayValue)
                    {
                        nearDots = 0;
                        nearDotsH = 0;
                        //判断周围8个点是否全为空
                        //边框像素
                        int iPix = 4;

                        #region BianKuanPixel
                        if (BianKuanPixel != 0 && BianKuanPixel > 4)
                            iPix = BianKuanPixel;
                        #endregion

                        if (i < iPix || i > argBUpdate.Width - (iPix + 1) || j < iPix || j > argBUpdate.Height - (iPix + 1))  //边框全去掉
                        {
                            //argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                            argBUpdate.SetPixel(i, j, Color.Green);
                        }
                        else
                        {
                            if (argBUpdate.GetPixel(i - 1, j - 1).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i, j - 1).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i + 1, j - 1).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i - 1, j).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i + 1, j).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i - 1, j + 1).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i, j + 1).R < dgGrayValue) nearDots++;
                            if (argBUpdate.GetPixel(i + 1, j + 1).R < dgGrayValue) nearDots++;


                            //去干扰线
                            if (argBUpdate.GetPixel(i, j).R < dgGrayValue) nearDotsH++;
                            if (argBUpdate.GetPixel(i, j + 1).R < dgGrayValue) nearDotsH++;
                            if (argBUpdate.GetPixel(i, j + 2).R < dgGrayValue) nearDotsH++;
                            if (argBUpdate.GetPixel(i, j + 3).R < dgGrayValue) nearDotsH++;
                            if (argBUpdate.GetPixel(i, j + 4).R < dgGrayValue) nearDotsH++;

                        }

                        if (nearDots < MaxNearPoints)
                        {
                            argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));   //去掉单点 && 粗细小3邻边点
                        }
                        else
                        {
                            //去干扰线
                            if (nearDotsH < 4)
                                argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));   //去掉单点 && 粗细小3邻边点
                        }
                    }
                    else  //背景
                        argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
        }

        /// <summary>
        /// 3×3中值滤波除杂，yuanbao,2007.10
        /// </summary>
        /// <param name="dgGrayValue"></param>
        void ClearNoise(int dgGrayValue, Bitmap argBUpdate)
        {
            int x, y;
            byte[] p = new byte[9]; //最小处理窗口3*3
            byte s;
            //byte[] lpTemp=new BYTE[nByteWidth*nHeight];
            int i, j;

            //--!!!!!!!!!!!!!!下面开始窗口为3×3中值  滤波!!!!!!!!!!!!!!!!
            for (y = 1; y < argBUpdate.Height - 1; y++) //--第一行和最后一行无法取窗口
            {
                for (x = 1; x < argBUpdate.Width - 1; x++)
                {
                    //取9个点的值
                    p[0] = argBUpdate.GetPixel(x - 1, y - 1).R;
                    p[1] = argBUpdate.GetPixel(x, y - 1).R;
                    p[2] = argBUpdate.GetPixel(x + 1, y - 1).R;
                    p[3] = argBUpdate.GetPixel(x - 1, y).R;
                    p[4] = argBUpdate.GetPixel(x, y).R;
                    p[5] = argBUpdate.GetPixel(x + 1, y).R;
                    p[6] = argBUpdate.GetPixel(x - 1, y + 1).R;
                    p[7] = argBUpdate.GetPixel(x, y + 1).R;
                    p[8] = argBUpdate.GetPixel(x + 1, y + 1).R;
                    //计算中值
                    for (j = 0; j < 5; j++)
                    {
                        for (i = j + 1; i < 9; i++)
                        {
                            if (p[j] > p[i])
                            {
                                s = p[j];
                                p[j] = p[i];
                                p[i] = s;
                            }
                        }
                    }
                    //      if (bmpobj.GetPixel(x, y).R < dgGrayValue)
                    argBUpdate.SetPixel(x, y, Color.FromArgb(p[4], p[4], p[4]));    //给有效值付中值
                }
            }
        }

        #endregion


    }

    public class ImageDeal
    {
        /// <summary>
        /// 边框全去掉
        /// </summary>
        public static void DeleteBianKuang(Bitmap argBitmap, int argPixel)
        {
            Color cBack = Color.Red;
            DeleteBianKuang(argBitmap, argPixel, ref cBack);
            for (int w = 0; w < argBitmap.Width; w++)
            {
                for (int h = 0; h < argBitmap.Height; h++)
                {
                    int iPixLast = argPixel;
                    if (w < argPixel || w > argBitmap.Width - (argPixel + 1) || h < argPixel || h > argBitmap.Height - (argPixel + 1))  //边框全去掉
                    {
                        argBitmap.SetPixel(w, h, cBack);
                    }
                }
            }
        }
        /// <summary>
        /// 取颜色
        /// </summary>
        public static void DeleteBianKuang(Bitmap argBitmap, int argPixel, ref Color argColor)
        {
            for (int w = 0; w < argBitmap.Width; w++)
            {
                for (int h = 0; h < argBitmap.Height; h++)
                {
                    if (w + 1 == argPixel && h + 1 == argPixel)
                    {
                        argColor = argBitmap.GetPixel(w, h);
                    }
                }
             }
        }
    }

    /// <summary>
    /// 开发测试时间：2013-01-29 11:50  地址：http://upay.10010.com/wap/mob/cardchargeinit
    /// </summary>
    public class CheckCode_LTWap
    {
        public static string strPathSource = Directory.GetCurrentDirectory();
        public static void MainTest(string argSourceFile, string argResultFile)
        {
            //读取图片
            string strPath = argSourceFile;
            Bitmap bOld = new Bitmap(strPath);
            Bitmap bUpdate = new Bitmap(bOld.Width, bOld.Height);
            DeleteBianKuang(ref bUpdate, ref bOld, 0);
            Graphics graphics = Graphics.FromImage(bUpdate);
            bUpdate.Save(argResultFile, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        public static void MainTest(Bitmap argBitmap, string argResultFile)
        {
            Bitmap bOld = argBitmap;
            Bitmap bUpdate = new Bitmap(bOld.Width, bOld.Height);
            DeleteBianKuang(ref bUpdate, ref bOld, 0);
            Graphics graphics = Graphics.FromImage(bUpdate);
            bUpdate.Save(argResultFile, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        public static void MainTest()
        {
            //读取图片
            string strPath = strPathSource + @"\pic\source\ltwap2.png";
            Bitmap bOld = new Bitmap(strPath);
            Bitmap bUpdate = new Bitmap(bOld.Width, bOld.Height);
            DeleteBianKuang(ref bUpdate, ref bOld, 0);
            Graphics graphics = Graphics.FromImage(bUpdate);
            bUpdate.Save(strPathSource + @"\pic\result\test" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
        /// <summary>
        /// 边框全去掉
        /// </summary>
        private static void DeleteBianKuang(ref Bitmap argResult, ref Bitmap argSource, int argPixel)
        {
            Color cBack = Color.FromArgb(0, 255, 255, 255);
            for (int w = 0; w < argSource.Width; w++)
            {
                for (int h = 0; h < argSource.Height; h++)
                {
                    Color color = argSource.GetPixel(w, h);
                    argResult.SetPixel(w, h, color);//无变化
                }
            }
            ClearNoise(0, 3, argResult);
        }
        /// <summary>
        ///  去掉杂点（适合杂点/杂线粗为1）
        /// </summary>
        /// <param name="dgGrayValue">背前景灰色界限</param>
        /// <returns></returns>
        private static void ClearNoise(int dgGrayValue, int MaxNearPoints, Bitmap argBUpdate)
        {
            Color piexl;
            int nearDots = 0;
            int nearDotsH = 0;
            //逐点判断
            for (int i = 0; i < argBUpdate.Width; i++)
            {
                for (int j = 0; j < argBUpdate.Height; j++)
                {
                    piexl = argBUpdate.GetPixel(i, j);
                    if (piexl.A != 0)
                    {
                        if (piexl.A > dgGrayValue)
                        {
                            nearDots = 0;
                            nearDotsH = 0;
                            //判断周围8个点是否全为空
                            //边框像素
                            int iPix = 10;
                            if (i < iPix || i > argBUpdate.Width - (iPix + 1) || j < iPix || j > argBUpdate.Height - (iPix + 1))  //边框全去掉
                            {
                                //argBUpdate.SetPixel(i, j, piexl);   //去掉单点 && 粗细小3邻边点
                                argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));//边框变为白色背景
                            }
                            else
                            {
                                if (argBUpdate.GetPixel(i - 1, j - 1).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i, j - 1).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i + 1, j - 1).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i - 1, j).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i + 1, j).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i - 1, j + 1).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i, j + 1).A < dgGrayValue) nearDots++;
                                if (argBUpdate.GetPixel(i + 1, j + 1).A < dgGrayValue) nearDots++;

                                //去干扰线
                                if (argBUpdate.GetPixel(i, j).A > dgGrayValue) nearDotsH++;
                                if (argBUpdate.GetPixel(i, j + 1).A > dgGrayValue) nearDotsH++;
                                if (argBUpdate.GetPixel(i, j + 2).A > dgGrayValue) nearDotsH++;
                                if (argBUpdate.GetPixel(i, j + 3).A > dgGrayValue) nearDotsH++;
                                if (argBUpdate.GetPixel(i, j + 4).A > dgGrayValue) nearDotsH++;
                                if (argBUpdate.GetPixel(i, j + 5).A > dgGrayValue) nearDotsH++;
                                if (argBUpdate.GetPixel(i, j + 6).A > dgGrayValue) nearDotsH++;
                            }
                            if (nearDots <= MaxNearPoints)
                            {
                            }
                            if (nearDotsH <= MaxNearPoints)
                            {
                                argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));//边框变为白色背景
                            }
                        }
                        else  //背景
                        {
                            argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));//边框变为白色背景
                        }
                        #region 去边框
                        int iPixLast = 10;
                        if (i < iPixLast || i > argBUpdate.Width - (iPixLast + 1) || j < iPixLast || j > argBUpdate.Height - (iPixLast + 1))  //边框全去掉
                        {
                            argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));//边框变为白色背景
                        }
                        #endregion
                    }
                    else
                    {
                        argBUpdate.SetPixel(i, j, Color.FromArgb(255, 255, 255));//透明色变为白色
                    }
                }
            }
        }
    }
}