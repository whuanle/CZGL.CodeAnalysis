using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 方法关键字
    /// </summary>
    [CLSCompliant(true)]
    public enum MethodKeyword
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
        /// <para>定义为抽象方法，则不能再有方法体。</para>
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
        /// <para>指隐藏父类的方法</para>
        /// </summary>
        [MemberDefineName(Name = "new")]
        New = 1 << 7,

        /// <summary>
        /// 暂不支持识别此类修饰符
        /// </summary>
        [MemberDefineName(Name = "new virsual")]
        NewVirtual = New | Virtual,

        /// <summary>
        /// 暂不支持识别此类修饰符
        /// </summary>
        [MemberDefineName(Name = "new static")]
        NewStatic = New | Static

#warning 支持 extern 方法关键字
#warning 要支持 ref readonly 方法
    }
}
