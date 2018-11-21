using System;
using System.Collections.Generic;
using System.Text;

namespace SW
{
    [Serializable]
    public class NameValue
    {

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public NameValue(string name, string value)
        {
            _Name = name;
            _Value = value;
        }

        public override string ToString()
        {
            return _Name;
        }

        [Newtonsoft.Json.JsonIgnore]
        public NameValue Self
        {
            get { return this; }
        }

        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //if (obj.GetType() != typeof(NameValue)) return false;
            try
            {
                if (((NameValue)obj).Value == this.Value)
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
    }
}
