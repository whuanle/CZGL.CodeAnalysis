using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCode.Reflect
{
    /// <summary>
    /// 类型工具
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public sealed class TypeTool<TType>
    {
       private static readonly TType[] Value = new TType[2];

        /// <summary>
        /// 获得一个类型的字节大小
        /// </summary>
        /// <remarks>引用类型一定是 4/8 字节，结构体才能计算处理</remarks>
        /// <returns></returns>
        public static int SizeOf()
        {
            unsafe
            {
                TypedReference
                    elem1 = __makeref(Value[0]),
                    elem2 = __makeref(Value[1]);
                unsafe
                { return (int)((byte*)*(IntPtr*)(&elem2) - (byte*)*(IntPtr*)(&elem1)); }
            }
        }
    }
}
