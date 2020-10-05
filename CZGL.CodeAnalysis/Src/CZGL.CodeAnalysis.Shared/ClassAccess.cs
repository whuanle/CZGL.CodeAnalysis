using System;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 类访问修饰符
    /// <para>嵌套类请使用 <see cref="MemberAccess"/></para>
    /// </summary>
    public enum ClassAccess
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "internal")]
        Internal = 1,

        [MemberDefineName(Name = ("public"))]
        Public = 1 << 1
    }
}
