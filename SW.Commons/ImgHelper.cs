using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Text;
using System.Collections;

namespace SW.Commons
{
    #region 水印

    //本文章是C#代码
    //---------------------------------------------------------------------------------------
    //图片水印事件比较麻烦的事，很多程序员不清楚怎么搞，小生这里写了个。代码简单，注释也已经写好，
    //有需要的朋友可以参考下（优点：可水印文字也可水印图片，两者一起也行）。自我测试过，已经满足图片水印的基本需求。需要注意的地方：
    //1、文字按照比例缩放我没有做，觉得没必要。
    //2、文字或者图片位置的确定，可以使用代码中的（X横坐标、Y纵坐标）定位，也可以使用写的枚举WherePosition定位，若（X横坐标、Y纵坐标）、WherePosition都传了，WherePosition优先。
    //3、水印图片存储是按照原路径覆盖存储，可自己修改下代码。
    //---------------------------------------------------------------------------------------
    //先附上如何使用代码（参数都有说明，这里不详细介绍）：
    //ImageProcessing ip = new ImageProcessing(
    //                null,//文字水印，可传空对象
    //                new WatermarkImg() { //不允许空对象，为必须
    //                    Imgpath = "E:\\test\\test.jpg", 
    //                    Watermarkpath = "E:\\water\\test.png",
    //                    Where=WherePosition.RigthBottom 
    //            });
    //            ip.setWatermark();
    //---------------------------------------------------------------------------------------

    /// <summary>
    /// 图片与文字水印
    /// </summary>
    public class ImageProcessing
    {
        /// <summary>
        /// 需要加水印图片
        /// </summary>
        private Image img;
        /// <summary>
        /// 水印图片
        /// </summary>
        private Image imgwater;
        /// <summary>
        /// 画布
        /// </summary>
        private Bitmap b;
        /// <summary>
        /// GDI
        /// </summary>
        private Graphics g;
        /// <summary>
        /// 水印文字实体
        /// </summary>
        private WatermarkText _tmpWatermarkText;
        /// <summary>
        /// 水印图片实体
        /// </summary>
        private WatermarkImg _tmpWatermarkImg;
        /// <summary>
        /// 水印图片缩放或扩大后宽度
        /// </summary>
        private int WatermarkWidth = 0;
        /// <summary>
        /// 水印图片缩放或扩大后高度
        /// </summary>
        private int WatermarkHeight = 0;
        /// <summary>
        /// 水印文字实体
        /// </summary>
        public WatermarkText TmpWatermarkText
        {
            get { return _tmpWatermarkText; }
            set { _tmpWatermarkText = value; }
        }
        /// <summary>
        /// 水印图片实体
        /// </summary>
        public WatermarkImg TmpWatermarkImg
        {
            get { return _tmpWatermarkImg; }
            set { _tmpWatermarkImg = value; }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="tmptext">水印文字实体（没有传null）</param>
        /// <param name="tmpimg">水印图片实体（不能为null对象）</param>
        public ImageProcessing(WatermarkText tmptext, WatermarkImg tmpimg)
        {
            _tmpWatermarkText = tmptext;
            _tmpWatermarkImg = tmpimg;
        }
        /// <summary>
        /// 原路径保存水印图片
        /// </summary>
        /// <returns>true：成功，其他：原因返回</returns>
        public string setWatermark()
        {
            string rel = "true";
            try
            {
                MemoryStream tmpms = getWatermark();
                byte[] data = tmpms.ToArray();
                FileStream fs = new FileStream(this._tmpWatermarkImg.Imgpath, FileMode.Create);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
                fs.Dispose();
                tmpms.Dispose();
            }
            catch (Exception ex)
            {
                rel = ex.Message;
            }
            return rel;
        }

        /// <summary>
        /// 获得图片内存流
        /// </summary>
        private MemoryStream getWatermark()
        {
            //需要制作的图
            img = Image.FromFile(this._tmpWatermarkImg.Imgpath);
            //把需要制作的图用来创建画布
            b = new Bitmap(img);
            //创建Graphics对象
            g = Graphics.FromImage(b);
            //设置相关图片质量
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (!string.IsNullOrEmpty(this._tmpWatermarkImg.Imgpath) && !string.IsNullOrEmpty(this._tmpWatermarkImg.Watermarkpath))
            {
                //设置图片
                setImage();
            }
            if (this._tmpWatermarkText != null && !string.IsNullOrEmpty(this._tmpWatermarkText.Text))
            {
                //设置文字
                setText();
            }
            //声明内存流
            MemoryStream stream = new MemoryStream();
            string tmp = System.IO.Path.GetExtension(this._tmpWatermarkImg.Imgpath);
            //覆盖原图存储
            b.Save(stream, setIF(tmp));
            //关闭并释放资源
            stream.Close();
            img.Dispose();
            //若水印图片存在才释放水印图片资源
            if (this._tmpWatermarkImg.Watermarkpath.Length > 0)
            {
                imgwater.Dispose();
            }
            b.Dispose();
            g.Dispose();
            return stream;
        }
        /// <summary>
        /// 设置图片格式
        /// </summary>
        /// <param name="tmp">原格式</param>
        private ImageFormat setIF(string tmp)
        {
            ImageFormat tmpif = ImageFormat.Png;
            switch (tmp.ToLower())
            {
                case ".png":
                    tmpif = ImageFormat.Png;
                    break;
                case ".gif":
                    tmpif = ImageFormat.Gif;
                    break;
                case ".jpg":
                    tmpif = ImageFormat.Jpeg;
                    break;
                case ".jpeg":
                    tmpif = ImageFormat.Jpeg;
                    break;
                case ".icon":
                    tmpif = ImageFormat.Icon;
                    break;
                default:
                    tmpif = ImageFormat.Bmp;
                    break; ;
            }
            return tmpif;
        }
        /// <summary>
        /// 设置文字位置
        /// </summary>
        /// <param name="where">位置枚举</param>
        private void setTextWhere(WherePosition where)
        {
            //需要添加水印图片宽高
            int imgW = img.Width;
            int imgH = img.Height;
            //文字宽高
            SizeF crSize = new SizeF();
            crSize = g.MeasureString(this._tmpWatermarkText.Text, this._tmpWatermarkText.TextFont);
            //边缘像素差
            int borderpx = this._tmpWatermarkText.Borderpx;
            switch (where)
            {
                case WherePosition.BottomMiddle:
                    this._tmpWatermarkText.X = imgW / 2 - crSize.Width / 2;
                    this._tmpWatermarkText.Y = imgH - crSize.Height - borderpx;
                    break;
                case WherePosition.Center:
                    this._tmpWatermarkText.X = imgW / 2 - crSize.Width / 2;
                    this._tmpWatermarkText.Y = imgH / 2 - crSize.Height / 2;
                    break;
                case WherePosition.LeftBottom:
                    this._tmpWatermarkText.X = borderpx;
                    this._tmpWatermarkText.Y = imgH - crSize.Height - borderpx;
                    break;
                case WherePosition.LeftTop:
                    this._tmpWatermarkText.X = borderpx;
                    this._tmpWatermarkText.Y = borderpx;
                    break;
                case WherePosition.RightTop:
                    this._tmpWatermarkText.X = imgW - crSize.Width - borderpx;
                    this._tmpWatermarkText.Y = borderpx;
                    break;
                case WherePosition.RigthBottom:
                    this._tmpWatermarkText.X = imgW - crSize.Width - borderpx;
                    this._tmpWatermarkText.Y = imgH - crSize.Height - borderpx;
                    break;
                case WherePosition.TopMiddle:
                    this._tmpWatermarkText.X = imgW / 2 - crSize.Width / 2;
                    this._tmpWatermarkText.Y = borderpx;
                    break;
            }
        }
        /// <summary>
        /// 设置图片位置
        /// </summary>
        /// <param name="where">位置枚举</param>
        private void setImgWhere(WherePosition where)
        {
            //需要添加水印图片宽高
            int imgW = img.Width;
            int imgH = img.Height;
            //水印图片处理后宽高
            int wimgW = WatermarkWidth;
            int wimgH = WatermarkHeight;
            //边缘像素差
            int borderpx = this._tmpWatermarkImg.Borderpx;
            switch (where)
            {
                case WherePosition.BottomMiddle:
                    _tmpWatermarkImg.X = imgW / 2 - wimgW / 2;
                    _tmpWatermarkImg.Y = imgH - wimgH - borderpx;
                    break;
                case WherePosition.Center:
                    _tmpWatermarkImg.X = imgW / 2 - wimgW / 2;
                    _tmpWatermarkImg.Y = imgH / 2 - wimgH / 2;
                    break;
                case WherePosition.LeftBottom:
                    _tmpWatermarkImg.X = borderpx;
                    _tmpWatermarkImg.Y = imgH - wimgH - borderpx;
                    break;
                case WherePosition.LeftTop:
                    _tmpWatermarkImg.X = borderpx;
                    _tmpWatermarkImg.Y = borderpx;
                    break;
                case WherePosition.RightTop:
                    _tmpWatermarkImg.X = imgW - wimgW - borderpx;
                    _tmpWatermarkImg.Y = borderpx;
                    break;
                case WherePosition.RigthBottom:
                    _tmpWatermarkImg.X = imgW - wimgW - borderpx;
                    _tmpWatermarkImg.Y = imgH - wimgH - borderpx;
                    break;
                case WherePosition.TopMiddle:
                    _tmpWatermarkImg.X = imgW / 2 - wimgW / 2;
                    _tmpWatermarkImg.Y = borderpx;
                    break;
            }
        }
        /// <summary>
        /// 设置图片文字
        /// </summary>
        private void setText()
        {
            //开始制作,画上需要的文字信息
            SolidBrush brush = new SolidBrush(this._tmpWatermarkText.TextColor);
            setTextWhere(this._tmpWatermarkText.Where);
            //设置旋转角度
            g.RotateTransform(this._tmpWatermarkText.Angle);
            g.DrawString(this._tmpWatermarkText.Text, this._tmpWatermarkText.TextFont, brush, this._tmpWatermarkText.X, this._tmpWatermarkText.Y);
        }
        /// <summary>
        /// 计算水印图片的比率并设置水印图片宽高
        /// </summary>
        private void setWaterImgWH()
        {
            double bl = 1d;
            //取背景的1/4宽度来比较
            if ((this.img.Width > imgwater.Width * 4) && (this.img.Height > imgwater.Height * 4))
            {
                bl = 1;
            }
            else if ((this.img.Width > imgwater.Width * 4) && (this.img.Height < imgwater.Height * 4))
            {
                bl = Convert.ToDouble(this.img.Height / 4) / Convert.ToDouble(imgwater.Height);

            }
            else if ((this.img.Width < imgwater.Width * 4) && (this.img.Height > imgwater.Height * 4))
            {
                bl = Convert.ToDouble(this.img.Width / 4) / Convert.ToDouble(imgwater.Width);
            }
            else
            {
                if ((this.img.Width * imgwater.Height) > (this.img.Height * imgwater.Width))
                {
                    bl = Convert.ToDouble(this.img.Height / 4) / Convert.ToDouble(imgwater.Height);

                }
                else
                {
                    bl = Convert.ToDouble(this.img.Width / 4) / Convert.ToDouble(imgwater.Width);

                }
            }
            WatermarkWidth = Convert.ToInt32(imgwater.Width * bl);
            WatermarkHeight = Convert.ToInt32(imgwater.Height * bl);
        }
        /// <summary>
        /// 设置水印图片
        /// </summary>
        private void setImage()
        {
            imgwater = Image.FromFile(this._tmpWatermarkImg.Watermarkpath);
            //先计算水印图片比例宽高
            setWaterImgWH();
            //再根据计算后的宽高设置位置
            setImgWhere(this._tmpWatermarkImg.Where);
            //接着设置旋转角度
            g.RotateTransform(this._tmpWatermarkImg.Angle);
            //最后开始制作,画上水印图片
            g.DrawImage(imgwater, this._tmpWatermarkImg.X, this._tmpWatermarkImg.Y, WatermarkWidth, WatermarkHeight);
        }
    }

    /// <summary>
    /// 水印文字或图片定位
    /// </summary>
    public enum WherePosition
    {
        /// <summary>
        /// 底部中间
        /// </summary>
        BottomMiddle,
        /// <summary>
        /// 正中间
        /// </summary>
        Center,
        /// <summary>
        /// 左下角
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 左上角
        /// </summary>
        LeftTop,
        /// <summary>
        /// 右上角
        /// </summary>
        RightTop,
        /// <summary>
        /// 右下角
        /// </summary>
        RigthBottom,
        /// <summary>
        /// 顶部中间
        /// </summary>
        TopMiddle,

    }

    /// <summary>
    /// 水印文字实体
    /// </summary>
    public class WatermarkText
    {
        private string _text;
        private Font _textFont;
        private Color _textColor;
        private float _angle;
        private float _x;
        private float _y;
        private WherePosition _where;
        private int _borderpx;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public WatermarkText()
        {
            _text = "";
            _textFont = new Font("微软雅黑", 10, FontStyle.Bold);
            _textColor = Color.Black;
            _angle = 0;
            _x = 5;
            _y = 5;
            _borderpx = 10;
        }
        /// <summary>
        /// 文字内容（默认值：无）
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        /// <summary>
        /// 文字字体（默认值：微软雅黑，大小10，加粗）
        /// </summary>
        public Font TextFont
        {
            get { return _textFont; }
            set { _textFont = value; }
        }
        /// <summary>
        /// 文字颜色（默认值：黑色）
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
        /// <summary>
        /// 文字旋转角度（默认值：0度）
        /// </summary>
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }
        /// <summary>
        /// 文字横坐标位置（默认值：5像素）（次优先级）
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// 文字宗坐标位置（默认值：5像素）（次优先级）
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// 比例定位（默认值：无）（最优先级）
        /// </summary>
        public WherePosition Where
        {
            get { return _where; }
            set { _where = value; }
        }
        /// <summary>
        /// 边缘像素差（默认值：10）
        /// </summary>
        public int Borderpx
        {
            get { return _borderpx; }
            set { _borderpx = value; }
        }
    }

    /// <summary>
    /// 水印图片实体
    /// </summary>
    public class WatermarkImg
    {
        private string _imgpath;
        private string _watermarkpath;
        private float _angle;
        private float _x;
        private float _y;
        private WherePosition _where;
        private int _borderpx;

        /// <summary>
        /// 初始化数据构造函数
        /// </summary>
        public WatermarkImg()
        {
            _imgpath = "";
            _watermarkpath = "";
            _angle = 0;
            _x = 5;
            _y = 5;
            _borderpx = 10;
        }
        /// <summary>
        /// 需要加水印图片路径（默认值：无）
        /// </summary>
        public string Imgpath
        {
            get { return _imgpath; }
            set { _imgpath = value; }
        }
        /// <summary>
        /// 水印图片路径（默认值：无）
        /// </summary>
        public string Watermarkpath
        {
            get { return _watermarkpath; }
            set { _watermarkpath = value; }
        }
        /// <summary>
        /// 文字旋转角度（默认值：0度）
        /// </summary>
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }
        /// <summary>
        /// 文字横坐标位置（默认值：5像素）（次优先级）
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// 文字宗坐标位置（默认值：5像素）（次优先级）
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// 比例定位（默认值：无）（最优先级）
        /// </summary>
        public WherePosition Where
        {
            get { return _where; }
            set { _where = value; }
        }
        /// <summary>
        /// 边缘像素差（默认值：10）
        /// </summary>
        public int Borderpx
        {
            get { return _borderpx; }
            set { _borderpx = value; }
        }
    }

    #endregion

    public class ImgHelper
    {


        /// <summary>
        /// ResizeImgSize
        /// </summary>
        /// <param name="srcImgPath"></param>
        /// <param name="newImgPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="model"></param>
        public void ResizeImgSize(string srcImgPath, string newImgPath,
            int width, int height, string model = "AUTO")
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(srcImgPath);
            //缩略图的宽度
            int imgWidth = width;
            //缩略图的高度
            int imgHeight = height;

            int x = 0;
            int y = 0;

            int originalWidth = image.Width;    //原始图片的宽度
            int originalHeight = image.Height;  //原始图片的高度
            if (width == originalWidth
                && height == originalHeight)
            {
                return;
            }
            switch (model)
            {
                case "AUTO":    //自动
                    if (originalWidth > originalHeight)
                    {
                        imgHeight = image.Height * width / image.Width;
                    }
                    else
                    {
                        imgWidth = image.Width * height / image.Height;
                    }
                    break;
                case "HW":      //指定高宽缩放,可能变形
                    break;
                case "W":       //指定宽度,高度按照比例缩放
                    imgHeight = image.Height * width / image.Width;
                    break;
                case "H":       //指定高度,宽度按照等比例缩放
                    imgWidth = image.Width * height / image.Height;
                    break;
                case "CUT":
                    if ((double)image.Width / (double)image.Height > (double)imgWidth / (double)imgHeight)
                    {
                        originalHeight = image.Height;
                        originalWidth = image.Height * imgWidth / imgHeight;
                        y = 0;
                        x = (image.Width - originalWidth) / 2;
                    }
                    else
                    {
                        originalWidth = image.Width;
                        originalHeight = originalWidth * height / imgWidth;
                        x = 0;
                        y = (image.Height - originalHeight) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imgWidth, imgHeight);
            //新建一个画板
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量查值法
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //设置高质量，低速度呈现平滑程度
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            graphic.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            graphic.DrawImage(image, new System.Drawing.Rectangle(0, 0, imgWidth, imgHeight),
                new System.Drawing.Rectangle(x, y, originalWidth, originalHeight),
                System.Drawing.GraphicsUnit.Pixel);

            //文字水印 
            //System.Drawing.Font f = new Font("宋体", 10);
            //System.Drawing.Brush b = new SolidBrush(Color.Black);
            //graphic.DrawString("myohmine", f, b, 10, 10);
            //graphic.Dispose();

            //图片水印 
            //System.Drawing.Image copyImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));
            //Graphics a = Graphics.FromImage(bitmap);
            //a.DrawImage(copyImage, new Rectangle(bitmap.Width - copyImage.Width, bitmap.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
            //copyImage.Dispose();
            //a.Dispose();
            //copyImage.Dispose();

            image.Dispose();
            graphic.Dispose();
            //保存缩略后的图片
            bitmap.Save(newImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Dispose();
        }

        public void ResizeImgSize(string srcImgPath, string newImgPath,
          int x, int y, int imgWidth, int imgHeight)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(srcImgPath);

            //新建一个bmp图片
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imgWidth, imgHeight);
            //新建一个画板
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量查值法
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //设置高质量，低速度呈现平滑程度
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            graphic.Clear(System.Drawing.Color.Transparent);

            //定位
            Rectangle srcR = new Rectangle(x, y, imgWidth, imgHeight);
            Rectangle destR = new Rectangle(0, 0, imgWidth, imgHeight);

            //在指定位置并且按指定大小绘制原图片的指定部分
            graphic.DrawImage(image, destR, srcR, GraphicsUnit.Pixel);
            image.Dispose();
            graphic.Dispose();

            //保存缩略后的图片
            bitmap.Save(newImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Dispose();

        }


        #region 新图片保存功能


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
        /// 根据图像文件名,读出类型
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


        #endregion

        #region 剪切图片对像(生成缩略图)

        /// <summary>
        /// 剪切图片(当图片没有指定的长宽长时居中显示,原为拉伸)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="toWidth"></param>
        /// <param name="toHeight"></param>
        /// <param name="isCutImage"></param>
        /// <returns></returns>
        public static Image CreateSmallImage(Image image, int toWidth, int toHeight, bool isCutImage)
        {
            try
            {
                //用于取消
                //Image.GetThumbnailImageAbort abort1 = new Image.GetThumbnailImageAbort(CreateSmallImageCallBack);
                //private static bool CreateSmallImageCallBack()
                //{
                //    return false;
                //}


                int width = image.Width;
                int height = image.Height;
                int num3 = toWidth;
                int num4 = toHeight;
                if ((width > num3) && (height > num4))
                {
                    if ((((double)width) / ((double)height)) > (((double)num3) / ((double)num4)))
                    {
                        num4 = (num3 * height) / width;
                    }
                    else
                    {
                        num3 = (num4 * width) / height;
                    }
                }
                else if (width > num3)
                {
                    num4 = (num3 * height) / width;
                }
                else if (height > num4)
                {
                    num3 = (num4 * width) / height;
                }
                else
                {
                    num3 = width;
                    num4 = height;
                }
                Bitmap bitmap = null;
                if (!isCutImage)
                {
                    bitmap = new Bitmap(image, new Size(num3, num4));
                }
                else
                {
                    int num5;//x
                    int num6;//y
                    int num7;//h
                    int num8;//w
                    if ((((double)width) / ((double)height)) > (((double)toWidth) / ((double)toHeight)))
                    {
                        num7 = height;
                        num8 = (height * toWidth) / toHeight;
                        num6 = 0;
                        num5 = (width - num8) / 2;
                    }
                    else
                    {
                        num8 = width;
                        num7 = (width * toHeight) / toWidth;
                        num5 = 0;
                        num6 = (height - num7) / 2;
                    }
                    bitmap = new Bitmap(toWidth, toHeight);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);


                    //不足不填充
                    if (width >= toWidth && height >= toHeight)
                    {
                        graphics.DrawImage(image, new Rectangle(0, 0, toWidth, toHeight), new Rectangle(num5, num6, num8, num7), GraphicsUnit.Pixel);
                    }
                    else if (width < toWidth && height < toWidth)
                    {
                        int x, y;
                        x = (toWidth - num3) / 2;
                        y = (toHeight - num4) / 2;
                        graphics.DrawImage(image, new Rectangle(x, y, num3, num4), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        int x, y, m = 0, n = 0, w = image.Width, h = image.Height;
                        x = (toWidth - w) / 2;
                        y = (toHeight - h) / 2;
                        if (w > toWidth)
                        {
                            x = 0;
                            m = (w - toWidth) / 2;
                            w = toWidth;
                        }
                        if (h > toHeight)
                        {
                            y = 0;
                            n = (h - toHeight) / 2;
                            h = toHeight;
                        }
                        graphics.DrawImage(image, new Rectangle(x, y, w, h), m, n, w, h, GraphicsUnit.Pixel);
                    }
                    graphics.Dispose();
                }
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 为指定图片对像生成缩略图
        /// </summary>
        /// <param name="image">图片对像</param>
        /// <param name="smallFileName">缩略图存放位置</param>
        /// <param name="newWidth">新宽</param>
        /// <param name="newHeight">新高</param>
        /// <param name="mimeType">类型 如image/jpeg 可为空</param>
        /// <param name="Quality">质量 小于100 </param>
        /// <param name="isCutImage">是否剪切,一般为true</param>
        /// <returns></returns>
        public static bool CreateSmallImage(Image image, string smallFileName, int newWidth, int newHeight, string mimeType, int Quality, bool isCutImage)
        {
            bool flag = false;
            Image image2 = CreateSmallImage(image, newWidth, newHeight, isCutImage);
            if (image2 != null)
            {
                try
                {
                    ImageSave(smallFileName, mimeType, Quality, image2);
                    flag = true;
                }
                catch
                {
                }
                finally
                {
                    image2.Dispose();
                    image2 = null;
                }
            }
            return flag;
        }
        #endregion

        #region 常用方法
        /// <summary>
        /// 根据指定的大图地址返回缩略图地址
        /// </summary>
        /// <param name="vImagePath">大图的虚拟路径</param>
        /// <returns>大图的缩略图</returns>
        public static string ImageThumbnaiGet(string vImagePath)
        {
            if (string.IsNullOrEmpty(vImagePath))
                return "";
            try
            {
                vImagePath = vImagePath.Replace("\\", "/");
                string fileName = vImagePath.Substring(vImagePath.LastIndexOf("/"), vImagePath.Length - vImagePath.LastIndexOf("/"));
                return (vImagePath.Replace(fileName, "/thumb" + fileName));
            }
            catch { return ""; }
            //var getSImage = function (filePath) {
            //    var filePath = getBImage(filePath);
            //    var fileName = filePath.toString().substring(filePath.toString().lastIndexOf("/"), filePath.toString().length);
            //    return (filePath.toString().replace(fileName, "/thumb" + fileName));
            //}
        }

        /// <summary>
        /// 根据指定的缩略图地址返回大图地址
        /// </summary>
        /// <param name="vImagePath"></param>
        /// <returns></returns>
        public static string ImageThumbnaiGetBig(string vImagePath)
        {
            if (string.IsNullOrEmpty(vImagePath))
                return "";
            try
            {
                vImagePath = vImagePath.Replace("\\", "/").ToLower();
                return (vImagePath.Replace("/thumb/", "/"));
            }
            catch { return ""; }
            //var getBImage = function (filePath) {
            //    return filePath.toString().replace("/thumb/", "/");
            //}
        }

        #endregion

        /// <summary>
        /// 组合图片到模板
        /// ImgHelper.GroupImagesToTemplate("{0}{1}",str1,str2)
        /// </summary>
        /// <param name="template">模板字符串0：大图，1：缩略图，空为逗号分隔</param>
        /// <param name="arg">多图片参数</param>
        /// <returns></returns>
        public static string GroupImagesToTemplate(string template, params string[] arg)
        {
            //imglist = ImgHelper.GroupImagesToTemplate("<td onclick=\"addImageMeth(this);\"><img src=\"{1}\" data=\"{0}\" /><input type=\"hidden\" name=\"imgpath\" value=\"{0}\"/></td>", tmodel.IdCardFront, tmodel.IdCardBack);
            List<string> imgList = new List<string>();
            // 图片
            foreach (string imgs in arg)
            {
                if (!string.IsNullOrEmpty(imgs))
                {
                    string temp = imgs.Trim();
                    if (temp.Contains(","))
                    {
                        foreach (string img in temp.Split(','))
                        {
                            if (!string.IsNullOrEmpty(img) && !imgList.Contains(img) && !img.Contains("nopic"))
                                imgList.Add(img);
                        }
                    }
                    else
                    {
                        if (!imgList.Contains(temp) && !temp.Contains("nopic"))
                            imgList.Add(temp);
                    }
                }
            }
            if (imgList.Count() > 0)
            {
                bool isTemplate = true;
                if (string.IsNullOrEmpty(template) || !template.Contains("{0}"))
                    isTemplate = false;
                StringBuilder sb = new StringBuilder();
                foreach (string imgs in imgList)
                {
                    if (isTemplate)
                    {
                        sb.AppendFormat(template, imgs, ImageThumbnaiGet(imgs));
                    }
                    else
                        sb.Append(imgs + ",");
                }
                string restr = sb.ToString();
                if (isTemplate)
                    return restr;
                else
                    return StringHelper.DelLastComma(restr);
            }
            else
            {
                return "";
            }
        }
    }
}
