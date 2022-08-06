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
        [DefineName(Name = "")]
        None,

        /// <summary>
        /// 类
        /// </summary>
        [DefineName(Name = "class")]
        Class,

        /// <summary>
        /// 委托
        /// </summary>
        [DefineName(Name = "delegate")]
        Delegate,

        /// <summary>
        /// 结构体
        /// </summary>
        [DefineName(Name = "struct")]
        Struct,

        /// <summary>
        /// 接口
        /// </summary>
        [DefineName(Name = "interface")]
        Interface,

        /// <summary>
        /// 基础类型
        /// <see cref="int"/>、<see cref="bool"/>、<see cref="byte"/> 等简单值类型
        /// </summary>
        [DefineName(Name = "BaseType")]
        BaseType,

        /// <summary>
        /// 枚举
        /// </summary>
        [DefineName(Name = "enum")]
        Enum,

        /// <summary>
        /// 构造函数
        /// </summary>
        [DefineName(Name = "constructor")]
        Constructor,

        /// <summary>
        /// 事件
        /// </summary>
        [DefineName(Name = "event")]
        Event,

        /// <summary>
        /// 字段
        /// </summary>
        [DefineName(Name = "field")]
        Field,

        /// <summary>
        /// 方法
        /// </summary>
        [DefineName(Name = "method")]
        Method,

        /// <summary>
        /// 属性
        /// </summary>
        [DefineName(Name = "property")]
        Property
    }
}
