using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 事件关键字
    /// </summary>
    public enum EventKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        [MemberDefineName(Name = "virtual")]
        Virtual = 1 << 2,

        [MemberDefineName(Name = "abstract")]
        Abstract = 1 << 3,

        [MemberDefineName(Name = "sealed")]
        Sealed = 1 << 4
    }
}
