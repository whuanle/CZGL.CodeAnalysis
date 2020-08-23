using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    public sealed class FieldBuilder : FieldTemplate<FieldBuilder>
    {
        public FieldBuilder()
        {
            _TBuilder = this;
        }


        /// <summary>
        /// 设置修饰符，是否为常量，是否为静态成员，是否只读
        /// <para>MemberQualifierType.Abstract 对字段无效</para>
        /// </summary>
        /// <param name="qualifierType"></param>
        /// <returns></returns>
        public override FieldBuilder SetQualifier(MemberQualifierType qualifierType = MemberQualifierType.Default)
        {
            Qualifier = RoslynHelper.GetName(qualifierType);
            return this;
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
            StringBuilder stringBuilder = new StringBuilder();
            bool isCanCreate = false;


            if (!string.IsNullOrEmpty(Visibility)) isCanCreate = true;
            stringBuilder.Append(Visibility);

            stringBuilder.Append(" ");
            stringBuilder.Append(Qualifier);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberType);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberName);

            stringBuilder.Append((string.IsNullOrWhiteSpace(MemberInit) ? null : " =" + MemberInit));
            stringBuilder.AppendLine(";");

            FieldDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
                .GetRoot()
                .DescendantNodes();
            if (isCanCreate)
                memberDeclaration = syntaxNodes
                .OfType<FieldDeclarationSyntax>()
                .Single();
            else
                memberDeclaration = SyntaxFactory.FieldDeclaration(syntaxNodes.OfType<VariableDeclarationSyntax>().Single());

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
