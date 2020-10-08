using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CZGL.Roslyn.States
{
    public class CompilationState
    {
        public string AssemblyName { get; set; }
        public string Path { get; set; }
        public HashSet<NamespaceBuilder> Namespaces { get; } = new HashSet<NamespaceBuilder>();

        public bool UseAutoAssembly { get; set; } = false;
        public HashSet<Assembly> Assemblies { get; } = new HashSet<Assembly>();
    }
}
