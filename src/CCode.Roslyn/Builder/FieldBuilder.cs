using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Roslyn.Builder
{
    public class FieldBuilder
    {
        public static FieldDeclarationSyntax? ToSyntax(string code)
        {
            return SyntaxFactory.ParseMemberDeclaration(code) as FieldDeclarationSyntax;
        }
    }

    public class PropertyBuilder
    {
        public static PropertyDeclarationSyntax? ToSyntax(string code)
        {
            return SyntaxFactory.ParseMemberDeclaration(code) as PropertyDeclarationSyntax;
        }
    }
}
