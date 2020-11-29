using System;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 枚举代表值缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumCache<T> : EnumCache where T : Enum
    {
        /// <summary>
        /// 获取枚举代表的值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Get(T t)
        {
            return EnumCache.GetValue(t);
        }
    }
}
