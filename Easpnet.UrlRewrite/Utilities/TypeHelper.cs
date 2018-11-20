namespace Easpnet.UrlRewrite.Utilities
{
    using System;
    using System.Reflection;

    internal sealed class TypeHelper
    {
        private TypeHelper()
        {
        }

        public static object Activate(string fullTypeName, object[] args)
        {
            string[] strArray = fullTypeName.Split(",".ToCharArray(), 2);
            if (strArray.Length != 2)
            {
                throw new ArgumentOutOfRangeException("fullTypeName", fullTypeName, MessageProvider.FormatString(Message.FullTypeNameRequiresAssemblyName, new object[0]));
            }
            return Activate(strArray[1].Trim(), strArray[0].Trim(), args);
        }

        public static object Activate(string assemblyName, string typeName, object[] args)
        {
            if (assemblyName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("assembly", assemblyName, MessageProvider.FormatString(Message.AssemblyNameRequired, new object[0]));
            }
            if (typeName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("typeName", typeName, MessageProvider.FormatString(Message.TypeNameRequired, new object[0]));
            }
            return AppDomain.CurrentDomain.CreateInstanceAndUnwrap(assemblyName, typeName, false, BindingFlags.Default, null, args, null, null, null);
        }
    }
}
