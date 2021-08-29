using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 编译状态机
    /// </summary>
    public class CompilationState
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 程序集输出路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 引用的命名空间
        /// </summary>
        public HashSet<NamespaceBuilder> Namespaces { get; } = new HashSet<NamespaceBuilder>();

        /// <summary>
        /// 是否自动引用程序集
        /// </summary>
        public bool UseAutoAssembly { get; set; } = false;

        /// <summary>
        /// 依赖的程序集
        /// </summary>
        public HashSet<Assembly> Assemblies { get; } = new HashSet<Assembly>();
    }
}
