using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Reflection;

namespace BlazorExample.Data
{
    public class CZGLRoslynService
    {
        private readonly string Code;
        public CZGLRoslynService(string code)
        {
            Code = code;
        }

        private List<string> Messages = new List<string>();
        public string[] Start()
        {
            // 编译选项
            // 编译选项可以不配置
            DomainOptionBuilder option = new DomainOptionBuilder()
                .WithPlatform(Platform.AnyCpu)                     // 生成可移植程序集
                .WithDebug(false)                                  // 使用 Release 编译
                .WithKind(OutputKind.DynamicallyLinkedLibrary)     // 生成动态库
                .WithLanguageVersion(LanguageVersion.CSharp7_3);   // 使用 C# 7.3

            string dllName = "N" + Guid.NewGuid().ToString() + ".dll";
            CompilationBuilder builder = CodeSyntax.CreateCompilation(dllName)
                .WithPath(Directory.GetParent(typeof(Program).Assembly.Location).FullName)
                .WithOption(option)                                // 可以省略
                .WithAutoAssembly()                                // 自动添加程序集引用
                .WithNamespace(NamespaceBuilder.FromCode(Code));


            try
            {
                if (builder.CreateDomain(out var messages))
                {
                    Messages.Add("编译成功！开始执行程序集进行验证！");
                    var assembly = Assembly.LoadFile(Directory.GetParent(typeof(Program).Assembly.Location).FullName + "/" + dllName);
                    var type = assembly.GetType("MySpace.Test");
                    var method = type.GetMethod("MyMethod");
                    object obj = Activator.CreateInstance(type);
                    string result = (string)method.Invoke(obj, null);

                    if (result.Equals("测试成功"))
                        Messages.Add("执行程序集测试成功！");
                    else
                        Messages.Add("执行程序集测试失败！");
                }
                else
                {
                    _ = messages.Execute(item =>
                    {
                        Messages.Add(@$"ID:{item.Id}
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
            return Messages.ToArray();
        }
    }
}