using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 事件关键字
    /// </summary>
    [CLSCompliant(true)]
    public enum EventKeyword
    {
        /// <summary>
        /// 无
        /// </summary>
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// 静态事件
        /// </summary>
        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        /// <summary>
        /// virtual事件
        /// </summary>
        [MemberDefineName(Name = "virtual")]
        Virtual = 1 << 2,

        /// <summary>
        /// 抽象事件
        /// </summary>
        [MemberDefineName(Name = "abstract")]
        Abstract = 1 << 3,

        /// <summary>
        /// 密封事件
        /// </summary>
        [MemberDefineName(Name = "sealed")]
        Sealed = 1 << 4
    }
}
