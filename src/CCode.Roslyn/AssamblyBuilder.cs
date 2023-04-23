using CCode.Roslyn.Builder;
using CCode.Roslyn.Template;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Roslyn
{
    public class AssamblyBuilder
    {
        private readonly CodeContext _codeContext;
        public AssamblyBuilder(CodeContext codeContext)
        {
            _codeContext = codeContext;
        }

        private readonly List<NamespaceDeclarationSyntax> _namespaces = new List<NamespaceDeclarationSyntax>();

        public AssamblyBuilder WithNamespace(NamespaceDeclarationSyntax @namespace)
        {
            _namespaces.Add(@namespace);
            return this;
        }

        
        /// <summary>
        /// 通过字符串构建类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AssamblyBuilder WithClass(string code)
        {
            
            return this;
        }

        public AssamblyBuilder WithClass()
        {

        }
    }
}
