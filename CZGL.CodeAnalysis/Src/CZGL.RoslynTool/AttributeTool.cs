using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CZGL.RoslynTool
{
    public class AttributeTool
    {
        /// <summary>
        /// 构建 AttributeListSyntax
        /// </summary>
        /// <param name="syntaxes"></param>
        /// <returns></returns>
        public static AttributeListSyntax CreateAttributeListSyntax(AttributeSyntax[] syntaxes)
        {
            var attributeList = new SeparatedSyntaxList<AttributeSyntax>();
            foreach (var item in syntaxes)
            {
                attributeList = attributeList.Add(item);
            }
            var list = SyntaxFactory.AttributeList(attributeList);

            return list;
        }

        /// <summary>
        /// 字符串生成特性
        /// </summary>
        /// <param name="attrCode"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// "[Display(Name = \"a\")]"
        /// </code>
        /// </example>
        public static AttributeListSyntax CreateAttributeList(string attrCode)
        {
            var result = CreateAttribute(attrCode);

            return SyntaxFactory.AttributeList(
                  SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(result));
        }

        /// <summary>
        /// 字符串生成特性列表
        /// <para>
        /// <example>
        /// <code>
        /// string[] code = new string[]
        /// {
        ///     "[Display(Name = \"a\")]",
        ///     "[Display(Name = \"b\")]"
        /// };
        /// Cbuilder.reateAttributeList(code);
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        /// <param name="attrCode"></param>
        /// <returns></returns>
        public static SyntaxList<AttributeListSyntax> CreateAttributeList(params string[] attrsCode)
        {
            List<AttributeListSyntax> syntaxes = new List<AttributeListSyntax>();

            foreach (var item in attrsCode)
            {
                var tmp = CreateAttribute(item);
                syntaxes.Add(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(tmp)));
            }

            return SyntaxFactory.List<AttributeListSyntax>(syntaxes.ToArray());
        }

    }
}
