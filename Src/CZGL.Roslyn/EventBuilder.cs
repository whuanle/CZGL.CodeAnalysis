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
    /// 定义事件
    /// </summary>
    public sealed class EventBuilder : EventTemplate<EventBuilder>
    {
        public EventBuilder()
        {
            _TBuilder = this;
        }


        /// <summary>
        /// 字符串直接生成事件
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            var syntaxs = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes();

            var memberDeclaration = SyntaxFactory.EventFieldDeclaration(syntaxs.OfType<VariableDeclarationSyntax>().Single());
            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));
            return memberDeclaration;
        }


        public EventFieldDeclarationSyntax Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Visibility);

            stringBuilder.Append(" ");
            stringBuilder.Append("event");
            
            stringBuilder.Append(" ");
            stringBuilder.Append(MemberType);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberName);

            stringBuilder.Append((string.IsNullOrWhiteSpace(MemberInit) ? null : " =" + MemberInit));
            stringBuilder.AppendLine(";");


            var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
                .GetRoot()
                .DescendantNodes();

            EventFieldDeclarationSyntax memberDeclaration;

                memberDeclaration = syntaxNodes
                .OfType<EventFieldDeclarationSyntax>()
                .Single();


            if (MemberAttrs.Count != 0)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(MemberAttrs.ToArray()));
            return memberDeclaration;
        }
    }
}
