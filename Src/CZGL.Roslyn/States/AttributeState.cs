using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 特性状态机
    /// </summary>
    public class AttributeState
    {
        /// <summary>
        /// 特性的构造函数传递的值
        /// </summary>
        /// <example>
        /// <code>[Display("abcd")] -> abcd</code>
        /// </example>
        public string Ctor { get; set; }

        /// <summary>
        /// 特性的属性的值
        /// </summary>
        public HashSet<string> Propertys { get; set; } = new HashSet<string>();
    }
}
