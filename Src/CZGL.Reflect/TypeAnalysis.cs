using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CZGL.Reflect
{
    /// <summary>
    /// 类型解析器
    /// </summary>
    public sealed class TypeAnalysis
    {
        #region 判断类型
        /// <summary>
        /// 判断 Type 和何种类型
        /// <para>接口、委托、类、值类型、枚举、结构体</para>
        /// </summary>
        /// <returns></returns>
        public static MemberType GetMemberType(Type type)
        {
            if (type.IsInterface)
                return MemberType.Interface;

            if (type.IsClass)
                return type.IsSubclassOf(typeof(Delegate)) ? MemberType.Delegate : MemberType.Class;
            if (type.IsValueType)
            {
                return GetValueType(type);
            }

            return MemberType.None;
        }


        /// <summary>
        /// 是否为值类型，是何种值类型
        /// <para>枚举、结构体、基础类型</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberType GetValueType(Type type)
        {
            if (!type.IsValueType)
                return MemberType.None;

            if (type.IsEnum)
                return MemberType.Enum;

            return type.IsPrimitive ? MemberType.BaseValue : MemberType.Struct;

            //return !type.IsValueType ? MemberType.None :
            //    type.IsEnum ? MemberType.Enum :
            //    type.IsPrimitive ? MemberType.Base : MemberType.Struct;
        }

        #endregion

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
                case Type type when t is Type: return GetAccess(type);
                case MethodInfo method when t is MethodInfo: return GetAccess(method);
                case ConstructorInfo constructor when t is ConstructorInfo: return GetAccess(constructor);
                case PropertyInfo property when t is PropertyInfo: return GetAccess(property);
                case FieldInfo field when t is FieldInfo: return GetAccess(field);
                default: throw new ArgumentNullException($"未能识别当前类型的访问权限");
            }


            //if (t is Type type)
            //{
            //    return GetAccess(type);
            //}

            //if (t is MethodInfo method)
            //{
            //    return GetAccess(method);
            //}

            //if (t is ConstructorInfo constructor)
            //{
            //    return GetAccess(constructor);
            //}

            //if (t is PropertyInfo property)
            //{
            //    return GetAccess(property);
            //}
            //if(t is FieldInfo field)
            //{
            //    return GetAccess(field);
            //}
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
            if (type.IsNested)
                return GetNestedAccessCode(type);

            return type.IsPublic ? AccessConstant.Public : AccessConstant.Internal;
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
                type.IsNestedFamORAssem ? MemberAccess.ProtectedInternal : throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetNestedAccessCode(Type type)
        {
            if (!type.IsNested)
                return GetAccessCode(type);

            return
                type.IsNestedPublic ? AccessConstant.Public :
                (type.IsNestedPrivate && type.IsNestedFamily) ? AccessConstant.PrivateProtected :
                type.IsNestedPrivate ? AccessConstant.Private :
                type.IsNestedAssembly ? AccessConstant.Internal :
                type.IsNestedFamily ? AccessConstant.Protected :
                type.IsNestedFamORAssem ? AccessConstant.ProtectedInternal : throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(MethodBase method)
        {
            return
                method.IsPublic ? AccessConstant.Public :
                (method.IsPrivate && method.IsFamily) ? AccessConstant.PrivateProtected :
                method.IsPrivate ? AccessConstant.Private :
                method.IsAssembly ? AccessConstant.Internal :
                method.IsFamily ? AccessConstant.Protected :
                method.IsFamilyOrAssembly ? AccessConstant.ProtectedInternal :
                throw new ArgumentNullException($"未能识别当前类型的访问权限");
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
                throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(FieldInfo info)
        {
            return
                info.IsPublic ? AccessConstant.Public :
                (info.IsPrivate && info.IsFamily) ? AccessConstant.PrivateProtected :
                info.IsPrivate ? AccessConstant.Private :
                info.IsAssembly ? AccessConstant.Internal :
                info.IsFamily ? AccessConstant.Protected :
                info.IsFamilyOrAssembly ? AccessConstant.ProtectedInternal :
                throw new ArgumentNullException($"未能识别当前类型的访问权限");
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
                throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 获取成员访问权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAccessCode(PropertyInfo property)
        {
            MethodInfo info = property.GetGetMethod() ?? property.GetSetMethod();
            if (info == null)
                throw new ArgumentNullException($"未能识别当前类型的访问权限，因为当前对象不存在 get 和 set 构造器");

            return
                info.IsPublic ? AccessConstant.Public :
                (info.IsPrivate && info.IsFamily) ? AccessConstant.PrivateProtected :
                info.IsPrivate ? AccessConstant.Private :
                info.IsAssembly ? AccessConstant.Internal :
                info.IsFamily ? AccessConstant.Protected :
                info.IsFamilyOrAssembly ? AccessConstant.ProtectedInternal :
                throw new ArgumentNullException($"未能识别当前类型的访问权限");
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
                throw new ArgumentNullException($"未能识别当前类型的访问权限，因为当前对象不存在 get 和 set 构造器");

            return
                info.IsPublic ? MemberAccess.Public :
                (info.IsPrivate && info.IsFamily) ? MemberAccess.PrivateProtected :
                info.IsPrivate ? MemberAccess.Private :
                info.IsAssembly ? MemberAccess.Internal :
                info.IsFamily ? MemberAccess.Protected :
                info.IsFamilyOrAssembly ? MemberAccess.ProtectedInternal :
                throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }

        #endregion

    }
}
