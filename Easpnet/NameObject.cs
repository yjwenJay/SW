
namespace Easpnet
{
    public class NameObject
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public NameObject(string name, object val)
        {
            Name = name;
            Value = val;
        }

        /// <summary>
        /// 新建对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static NameObject New(string name, object val)
        {
            return new NameObject(name, val);
        }

    }
}
