using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 构造函数状态机
    /// </summary>
    public class CtorState
    {
        /// <summary>
        /// 执行何种构造函数
        /// </summary>
        public string BaseOrThis { get; set; }
    }
}
