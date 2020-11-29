using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CZGL.Reflect.Units
{

    /// <summary>
    /// 解析访问权限
    /// </summary>
    public static class AccessAnalysis
    {
        // type
        // method
        // construct
        // property
        // field

        #region 访问权限

        /// <summary>
        /// 获得成员的访问权限
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess<T>(T t) where T : MemberInfo
        {
            switch (t)
            {
                case Type type: return GetAccess(type);
                case MethodInfo method: return GetAccess(method);
                case ConstructorInfo constructor: return GetAccess(constructor);
                case PropertyInfo property: return GetAccess(property);
                case FieldInfo field: return GetAccess(field);

                default: throw new MemberAccessException($"未能识别当前类型的访问权限");
            }
        }

        /// <summary>
        /// 获取一个能够在命名空间下直接定义的类型的访问权限
        /// <para>结构体、枚举、类、委托等</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(Type type)
        {
            if (type.IsNested)
                return GetNestedAccess(type);

            return type.IsPublic ? MemberAccess.Public : MemberAccess.Internal;
        }

        /// <summary>
        /// 获取一个能够在命名空间下直接定义的类型的访问权限
        /// <para>结构体、枚举、类、委托等</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(Type type)
        {
            return EnumCache.GetValue(GetAccess(type));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetNestedAccess(Type type)
        {
            if (!type.IsNested)
                return GetAccess(type);

            return
                type.IsNestedPublic ? MemberAccess.Public :
                (type.IsNestedPrivate && type.IsNestedFamily) ? MemberAccess.PrivateProtected :
                type.IsNestedPrivate ? MemberAccess.Private :
                type.IsNestedAssembly ? MemberAccess.Internal :
                type.IsNestedFamily ? MemberAccess.Protected :
                type.IsNestedFamORAssem ? MemberAccess.ProtectedInternal : throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetNestedAccessCode(Type type)
        {
            return EnumCache.GetValue(GetNestedAccess(type));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(MethodBase method)
        {
            return EnumCache.GetValue(GetAccess(method));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(MethodBase method)
        {
            return
                method.IsPublic ? MemberAccess.Public :
                (method.IsPrivate && method.IsFamily) ? MemberAccess.PrivateProtected :
                method.IsPrivate ? MemberAccess.Private :
                method.IsAssembly ? MemberAccess.Internal :
                method.IsFamily ? MemberAccess.Protected :
                method.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
                throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(FieldInfo info)
        {
            return EnumCache.GetValue(GetAccess(info));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(FieldInfo info)
        {
            return
                info.IsPublic ? MemberAccess.Public :
                (info.IsPrivate && info.IsFamily) ? MemberAccess.PrivateProtected :
                info.IsPrivate ? MemberAccess.Private :
                info.IsAssembly ? MemberAccess.Internal :
                info.IsFamily ? MemberAccess.Protected :
                info.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
                throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(PropertyInfo property)
        {
            return EnumCache.GetValue(GetAccess(property));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(PropertyInfo property)
        {
            MethodInfo info = property.GetGetMethod() ?? property.GetSetMethod();
            if (info == null)
                throw new MemberAccessException($"未能识别当前类型的访问权限，因为当前对象不存在 get 和 set 构造器");

            return
                info.IsPublic ? MemberAccess.Public :
                (info.IsPrivate && info.IsFamily) ? MemberAccess.PrivateProtected :
                info.IsPrivate ? MemberAccess.Private :
                info.IsAssembly ? MemberAccess.Internal :
                info.IsFamily ? MemberAccess.Protected :
                info.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
                throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        #endregion

    }
}