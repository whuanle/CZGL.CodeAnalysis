using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis
{

    /// <summary>
    /// 字典表
    /// </summary>
#if DEBUG
    public
#else
        private
# endif
        class Dictionaries
    {
        private static readonly Dictionary<Type, string> ValueTypeAlias = new Dictionary<Type, string>()
        {
            { typeof(bool),"bool"},
            { typeof(byte),"byte"},
            { typeof(short),"short"},
            { typeof(decimal),"decimal"},
            {typeof(sbyte),"sbyte" },
            { typeof(ushort),"ushort"},
            { typeof(int),"int"},
            { typeof(uint),"uint"},
            { typeof(long),"long"},
            { typeof(ulong),"ulong"},
            { typeof(double),"double"},
            { typeof(float),"float"},
            { typeof(object),"object"}
        };

        /// <summary>
        /// 查找值类型的别名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FindAlias(Type type)
        {
            string name;
            bool isExist = ValueTypeAlias.TryGetValue(type, out name);
            if (isExist)
                return name;
            return type.Name;
        }
    }
}
