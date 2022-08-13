using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Shared
{
    /// <summary>
    /// 方法关键字。
    /// </summary>
    [CLSCompliant(true)]
    public enum MethodKeyword : int
    {
        /// <summary>
        /// None
        /// </summary>
        [DefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// static
        /// </summary>
        [DefineName(Name = "static")]
        Static = 1,

        /// <summary>
        /// abstract
        /// </summary>
        [DefineName(Name = "abstract")]
        Abstract = 1 << 1,

        /// <summary>
        /// virtual
        /// </summary>
        [DefineName(Name = "virtual")]
        Virtual = 1 << 2,

        /// <summary>
        /// override
        /// </summary>
        [DefineName(Name = "override")]
        Override = 1 << 3,

        /// <summary>
        /// sealed override
        /// </summary>
        [DefineName(Name = "sealed override")]
        SealedOverride = 1 << 4,

        /// <summary>
        /// ref readonly
        /// </summary>
        [DefineName(Name = "ref readonly")]
        RefReadonly = 1 << 5,

        /// <summary>
        /// static ref readonly
        /// </summary>
        [DefineName(Name = "static ref readonly")]
        StaticRefReadonly = 1 << 6,

        /// <summary>
        /// override ref readonly
        /// </summary>
        [DefineName(Name = "virtual ref readonly")]
        VirtualRefReadonly = 1 << 7,

        /// <summary>
        /// override ref readonly
        /// </summary>
        [DefineName(Name = "ovveride ref readonly")]
        OvverideRefReadonly = 1 << 8,

        /// <summary>
        /// extern 方法，表明此方法使用了非托管库的函数。
        /// <para>由于 extern 必须与 static 一起使用，因此这里使用 static extern 表达 extern 方法。</para>
        /// </summary>
        [DefineName(Name = "static extern")]
        StaticExtern = 1 << 9
    }
}
