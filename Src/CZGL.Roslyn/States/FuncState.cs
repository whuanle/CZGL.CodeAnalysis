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
        /// 使用代码直接生成
        /// </summary>
        public bool UseCode { get; set; } = false;

        /// <summary>
        /// 使用代码
        /// </summary>
        public string Code { get; set; }

    }
}
