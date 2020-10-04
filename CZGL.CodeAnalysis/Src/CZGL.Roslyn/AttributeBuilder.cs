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
#if DEBUG
        public
#else
internal
#endif
         static SyntaxList<AttributeListSyntax> CreateAttributeList(params string[] attrsCode)
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
#if DEBUG
        public
#else
internal
#endif
         static AttributeListSyntax CreateAttributeList(string attrCode)
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
#if DEBUG
        public
#else
internal
#endif
            static AttributeListSyntax CreateAttributeList(AttributeSyntax attrbuteSyntax)
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
#if DEBUG
        public
#else
internal
#endif
            static AttributeSyntax CreateAttribute(string attrCode)
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

            stringBuilder.Append(MemberName);
            bool isCtor = !string.IsNullOrEmpty(MemberCtor);
            bool isProperty = !string.IsNullOrEmpty(MemberPropertys);

            if (isCtor || isProperty)
            {
                stringBuilder.Append("(");
                if (isCtor)
                    stringBuilder.Append(MemberCtor);
                if (isCtor && isProperty)
                    stringBuilder.Append(",");
                if (isProperty)
                    stringBuilder.Append(MemberPropertys);
                stringBuilder.Append(")");
            }

            stringBuilder.Append("]");

            return CreateAttribute(stringBuilder.ToString());
        }

        /// <summary>
        /// 构建特性注解
        /// </summary>
        /// <returns></returns>
#if DEBUG
        public
#else
internal
#endif   
        AttributeListSyntax BuildAttributeListSyntax()
        {
            return AttributeBuilder.CreateAttributeList(Build());
        }

        /// <summary>
        /// 获得格式化代码
        /// </summary>
        /// <returns></returns>
        public override string FullCode()
        {
            return Build().NormalizeWhitespace().ToFullString();
        }

        // protected internal readonly List<AttributeSyntax> MemberAttrSyntaxs = new List<AttributeSyntax>();
        ///// <summary>
        ///// 添加一个特性
        ///// </summary>
        ///// <param name="builder">特性构建器</param>
        ///// <returns></returns>
        //public virtual TBuilder AddAttribute(Action<AttrbuteTemplate<AttributeBuilder>> builder)
        //{
        //    AttributeBuilder attributeBuilder = new AttributeBuilder();
        //    builder.Invoke(attributeBuilder);
        //    MemberAttrSyntaxs.Add(attributeBuilder.Build());
        //    return _TBuilder;
        //}
    }
}
