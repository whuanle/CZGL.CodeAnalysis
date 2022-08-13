using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCode.Roslyn
{
    /// <summary>
    /// 成员特性构建器，生成成员的特性注解列表
    /// </summary>
    public abstract partial class AttrbuteTemplate : BaseTemplate<AttrbuteTemplate>
    {
        /// <summary>
        /// 特性语法树列表
        /// </summary>
        protected readonly List<AttributeSyntax> _attributes = new List<AttributeSyntax>();

        /// <summary>
        /// 语法树，<see cref="AttributeListSyntax"/>
        /// </summary>
        public SyntaxNode Syntax => GetNode();

        /// <summary>
        /// 将代码中的特性注解取出，放置到当前语法树中。
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns></returns>
        public AttrbuteTemplate WithAdd(string code)
        {
            var list = ToSyntax(code);
            _attributes.AddRange(list);
            return this;
        }

        /// <summary>
        /// 将代码中的特性注解转换为语法树。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected static IEnumerable<AttributeSyntax> ToSyntax(string code)
        {
            var list = CSharpSyntaxTree.ParseText(code)
                .GetRoot()
                .DescendantNodes()
                .OfType<AttributeSyntax>();
            return list;
        }

        /// <inheritdoc/>
        public override SyntaxNode GetNode()
        {
            return SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(_attributes));
        }

        /// <inheritdoc/>
        public override string ToFormatCode(CodeContext? codeContext)
        {
            if (codeContext == null) return GetNode().ToFullString();
            return GetNode().NormalizeWhitespace(
                indentation: codeContext.Indentation,
                eol: codeContext.Eol,
                elasticTrivia: codeContext.ElasticTrivia).ToFullString();
        }
    }
}
