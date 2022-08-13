using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 方法构建器
    /// </summary>
    public sealed class MethodBuilder : MethodBaseTemplate<MethodBuilder>
    {
        internal MethodBuilder()
        {
            _TBuilder = this;
        }

        internal MethodBuilder(string name) : this()
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
           .OfType<MethodDeclarationSyntax>().FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建方法，请检查代码是否有语法错误！");

            // 添加特性
            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
        }

        /// <summary>
        /// 此方法代码体为空
        /// <para>如果方法体中没有任何代码，请使用此函数指定</para>
        /// </summary>
        /// <returns></returns>
        public MethodBuilder WithDefaultBlock()
        {
            _method.BlockCode = "\n";
            return this;
        }

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        /// <summary>
        /// 完整输出代码
        /// <para>不会对代码进行检查，直接输出当前已经定义的代码</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public override string ToFullCode()
        {
            if (_base.UseCode)
                return _base.Code;

            const string Template = @"{Attributes}{Access}{Keyword}{ReturnType} {Name}{GenericParams}({Params}){GenericList} {BlockCode}";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewLine())
                .Replace("{Access}", _member.Access.CodeNewSpace())
                .Replace("{Keyword}", _method.Keyword.CodeNewSpace())
                .Replace("{ReturnType}", _func.ReturnType)
                .Replace("{Name}", _base.Name)
                .Replace("{GenericParams}", _member.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{Params}", _func.Params.Join(","))
                .Replace("{GenericList}", _member.GenericParams.GetWhereCode(true).CodeNewBefore("\n"))
                .Replace("{BlockCode}", _method.BlockCode.CodeNewBefore("\n{").CodeNewAfter("\n}") ?? @";");

            return code;
        }
    }
}
