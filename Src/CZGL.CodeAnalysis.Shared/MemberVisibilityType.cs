using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 类成员访问修饰符
    /// <para>请不要组合枚举</para>
    /// <para>嵌套类、属性、字段、方法</para>
    /// </summary>
    public enum MemberVisibilityType
    {
        [MemberDefineName(Name = "internal")]
        Internal = 0,

        [MemberDefineName(Name = "public")]
        Public = 1,

        [MemberDefineName(Name = "protected")]
        Protected = 2,

        [MemberDefineName(Name = "private")]
        Private = 4,

        [MemberDefineName(Name = "protected internal")]
        ProtectedInternal = 8
    }
}
