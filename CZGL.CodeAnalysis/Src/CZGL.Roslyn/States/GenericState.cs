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
        public string Name { get; set; }
        public HashSet<string> Constraints { get; set; } = new HashSet<string>();
    }
}
