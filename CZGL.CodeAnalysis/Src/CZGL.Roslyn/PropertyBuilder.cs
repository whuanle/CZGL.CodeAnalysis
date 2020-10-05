using CZGL.CodeAnalysis.Shared;
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
    /// 属性构建器
    /// </summary>
    public sealed class PropertyBuilder : PropertyTemplate<PropertyBuilder>
    {
        internal PropertyBuilder()
        {
            _TBuilder = this;
        }

        internal PropertyBuilder(string name) : this()
        {
            _base.Name = name;
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
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        public PropertyDeclarationSyntax BuildSyntax()
        {

            PropertyDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();
            memberDeclaration = syntaxNodes
            .OfType<PropertyDeclarationSyntax>()
            .Single();

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
            const string Template = @"{Attributes} {Access} {Keyword} {Type} {Name} 
{
{get}
{set}
} {InitCode}";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewLine())
                .Replace("{Access", _member.Access.CodeNewSpace())
                .Replace("{Keyword}", _variable.Keyword.CodeNewSpace())
                .Replace("{Type}", _variable.MemberType)
                .Replace("{Name}", _base.Name)
                .Replace("{get}",_property.GetBlock)
                .Replace("{set}", _property.SetBlock)
                .Replace("{InitCode}", _variable.InitCode.CodeNewBefore(" = "));

            return code;
        }
    }
}
