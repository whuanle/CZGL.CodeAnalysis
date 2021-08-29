using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 属性状态机
    /// </summary>
    public class PropertyState : FieldState
    {
        /// <summary>
        /// get
        /// </summary>
        public string GetBlock { get; set; } = "get;";

        /// <summary>
        /// set
        /// </summary>
        public string SetBlock { get; set; } = "set;";
    }
}
