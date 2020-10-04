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
        public string GetBlock { get; set; } = "get;";
        public string SetBlock { get; set; } = "set;";
    }
}
