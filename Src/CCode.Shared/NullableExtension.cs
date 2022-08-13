using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Shared
{
    /// <summary>
    /// 可空类型扩展。
    /// </summary>
    public static class NullableExtension
    {
        /// <summary>
        /// 是否位可空类型。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
