using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 泛型约束类型
    /// </summary>
    public enum GenericKeyword
    {
        /// <summary>
        /// 无任何约束
        /// </summary>
        NoConstrant = 0,

        /// <summary>
        /// 值类型约束
        /// <para>不能与其它约束一起使用</para>
        /// </summary>
        Struct = 1,

        /// <summary>
        /// 引用类型约束
        /// <para>必须放到开头</para>
        /// </summary>
        Class = 1 << 1,

        /// <summary>
        /// 非空约束
        /// <para>必须放到开头</para>
        /// </summary>
        Notnull = 1 << 2,

        /// <summary>
        /// 不可为 null 的非托管类型
        /// <para>不能与其它约束一起使用</para>
        /// </summary>
        Unmanaged = 1 << 3,

        /// <summary>
        /// 具有无参数构造函数
        /// <para>必须放在最后</para>
        /// </summary>
        New = 1 << 4,

        /// <summary>
        /// 必须继承基类
        /// <para>必须放在开头</para>
        /// </summary>
        BaseClass = 1 << 5,

        /// <summary>
        /// 必须继承接口
        /// <para>不限定</para>
        /// </summary>
        Interface = 1 << 6,

        /// <summary>
        /// 参数必须是为 U 提供的参数或派生自为 U 提供的参数
        /// <para>不限定</para>
        /// </summary>
        TU = 1 << 7,

        /// <summary>
        /// 类型参数必须是可为 null 或不可为 null 的引用类型
        /// </summary>
        ClassNull = 1<<8
    }
}
