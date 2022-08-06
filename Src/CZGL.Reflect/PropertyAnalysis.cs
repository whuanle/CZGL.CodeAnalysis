using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CZGL.Reflect
{
    /// <summary>
    /// 
    /// </summary>
    public static class PropertyAnalysis
    {
        /// <summary>
        /// 获取访问权限修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(this PropertyInfo info)
        {
            return AccessAnalysis.GetPropertyAccess(info);
        }

        /// <summary>
        /// 获取修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static PropertyKeyword GetKeyword(this PropertyInfo info)
        {
            return KeywordAnalysis.GetKeyword(info);
        }

        /// <summary>
        /// 获取特性列表
        /// </summary>
        /// <returns></returns>
        public static string GetAttributes(this PropertyInfo info)
        {
            return info.GetCustomAttributesData().ToString();
        }
    }
}
