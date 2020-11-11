using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Reflection;

namespace ConsoleTest
{
    class Program
    {
        delegate void A();
        static event A Test;

        public static int a { get; set; }
        
        static void Main(string[] args)
        {
            PropertyInfo info = typeof(Program).GetProperty("a");
           
            Console.WriteLine(info.Attributes.ToString());

            // 编译选项
            // 编译选项可以不配置
            DomainOptionBuilder option = new DomainOptionBuilder()
                .WithPlatform(Platform.AnyCpu)                     // 生成可移植程序集
                .WithDebug(false)                                  // 使用 Release 编译
                .WithKind(OutputKind.DynamicallyLinkedLibrary)     // 生成动态库
                .WithLanguageVersion(LanguageVersion.CSharp7_3);   // 使用 C# 7.3


            CompilationBuilder builder = CodeSyntax.CreateCompilation("Test.dll")
                .WithPath(Directory.GetParent(typeof(Program).Assembly.Location).FullName)
                .WithOption(option)                                // 可以省略
                .WithAutoAssembly()                                // 自动添加程序集引用
                .WithNamespace(NamespaceBuilder.FromCode(@"using System;
    namespace MySpace
    {      
        public class Test
        {
            public string MyMethod()
            {
                Console.WriteLine(""程序集运行成功"");
                return ""测试成功"";
        }
    }
}
"));

            try
            {
                if (builder.CreateDomain(out var messages))
                {
                    Console.WriteLine("编译成功！开始执行程序集进行验证！");
                    var assembly = Assembly.LoadFile(Directory.GetParent(typeof(Program).Assembly.Location).FullName + "/Test.dll");
                    var type = assembly.GetType("MySpace.Test");
                    var method = type.GetMethod("MyMethod");
                    object obj = Activator.CreateInstance(type);
                    string result = (string)method.Invoke(obj, null);

                    if (result.Equals("测试成功"))
                        Console.WriteLine("执行程序集测试成功！");
                    else
                        Console.WriteLine("执行程序集测试失败！");
                }
                else
                {
                    _ = messages.Execute(item =>
                    {
                        Console.WriteLine(@$"ID:{item.Id}
严重程度:{item.Severity}     
位置：{item.Location.SourceSpan.Start}~{item.Location.SourceSpan.End}
消息:{item.Descriptor.Title}   {item}");
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
            }



            //List<PortableExecutableReference> references = assemblies.Select(c => MetadataReference.CreateFromStream(c)).ToList();



            //var tmp = DependencyContext.Default.RuntimeLibraries
            //    .Execute(item =>
            //    {
            //        item.Dependencies.Execute(itemNode =>
            //        {
            //            var t = $"{itemNode.Name}.dll";
            //            Console.WriteLine(item.Path + "|" + item.HashPath + "|" + item.Path);
            //            var c = MetadataReference.CreateFromFile(t);
            //            references.Add(c);
            //        });

            //    }).ToArray();


            //PortableExecutableReference[] mscorlibs = references.ToArray();

            //PortableExecutableReference mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            //CSharpCompilation compilation = CSharpCompilation.Create("MyCompilation",
            //    syntaxTrees: new[] { tree }, references: mscorlibs);

            ////Emitting to file is available through an extension method in the Microsoft.CodeAnalysis namespace
            //EmitResult emitResult = compilation.Emit("output.dll", "output.pdb");

            ////If our compilation failed, we can discover exactly why.
            //if (!emitResult.Success)
            //{
            //    foreach (var diagnostic in emitResult.Diagnostics)
            //    {
            //        Console.WriteLine(diagnostic.ToString());
            //    }
            //}



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

            // CompilationBuilder compilation = new CompilationBuilder();
            //compilation.Test(build);
            Console.ReadKey();

        }

    }
}
