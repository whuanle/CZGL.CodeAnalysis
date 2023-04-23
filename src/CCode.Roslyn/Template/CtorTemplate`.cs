using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CCode.Roslyn.Template
{
    /// <summary>
    /// 构造函数构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class CtorTemplate<TBuilder> : BaseTemplate where TBuilder : CtorTemplate<TBuilder>
    {
        /// <inheritdoc/>
        public override SyntaxNode GetNode()
        {
            AttributeListSyntax attrList = SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(_attributes));
            return SyntaxFactory.ConstructorDeclaration(
                attributeLists: SyntaxFactory.List<AttributeListSyntax>(new[] { attrList }),
                modifiers: SyntaxFactory.TokenList(base._accessToken, base.),
                identifier: base._name.Identifier,
                parameterList: base._parameters,
                initializer: null,
                body: null
                );
        }

        /// <inheritdoc/>
        public virtual string ToFormatCode(CodeContext? codeContext)
        {
            if (codeContext == null) return GetNode().ToFullString();
            return GetNode().NormalizeWhitespace(
                indentation: codeContext.Indentation,
                eol: codeContext.Eol,
                elasticTrivia: codeContext.ElasticTrivia).ToFullString();
        }
    }
}
