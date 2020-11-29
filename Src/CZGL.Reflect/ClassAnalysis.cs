using CZGL.CodeAnalysis.Models;
using CZGL.CodeAnalysis.Shared;
using CZGL.Reflect.Units;
using System;
using System.Linq;
using System.Reflection;

namespace CZGL.Reflect
{
    /// <summary>
    /// 解析一个类型
    /// </summary>
    public static class ClassAnalysis
    {
        
        /// <summary>
        /// 获取访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(this Type type)
        {
            return AccessAnalysis.GetAccess(type);
        }

        /// <summary>
        /// 获取修饰符关键字
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ClassKeyword GetKeyword(this Type type)
        {
            return KeywordAnalysis.GetClassKeyword(type);
        }

        /// <summary>
        /// 类能否被继承
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCanInherited(this Type type)
        {
            if (type.IsValueType||!type.IsClass||type==typeof(Delegate))
                return false;

            var keyword = KeywordAnalysis.GetClassKeyword(type);
            if (keyword == ClassKeyword.Abstract || keyword == ClassKeyword.Sealed || keyword == ClassKeyword.Static)
                return false;
            return true;
        }
        
        /// <summary>
        /// 获取泛型类的名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenericeName(this Type type)
        {
            return GenericeAnalysis.GetGenericeName(type);
        }

        /// <summary>
        /// 是否有继承
        /// <para>是否有继承接口等</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsInherite(this Type type)
        {
            if (type.IsValueType || type == typeof(Delegate))
                return false;


            var baseType = type.BaseType != typeof(object) ? true : false;
            var interfaces = type.GetInterfaces().Any() ? true: false;
            return baseType || interfaces;
        }

        /// <summary>
        /// 解析出一个类的分析参数以及泛型约束
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static GenericeParamterInfo[] GetGenericeParam(this Type type)
        {
            return GenericeAnalysis.GenericeParamterAnalysis(type);
        }

        /// <summary>
        /// 判断此类(嵌套类)是否是 new
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNew(Type type)
        {
            if (type.IsClass && type != typeof(Delegate) && type.BaseType != typeof(object))
                if (!type.IsSealed && type.IsNested)
                    return type.BaseType.GetMember(type.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null;

            return false;
        }
    }
}
