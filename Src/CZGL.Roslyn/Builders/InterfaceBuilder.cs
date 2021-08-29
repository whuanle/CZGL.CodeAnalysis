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
    /// 接口构建器
    /// </summary>
    public class InterfaceBuilder : InterfaceTemplate<InterfaceBuilder>
    {
        internal InterfaceBuilder()
        {
            _TBuilder = this;
        }

        internal InterfaceBuilder(string name) : this()
        {
            base.WithName(name);
        }

        /// <summary>
        /// 通过字符串代码生成类
        /// </summary>
        /// <param name="Code">字符串代码</param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax BuildSyntax(string Code)
        {
            InterfaceDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                .FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建接口，请检查代码语法是否有错误！");

            return memberDeclaration;
        }

        /// <summary>
        /// 生成接口语法树
        /// </summary>
        /// <returns></returns>
        public InterfaceDeclarationSyntax BuildSyntax()
        {
            InterfaceDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(ToFullCode())
               .GetRoot()
               .DescendantNodes()
               .OfType<InterfaceDeclarationSyntax>()
               .FirstOrDefault();

            //if (_member.Atributes.Count != 0)
            //    memberDeclaration = memberDeclaration
            //        .WithAttributeLists(CodeSyntax.CreateAttributeList(_member.Atributes.ToArray()));

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建接口，请检查代码语法是否有错误！");

            return memberDeclaration;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static InterfaceBuilder FromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            return new InterfaceBuilder().WithFromCode(code);
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

            const string Template = @"{Attributes}{Access} interface {Name}{GenericParams} {:}{Interfaces}{GenericList}
{
{Properties}
{Methods}
}";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewAfter("\n"))
                .Replace("{Access}", _member.Access)
                .Replace("{Name}", _base.Name)
                .Replace("{GenericParams}", _member.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{:}", _objectState.Interfaces.Any() ? ":" : "")
                .Replace("{Interfaces}", _objectState.Interfaces.Join(","))
                .Replace("{GenericList}", _member.GenericParams.GetWhereCode(true).CodeNewBefore("\n"))
                .Replace("{Properties}", _objectState.Propertys.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Methods}", _objectState.Methods.Select(item => item.ToFullCode()).Join("\n"));

            return code;
        }
    }
}
