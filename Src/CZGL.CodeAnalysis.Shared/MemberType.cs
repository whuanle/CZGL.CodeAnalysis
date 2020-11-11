using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 何种成员
    /// </summary>
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
        Enum,
        Constructor,
        Event,
        Field,
        Method,
        Property
    }
}
