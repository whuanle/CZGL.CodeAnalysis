using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 结构体修饰符
    /// </summary>
    [CLSCompliant(true)]
    public enum StructKeyword
    {
        /// <summary>
        /// None
        /// </summary>
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// readonly
        /// </summary>
        [MemberDefineName(Name = "readonly")]
        Readonly = 1,

        /// <summary>
        /// ref
        /// </summary>
        [MemberDefineName(Name = "ref")]
        Ref = 1 << 1,

        /// <summary>
        /// readonly ref
        /// </summary>
        [MemberDefineName(Name = "readonly ref")]
        ReadonlyRef = Readonly | Ref
    }
}
