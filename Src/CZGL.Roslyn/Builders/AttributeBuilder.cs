using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 成员特性构建器
    /// </summary>
    public sealed class AttributeBuilder : AttrbuteTemplate<AttributeBuilder>
    {
        internal AttributeBuilder()
        {
            _TBuilder = this;
        }
        internal AttributeBuilder(string name) : this()
        {
            _base.Name = name;
        }

        /// <summary>
        /// 构建 AttributeSyntax
        /// </summary>
        /// <returns></returns>
        public  AttributeSyntax BuildSyntax()
        {
            return CodeSyntax.CreateCodeAttribute(ToFullCode());
        }

        /// <summary>
        /// 构建特性注解
        /// </summary>
        /// <returns></returns>
        public AttributeListSyntax BuildAttributeListSyntax()
        {
            return CodeSyntax.CreateAttributeList(BuildSyntax());
        }

        /// <summary>
        /// 完整输出代码
        /// <para>不会对代码进行检查，直接输出当前已经定义的代码</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public override string ToFullCode()
        {
            const string Template1 = @"[{Name}]";
            const string Template2 = @"[{Name}({Ctor})]";
            const string Template3 = @"[{Name}({Propertys})]";
            const string Template4 = @"[{Name}({Ctor},{Propertys})]";
            string code;

            if (string.IsNullOrEmpty(_attribute.Ctor))
            {
                if (_attribute.Propertys.Count == 0)
                {
                    code = Template1.Replace("{Name}", _base.Name);
                }
                else
                    code = Template3.Replace("{Name}", _base.Name).Replace("{Propertys}", _attribute.Propertys.Join(","));
            }
            else
            {
                if (_attribute.Propertys.Count == 0)
                {
                    code = Template2.Replace("{Name}", _base.Name).Replace("{Ctor}", _attribute.Ctor);
                }
                else
                    code = Template4.Replace("{Name}", _base.Name)
                        .Replace("{Ctor}", _attribute.Ctor)
                        .Replace("{Propertys}", _attribute.Propertys.Join(","));
            }

            return code;
        }

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
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
