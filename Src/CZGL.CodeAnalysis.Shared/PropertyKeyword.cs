using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 属性可用的关键字
    /// </summary>
    [Flags]
    public enum PropertyKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        [MemberDefineName(Name = "abstract")]
        Abstract = 1 << 3,

        [MemberDefineName(Name = "virtual")]
        Virtual = 1 << 4,

        [MemberDefineName(Name = "override")]
        Override = 1 << 5,

        [MemberDefineName(Name = "sealed override")]
        SealedOverride = 1 << 6,

        [MemberDefineName(Name = "new")]
        New = 1 << 7,

         /// <summary>
        /// 暂不支持识别此类修饰符
        /// </summary>
        [MemberDefineName(Name = "new virsual")]
        NewVirtual = New | Virtual,

        
        [MemberDefineName(Name = "new static")]
        NewStatic = New | Static
    }
}
