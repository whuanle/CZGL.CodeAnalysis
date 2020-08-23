using System;

namespace CZGL.CodeAnalysis.Shared
{
    
    /// <summary>
    /// 类访问修饰符
    /// </summary>
    public enum ClassVisibilityType
    {
        [MemberDefineName(Name ="internal")]
        Internal = 0,

        [MemberDefineName(Name =("public"))]
        Public = 1
    }
}
