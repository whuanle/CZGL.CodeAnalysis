using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    public class AttributeState
    {
        public string Name { get; set; }
        public string Ctor { get; set; }
        public HashSet<string> Propertys { get; set; } = new HashSet<string>();
    }
}
