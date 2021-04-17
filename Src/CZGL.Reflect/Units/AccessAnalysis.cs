using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CZGL.Reflect
{

    /// <summary>
    /// 解析访问权限
    /// </summary>
    [CLSCompliant(true)]
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
        /// <returns><see cref="MemberAccess"/></returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
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
        /// <param name="type">类型</param>
        /// <returns><see cref="MemberAccess"/></returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetNestedAccess(Type type)
        {
            if (!type.IsNested)
                return GetAccess(type);

            if (type.IsNestedPublic) return MemberAccess.Public;
            if (type.IsNestedPrivate && type.IsNestedFamily) return MemberAccess.PrivateProtected;
            if (type.IsNestedPrivate) return MemberAccess.Private;
            if (type.IsNestedAssembly) return MemberAccess.Internal;
            if (type.IsNestedFamily) return MemberAccess.Protected;
            if (type.IsNestedFamORAssem) return MemberAccess.ProtectedInternal;

            throw new MemberAccessException($"未能识别当前类型的访问权限");

            //return
            //    type.IsNestedPublic ? MemberAccess.Public :
            //    type.IsNestedPrivate && type.IsNestedFamily ? MemberAccess.PrivateProtected :
            //    type.IsNestedPrivate ? MemberAccess.Private :
            //    type.IsNestedAssembly ? MemberAccess.Internal :
            //    type.IsNestedFamily ? MemberAccess.Protected :
            //    type.IsNestedFamORAssem ? MemberAccess.ProtectedInternal : throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>public、private ... ...</returns>
        public static string GetNestedAccessCode(Type type)
        {
            return EnumCache.GetValue(GetNestedAccess(type));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>public、private ... ...</returns>
        public static string GetAccessCode(MethodInfo method)
        {
            return EnumCache.GetValue(GetAccess(method));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="method"></param>
        /// <returns>public、private ... ...</returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetAccess(MethodInfo method)
        {
            if (method.IsPublic) return MemberAccess.Public;
            if (method.IsPrivate && method.IsFamily) return MemberAccess.PrivateProtected;
            if (method.IsPrivate) return MemberAccess.Private;
            if (method.IsAssembly) return MemberAccess.Internal;
            if (method.IsFamily) return MemberAccess.Protected;
            if (method.IsFamilyOrAssembly) return MemberAccess.ProtectedInternal;

            throw new MemberAccessException($"未能识别当前类型的访问权限");

            //return
            //    method.IsPublic ? MemberAccess.Public :
            //    method.IsPrivate && method.IsFamily ? MemberAccess.PrivateProtected :
            //    method.IsPrivate ? MemberAccess.Private :
            //    method.IsAssembly ? MemberAccess.Internal :
            //    method.IsFamily ? MemberAccess.Protected :
            //    method.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
            //    throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns>访问修饰符</returns>
        public static string GetAccessCode(FieldInfo info)
        {
            return EnumCache.GetValue(GetAccess(info));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns><see cref="MemberAccess"/></returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetAccess(FieldInfo info)
        {
            if (info.IsPublic) return MemberAccess.Public;
            if (info.IsPrivate && info.IsFamily) return MemberAccess.PrivateProtected;
            if (info.IsPrivate) return MemberAccess.Private;
            if (info.IsAssembly) return MemberAccess.Internal;
            if (info.IsFamily) return MemberAccess.Protected;
            if (info.IsFamilyOrAssembly) return MemberAccess.ProtectedInternal;
            throw new MemberAccessException($"未能识别当前类型的访问权限");

            //return
            //    info.IsPublic ? MemberAccess.Public :
            //    info.IsPrivate && info.IsFamily ? MemberAccess.PrivateProtected :
            //    info.IsPrivate ? MemberAccess.Private :
            //    info.IsAssembly ? MemberAccess.Internal :
            //    info.IsFamily ? MemberAccess.Protected :
            //    info.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
            //    throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>访问修饰符</returns>
        public static string GetAccessCode(PropertyInfo property)
        {
            return EnumCache.GetValue(GetAccess(property));
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>访问修饰符</returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetAccess(PropertyInfo property)
        {
            MethodInfo info = property.GetGetMethod() ?? property.GetSetMethod();
            if (info == null)
                throw new MemberAccessException($"未能识别当前类型的访问权限，因为当前对象不存在 get 和 set 构造器");

            if (info.IsPublic) return MemberAccess.Public;
            if (info.IsPrivate && info.IsFamily) return MemberAccess.PrivateProtected;
            if (info.IsPrivate) return MemberAccess.Private;
            if (info.IsAssembly) return MemberAccess.Internal;
            if (info.IsFamily) return MemberAccess.Protected;
            if (info.IsFamilyOrAssembly) return MemberAccess.ProtectedInternal;
            throw new MemberAccessException($"未能识别当前类型的访问权限");

            //return
            //    info.IsPublic ? MemberAccess.Public :
            //    info.IsPrivate && info.IsFamily ? MemberAccess.PrivateProtected :
            //    info.IsPrivate ? MemberAccess.Private :
            //    info.IsAssembly ? MemberAccess.Internal :
            //    info.IsFamily ? MemberAccess.Protected :
            //    info.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
            //    throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        #endregion

    }
}