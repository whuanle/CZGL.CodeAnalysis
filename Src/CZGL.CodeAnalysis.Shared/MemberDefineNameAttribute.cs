using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 为枚举定义注释别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class MemberDefineNameAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
