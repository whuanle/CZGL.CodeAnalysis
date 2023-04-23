using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
// using CZGL.CodeAnalysis.Shared;
// using CZGL.Roslyn;

namespace ConsoleApp
{
    public class A
    {
        public virtual void B() { }
    }
    public class C : A
    {
        public sealed override void B()
        {
            base.B();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var code =
                """
                /// <summary>
                /// aaa
                /// </summary>
                public int a = 0;
                """;
            //SyntaxFactory.VariableDeclaration();
            //SyntaxFactory.VariableDeclarator();
            var a = SyntaxFactory.ParseMemberDeclaration(code) as FieldDeclarationSyntax;
            var tokens = a.ChildTokens();
            foreach (var item in tokens)
            {
                Console.WriteLine(item.ToFullString());
            }
        }
    }
}
