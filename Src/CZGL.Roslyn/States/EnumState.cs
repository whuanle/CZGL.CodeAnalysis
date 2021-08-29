using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 枚举状态机
    /// </summary>
    public class EnumState
    {
        /// <summary>
        /// 枚举的字段
        /// </summary>
        public HashSet<string> Fields { get; set; } = new HashSet<string>();
    }
}
