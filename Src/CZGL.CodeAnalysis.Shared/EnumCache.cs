using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 枚举信息缓存。
    /// </summary>
    [CLSCompliant(true)]
    public static class EnumCache
    {
        #region 缓存

        //泛型缓存
        private static class Cache<T> where T : Enum
        {
            private static readonly IReadOnlyDictionary<T, string> _Cache = GetDictionary();
            public static IReadOnlyDictionary<T, string> Data => _Cache;

            // 获取枚举所有值并存储到字典中。
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Dictionary<T, string> GetDictionary()
            {
                Dictionary<T, string> dictionary = new Dictionary<T, string>();
                Type type = typeof(T);

                foreach (var item in type.GetFields(BindingFlags.Static | BindingFlags.Public))
                {
                    var attr = item.GetCustomAttributes().Single(x => x.GetType() == typeof(MemberDefineNameAttribute)) as MemberDefineNameAttribute;
                    var name = attr!.Name;
                    dictionary.Add((T)Enum.Parse(type, item.Name), name);
                }
                return dictionary;
            }
        }

        /// <summary>
        /// 获取 <see cref="ClassKeyword"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>类修饰符</returns>
        public static string GetClassKeyword(ClassKeyword value) => Cache<ClassKeyword>.Data[value];

        /// <summary>
        /// 获取 <see cref="RecordKeyword"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>类修饰符</returns>
        public static string GetRecordKeyword(RecordKeyword value) => Cache<RecordKeyword>.Data[value];

        /// <summary>
        /// 获取 <see cref="StructKeyword"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>结构体修饰符</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetStructKword(StructKeyword value) => Cache<StructKeyword>.Data[value];

        /// <summary>
        /// 获取 <see cref="EventKeyword"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>事件类型</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEventKeyword(EventKeyword value) => Cache<EventKeyword>.Data[value];

        /// <summary>
        /// 获取 <see cref="GenericKeyword"/>  枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>泛型类型</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGenericKeyword(GenericKeyword value) => Cache<GenericKeyword>.Data[value];

        /// <summary>
        /// 获取 <see cref="FieldKeyword"/>  枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>字段</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetFieldKeyword(FieldKeyword value) => Cache<FieldKeyword>.Data[value];

        /// <summary>
        /// 获取 <see cref="MemberAccess"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>成员访问修饰符</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMemberAccess(MemberAccess value) => Cache<MemberAccess>.Data[value];

        /// <summary>
        /// 获取 <see cref="MemberType"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>成员类型</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMemberType(MemberType value) => Cache<MemberType>.Data[value];

        /// <summary>
        /// 获取 <see cref="MethodKeyword"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>方法修饰符</returns>
        public static string GetMethodKeyword(MethodKeyword value) => Cache<MethodKeyword>.Data[value];


        /// <summary>
        /// 获取 <see cref="NamespaceAccess"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetNamespaceAccess(NamespaceAccess value) => Cache<NamespaceAccess>.Data[value];


        /// <summary>
        /// 获取 <see cref="PropertyKeyword"/> 枚举代表的信息。
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>属性修饰符</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetPropertyKeyword(PropertyKeyword value) => Cache<PropertyKeyword>.Data[value];

        #endregion


        /// <summary>
        /// 获取某个枚举的代表的值。
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>修饰符或关键字</returns>
        public static string GetValue<T>(T value) where T : Enum => Cache<T>.Data[value];

        /// <summary>
        /// 转换标识符。
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static PropertyKeyword ToPropertyKeyword(this MethodKeyword keyword)
        {
            return (PropertyKeyword)((int)keyword);
            //switch (keyword)
            //{
            //    case MethodKeyword.Default: return PropertyKeyword.Default;
            //    case MethodKeyword.Static: return PropertyKeyword.Static;
            //    case MethodKeyword.Abstract: return PropertyKeyword.Abstract;
            //    case MethodKeyword.Virtual: return PropertyKeyword.Virtual;
            //    case MethodKeyword.Override: return PropertyKeyword.Override;
            //    case MethodKeyword.SealedOverride: return PropertyKeyword.SealedOverride;
            //    case MethodKeyword.NewVirtual: return PropertyKeyword.NewVirtual;
            //    case MethodKeyword.NewStatic: return PropertyKeyword.NewStatic;
            //}
            //throw new NotSupportedException("不支持此转换！");
        }

        /// <summary>
        /// 转换标识符。
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static MethodKeyword ToMethodKeyword(this PropertyKeyword keyword)
        {
            return (MethodKeyword)((int)keyword);
            //switch (keyword)
            //{
            //    case MethodKeyword.Default: return PropertyKeyword.Default;
            //    case MethodKeyword.Static: return PropertyKeyword.Static;
            //    case MethodKeyword.Abstract: return PropertyKeyword.Abstract;
            //    case MethodKeyword.Virtual: return PropertyKeyword.Virtual;
            //    case MethodKeyword.Override: return PropertyKeyword.Override;
            //    case MethodKeyword.SealedOverride: return PropertyKeyword.SealedOverride;
            //    case MethodKeyword.NewVirtual: return PropertyKeyword.NewVirtual;
            //    case MethodKeyword.NewStatic: return PropertyKeyword.NewStatic;
            //}
            //throw new NotSupportedException("不支持此转换！");
        }

    }
}
