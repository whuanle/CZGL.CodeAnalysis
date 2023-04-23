using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;


namespace CCode.Roslyn.Template
{
    public abstract class NamespaceTemplate<TBuilder> : BaseTemplate<TBuilder>
        where TBuilder : ClassTemplate<TBuilder>
    {

        protected readonly List<ClassDeclarationSyntax> _class = new List<ClassDeclarationSyntax>();
    }
}
