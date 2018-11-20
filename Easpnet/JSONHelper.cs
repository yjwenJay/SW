using System.Text;
using System.Collections;

namespace Easpnet
{
    /// <summary>
    /// 构造JSON数据通用类
    /// </summary>
    public class JSONHelper
    {
        //是否成功  
        private bool success;
        //错误提示信息  
        private string error;
        //总记  
        private int totalCount;
        //数据  
        private string singleInfo;

        private ArrayList arrData;
        #region 初始化JSONHelp的所有对象
        public JSONHelper()
        {
            error = string.Empty;
            singleInfo = string.Empty;
            totalCount = 0;
            success = false;
            arrData = new ArrayList();
        }
        #endregion

        #region 重置JSONHelp的所有对象
        public void ResetJSONHelp()
        {
            error = string.Empty;
            singleInfo = string.Empty;
            totalCount = 0;
            success = false;
            arrData.Clear();
        }
        #endregion

        #region 对象与对象之间分割符
        public void addItemOk()
        {
            arrData.Add("<br>");
        }
        #endregion

        #region 在数组里添加key,value
        public void addItem(string name, string value)
        {
            arrData.Add("\"" + name + "\":" + "\"" + value + "\"");
        }
        #endregion

        #region 返回组装好的json字符串
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"totalCount\":\"" + this.totalCountS + "\",");
            sb.Append("\"error\":\"" + this.errorS + "\",");
            sb.Append("\"success\":\"" + this.successS + "\",");
            sb.Append("\"info\":\"" + this.singleInfo + "\",");
            sb.Append("\"data\":[");

            int index = 0;

            foreach (string val in arrData)
            {
                index++;

                if (index == 1)
                {
                    sb.Append("{");
                }

                if (val != "<br>")
                {
                    sb.Append(val + ",");
                }
                else
                {
                    sb = sb.Replace(",", "", sb.Length - 1, 1);
                    sb.Append("},");
                    if (index < arrData.Count)
                    {
                        sb.Append("{");
                    }
                }


            }
            sb = sb.Replace(",", "", sb.Length - 1, 1);
            sb.Append("]}");


            return sb.ToString();

        }
        #endregion
        public string errorS
        {
            get
            {
                return this.error;
            }
            set
            {
                this.error = value;
            }
        }

        public bool successS
        {
            get
            {
                return this.success;
            }
            set
            {
                this.success = value;
            }
        }

        public int totalCountS
        {
            get
            {
                return this.totalCount;
            }
            set
            {
                this.totalCount = value;
            }
        }

        public string SingleInfo
        {
            get { return singleInfo; }
            set { singleInfo = value; }
        }

    }
}
