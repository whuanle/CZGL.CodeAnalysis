﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 字段的关键字
    /// <para>该枚举可组合，但不能随意组合，建议使用枚举中已定义的组合。</para>
    /// </summary>
    [Flags]
    [CLSCompliant(true)]
    public enum FieldKeyword
    {
        /// <summary>
        /// 无
        /// </summary>
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// 常量字段
        /// </summary>
        [MemberDefineName(Name = "const")]
        Const = 1,

        /// <summary>
        /// 静态字段
        /// </summary>
        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        /// <summary>
        /// 只读字段
        /// </summary>
        [MemberDefineName(Name = "readonly")]
        Readonly = 1 << 2,

        /// <summary>
        /// 静态只读字段
        /// </summary>
        [MemberDefineName(Name = "static readonly")]
        StaticReadonly = Static | Readonly,

        /// <summary>
        /// volatile 字段
        /// </summary>
        [MemberDefineName(Name = "volatile")]
        Volatile = StaticReadonly << 1,

        /// <summary>
        /// volatile static 修饰的字段
        /// </summary>
        [MemberDefineName(Name = "volatile static")]
        VolatileStatic = Volatile | Static
    }
}
