using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using CZGL.Roslyn.Models;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;

namespace CZGL.CodeAnalysis.Roslyn
{
    public sealed class ClassBuilder: ClassTemplate<ClassBuilder>
    {
        public ClassBuilder()
        {
            _TBuilder = this;
        }


        public static ClassDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            ClassDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        public ClassDeclarationSyntax Build()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ClassDeclarationSyntax memberDeclaration;

            stringBuilder.Append(Visibility);

            stringBuilder.Append(" ");
            stringBuilder.Append(Qualifier);

            stringBuilder.Append(" ");
            stringBuilder.Append("class");

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberName);

            bool isBase = false;
            bool isInterfaces = false;

            if (!string.IsNullOrEmpty(BaseTypeName))
                isBase = true;
            if (BaseInterfaces.Count != 0)
                isInterfaces = true;
            if (isBase || isInterfaces)
                stringBuilder.Append(":");
            if (isBase)
            {
                stringBuilder.Append(BaseTypeName);
                if(isInterfaces)
                {
                    stringBuilder.Append(",");
                    stringBuilder.Append(string.Join(",", BaseInterfaces));
                }    
            }

            else if (isInterfaces)
                stringBuilder.Append(string.Join(",", BaseInterfaces));

            stringBuilder.Append(Constraint);

            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("}");


             memberDeclaration = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Single();

            if (MemberAttrs.Count != 0)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(MemberAttrs.ToArray()));

            return memberDeclaration;

        }

    }
}
