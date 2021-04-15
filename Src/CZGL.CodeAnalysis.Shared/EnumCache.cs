using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.CompilerServices;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 枚举信息缓存
    /// </summary>
    public static class EnumCache
    {
        static EnumCache()
        {
            _ = _ClassKeyWork.Value;
            _ = _StructKeyWork.Value;
            _ = _EventKeyword.Value;
            _ = _FieldKeyword.Value;
            _ = _GenericKeyword.Value;
            _ = _MemberAccess.Value;
            _ = _MemberType.Value;
            _ = _MethodKeyword.Value;
            _ = _NamespaceAccess.Value;
            _ = _PropertyKeyword.Value;
        }

        #region 生成缓存

        /// <summary>
        /// 获取枚举所有值并存储到字典中
        /// </summary>
        /// <param name="dictionary"></param>
        /// <typeparam name="T"></typeparam>
        private static Dictionary<T, string> GetDictionary<T>(Dictionary<T, string> dictionary) where T : Enum
        {
            Type type = typeof(T);
            var enumValues = Enum.GetValues(type);
            foreach (var item in enumValues)
            {
                FieldInfo field = type.GetField(Enum.GetName(type, item));
                var attrs = field.GetCustomAttributesData();
                var name = GetDisplayNameValue(attrs);
                dictionary.Add((T)item, name);
            }

            return dictionary;

            string GetDisplayNameValue(IList<CustomAttributeData> attrs)
            {
                if (attrs == null || attrs.Count == 0)
                    return null;
                var argument = attrs.FirstOrDefault(x => x.AttributeType.Name == nameof(MemberDefineNameAttribute)).NamedArguments;
                return argument.FirstOrDefault(x => x.MemberName == nameof(MemberDefineNameAttribute.Name)).TypedValue.Value.ToString();
            }
        }

        #endregion


        #region 缓存

        /// <summary>
        /// ClassKeyWork 缓存
        /// </summary>
        /// <returns></returns>
        private static readonly Lazy<Dictionary<ClassKeyword, string>> _ClassKeyWork = new Lazy<Dictionary<ClassKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<ClassKeyword, string>());
        });

        private static readonly Lazy<Dictionary<StructKeyword, string>> _StructKeyWork = new Lazy<Dictionary<StructKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<StructKeyword, string>());
        });

        private static Lazy<Dictionary<EventKeyword, string>> _EventKeyword = new Lazy<Dictionary<EventKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<EventKeyword, string>());
        });

        private static Lazy<Dictionary<FieldKeyword, string>> _FieldKeyword = new Lazy<Dictionary<FieldKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<FieldKeyword, string>());
        });

        private static Lazy<Dictionary<GenericKeyword, string>> _GenericKeyword = new Lazy<Dictionary<GenericKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<GenericKeyword, string>());
        });

        private static Lazy<Dictionary<MemberAccess, string>> _MemberAccess = new Lazy<Dictionary<MemberAccess, string>>(() =>
        {
            return GetDictionary(new Dictionary<MemberAccess, string>());
        });

        private static Lazy<Dictionary<MemberType, string>> _MemberType = new Lazy<Dictionary<MemberType, string>>(() =>
        {
            return GetDictionary(new Dictionary<MemberType, string>());
        });

        private static Lazy<Dictionary<MethodKeyword, string>> _MethodKeyword = new Lazy<Dictionary<MethodKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<MethodKeyword, string>());
        });

        private static Lazy<Dictionary<NamespaceAccess, string>> _NamespaceAccess = new Lazy<Dictionary<NamespaceAccess, string>>(() =>
        {
            return GetDictionary(new Dictionary<NamespaceAccess, string>());
        });

        private static Lazy<Dictionary<PropertyKeyword, string>> _PropertyKeyword = new Lazy<Dictionary<PropertyKeyword, string>>(() =>
        {
            return GetDictionary(new Dictionary<PropertyKeyword, string>());
        });

        #endregion


        #region 获取

        /// <summary>
        /// 获取 ClassKeyword 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetClassKeyword(ClassKeyword value)
        {
            return _ClassKeyWork.Value[value];
        }

        public static string GetStructKword(StructKeyword value)
        {
            return _StructKeyWork.Value[value];
        }

        /// <summary>
        /// 获取 EventKeyword 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetEventKeyword(EventKeyword value)
        {
            return _EventKeyword.Value[value];
        }

        /// <summary>
        /// 获取 GenericKeyword 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetGenericKeyword(GenericKeyword value)
        {
            return _GenericKeyword.Value[value];
        }

        /// <summary>
        /// 获取 FieldKeyword 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetFieldKeyword(FieldKeyword value)
        {
            return _FieldKeyword.Value[value];
        }

        /// <summary>
        /// 获取 MemberAccess 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetMemberAccess(MemberAccess value)
        {
            return _MemberAccess.Value[value];
        }


        /// <summary>
        /// 获取 MemberType 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetMemberType(MemberType value)
        {
            return _MemberType.Value[value];
        }



        /// <summary>
        /// 获取 MethodKeyword 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetMethodKeyword(MethodKeyword value)
        {
            return _MethodKeyword.Value[value];
        }


        /// <summary>
        /// 获取 NamespaceAccess 枚举代表的信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetNamespaceAccess(NamespaceAccess value)
        {
            return _NamespaceAccess.Value[value];
        }


        public static string GetPropertyKeyword(PropertyKeyword value)
        {
            return _PropertyKeyword.Value[value];
        }

        #endregion


        /// <summary>
        /// 获取某个枚举的代表的值
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetValue<T>(T value) where T : Enum
        {
            switch (value)
            {
                case ClassKeyword keyword: return GetClassKeyword(keyword);
                case StructKeyword keyword: return GetStructKword(keyword);
                case EventKeyword keyword: return GetEventKeyword(keyword);
                case FieldKeyword keyword: return GetFieldKeyword(keyword);
                case GenericKeyword keyword: return GetGenericKeyword(keyword);
                case MemberAccess keyword: return GetMemberAccess(keyword);
                case MemberType keyword: return GetMemberType(keyword);
                case MethodKeyword keyword: return GetMethodKeyword(keyword);
                case NamespaceAccess keyword: return GetNamespaceAccess(keyword);
                case PropertyKeyword keyword: return GetPropertyKeyword(keyword);
            }

            throw new NotSupportedException("不支持所指定的枚举类型");
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyKeyword ToPropertyKeyword(this MethodKeyword keyword)
        {
            switch (keyword)
            {
                case MethodKeyword.Default: return PropertyKeyword.Default;
                case MethodKeyword.Static: return PropertyKeyword.Static;
                case MethodKeyword.Abstract: return PropertyKeyword.Abstract;
                case MethodKeyword.Virtual: return PropertyKeyword.Virtual;
                case MethodKeyword.Override: return PropertyKeyword.Override;
                case MethodKeyword.SealedOverride: return PropertyKeyword.SealedOverride;
                case MethodKeyword.NewVirtual: return PropertyKeyword.NewVirtual;
                case MethodKeyword.NewStatic: return PropertyKeyword.NewStatic;
            }
            throw new NotSupportedException("不支持此转换！");
        }
    }
}
