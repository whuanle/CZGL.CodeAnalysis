using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{

    public class FuncState
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public string ReturnType { get; set; } = "void";

        /// <summary>
        /// 参数列表
        /// </summary>
        public HashSet<string> Params { get; set; } = new HashSet<string>();

        /// <summary>
        /// 泛型参数以及泛型参数约束
        /// </summary>
        public Dictionary<string, HashSet<string>> GenericParams { get; set; } = new Dictionary<string, HashSet<string>>();

    }
}
