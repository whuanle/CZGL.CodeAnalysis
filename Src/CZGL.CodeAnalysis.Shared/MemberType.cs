using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 何种成员
    /// <para>指示当前成员是何种类型/结构。</para>
    /// </summary>
    [CLSCompliant(true)]
    public enum MemberType
    {
        /// <summary>
        /// 不是目标类型
        /// </summary>
        None,

        /// <summary>
        /// 类
        /// </summary>
        Class,

        /// <summary>
        /// 委托
        /// </summary>
        Delegate,

        /// <summary>
        /// 结构体
        /// </summary>
        Struct,
        /// <summary>
        /// 接口
        /// </summary>
        Interface,

        /// <summary>
        /// 基础类型
        /// <see cref="int"/>、<see cref="bool"/>、<see cref="byte"/> 等简单值类型
        /// </summary>
        BaseValue,

        /// <summary>
        /// 枚举
        /// </summary>
        Enum,

        /// <summary>
        /// 构造函数
        /// </summary>
        Constructor,

        /// <summary>
        /// 事件
        /// </summary>
        Event,

        /// <summary>
        /// 字段
        /// </summary>
        Field,

        /// <summary>
        /// 方法
        /// </summary>
        Method,

        /// <summary>
        /// 属性
        /// </summary>
        Property
    }
}
