using System;
using System.Collections;
namespace SW
{
    [Serializable]
    /// <summary>
    /// 名称-ID类
    /// </summary>    
    public class NameId
    {
        private string _Name;
        /// <summary>
        /// 名称 
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _Id;
        /// <summary>
        /// Id - 对应数据库中的主键
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 返回对象本身
        /// </summary>
        public NameId Self
        {
            get { return this; }
        }



        public NameId()
        {

        }

        /// <summary>
        /// 构造NameId类
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="id">Id号</param>
        public NameId(string name, int id)
        {
            _Name = name;
            _Id = id;
            if (id <= 0)
            {
                _Name = "";
            }
        }


        /// <summary>
        /// 返回格式为 Id:Name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_Id > 0)
            {
                return _Id.ToString() + ":" + _Name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 从格式为 Id:Name的字符串构造对象
        /// </summary>
        /// <param name="s">要分解的字符串</param>
        public static NameId From(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            string[] sp = s.Split(new char[] { ':' });
            NameId ni = new NameId();

            try
            {
                ni.Id = DbConvert.ToInt32(sp[0]);

                //循环数组，分别把后面的内容加到Name里面，防止Name中有冒号而造成的数组损坏
                for (int i = 1; i < sp.Length; i++)
                {
                    ni.Name += sp[i];
                }

                //保证ni.Name不为空
                if (ni.Name == null) ni.Name = "";
            }
            catch
            {
                ni.Id = 0;
                ni.Name = "-";
            }
            return ni;
        }


        /// <summary>
        /// 从object构造对象,自动将object转化为格式为 Id:Name的string
        /// </summary>
        /// <param name="obj">要分解的对象</param>
        public static NameId From(object obj)
        {
            string s = DbConvert.ToString(obj);
            string[] sp = s.Split(new char[] { ':' });
            NameId ni = new NameId();

            try
            {
                ni.Id = DbConvert.ToInt32(sp[0]);
                //循环数组，分别把后面的内容加到Name里面，防止Name中有冒号而造成的数组损坏
                for (int i = 1; i < sp.Length; i++)
                {
                    ni.Name += sp[i];
                }

                //保证ni.Name不为空
                if (ni.Name == null) ni.Name = "";
            }
            catch
            {
                ni.Id = 0;
                ni.Name = "-";
            }
            return ni;
        }

        /// <summary>
        /// 判断两个对象是否相等。这里只要属性 Id相同。则视为两个对象相等
        /// </summary>
        /// <param name="obj">要进行比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (((NameId)obj).Id == this.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 判定对象是否为空值
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this._Id == 0;
        }
    }
}