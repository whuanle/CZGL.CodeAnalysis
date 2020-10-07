
using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        public delegate void T();
        public event T A;
        public delegate void A1();
        public event A1 a1;
        static void Main(string[] args)
        {
            AttributeBuilder builder = CodeSyntax.CreateAttribute("Key");

            var result = builder.ToFormatCode();

            Console.WriteLine(result);
            //var a = DependencyContext.Default.CompileLibraries;
            //var b = a.Count;

            //foreach (var item in DependencyContext.Default.CompileLibraries.SelectMany(cl=>cl.ResolveReferencePaths()))
            //{
            //    Console.WriteLine(item);
            //}

            //GenericBuilder generic = new GenericBuilder();
            //generic.AddConstarint(new GenericScheme("T1", GenericConstraintsType.Struct));
            //generic.AddConstarint(new GenericScheme("T2", GenericConstraintsType.Class));
            //generic.AddConstarint(new GenericScheme("T3", GenericConstraintsType.Notnull));
            //generic.AddConstarint(new GenericScheme("T4", GenericConstraintsType.Unmanaged));
            //generic.AddConstarint(new GenericScheme("T5", GenericConstraintsType.New));
            //// 如果能够反射拿到 Type
            //generic.AddConstarint(new GenericScheme("T6", GenericConstraintsType.BaseClass, typeof(int)));
            //// 如果要以字符串定义基类类型，请使用 此API
            //generic.AddBaseClassConstarint("T7", " IEnumerable<int>");
            //generic.AddTUConstarint("T8", "T2");
            //generic.AddConstarint(new GenericScheme("T9", GenericConstraintsType.Class, GenericConstraintsType.New));
            //var syntax = generic.Build();
            //var result = syntax.ToFullString();
            //Console.WriteLine(result);

            //ClassBuilder buidler = new ClassBuilder();
            //var build = buidler.SetVisibility(ClassVisibilityType.Public)
            //    .SetName("Test")
            //    .AddMethodMember(b =>
            //    {
            //        b.SetVisibility(MemberVisibilityType.Public)
            //        .SetRondomName()
            //        .SetBlock("System.Console.WriteLine(\"111\");");
            //    })
            //    .Build();

            //CompilationBuilder compilation = new CompilationBuilder();
            //compilation.Test(build);
            Console.ReadKey();

        }

    }
}
