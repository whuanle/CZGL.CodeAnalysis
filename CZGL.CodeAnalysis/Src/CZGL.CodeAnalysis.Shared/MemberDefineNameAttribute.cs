using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MemberDefineNameAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
