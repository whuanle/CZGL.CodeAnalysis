using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 字段构建器
    /// </summary>
    public sealed class FieldBuilder : FieldTemplate<FieldBuilder>
    {
        internal FieldBuilder()
        {
            _TBuilder = this;
        }

        internal FieldBuilder(string name) : this()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="Code">字段</param>
        /// <param name="attrs">特性注解列表</param>
        /// <returns></returns>
        public static FieldDeclarationSyntax BuildSyntax(string Code, string[] attrs = null)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code).GetRoot().DescendantNodes();
            FieldDeclarationSyntax memberDeclaration = syntaxNodes
                .OfType<FieldDeclarationSyntax>()
                .FirstOrDefault();
            if (memberDeclaration == null)
                memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().Single());

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        public FieldDeclarationSyntax BuildSyntax()
        {
            bool isCanCreate = false;


            if (!string.IsNullOrEmpty(_member.Access)) isCanCreate = true;

            FieldDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();
            if (isCanCreate)
                memberDeclaration = syntaxNodes
                .OfType<FieldDeclarationSyntax>()
                .Single();
            else
                memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().Single());

            // 添加特性
            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }


            return memberDeclaration;
        }


        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        public override string ToFullCode()
        {
            const string Template = @"{Attributes} {Access} {Keyword} {Type} {Name} {InitCode};";
            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewLine())
                .Replace("{Access", _member.Access.CodeNewSpace())
                .Replace("{Keyword}", _variable.Keyword.CodeNewSpace())
                .Replace("{Type}", _variable.MemberType)
                .Replace("{Name}", _base.Name)
                .Replace("{InitCode}", _variable.InitCode.CodeNewBefore(" = "));

            return code;
        }
    }
}
