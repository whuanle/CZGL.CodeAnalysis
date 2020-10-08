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
        public HashSet<string> Usings { get; } = new HashSet<string>();

        public HashSet<InterfaceBuilder> Interfaces { get; } = new HashSet<InterfaceBuilder>();
        public HashSet<StructBuilder> Structs { get; } = new HashSet<StructBuilder>();
        public HashSet<ClassBuilder> Classes { get; } = new HashSet<ClassBuilder>();
        public HashSet<EnumBuilder> Enums { get; } = new HashSet<EnumBuilder>();
        public HashSet<DelegateBuilder> Delegates { get; } = new HashSet<DelegateBuilder>();
        public HashSet<EventBuilder> Events { get; } = new HashSet<EventBuilder>();
    }
}
