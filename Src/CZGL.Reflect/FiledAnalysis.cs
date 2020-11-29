using CZGL.CodeAnalysis.Shared;
using CZGL.Reflect.Units;
using System.Reflection;

namespace CZGL.Reflect
{
    /// <summary>
    /// 字段分析
    /// </summary>
    public static class FiledAnalysis
    {
        /// <summary>
        /// 获取字段的访问权限修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(this FieldInfo info)
        {
            return AccessAnalysis.GetAccess(info);
        }

        /// <summary>
        /// 获取修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static FieldKeyword GetKeyword(this FieldInfo info)
        {
            return KeywordAnalysis.GetFieldKeyword(info);
        }

        /// <summary>
        /// 获取特性列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetAttributes(this FieldInfo info)
        {
            return AttributeAnalysis.GetAttributes(info.GetCustomAttributesData());
        }

    }
}
