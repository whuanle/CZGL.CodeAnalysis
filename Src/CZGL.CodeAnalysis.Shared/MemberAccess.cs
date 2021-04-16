using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 命名空间中所有种类的成员的访问修饰符
    /// <para>接口，枚举，嵌套类、属性、字段、方法等</para>
    /// </summary>
    [Flags]
    [CLSCompliant(true)]
    public enum MemberAccess
    {
        /// <summary>
        /// None
        /// </summary>
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// internal
        /// </summary>
        [MemberDefineName(Name = "internal")]
        Internal = 1,

        /// <summary>
        /// public
        /// </summary>
        [MemberDefineName(Name = "public")]
        Public = 1 << 1,

        /// <summary>
        /// protected
        /// </summary>
        [MemberDefineName(Name = "protected")]
        Protected = 1 << 2,

        /// <summary>
        /// private
        /// </summary>
        [MemberDefineName(Name = "private")]
        Private = 1 << 3,

        /// <summary>
        /// protected internal
        /// </summary>
        [MemberDefineName(Name = "protected internal")]
        ProtectedInternal = Protected | Internal,

        /// <summary>
        /// private protected
        /// <para><b>只允许 C# 7.2 以上项目使用(C# 7.2 新增)。</b></para>
        /// </summary>
        [MemberDefineName(Name = "private protected")]
        PrivateProtected = Private | Protected
    }
}
