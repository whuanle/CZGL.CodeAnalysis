using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 命名空间状态机
    /// </summary>
    public class NamespaceState
    {
        /// <summary>
        /// 命名空间引用的其它命名空间
        /// </summary>
        public HashSet<string> Usings { get; } = new HashSet<string>();

        /// <summary>
        /// 命名空间下的接口
        /// </summary>
        public HashSet<InterfaceBuilder> Interfaces { get; } = new HashSet<InterfaceBuilder>();

        /// <summary>
        /// 命名空间下的结构体
        /// </summary>
        public HashSet<StructBuilder> Structs { get; } = new HashSet<StructBuilder>();

        /// <summary>
        /// 命名空间下的类
        /// </summary>
        public HashSet<ClassBuilder> Classes { get; } = new HashSet<ClassBuilder>();

        /// <summary>
        /// 命名空间下的枚举
        /// </summary>
        public HashSet<EnumBuilder> Enums { get; } = new HashSet<EnumBuilder>();

        /// <summary>
        /// 命名空间下的委托
        /// </summary>
        public HashSet<DelegateBuilder> Delegates { get; } = new HashSet<DelegateBuilder>();

        /// <summary>
        /// 命名空间下的事件
        /// </summary>
        public HashSet<EventBuilder> Events { get; } = new HashSet<EventBuilder>();
    }
}
