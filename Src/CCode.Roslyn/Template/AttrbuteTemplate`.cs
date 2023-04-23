using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace CCode.Roslyn
{
    /// <summary>
    /// 成员特性构建器，生成成员的特性注解列表
    /// </summary>
    public abstract partial class AttrbuteTemplate : BaseTemplate<AttrbuteTemplate>
    {
        /// <summary>
        /// 语法树，<see cref="AttributeListSyntax"/>
        /// </summary>
        public SyntaxNode Syntax => GetNode();

        /// <summary>
        /// 将代码中的特性注解转换为语法树。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IEnumerable<AttributeSyntax> ToSyntax(string code)
        {
            SyntaxFactory.ParseAttributeArgumentList(code);
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
    }
}
