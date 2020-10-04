using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 方法关键字
    /// </summary>
    public enum MethodKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        [MemberDefineName(Name = "abstract")]
        Abstract = 1 << 3,

        [MemberDefineName(Name = "virtual")]
        Virtual = 1 << 4,

        [MemberDefineName(Name = "override")]
        Override = 1 << 5,

        [MemberDefineName(Name = "sealed override")]
        SealedOverride = 1 << 6
    }
}
