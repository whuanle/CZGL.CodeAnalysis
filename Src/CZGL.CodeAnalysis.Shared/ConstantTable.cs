using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 常量表。
    /// <para>记录常量映射关系，简化反射过程中的逻辑分析。</para>
    /// </summary>
    [CLSCompliant(true)]
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
        /// 获取基本类型的表达名称。
        /// <para>对于 System.Int32 等基础类型，会获得 int 这样的常用名称。不能用于泛型类型、数组。</para>
        /// </summary>
        /// <typeparam name="TObject">任意对象类型</typeparam>
        /// <param name="value">任意对象</param>
        /// <returns>基础类型的常用表达名称。如 int，而不是 System.Int32 。</returns>
        public static string GetBaseTypeName<TObject>(TObject value)
        {
            TypeCode typeCode;
            if (value is IConvertible temp)
            {
                typeCode = temp.GetTypeCode();

                if (typeCode == TypeCode.Object)
                    return value.GetType().Name;
            }
            else return value.GetType().Name;

            if (BaseTypeNameTable.TryGetValue((int)typeCode, out var name))
                return name;

            return string.Empty;
        }

        /// <summary>
        /// 获取基本类型的表达名称。
        /// <para>对于 System.Int32 等基础类型，会获得 int 这样的常用名称。不能用于泛型类型、数组。</para>
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns>基础类型的常用表达名称。如 int，而不是 System.Int32 。</returns>
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
