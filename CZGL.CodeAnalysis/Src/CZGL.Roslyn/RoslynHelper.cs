using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CZGL.Roslyn
{
    public static class RoslynHelper
    {
        // 后面创建缓存，不需要运行时动态反射获取枚举信息，应当在启动时就缓存起来

        private static string runtimePath = Path.Combine(typeof(RoslynHelper).Assembly.Location,"aaa.dll");
        public static void CreateDll(ClassDeclarationSyntax classDeclaration)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText( classDeclaration.NormalizeWhitespace().ToFullString());

            var compilation = CSharpCompilation.Create("Test.dll", new SyntaxTree[] { syntaxTree });
            try
            {
                var result = compilation.Emit(@"c:\temp\Test.dll");

                Console.WriteLine(result.Success ? "Sucess!!" : "Failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }



        public static SyntaxList<AttributeListSyntax> BuildAttributeListSyntax(params string[] attrs)
        {
            List<AttributeListSyntax> list = new List<AttributeListSyntax>();
            foreach (var item in attrs)
            {
                list.Add(SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(item)))));
            }
            return SyntaxFactory.List<AttributeListSyntax>(list.ToArray());
        }

    }
}
