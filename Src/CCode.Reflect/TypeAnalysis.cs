using CZGL.CodeAnalysis.Shared;
using System;
using System.Reflection;

namespace CCode.Reflect
{
    /// <summary>
    /// 类型解析器
    /// </summary>
    public static class TypeAnalysis
    {
        #region 判断类型
        /// <summary>
        /// 判断 Type 和何种类型
        /// <para>接口、委托、类、值类型、枚举、结构体</para>
        /// </summary>
        /// <returns></returns>
        public static MemberType GetMemberType(this Type type)
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
        public static MemberType GetValueType(this Type type)
        {
            if (!type.IsValueType)
                return MemberType.None;

            if (type.IsEnum)
                return MemberType.Enum;

            return type.IsPrimitive ? MemberType.BaseType : MemberType.Struct;

            //return !type.IsValueType ? MemberType.None :
            //    type.IsEnum ? MemberType.Enum :
            //    type.IsPrimitive ? MemberType.Base : MemberType.Struct;
        }

        /// <summary>
        /// 获取成员类型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static MemberTypes GetMemberType(MemberInfo info) => info.MemberType;

        #endregion
    }
}
