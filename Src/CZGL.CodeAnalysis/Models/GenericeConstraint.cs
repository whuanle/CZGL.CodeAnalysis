using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    /// <summary>
    /// 泛型约束的信息
    /// </summary>
    public class GenericeConstraint
    {
        public GenericeConstraint() { }
        public GenericeConstraint(string name, ConstraintScheme scheme, Type constraintType = null)
        {
            Name = name;
            ConstraintScheme = scheme;
            ConstraintType = constraintType;
        }
        /// <summary>
        /// 约束的名称
        /// <para>struct、class、notnull、unmanaged、new()等</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 如果是接口约束和类型约束，则会记录此类型的 Type
        /// </summary>
        public Type ConstraintType { get; set; }

        /// <summary>
        /// 约束类型
        /// </summary>
        public ConstraintScheme ConstraintScheme { get; set; }
    }
}
