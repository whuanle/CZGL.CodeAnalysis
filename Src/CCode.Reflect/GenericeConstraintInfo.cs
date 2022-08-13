using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Reflect
{
    /// <summary>
    /// 表示泛型参数的一个约束信息。
    /// </summary>
    public class GenericeConstraintInfo
    {
        /// <summary>
        /// 表示泛型参数的一个约束信息
        /// </summary>
        /// <param name="keyword">泛型关键字</param>
        /// <param name="location">摆放位置</param>
        /// <param name="value">具体约束名称</param>
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
