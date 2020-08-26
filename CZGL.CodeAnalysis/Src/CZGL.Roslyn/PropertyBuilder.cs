using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    public sealed class PropertyBuilder : PropertyTemplate<PropertyBuilder>
    {
        public PropertyBuilder()
        {
            _TBuilder = this;
        }

        /// <summary>
        /// 通过字符串代码直接生成属性
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// public int A 
        /// { 
        ///   get{return B;}
        ///   set{B = value;}
        ///   }
        /// </code>
        /// </example>
        public static PropertyDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            PropertyDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes();
            memberDeclaration = syntaxNodes
            .OfType<PropertyDeclarationSyntax>()
            .Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        public PropertyDeclarationSyntax Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(MemberVisibility);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberQualifier);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberType);
            stringBuilder.Append(" " + MemberName);

            stringBuilder.AppendLine("{");

            stringBuilder.AppendLine(GetBlock);
            stringBuilder.AppendLine(SetBlock);

            stringBuilder.AppendLine("}");
            stringBuilder.Append(string.IsNullOrWhiteSpace(MemberInit) ? null : (" =" + MemberInit + ";"));

            PropertyDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
                .GetRoot()
                .DescendantNodes();
            memberDeclaration = syntaxNodes
            .OfType<PropertyDeclarationSyntax>()
            .Single();

            // 添加特性
            if (MemberAttrs.Count != 0)
            {
                var tmp = AttributeBuilder.CreateAttributeList(MemberAttrs.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
        }
    }
}
