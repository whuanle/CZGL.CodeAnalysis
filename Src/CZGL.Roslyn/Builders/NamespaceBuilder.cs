﻿using CZGL.Roslyn.States;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 构建命名空间
    /// </summary>
    public sealed class NamespaceBuilder : NamespaceTemplate<NamespaceBuilder>
    {
        internal NamespaceBuilder()
        {
            _TBuilder = this;
        }

        /// <summary>
        /// 创建命名空间
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        internal NamespaceBuilder(string namespaceName) : this()
        {
            _base.Name = namespaceName;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static NamespaceBuilder FromCode(string Code)
        {
            return new NamespaceBuilder().WithFromCode(Code);
        }

        /// <summary>
        /// 构建命名空间语法树
        /// </summary>
        /// <returns></returns>
        public override NamespaceDeclarationSyntax BuildSyntax()
        {
            NamespaceDeclarationSyntax memberDeclaration;

            memberDeclaration = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                .Single();

            return memberDeclaration;
        }

    }
}
