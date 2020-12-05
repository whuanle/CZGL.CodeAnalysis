using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Reflect.Units
{
    /// <summary>
    /// 表示泛型参数的一个约束信息
    /// </summary>
    public struct GenericeConstraintInfo
    {
        public GenericeConstraintInfo(GenericKeyword keyword, ConstraintLocation location, string value)
        {
            Keyword = keyword;
            Location = location;
            Value = value;
        }

        /// <summary>
        /// 泛型约束类型
        /// </summary>
        public GenericKeyword Keyword { get; private set; }

        /// <summary>
        /// 泛型约束位置
        /// </summary>
        public ConstraintLocation Location { get; private set; }

        /// <summary>
        /// 泛型约束的值
        /// </summary>
        public string Value { get; private set; }
    }
}
