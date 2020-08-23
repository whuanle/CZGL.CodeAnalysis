using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 类的修饰符
    /// <para>密封类、静态类、抽象类</para>
    /// </summary>
    public enum ClassQualifierType
    {
        Default = 0,
        Sealed = 1,
        Static = 2,
        Abstract = 3
    }
}
