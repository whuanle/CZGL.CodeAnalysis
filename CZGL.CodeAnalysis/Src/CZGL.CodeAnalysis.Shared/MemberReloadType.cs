
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    public enum MemberReloadType
    {
        [MemberDefineName(Name ="")]
        Default = 0,

        [MemberDefineName(Name = "new")]
        New = 1,

        [MemberDefineName(Name = "virtual")]
        Virtual = 2,

        [MemberDefineName(Name = "override")]
        Override = 4
    }
}
