using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 方法状态机
    /// </summary>
    public class MethodState
    {
        /// <summary>
        /// 方法关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 方法是否具有代码体，例如抽象方法就没有代码体
        /// </summary>
        public bool IsHasBlockCode { get; set; } = true;

        /// <summary>
        /// 方法的代码块
        /// </summary>
        public string BlockCode { get; set; }
    }
}
