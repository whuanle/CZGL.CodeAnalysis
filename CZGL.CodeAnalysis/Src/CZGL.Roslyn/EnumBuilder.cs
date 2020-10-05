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
    public class EnumBuilder : EnumTemplate<EnumBuilder>
    {
        // 加上 [Falg]
        internal EnumBuilder()
        {
            _TBuilder = this;
        }

        internal EnumBuilder(string name) : this()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 生成语法树
        /// </summary>
        /// <returns></returns>
        public EnumDeclarationSyntax BuildSyntax()
        {
            var syntaxs = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();

            var memberDeclaration = syntaxs.OfType<EnumDeclarationSyntax>().Single();
            if (_member.Atributes.Count!=0)
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
            const string Template = @"
{Access} enum {Name}
{
{Fields}
}";

            var code = Template
                .Replace("{Access}",_member.Access)
                .Replace("{Name}",_base.Name)
                .Replace("{Fields}",_enum.Fields.Join(",\n"));

            return code;
        }
    }
}
