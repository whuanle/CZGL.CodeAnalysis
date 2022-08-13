using System;

namespace CCode.Shared
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
        [DefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// internal
        /// </summary>
        [DefineName(Name = "internal")]
        Internal = 1,

        /// <summary>
        /// public
        /// </summary>
        [DefineName(Name = "public")]
        Public = 1 << 1
    }
}
