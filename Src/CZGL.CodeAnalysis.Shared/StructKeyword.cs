using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 结构体访问修饰符
    /// </summary>
    public enum StructKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// 只能定义在嵌套类中
        /// </summary>
        [MemberDefineName(Name = "readonly")]
        Readonly = 1
    }
}
