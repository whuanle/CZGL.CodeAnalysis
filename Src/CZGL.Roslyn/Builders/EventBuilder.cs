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
    /// 事件构建器
    /// </summary>
    public sealed class EventBuilder : EventTemplate<EventBuilder>
    {
        internal EventBuilder()
        {
            _TBuilder = this;
        }

        internal EventBuilder(string name):this()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static EventBuilder FromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            return new EventBuilder().WithFromCode(code);
        }

        /// <summary>
        /// 字符串直接生成事件
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax BuildSyntax(string Code, string[] attrs = null)
        {
            var syntaxs = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes();

            var memberDeclaration = SyntaxFactory.EventFieldDeclaration(syntaxs.OfType<VariableDeclarationSyntax>().Single());
            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));
            return memberDeclaration;
        }

        /// <summary>
        /// 语法树
        /// </summary>
        /// <returns></returns>
        public EventFieldDeclarationSyntax BuildSyntax()
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();

            EventFieldDeclarationSyntax memberDeclaration;

                memberDeclaration = syntaxNodes
                .OfType<EventFieldDeclarationSyntax>()
                .FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("无法构建事件，请检查代码语法是否有错误！");


            if (_member.Atributes.Count != 0)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(_member.Atributes.ToArray()));
            return memberDeclaration;
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

            const string Template = @"{Access}{Keyword} event {Delegate} {Name}{InitCode};";

            var code = Template
                .Replace("{Access}", _member.Access.CodeNewAfter())
                .Replace("{Keyword}", _variable.Keyword.CodeNewAfter())
                .Replace("{Delegate}", _variable.MemberType)
                .Replace("{Name}",_base.Name)
                .Replace("{InitCode}", _variable.InitCode.CodeNewBefore(" = "));

            return code;
        }
    }
}
