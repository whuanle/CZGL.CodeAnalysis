using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    using Microsoft.CodeAnalysis.CSharp;
    using System;
    namespace MySpace
    {
        public class Test
        {
            public string MyMethod()
            {
                Console.WriteLine("程序集运行成功");
                return "测试成功";
            }
        }
    }

    public class CompilationTests
    {
        ITestOutputHelper _tempOutput;
        public CompilationTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void 编译程序集()
        {
            // 编译选项
            DomainOptionBuilder option = new DomainOptionBuilder()
                .WithPlatform(Platform.AnyCpu)                     // 生成可移植程序集
                .WithDebug(false)                                  // 使用 Release 编译
                .WithKind(OutputKind.DynamicallyLinkedLibrary)     // 生成动态库
                .WithLanguageVersion(LanguageVersion.CSharp7_3);   // 使用 C# 7.3


            CompilationBuilder builder = CodeSyntax.CreateCompilation("Test.dll")
                .WithPath(Directory.GetParent(typeof(CompilationTests).Assembly.Location).FullName)
                .WithOption(option)
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
                    var assembly = Assembly.LoadFile(Directory.GetParent(typeof(CompilationTests).Assembly.Location).FullName + "/Test.dll");
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

        }
    }
}
