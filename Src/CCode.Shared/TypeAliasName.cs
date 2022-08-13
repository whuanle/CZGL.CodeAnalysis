using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCode.Shared
{
    /// <summary>
    /// 常量表。
    /// <para>记录常量映射关系，简化反射过程中的逻辑分析。</para>
    /// </summary>
    [CLSCompliant(true)]
    public static class TypeAliasName
    {
        /// <summary>
        /// 获取基本类型的表达名称，如果类型在 <see cref="TypeCode"/> 的表示中，不包括 DBNull，则返回常用类型名称。
        /// <para>对于非 Object 类型的引用类型，返回 null。</para>
        /// </summary>
        /// <param name="type">类型</param>
        public static string? GetName(Type type)
        {

            TypeCode typeCode = Type.GetTypeCode(type);
            if (typeCode == TypeCode.Object && type != typeof(object)) return null;

            switch (typeCode)
            {
                case TypeCode.Object: return "object";
                case TypeCode.Boolean: return "bool";
                case TypeCode.Char: return "char";
                case TypeCode.SByte: return "sbyte";
                case TypeCode.Byte: return "byte";
                case TypeCode.Int16: return "short";
                case TypeCode.UInt16: return "ushort";
                case TypeCode.Int32: return "int";
                case TypeCode.UInt32: return "uint";
                case TypeCode.Int64: return "long";
                case TypeCode.UInt64: return "ulong";
                case TypeCode.Single: return "float";
                case TypeCode.Double: return "double";
                case TypeCode.Decimal: return "decimal";
                case TypeCode.String: return "string";
                default: return null;
            }
        }
    }
}
