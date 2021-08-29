using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 一个泛型参数，以及泛型约束
    /// </summary>
    public class GenericState
    {
        /// <summary>
        /// 泛型对象、泛型方法、泛型委托的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 泛型约束
        /// </summary>
        public HashSet<string> Constraints { get; set; } = new HashSet<string>();
    }
}
