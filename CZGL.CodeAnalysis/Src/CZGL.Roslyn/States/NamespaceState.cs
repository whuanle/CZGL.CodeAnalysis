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
        public HashSet<string> Usings { get; set; } = new HashSet<string>();

        public List<InterfaceBuilder> Interfaces { get; set; }
        public List<StructBuilder> Structs { get; set; }
        public List<ClassBuilder> Classes { get; set; }
        public List<EnumBuilder> Enums { get; set; }
        public List<DelegateBuilder> Delegates { get; set; }
        public List<EventBuilder> Events { get; set; }
    }
}
