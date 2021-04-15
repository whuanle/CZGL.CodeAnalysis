using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Reflect
{
    /// <summary>
    /// 类型工具
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public sealed class TypeTool<TType>
    {
       private static readonly TType[] Value = new TType[2];

        /// <summary>
        /// 获得一个类型的字节大小（未实例化时）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static uint SizeOf<T>()
        {
            unsafe
            {
                TypedReference
                    elem1 = __makeref(Value[0]),
                    elem2 = __makeref(Value[1]);
                unsafe
                { return (uint)((byte*)*(IntPtr*)(&elem2) - (byte*)*(IntPtr*)(&elem1)); }
            }
        }
    }
}
