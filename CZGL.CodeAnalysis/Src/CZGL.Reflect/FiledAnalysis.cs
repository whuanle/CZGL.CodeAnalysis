using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CZGL.Reflect
{
    /// <summary>
    /// 字段分析
    /// </summary>
    public class FiledAnalysis
    {
        private readonly FieldInfo _info;
        public FiledAnalysis(FieldInfo info)
        {
            _info = info;
        }

        public MemberAccess Access
        {
            get
            {
                return TypeAnalysis.GetAccess(_info);
            }
        }

        public string AccessCode
        {
            get
            {
                return TypeAnalysis.GetAccessCode(_info);
            }
        }

        /// <summary>
        /// 获取修饰符关键字
        /// </summary>
        public FieldKeyword Keyword
        {
            get
            {
                return GetFieldKeyword(_info);
            }
        }

        /// <summary>
        /// 获取修饰符关键字
        /// </summary>
        public string KeywordCode
        {
            get
            {
                return GetFieldKeywordCode(_info);
            }
        }

        public static FieldKeyword GetFieldKeyword(FieldInfo info)
        {
            if (info.IsLiteral)
                return FieldKeyword.Const;
            if (info.IsStatic && info.IsInitOnly)
                return FieldKeyword.StaticReadonly;
            bool isVolatile = info.GetRequiredCustomModifiers().Any(x => x == typeof(IsVolatile));
            if (info.IsStatic)
            {
                if (isVolatile) return FieldKeyword.VolatileStatic;
                return FieldKeyword.Static;
            }
            if (isVolatile) return FieldKeyword.Volatile;
            if (info.IsInitOnly)
                return FieldKeyword.Readonly;

            return FieldKeyword.Default;
        }
        public static string GetFieldKeywordCode(FieldInfo info)
        {
            if (info.IsLiteral)
                return "const";
            if (info.IsStatic && info.IsInitOnly)
                return "readonly static";
            bool isVolatile = info.GetRequiredCustomModifiers().Any(x => x == typeof(IsVolatile));
            if (info.IsStatic)
            {
                if (isVolatile) return "volatile static";
                return "static";
            }
            if (isVolatile) return "volatile";
            if (info.IsInitOnly)
                return "readonly";

            return string.Empty;
        }
    }
}
