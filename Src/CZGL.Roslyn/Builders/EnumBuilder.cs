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
    /// 枚举
    /// </summary>
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
