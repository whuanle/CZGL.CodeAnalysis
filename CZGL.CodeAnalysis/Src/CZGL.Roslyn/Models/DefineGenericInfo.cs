using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Models
{
    public class DefineGenericInfo
    {
        /// <summary>
        /// 泛型的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 泛型参数类型
        /// </summary>
        public TypeParameterSyntax Parameter { get; set; }

        /// <summary>
        /// 泛型约束
        /// </summary>
        public List<TypeParameterConstraintClauseSyntax> ConstraintClauseSyntax { get; set; } = new List<TypeParameterConstraintClauseSyntax>();
    }
}
