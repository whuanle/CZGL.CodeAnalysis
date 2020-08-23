using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    public class MethodDefineResult: MethodDefine
    {
        public MethodInfo MethodInfo { get; set; }
        public ParameterInfo ReturnParam { get; set; }
    }
}
