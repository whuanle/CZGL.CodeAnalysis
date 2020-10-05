using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 命名空间中所有种类的成员的访问修饰符
    /// <para>接口，枚举，嵌套类、属性、字段、方法等</para>
    /// </summary>
    [Flags]
    public enum MemberAccess
    {
        [MemberDefineName(Name = "internal")]
        Internal = 0,

        [MemberDefineName(Name = "public")]
        Public = 1,

        [MemberDefineName(Name = "protected")]
        Protected = 1 << 1,

        [MemberDefineName(Name = "private")]
        Private = 1 << 2,

        [MemberDefineName(Name = "protected internal")]
        ProtectedInternal = Protected | Internal,

        /// <summary>
        // 只允许 C# 7.2 以上项目使用(C# 7.2 新增)
        /// </summary>
        [MemberDefineName(Name = "private protected")]
        PrivateProtected = Private | Protected
    }
}
