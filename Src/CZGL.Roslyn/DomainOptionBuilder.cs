﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 程序集编译配置构建器
    /// </summary>
    public class DomainOptionBuilder
    {
        private readonly DomainOptionState _option = new DomainOptionState();
        internal LanguageVersion LanguageVersion => _option.LanguageVersion;
        internal string[] Environments => _option.Environments.ToArray();
        internal CSharpCompilationOptions Build()
        {
            if (_option.Environments.Count == 0)
            {
                _option.Environments.Add(_option.OptimizationLevel == OptimizationLevel.Debug ? "DEBUG" : "RESEALE");
            }

            return new CSharpCompilationOptions(
              concurrentBuild: true,
              metadataImportOptions: MetadataImportOptions.All,
              outputKind: _option.OutputKind,
              optimizationLevel: _option.OptimizationLevel,
              allowUnsafe: _option.AllowUnsafe,
              platform: _option.Platform,
              checkOverflow: _option.CheckOverflow,
              assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default);
        }

        /// <summary>
        /// 程序集要编译成何种项目
        /// <para>默认编译成动态库</para>
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public DomainOptionBuilder WithKind(OutputKind kind = OutputKind.DynamicallyLinkedLibrary)
        {
            _option.OutputKind = kind;
            return this;
        }

        /// <summary>
        /// 程序集是否使用 DEBUG 条件编译
        /// <para>默认使用 RELEASE 编译程序</para>
        /// </summary>
        /// <param name="isDebug"></param>
        /// <returns></returns>
        public DomainOptionBuilder WithDebug(bool isDebug = false)
        {
            _option.OptimizationLevel = isDebug ? OptimizationLevel.Debug : OptimizationLevel.Release;
            return this;
        }

        /// <summary>
        /// 是否允许项目使用不安全代码
        /// </summary>
        /// <param name="isAllow"></param>
        /// <returns></returns>
        public DomainOptionBuilder WIthAllowUnsafe(bool isAllow = false)
        {
            _option.AllowUnsafe = isAllow;
            return this;
        }

        /// <summary>
        /// 指定公共语言运行库（CLR）的哪个版本可以运行程序集
        /// </summary>
        /// <param name="platform">默认为可移植的</param>
        /// <returns></returns>
        public DomainOptionBuilder WithPlatform(Platform platform = Platform.AnyCpu)
        {
            _option.Platform = platform;
            return this;
        }

        /// <summary>
        /// 是否在默认情况下强制执行整数算术的边界检查
        /// </summary>
        /// <param name="checkOverflow"></param>
        /// <returns></returns>
        public DomainOptionBuilder WithCheckOverflow(bool checkOverflow = false)
        {
            _option.CheckOverflow = checkOverflow;
            return this;
        }

        /// <summary>
        /// 要使用的语言版本
        /// <para>如果直接通过代码生成，代码版本任意；如果通过 API 生成，目前项目的语法只考虑到 7.3</para>
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public DomainOptionBuilder WithLanguageVersion(LanguageVersion version = LanguageVersion.CSharp7_3)
        {
            _option.LanguageVersion = version;
            return this;
        }

        /// <summary>
        /// 编译条件字符串
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public DomainOptionBuilder WithEnvironment(params string[] environment)
        {
            _ = environment.Execute(item =>
            {
                _option.Environments.Add(item);
            });

            return this;
        }

#warning 此功能未完成，允许设置使用哪个版本的 .NET 发布，例如  .NET Standard 2.0,.NET Core 3.1
        internal class DomainOptionState
        {
            /// <summary>
            /// 当前要编译的程序集是何种类型的项目
            /// </summary>
            public OutputKind OutputKind { get; set; } = OutputKind.DynamicallyLinkedLibrary;

            /// <summary>
            /// Debug 还是 Release
            /// </summary>
            public OptimizationLevel OptimizationLevel { get; set; } = OptimizationLevel.Release;

            /// <summary>
            /// 是否允许使用不安全代码
            /// </summary>
            public bool AllowUnsafe { get; set; } = false;

            /// <summary>
            /// 生成目标平台
            /// </summary>
            public Platform Platform { get; set; } = Platform.AnyCpu;

            /// <summary>
            /// 是否检查语法
            /// </summary>
            public bool CheckOverflow { get; set; } = false;

            /// <summary>
            /// 语言版本
            /// </summary>
            public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.CSharp7_3;

            /// <summary>
            /// 环境
            /// </summary>
            public HashSet<string> Environments { get; } = new HashSet<string>();
        }
    }
}
