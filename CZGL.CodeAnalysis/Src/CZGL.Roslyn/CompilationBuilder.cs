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
    public class CompilationBuilder
    {
        private static string runtimePath = Path.Combine(Path.GetDirectoryName(typeof(RoslynHelper).Assembly.Location), "aaa.dll");
        public void Test(ClassDeclarationSyntax classDeclaration)
        {
            var syntaxTree = ParseToSyntaxTree(classDeclaration.NormalizeWhitespace().ToFullString());
            var compilation = BuildCompilation(syntaxTree);
            var result = compilation.Emit(runtimePath);
            foreach (var item in result.Diagnostics)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// 将代码转为语法树
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SyntaxTree ParseToSyntaxTree(string code)
        {
            var parseOptions = new CSharpParseOptions(LanguageVersion.Latest, preprocessorSymbols: new[] { "RELEASE" });
            // 有许多其他配置项，最简单这些就可以了
            return CSharpSyntaxTree.ParseText(code, parseOptions);
        }


        /// <summary>
        /// 编译代码
        /// </summary>
        /// <param name="syntaxTree"></param>
        /// <returns></returns>

        public CSharpCompilation BuildCompilation(SyntaxTree syntaxTree)
        {
            var compilationOptions = new CSharpCompilationOptions(
              concurrentBuild: true,
              metadataImportOptions: MetadataImportOptions.All,
              outputKind: OutputKind.DynamicallyLinkedLibrary,
              optimizationLevel: OptimizationLevel.Release,
              allowUnsafe: true,
              platform: Platform.AnyCpu,
              checkOverflow: false,
              assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default);
            // 有许多其他配置项，最简单这些就可以了

            var references = AppDomain.CurrentDomain.GetAssemblies()
              .Where(i => !i.IsDynamic && !string.IsNullOrWhiteSpace(i.Location))
              .Distinct()
              .Select(i => MetadataReference.CreateFromFile(i.Location));
            // 获取编译时所需用到的dll， 这里我们直接简单一点 copy 当前执行环境的
            return CSharpCompilation.Create("aaa.dll", new SyntaxTree[] { syntaxTree }, references, compilationOptions);
        }
    }
}
