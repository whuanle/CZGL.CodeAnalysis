using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    public sealed class MethodBuilder : MethodTemplate<MethodBuilder>
    {
        internal MethodBuilder()
        {
            _TBuilder = this;
        }

        internal MethodBuilder(string name):this()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 通过字符串代码直接生成方法
        /// <example>
        /// <code>
        /// public int A 
        /// { 
        ///   get{return B;}
        ///   set{B = value;}
        ///   }
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax BuildSyntax(string Code, string[] attrs = null)
        {
            MethodDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes();
            memberDeclaration = syntaxNodes
            .OfType<MethodDeclarationSyntax>()
            .Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        /// <summary>
        /// 构建方法
        /// </summary>
        /// <returns></returns>
        public MethodDeclarationSyntax BuildSyntax()
        {
            var code = $@"class Test66666666
{{
                {ToFullCode()}
}}";
            MethodDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(code)
                .GetRoot()
                .DescendantNodes();

            memberDeclaration = syntaxNodes
           .OfType<MethodDeclarationSyntax>().Single();

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
            const string Template = @"{Attributes} {Access} {Keyword} {ReturnType} {Name}{GenericParams}({Params}) {BaseOrThis} {GenericList} {BlockCode}";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewLine())
                .Replace("{Access", _member.Access.CodeNewSpace())
                .Replace("{Keyword}", _method.Keyword.CodeNewSpace())
                .Replace("{ReturnType}", _func.ReturnType)
                .Replace("{Name}", _base.Name)
                .Replace("{GenericParams}",_func.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{Params}", _func.Params.Join(","))
                .Replace("{BaseOrThis}",_method.BaseOrThis.CodeNewBefore(":"))
                .Replace("{GenericList}", _func.GenericParams.GetWhereCode())
                .Replace("{BlockCode}", _method.BlockCode.CodeNewBefore("\n{").CodeNewAfter("\n}")??";");

            return code;
        }
    }
}
