﻿using CZGL.CodeAnalysis.Shared;
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
    public sealed class DelegateBuilder : DelegateTemplate<DelegateBuilder>
    {
        internal DelegateBuilder()
        {
            _TBuilder = this;
        }

        internal DelegateBuilder(string name):this()
        {
            _base.Name = name;
        }



        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static DelegateBuilder FromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            return new DelegateBuilder().WithFromCode(code);
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
                .OfType<DelegateDeclarationSyntax>().FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("请检查代码语法是否有错误！");

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        /// <summary>
        /// 构建委托
        /// </summary>
        /// <returns></returns>
        public DelegateDeclarationSyntax BuildSyntax()
        {
            DelegateDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();

            memberDeclaration = syntaxNodes
           .OfType<DelegateDeclarationSyntax>().FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("请检查代码语法是否有错误！");

            // 添加特性
            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
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

            const string Template = @"{Attributes}{Access}delegate {ReturnType} {Name}{GenericParams}({Params}){GenericList};";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewLine())
                .Replace("{Access}", _member.Access.CodeNewSpace())
                .Replace("{ReturnType}", _func.ReturnType)
                .Replace("{Name}", _base.Name)
                .Replace("{GenericParams}", _member.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{Params}", _func.Params.Join(","))
                .Replace("{GenericList}", _member.GenericParams.GetWhereCode(true).CodeNewBefore("\n"));

            return code;
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
    }
}
