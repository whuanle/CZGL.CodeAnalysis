using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 成员成员。
    /// <para>指示当前成员是何种类型/结构。</para>
    /// </summary>
    [CLSCompliant(true)]
    public enum MemberType : int
    {
        /// <summary>
        /// 不是目标类型
        /// </summary>
        [MemberDefineName(Name = "")]
        None,

        /// <summary>
        /// 类
        /// </summary>
        [MemberDefineName(Name = "class")]
        Class,

        /// <summary>
        /// 委托
        /// </summary>
        [MemberDefineName(Name = "delegate")]
        Delegate,

        /// <summary>
        /// 结构体
        /// </summary>
        [MemberDefineName(Name = "struct")]
        Struct,

        /// <summary>
        /// 接口
        /// </summary>
        [MemberDefineName(Name = "interface")]
        Interface,

        /// <summary>
        /// 基础类型
        /// <see cref="int"/>、<see cref="bool"/>、<see cref="byte"/> 等简单值类型
        /// </summary>
        [MemberDefineName(Name = "BaseType")]
        BaseType,

        /// <summary>
        /// 枚举
        /// </summary>
        [MemberDefineName(Name = "enum")]
        Enum,

        /// <summary>
        /// 构造函数
        /// </summary>
        [MemberDefineName(Name = "constructor")]
        Constructor,

        /// <summary>
        /// 事件
        /// </summary>
        [MemberDefineName(Name = "event")]
        Event,

        /// <summary>
        /// 字段
        /// </summary>
        [MemberDefineName(Name = "field")]
        Field,

        /// <summary>
        /// 方法
        /// </summary>
        [MemberDefineName(Name = "method")]
        Method,

        /// <summary>
        /// 属性
        /// </summary>
        [MemberDefineName(Name = "property")]
        Property
    }
}
