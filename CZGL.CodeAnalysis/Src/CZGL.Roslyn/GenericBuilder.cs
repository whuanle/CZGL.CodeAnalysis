using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 泛型构建器
    /// </summary>
    public sealed class GenericBuilder : GenericTemplate<GenericBuilder>
    {
        public GenericBuilder()
        {
            _TBuilder = this;
        }

        ///// <summary>
        ///// 生成泛型参数
        ///// </summary>
        ///// <returns></returns>
        //internal SyntaxNodeOrToken[] BuildTypeParameterListSyntax()
        //{
        //    List<SyntaxNodeOrToken> tokenss = new List<SyntaxNodeOrToken>();
        //    var syntaxs = tokens.Values.Select(x => x.Parameter).ToArray();

        //    for (int i = 0; i < syntaxs.Length; i++)
        //    {
        //        tokenss.Add(syntaxs[i]);
        //        if (i < syntaxs.Length - 1)
        //            tokenss.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
        //    }
        //    return tokenss.ToArray();
        //}

        //protected internal TypeParameterConstraintClauseSyntax BuildOne(GenericConstarintInfo info)
        //{
        //    var strCode = BuildString(info);

        //    TypeParameterConstraintClauseSyntax memberDeclaration;
        //    var syntaxNodes = CSharpSyntaxTree.ParseText(strCode.ToString())
        //        .GetRoot()
        //        .DescendantNodes();
        //    Console.WriteLine(syntaxNodes.FirstOrDefault().NormalizeWhitespace().ToFullString());
        //    memberDeclaration = syntaxNodes
        //    .OfType<TypeParameterConstraintClauseSyntax>()
        //    .Single();
        //    //else
        //    //    memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().Single());
        //    return memberDeclaration;
        //}
        private string BuildClassString()
        {
            StringBuilder stringBuilder = new StringBuilder(60);
            stringBuilder.Append("public class GenericClass<");
            stringBuilder.Append(string.Join(",", Constarints.Select(x => x.Key).ToArray()));
            stringBuilder.Append(">");

            foreach (var item in Constarints.Values)
            {
                stringBuilder.Append("where ");
                stringBuilder.Append(item.Name);
                stringBuilder.Append(":");
                stringBuilder.AppendLine(string.Join(",", item.Constraints.ToArray()));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 为类生成泛型
        /// </summary>
        /// <returns></returns>
        public SyntaxList<TypeParameterConstraintClauseSyntax> Build()
        {
            var Code = BuildClassString();
            TypeParameterConstraintClauseSyntax[] memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes();
            memberDeclaration = syntaxNodes
            .OfType<TypeParameterConstraintClauseSyntax>()
            .ToArray();

            return SyntaxFactory.List(memberDeclaration);


            // return _classDeclaration.WithTypeParameterList(SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList<TypeParameterSyntax>(BuildTypeParameterListSyntax()))).WithConstraintClauses(SyntaxFactory.List(BuildTypeParameterConstraintClauseSyntax()));
        }


        /// <summary>
        /// 获得格式化代码
        /// </summary>
        /// <returns></returns>
        public override string FullCode()
        {
            return Build().ToFullString();
        }
    }
}
