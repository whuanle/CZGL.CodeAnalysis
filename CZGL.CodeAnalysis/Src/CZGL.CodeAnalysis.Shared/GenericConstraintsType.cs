using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 泛型约束类型
    /// </summary>
    public enum GenericConstraintsType
    {
        /// <summary>
        /// 无任何约束
        /// </summary>
        NotHaveConstrant = 0,

        /// <summary>
        /// 值类型约束
        /// <para>Red</para>
        /// </summary>
        Struct = 1,

        /// <summary>
        /// 引用类型约束
        /// <para>Yellow</para>
        /// </summary>
        Class = 2,

        /// <summary>
        /// 非空约束
        /// <para>Yellow</para>
        /// </summary>
        Notnull = 4,

        /// <summary>
        /// 不可为 null 的非托管类型
        /// <para>Red</para>
        /// </summary>
        Unmanaged = 8,

        /// <summary>
        /// 具有无参数构造函数
        /// <para>Orange</para>
        /// </summary>
        New = 16,

        /// <summary>
        /// 必须继承基类
        /// <para>Yellow</para>
        /// </summary>
        BaseClass = 32,

        /// <summary>
        /// 必须继承接口
        /// <para>Blue</para>
        /// </summary>
        Interface = 64,

        /// <summary>
        /// 参数必须是为 U 提供的参数或派生自为 U 提供的参数
        /// <para>Blue</para>
        /// </summary>
        TU = 128
    }
}
