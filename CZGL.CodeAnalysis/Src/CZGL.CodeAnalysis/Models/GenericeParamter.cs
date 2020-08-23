using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    /// <summary>
    /// 泛型参数的信息
    /// </summary>
    public interface GenericeParamter
    {
        /// <summary>
        /// 当前参数是否为已经定义类型
        /// </summary>
        bool IsHasefineGenerice { get; }

        /// <summary>
        /// 当前参数是否定义了泛型约束
        /// </summary>
        bool IsConstraint { get; }

        /// <summary>
        /// 泛型约束的约束名称
        /// </summary>
        GenericeConstraint[] Constraints { get; }

        /// <summary>
        /// 参数的名称
        /// <para>如果为未定义的泛型参数，则返回定义时的名称</para>
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 参数详细名称
        /// <para>带命名空间前缀；如果为未定义的泛型参数，则为 null </para>
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 参数的类型
        ///  <para>如果为未定义的泛型参数，则为 null </para>
        /// </summary>
        Type ParamterType { get; }

    }
}
