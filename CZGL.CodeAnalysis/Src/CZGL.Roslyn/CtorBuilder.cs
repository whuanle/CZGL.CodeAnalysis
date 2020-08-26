using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{

    public sealed class CtorBuilder : CtorTemplate<CtorBuilder>
    {
        public CtorBuilder()
        {
            _TBuilder = this;
        }

        public static ConstructorDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code).GetRoot().DescendantNodes();
            ConstructorDeclarationSyntax memberDeclaration = syntaxNodes
                .OfType<ConstructorDeclarationSyntax>()
                .FirstOrDefault();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        public ConstructorDeclarationSyntax Build()
        {
            StringBuilder stringBuilder = new StringBuilder($"class {MemberName}");
            stringBuilder.AppendLine("{");
            stringBuilder.Append(MemberVisibility);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberQualifier);


            stringBuilder.Append(" ");
            stringBuilder.Append(MemberName);

            stringBuilder.Append($"({FuncParams})");
            if (!string.IsNullOrEmpty(BaseCtor))
                stringBuilder.Append(":" + BaseCtor);
            else if (!string.IsNullOrEmpty(ThisCtor))
                stringBuilder.Append(":" + ThisCtor);
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(BlockCode);
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine("}");
            ConstructorDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
                .GetRoot()
                .DescendantNodes();

            memberDeclaration = syntaxNodes
           .OfType<ConstructorDeclarationSyntax>().Single();

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
