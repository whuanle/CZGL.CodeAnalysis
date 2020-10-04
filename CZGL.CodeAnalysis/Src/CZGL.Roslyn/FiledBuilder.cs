using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 字段构建器
    /// </summary>
    public sealed class FieldBuilder : FieldTemplate<FieldBuilder>
    {
        public FieldBuilder()
        {
            _TBuilder = this;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="Code">字段</param>
        /// <param name="attrs">特性注解列表</param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code).GetRoot().DescendantNodes();
            FieldDeclarationSyntax memberDeclaration = syntaxNodes
                .OfType<FieldDeclarationSyntax>()
                .FirstOrDefault();
            if (memberDeclaration == null)
                memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().Single());

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        public FieldDeclarationSyntax Build()
        {
            //StringBuilder stringBuilder = new StringBuilder();
            //bool isCanCreate = false;


            //if (!string.IsNullOrEmpty(MemberVisibility)) isCanCreate = true;
            //stringBuilder.Append(MemberVisibility);

            //stringBuilder.Append(" ");
            //stringBuilder.Append(MemberQualifier);

            //stringBuilder.Append(" ");
            //stringBuilder.Append(MemberType);

            //stringBuilder.Append(" ");
            //stringBuilder.Append(MemberName);

            //stringBuilder.Append((string.IsNullOrWhiteSpace(MemberInit) ? null : " =" + MemberInit));
            //stringBuilder.AppendLine(";");

            //FieldDeclarationSyntax memberDeclaration;
            //var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
            //    .GetRoot()
            //    .DescendantNodes();
            //if (isCanCreate)
            //    memberDeclaration = syntaxNodes
            //    .OfType<FieldDeclarationSyntax>()
            //    .Single();
            //else
            //    memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().Single());

            //// 添加特性
            //if (MemberAttrs.Count != 0)
            //{
            //    var tmp = AttributeBuilder.CreateAttributeList(MemberAttrs.ToArray());
            //    memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            //}


            //return memberDeclaration;
            return null;
        }


        public override string ToFormatCode()
        {
            throw new NotImplementedException();
        }

        public override string ToFullCode()
        {
            throw new NotImplementedException();
        }
    }
}
