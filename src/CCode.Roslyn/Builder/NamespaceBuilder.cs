using System;
using System.Collections.Generic;
using System.Text;
using CCode.Roslyn.Template;

namespace CCode.Roslyn.Builder
{
    public class NamespaceBuilder : NamespaceTemplate<NamespaceBuilder>
    {
        internal NamespaceBuilder(AssamblyBuilder assamblyBuilder) : base(assamblyBuilder) { }
    }
}
