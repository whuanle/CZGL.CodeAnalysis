using System;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 命名空间中的成员修饰符
    /// <para>嵌套成员请使用 <see cref="MemberAccess"/></para>
    /// </summary>
    public enum NamespaceAccess
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "internal")]
        Internal = 1,

        [MemberDefineName(Name = "public")]
        Public = 1 << 1
    }
}
