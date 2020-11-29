using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 类的修饰符
    /// <para>密封类、静态类、抽象类</para>
    /// </summary>
    public enum ClassKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "sealed")]
        Sealed = 1,

        [MemberDefineName(Name = "static")]
        Static = 2,

        [MemberDefineName(Name = "abstract")]
        Abstract = 3,
        
        /// <summary>
        /// 只能定义在嵌套类中
        /// </summary>
        [MemberDefineName(Name = "new")]
        New = 4
    }
}
