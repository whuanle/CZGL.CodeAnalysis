using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 属性可用的关键字
    /// </summary>
    [Flags]
    [CLSCompliant(true)]
    public enum PropertyKeyword
    {
        /// <summary>
        /// None
        /// </summary>
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// static
        /// </summary>
        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        /// <summary>
        /// abstract
        /// </summary>
        [MemberDefineName(Name = "abstract")]
        Abstract = 1 << 3,

        /// <summary>
        /// virtual
        /// </summary>
        [MemberDefineName(Name = "virtual")]
        Virtual = 1 << 4,

        /// <summary>
        /// override
        /// </summary>
        [MemberDefineName(Name = "override")]
        Override = 1 << 5,

        /// <summary>
        /// sealed override
        /// </summary>
        [MemberDefineName(Name = "sealed override")]
        SealedOverride = 1 << 6,

        /// <summary>
        /// new
        /// </summary>
        [MemberDefineName(Name = "new")]
        New = 1 << 7,

         /// <summary>
        /// 暂不支持识别此修饰符，因为当其设置为 virsual 时已经生效，使用 new 只不过隐藏编译器提示。
        /// </summary>
        [MemberDefineName(Name = "new virsual")]
        NewVirtual = New | Virtual,

        /// <summary>
        /// new static
        /// </summary>
        [MemberDefineName(Name = "new static")]
        NewStatic = New | Static
    }
}
