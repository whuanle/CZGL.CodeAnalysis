using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 构建委托
    /// </summary>
    public sealed class DelegateBuilder : FuncTemplate<DelegateBuilder>
    {
        internal DelegateBuilder()
        {
            _TBuilder = this;
        }

        internal DelegateBuilder(string name):base()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="visibilityType">标记</param>
        /// <returns></returns>
        public DelegateBuilder WithAccess(NamespaceAccess access = NamespaceAccess.Internal)
        {
            _member.Access = RoslynHelper.GetName(access);
            return _TBuilder;
        }

        /// <summary>
        /// 通过字符串直接生成委托
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// var tmp = "public delegate void Test(string a,string b)";
        /// Build(tmp);
        /// </code>
        /// </example>
        public static DelegateDeclarationSyntax BuildSyntax(string Code, string[] attrs = null)
        {

            var memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<DelegateDeclarationSyntax>().Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        /// <summary>
        /// 构建方法
        /// </summary>
        /// <returns></returns>
        public DelegateDeclarationSyntax BuildSyntax()
        {
            DelegateDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();

            memberDeclaration = syntaxNodes
           .OfType<DelegateDeclarationSyntax>().Single();

            // 添加特性
            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
        }



        public override string ToFullCode()
        {
            const string Template = @"{Attributes} {Access} {ReturnType} {Name}{GenericParams}({Params}) {GenericList};";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewLine())
                .Replace("{Access", _member.Access.CodeNewSpace())
                .Replace("{ReturnType}", _func.ReturnType)
                .Replace("{Name}", _base.Name)
                .Replace("{GenericParams}", _func.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{Params}", _func.Params.Join(","))
                .Replace("{GenericList}", _func.GenericParams.GetWhereCode());

            return code;
        }

        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }
    }
}
