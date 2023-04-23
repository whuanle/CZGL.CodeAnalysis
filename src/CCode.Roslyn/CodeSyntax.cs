using CCode.Roslyn.Builder;
using CCode.Roslyn.Template;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCode.Roslyn
{
    /// <summary>
    /// 代码工具
    /// </summary>
    public static class CodeSyntax
    {
  
        public static ClassDeclarationSyntax CreateClass(string code)
        {
            return ClassBuilder.ToSyntax(code);
        }
    }
}
