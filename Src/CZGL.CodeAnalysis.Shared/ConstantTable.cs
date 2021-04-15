using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 常量表
    /// </summary>
    public static class ConstantTable
    {
        private static readonly Dictionary<int, string> BaseTypeNameTable = new Dictionary<int, string>
        {
            {(int)TypeCode.Empty,""},
            {(int)TypeCode.Object,"object"},
            {(int)TypeCode.Boolean ,"bool"},
            {(int)TypeCode.Char ,"char" },
            {(int)TypeCode.SByte,"sbyte" },
            {(int)TypeCode.Byte ,"byte" },
            {(int)TypeCode.Int16,"short"},
            {(int)TypeCode.UInt16 ,"ushort"},
            {(int)TypeCode.Int32,"int" },
            {(int)TypeCode.UInt32 ,"uint"},
            {(int)TypeCode.Int64,"long"},
            {(int)TypeCode.UInt64,"ulong"},
            {(int)TypeCode.Single,"float"},
            {(int)TypeCode.Double ,"double"},
            {(int)TypeCode.Decimal ,"decimal"},
            {(int)TypeCode.String,"string" },
        };

        /// <summary>
        /// 获取基本类型的表达名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetBaseTypeName(object value)
        {
            TypeCode typeCode;
            if (value is IConvertible temp)
            {
                typeCode = temp.GetTypeCode();
            }
            else return value.GetType().Name;

            if (BaseTypeNameTable.TryGetValue((int)typeCode, out var name))
                return name;

            if (typeCode == TypeCode.Object)
                return value.GetType().Name;

            return string.Empty;
        }

        /// <summary>
        /// 获取基本类型的表达名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetBaseTypeName(Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);
            if (BaseTypeNameTable.TryGetValue((int)typeCode, out var name))
                return name;

            if (typeCode == TypeCode.Object)
                return type.Name;

            return string.Empty;
        }
    }
}
