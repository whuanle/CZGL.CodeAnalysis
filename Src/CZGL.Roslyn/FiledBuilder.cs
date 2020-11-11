using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 字段构建器
    /// </summary>
    public sealed class FieldBuilder : FieldTemplate<FieldBuilder>
    {
        internal FieldBuilder()
        {
            _TBuilder = this;
        }

        internal FieldBuilder(string name) : this()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="Code">字段</param>
        /// <param name="attrs">特性注解列表</param>
        /// <returns></returns>
        public static FieldDeclarationSyntax BuildSyntax(string Code, string[] attrs = null)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code).GetRoot().DescendantNodes();
            FieldDeclarationSyntax memberDeclaration = syntaxNodes
                .OfType<FieldDeclarationSyntax>()
                .FirstOrDefault();
            if (memberDeclaration == null)
                memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().FirstOrDefault());

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建字段，请检查代码是否有语法错误！");


            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        /// <summary>
        /// 获取字段语法树
        /// <para>如果使用字符串代码一次性构建，请使用 <see cref="BuildCodeSyntax()"/> 方法</para>
        /// </summary>
        /// <returns></returns>
        public FieldDeclarationSyntax BuildSyntax()
        {
            if (_base.UseCode)
                return SyntaxFactory.FieldDeclaration(BuildCodeSyntax().Declaration);


            bool isCanCreate = string.IsNullOrEmpty(_member.Access)?false:true;

            FieldDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullAttriCode())
                .GetRoot()
                .DescendantNodes();

            if (isCanCreate)
                memberDeclaration = syntaxNodes
                .OfType<FieldDeclarationSyntax>()
                .FirstOrDefault();
            else
                memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().FirstOrDefault());


            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建字段，请检查代码是否有语法错误！");


            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }


            return memberDeclaration;
        }

        /// <summary>
        /// 获取变量语法树
        /// </summary>
        /// <returns></returns>
        public LocalDeclarationStatementSyntax BuildCodeSyntax()
        {
            var code = ToFullCode();
            var syntaxNodes = CSharpSyntaxTree.ParseText(code)
                .GetRoot()
                .DescendantNodes();

            var node = syntaxNodes.OfType<LocalDeclarationStatementSyntax>().FirstOrDefault();
            return node;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static FieldBuilder FromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            return new FieldBuilder().WithFromCode(code);
        }




        public override string ToFormatCode()
        {
            if (_base.UseCode)
                return BuildCodeSyntax().NormalizeWhitespace().ToFullString();

            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        public override string ToFullCode()
        {
            if (_base.UseCode)
                return _base.Code;

            return ToFullAttriCode(true);
        }

        private string ToFullAttriCode(bool isUseAttribute=false)
        {
            if (_base.UseCode)
                return _base.Code;

            const string Template = @"{Attributes}{Access}{Keyword}{Type} {Name}{InitCode};";
            var code = Template
                .Replace("{Attributes}", isUseAttribute?_member.Atributes.Join("\n").CodeNewLine():"")
                .Replace("{Access}", _member.Access.CodeNewSpace())
                .Replace("{Keyword}", _variable.Keyword.CodeNewSpace())
                .Replace("{Type}", _variable.MemberType)
                .Replace("{Name}", _base.Name)
                .Replace("{InitCode}", _variable.InitCode.CodeNewBefore(" = "));

            return code;
        }
    }
}
