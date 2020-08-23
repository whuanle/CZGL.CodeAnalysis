using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    /// <summary>
    /// 类的定义信息
    /// </summary>
    public interface ClassDefine
    {
        /// <summary>
        /// 当前类类型
        /// </summary>
        Type ClassType { get; }

        /// <summary>
        /// 类定义
        /// </summary>
        string DefinitionString { get; }

        /// <summary>
        /// 是否密封类
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// 是否静态类
        /// </summary>
        bool IsStatic { get; }

        /// <summary>
        /// 是否抽象类
        /// </summary>
        bool IsAbstract { get; }
    }
}
