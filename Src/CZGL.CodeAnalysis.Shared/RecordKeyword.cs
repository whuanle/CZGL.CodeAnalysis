using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// Record 类的修饰符。
    /// <para>表示一个类为密封类、静态类、抽象类之一。</para>
    /// </summary>
    [CLSCompliant(true)]
    public enum RecordKeyword : int
    {
        /// <summary>
        /// 没有任何修饰符号
        /// </summary>
        [MemberDefineName(Name = "")]
        Default = 0,

        /// <summary>
        /// 安全类(不可被继承的类)
        /// <para><c><b>sealed</b> class Test{}</c></para>
        /// </summary>
        [MemberDefineName(Name = "sealed")]
        Sealed = 1,

        /// <summary>
        /// 抽象类
        /// <para><c><b>abstract</b> class Test{}</c></para>
        /// </summary>
        [MemberDefineName(Name = "abstract")]
        Abstract = 3,

        /// <summary>
        /// 嵌套类
        /// </summary>
        [MemberDefineName(Name = "new")]
        New = 4
    }
}
