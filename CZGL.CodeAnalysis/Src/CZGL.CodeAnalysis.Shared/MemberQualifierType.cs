using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// <para>请不要组合枚举</para>
    /// </summary>
    public enum MemberQualifierType
    {
        [MemberDefineName(Name = " ")]
        Default = 0,

        [MemberDefineName(Name ="const")]
        Const = 1,

        [MemberDefineName(Name = "static")]
        Static = 2,

        [MemberDefineName(Name = "readonly")]
        Readonly = 4,

        [MemberDefineName(Name = "abstract")]
        Abstract = 8,

        [MemberDefineName(Name = "static readonly")]
        StaticReadonly = 6,

    }
}
