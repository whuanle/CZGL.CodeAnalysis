using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 结构体修饰符
    /// </summary>
    public enum StructKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "readonly")]
        Readonly = 1,


        [MemberDefineName(Name = "ref")]
        Ref = 1 << 1,

        [MemberDefineName(Name = "readonly ref")]
        ReadonlyRef = Readonly | Ref
    }
}
