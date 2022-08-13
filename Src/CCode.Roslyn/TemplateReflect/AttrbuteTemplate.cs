using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CCode.Reflect;

namespace CCode.Roslyn
{
    public abstract partial class AttrbuteTemplate
    {
        public AttrbuteTemplate WithAdd(CustomAttributeData attributeData)
        {
            WithAdd(AttributeAnalysis.GetDefine(attributeData).View());
        }
    }
}
