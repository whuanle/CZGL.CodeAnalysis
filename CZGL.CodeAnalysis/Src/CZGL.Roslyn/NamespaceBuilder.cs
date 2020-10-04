using CZGL.Roslyn.States;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 构建命名空间
    /// </summary>
    public class NamespaceBuilder : NamespaceTemplate<NamespaceBuilder>
    {
        public NamespaceBuilder()
        {
            _TBuilder = this;
        }

        /// <summary>
        /// 创建命名空间
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        public NamespaceBuilder(string namespaceName):base(namespaceName)
        {
            _TBuilder = this;
        }

        internal override NamespaceDeclarationSyntax Build()
        {
            NamespaceDeclarationSyntax memberDeclaration;

            memberDeclaration = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                .Single();

            return memberDeclaration;
        }

    }
}
