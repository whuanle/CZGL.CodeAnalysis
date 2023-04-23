using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CCode.Roslyn
{
	/// <summary>
	/// 委托构建器模板
	/// </summary>
	public abstract class DelegateTemplate : MethodBaseTemplate<DelegateTemplate>
	{
        //protected DelegateDeclarationSyntax _deleteSyntax = SyntaxFactory.DelegateDeclaration();
    }
}
