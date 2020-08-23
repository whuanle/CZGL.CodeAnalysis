using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 创建特性
    /// </summary>
    public class AttributeBuilder : AttrbuteTemplate<AttributeBuilder>
    {
        public AttributeBuilder()
        {
            _TBuilder = this;
        }


        /// <summary>
        /// 构建 AttributeListSynta
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
        /// 字符串生成特性列表
        /// </summary>
        /// <param name="attrCode"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// {"[Display(Name = \"a\")]","[Display(Name = \"b\")]"}
        /// </code>
        /// </example>
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
        /// 字符串生成特性
        /// </summary>
        /// <param name="attrbuteSyntax"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// "[Display(Name = \"a\")]"
        /// </code>
        /// </example>
        public static AttributeListSyntax CreateAttributeList(AttributeSyntax attrbuteSyntax)
        {
            return SyntaxFactory.AttributeList(
                  SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(attrbuteSyntax));
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
        public static AttributeSyntax CreateAttribute(string attrCode)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(attrCode).GetRoot().DescendantNodes();
            var member = syntaxNodes.OfType<AttributeSyntax>().FirstOrDefault();
            return member;
        }

        /// <summary>
        /// 构建 AttributeSyntax
        /// </summary>
        /// <returns></returns>
        public AttributeSyntax Build()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");

            stringBuilder.Append(Name);
            bool isCtor = !string.IsNullOrEmpty(Ctor);
            bool isProperty = !string.IsNullOrEmpty(Propertys);

            if (isCtor || isProperty)
            {
                stringBuilder.Append("(");
                if (isCtor)
                    stringBuilder.Append(Ctor);
                if (isCtor && isProperty)
                    stringBuilder.Append(",");
                if (isProperty)
                    stringBuilder.Append(Propertys);
                stringBuilder.Append(")");
            }

            stringBuilder.Append("]");

            Console.WriteLine(CreateAttribute(stringBuilder.ToString()).NormalizeWhitespace().ToFullString());

            return CreateAttribute(stringBuilder.ToString());
        }

        /// <summary>
        /// 构建特性注解
        /// </summary>
        /// <returns></returns>
        public AttributeListSyntax BuildAttributeListSyntax()
        {
            return AttributeBuilder.CreateAttributeList(Build());
        }

    }
}
