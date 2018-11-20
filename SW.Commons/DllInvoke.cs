using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SW.Commons
{
    /// <summary>
    /// 动态DLL调用类
    /// </summary>
    public class DllInvoke
    {
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(String path);

        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, String funcName);

        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);

        private IntPtr hLib;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DLLPath">dll绝对路径</param>
        public DllInvoke(String DLLPath)
        {
            hLib = LoadLibrary(DLLPath);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DllInvoke()
        {
            FreeLibrary(hLib);
        }

        /// <summary>
        /// 将要执行的函数转换为委托
        /// </summary>
        /// <param name="APIName"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Delegate Invoke(String APIName, Type t)
        {
            IntPtr api = GetProcAddress(hLib, APIName);
            return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);
        }
    }
}
