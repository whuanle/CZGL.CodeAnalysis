using CZGL.Roslyn.States;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 程序集编译构建器
    /// </summary>
    public class CompilationBuilder
    {
        private readonly CompilationState _state = new CompilationState();
        private DomainOptionBuilder _option;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option">编译配置</param>
        /// <returns></returns>
        public CompilationBuilder WithOption(DomainOptionBuilder option)
        {
            _option = option;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder">编译配置器</param>
        /// <returns></returns>
        public CompilationBuilder WithOption(Action<DomainOptionBuilder> builder)
        {
            DomainOptionBuilder option = new DomainOptionBuilder();
            builder.Invoke(option);
            _option = option;
            return this;
        }


        /// <summary>
        /// 程序集名称
        /// </summary>
        /// <param name="assemblyName">程序集名称，记得带 .dll </param>
        internal CompilationBuilder(string assemblyName)
        {
            _state.AssemblyName = assemblyName;
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        /// <param name="path">生成位置</param>
        /// <param name="assemblyName">程序集名称，记得带 .dll </param>
        internal CompilationBuilder(string path,string assemblyName)
        {
            _state.Path = path;
            _state.AssemblyName = assemblyName;
        }

        /// <summary>
        /// 程序集生成路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public CompilationBuilder WithPath(string path)
        {
            _state.Path = path;
            return this;
        }

        /// <summary>
        /// 添加要编译的命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public CompilationBuilder WithNamespace(NamespaceBuilder builder)
        {
            _state.Namespaces.Add(builder);
            return this;
        }

        /// <summary>
        /// 添加要编译的命名空间
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public CompilationBuilder WithNamespace(string namespaceName, Action<NamespaceBuilder> builder)
        {
            if (string.IsNullOrEmpty(namespaceName))
                throw new ArgumentNullException(nameof(namespaceName));

            NamespaceBuilder space = new NamespaceBuilder(namespaceName);
            builder.Invoke(space);
            _state.Namespaces.Add(space);
            return this;
        }

        /// <summary>
        /// 添加程序集引用
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public CompilationBuilder WithAssembly(Assembly assembly)
        {
            _state.Assemblies.Add(assembly);
            return this;
        }

        /// <summary>
        /// 自动识别此类型所在程序集所依赖的所有程序集并引用
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public CompilationBuilder WithAssembly(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            _state.Assemblies.Add(type.Assembly);
            return this;
        }

        /// <summary>
        /// 自动添加程序集引用
        /// </summary>
        /// <returns></returns>
        public CompilationBuilder WithAutoAssembly()
        {
            _state.UseAutoAssembly = true;
            return this;
        }

        /// <summary>
        /// 创建程序集
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <param name="messages">代码编译时的分析结果</param>
        public bool CreateDomain(out ImmutableArray<Diagnostic> messages)
        {
            HashSet<PortableExecutableReference> references = new HashSet<PortableExecutableReference>();

            if(_state.UseAutoAssembly)
            _ = AppDomain.CurrentDomain.GetAssemblies()
               .Where(i => !i.IsDynamic && !string.IsNullOrWhiteSpace(i.Location))
               .Distinct()
               .Select(i => MetadataReference.CreateFromFile(i.Location))
               .Execute(item =>
               {
                   references.Add(item);
               });

            _ = _state.Assemblies.Select(x => x.GetFiles()).Execute(item =>
            {
                item.Execute(file =>
                {
                    references.Add(MetadataReference.CreateFromStream(file));
                });
            });

            _option = _option ?? new DomainOptionBuilder();

            CSharpCompilationOptions options = _option.Build();

            SyntaxTree[] syntaxTrees = _state.Namespaces.Select(item => ParseToSyntaxTree(item.ToFullCode(), _option)).ToArray();

            var result = BuildCompilation(_state.Path, _state.AssemblyName, syntaxTrees, references.ToArray(), options);

            messages = result.Diagnostics;
            return result.Success;
        }

        /// <summary>
        /// 通过类构建器直接生成程序集
        /// </summary>
        /// <param name="builder">类构建器</param>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="option">程序集配置</param>
        /// <param name="messages">编译时的消息</param>
        /// <returns></returns>
        public static bool CreateDomain(ClassBuilder builder, string assemblyPath, string assemblyName, DomainOptionBuilder option, out ImmutableArray<Diagnostic> messages)
        {
            HashSet<PortableExecutableReference> references = new HashSet<PortableExecutableReference>();

            _ = AppDomain.CurrentDomain.GetAssemblies()
               .Where(i => !i.IsDynamic && !string.IsNullOrWhiteSpace(i.Location))
               .Distinct()
               .Select(i => MetadataReference.CreateFromFile(i.Location))
               .Execute(item =>
               {
                   references.Add(item);
               });

            CSharpCompilationOptions options = (option ?? new DomainOptionBuilder()).Build();

            var syntaxTree = ParseToSyntaxTree(builder.ToFullCode(), option);
            var result = BuildCompilation(assemblyPath, assemblyName, new SyntaxTree[] { syntaxTree }, references.ToArray(), options);
            messages = result.Diagnostics;
            return result.Success;
        }

        /// <summary>
        /// 将代码转为语法树
        /// </summary>
        /// <param name="code"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static SyntaxTree ParseToSyntaxTree(string code, DomainOptionBuilder option)
        {
            var parseOptions = new CSharpParseOptions(option.LanguageVersion, preprocessorSymbols: option.Environments);

            return CSharpSyntaxTree.ParseText(code, parseOptions);
        }


        /// <summary>
        /// 编译代码
        /// </summary>
        /// <param name="path">程序集位置</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="syntaxTrees">代码语法树</param>
        /// <param name="references">依赖</param>
        /// <param name="options">编译配置</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static EmitResult BuildCompilation(
            string path,
            string assemblyName,
            SyntaxTree[] syntaxTrees,
            PortableExecutableReference[] references,
            CSharpCompilationOptions options)
        {
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees, references, options);

            var result = compilation.Emit(Path.Combine(path, assemblyName));
            return result;
        }
    }
}
