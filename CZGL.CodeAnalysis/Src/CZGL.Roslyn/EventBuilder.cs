﻿using CZGL.Roslyn.Templates;
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


        public EventFieldDeclarationSyntax BuildSyntax()
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();

            EventFieldDeclarationSyntax memberDeclaration;

                memberDeclaration = syntaxNodes
                .OfType<EventFieldDeclarationSyntax>()
                .Single();


            if (_member.Atributes.Count != 0)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(_member.Atributes.ToArray()));
            return memberDeclaration;
        }

        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        public override string ToFullCode()
        {
            const string Template = @"{Access} {Keyword} event {Delegate} {Name};";

            var code = Template
                .Replace("{Access}", _member.Access)
                .Replace("{Keyword}", _variable.Keyword)
                .Replace("{Delegate}", _variable.MemberType)
                .Replace("{Name}",_base.Name);

            return code;
        }
    }
}
