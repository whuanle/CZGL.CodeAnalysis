using System;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 命名空间中的成员修饰符。
    /// <para>嵌套成员请使用 <see cref="MemberAccess"/></para>
    /// </summary>
    [CLSCompliant(true)]
    public enum NamespaceAccess : int
    {
        /// <summary>
        /// 无
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
        Public = 1 << 1
    }
}
