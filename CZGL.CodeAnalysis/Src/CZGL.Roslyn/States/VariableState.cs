using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 字段、属性状态机
    /// </summary>
    public class VariableState
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 成员类型
        /// </summary>
        public string MemberType { get; set; }

        /// <summary>
        /// 初始化器
        /// </summary>
        public string InitCode { get; set; }
    }
}
