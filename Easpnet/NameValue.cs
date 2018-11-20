using System;

namespace Easpnet
{
    /// <summary>
    /// 键值对
    /// </summary>
    [Serializable]
    public class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public NameValue(string name, string val)
        {
            Name = name;
            Value = val;
        }
    }
}
