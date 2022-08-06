using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 类的修饰符
    /// <para>表示一个类为密封类、静态类、抽象类之一，枚举不可组合</para>
    /// </summary>
    [CLSCompliant(true)]
    public enum ClassKeyword : int
    {
        /// <summary>
        /// 没有任何修饰符号。
        /// </summary>
        [DefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// 安全类(不可被继承的类)。
        /// </summary>
        [DefineName(Name = "sealed")]
        Sealed = 1,

        /// <summary>
        /// 静态类型。
        /// </summary>
        [DefineName(Name = "static")]
        Static = 2,

        /// <summary>
        /// 抽象类。
        /// </summary>
        [DefineName(Name = "abstract")]
        Abstract = 3
    }
}
