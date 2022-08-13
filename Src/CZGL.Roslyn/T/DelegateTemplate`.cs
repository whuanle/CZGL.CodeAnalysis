using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 委托构建器模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class DelegateTemplate<TBuilder> : MemberTemplate<TBuilder>
        where TBuilder : DelegateTemplate<TBuilder>
    {
        protected DelegateDeclarationSyntax _deleteSyntax = SyntaxFactory.DelegateDeclaration();
    }
}
