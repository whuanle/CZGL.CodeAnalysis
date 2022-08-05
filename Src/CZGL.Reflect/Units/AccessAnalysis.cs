using CZGL.CodeAnalysis.Shared;
using System;
using System.Reflection;

namespace CZGL.Reflect
{
    /// <summary>
    /// 访问符解析器。
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
        /// 获得成员的访问权限。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns><see cref="MemberAccess"/></returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetAccess<T>(T t) where T : MemberInfo
        {
            switch (t)
            {
                case Type type: return GetTypeAccess(type);
                case MethodInfo method: return GetMethodAccess(method);
                case ConstructorInfo constructor: return GetMethodAccess(constructor);
                case PropertyInfo property: return GetPropertyAccess(property);
                case FieldInfo field: return GetFieldAccess(field);

                default: throw new MemberAccessException($"未能识别当前类型的访问权限");
            }
        }

        /// <summary>
        /// 获取成员的访问修饰符。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="MemberAccessException"></exception>
        public static string GetAccessString<T>(T t) where T : MemberInfo
        {
            switch (t)
            {
                case Type type: return GetTypeAccessString(type);
                case MethodInfo method: return GetMethodAccessString(method);
                case ConstructorInfo constructor: return GetMethodAccessString(constructor);
                case PropertyInfo property: return GetPropertyAccessString(property);
                case FieldInfo field: return GetFieldAccessString(field);

                default: throw new MemberAccessException($"未能识别当前类型的访问权限");
            }
        }

        /// <summary>
        /// 获取一个能够在命名空间下直接定义的类型的访问权限。
        /// <para>结构体、枚举、类、委托等</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberAccess GetTypeAccess(Type type)
        {
            if (type.IsNested)
                return GetNestedTypeAccess(type);

            return type.IsPublic ? MemberAccess.Public : MemberAccess.Internal;
        }

        /// <summary>
        /// 获取一个能够在命名空间下直接定义的类型的访问权限。
        /// <para>结构体、枚举、类、委托等</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeAccessString(Type type) => EnumCache.GetValue(GetTypeAccess(type));

        /// <summary>
        /// 获取嵌套类型访问权限。
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns><see cref="MemberAccess"/></returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetNestedTypeAccess(Type type)
        {
            if (!type.IsNested) return GetTypeAccess(type);

            if (type.IsNestedPublic) return MemberAccess.Public;
            if (type.IsNestedAssembly && !type.IsNestedFamily) return MemberAccess.Internal;
            if (type.IsNestedFamily && !type.IsNestedAssembly) return MemberAccess.Protected;
            if (type.IsNestedPrivate) return MemberAccess.Private;
            if (type.IsNestedFamANDAssem) return MemberAccess.PrivateProtected;
            if (type.IsNestedFamORAssem) return MemberAccess.ProtectedInternal;

            throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>public、private ... ...</returns>
        public static string GetNestedTypeAccessString(Type type) => EnumCache.GetValue(GetNestedTypeAccess(type));

        /// <summary>
        /// 获取方法访问权限
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>public、private ... ...</returns>
        public static string GetMethodAccessString(MethodBase method) => EnumCache.GetValue(GetMethodAccess(method));

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="method"></param>
        /// <returns>public、private ... ...</returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetMethodAccess(MethodBase method)
        {
            if (method.IsPublic) return MemberAccess.Public;
            if (method.IsAssembly && !method.IsFamily) return MemberAccess.Internal;
            if (method.IsFamily && !method.IsAssembly) return MemberAccess.Protected;
            if (method.IsPrivate) return MemberAccess.Private;
            if (method.IsFamilyAndAssembly) return MemberAccess.PrivateProtected;
            if (method.IsFamilyOrAssembly) return MemberAccess.ProtectedInternal;

            throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns>访问修饰符</returns>
        public static string GetFieldAccessString(FieldInfo info) => EnumCache.GetValue(GetFieldAccess(info));

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns><see cref="MemberAccess"/></returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetFieldAccess(FieldInfo info)
        {
            if (info.IsPublic) return MemberAccess.Public;
            if (info.IsAssembly && !info.IsFamily) return MemberAccess.Internal;
            if (info.IsFamily && !info.IsAssembly) return MemberAccess.Protected;
            if (info.IsPrivate) return MemberAccess.Private;
            if (info.IsFamilyAndAssembly) return MemberAccess.PrivateProtected;
            if (info.IsFamilyOrAssembly) return MemberAccess.ProtectedInternal;

            throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>访问修饰符</returns>
        public static string GetPropertyAccessString(PropertyInfo property) => EnumCache.GetValue(GetPropertyAccess(property));

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>访问修饰符</returns>
        /// <exception cref="MemberAccessException">未能识别当前类型的访问权限</exception>
        public static MemberAccess GetPropertyAccess(PropertyInfo property)
        {
            // GetGetMethod() 会报错
            MethodInfo info = property.GetMethod ?? throw new MemberAccessException($"未能识别当前类型的访问权限，因为当前对象不存在 get 构造器");

            if (info.IsPublic) return MemberAccess.Public;
            if (info.IsAssembly && !info.IsFamily) return MemberAccess.Internal;
            if (info.IsFamily && !info.IsAssembly) return MemberAccess.Protected;
            if (info.IsPrivate) return MemberAccess.Private;
            if (info.IsFamilyAndAssembly) return MemberAccess.PrivateProtected;
            if (info.IsFamilyOrAssembly) return MemberAccess.ProtectedInternal;

            throw new MemberAccessException($"未能识别当前类型的访问权限");
        }

        #endregion

    }
}