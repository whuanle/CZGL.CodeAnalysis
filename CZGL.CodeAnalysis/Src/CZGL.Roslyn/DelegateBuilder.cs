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
    /// 构建委托
    /// </summary>
    public sealed class DelegateBuilder : FuncTemplate<DelegateBuilder>
    {
        public DelegateBuilder()
        {
            _TBuilder = this;
        }

        public delegate void A();



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
        public static DelegateDeclarationSyntax Build(string Code, string[] attrs = null)
        {

            var memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<DelegateDeclarationSyntax>().Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        /// <summary>
        /// 构建方法
        /// </summary>
        /// <returns></returns>
        public DelegateDeclarationSyntax Build()
        {
            // StringBuilder stringBuilder = new StringBuilder();

            // stringBuilder.Append(MemberVisibility);

            // stringBuilder.Append(" ");
            // stringBuilder.Append("delegate");

            // stringBuilder.Append(" ");
            // stringBuilder.Append(FuncReturnType);

            // stringBuilder.Append(" ");
            // stringBuilder.Append(MemberName);

            // stringBuilder.AppendLine($"({FuncParams})");

            // stringBuilder.AppendLine(";");
            // DelegateDeclarationSyntax memberDeclaration = default;
            // var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
            //     .GetRoot()
            //     .DescendantNodes();

            // memberDeclaration = syntaxNodes
            //.OfType<DelegateDeclarationSyntax>().Single();

            // // 添加特性
            // if (MemberAttrs.Count != 0)
            // {
            //     var tmp = AttributeBuilder.CreateAttributeList(MemberAttrs.ToArray());
            //     memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            // }

            // return memberDeclaration;

            return null;
        }



        public override string ToFullCode()
        {
            throw new NotImplementedException();
        }

        public override string ToFormatCode()
        {
            throw new NotImplementedException();
        }
    }
}
