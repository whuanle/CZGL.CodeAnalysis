using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 成员特性构建器
    /// </summary>
    public class AttributeBuilder : AttrbuteTemplate<AttributeBuilder>
    {
        public AttributeBuilder()
        {
            _TBuilder = this;
        }


        /// <summary>
        /// 构建 AttributeSyntax
        /// </summary>
        /// <returns></returns>
        public AttributeSyntax Build()
        {
            return CodeSyntax.CreateAttribute(ToFullCode());
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
            return CodeSyntax.CreateAttributeList(Build());
        }

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
                    code = Template1;
                }
                else
                    code = Template3.Replace("{Propertys}",_attribute.Propertys.Join(","));
            }
            else
            {
                if (_attribute.Propertys.Count == 0)
                {
                    code = Template2.Replace("{Ctor})",_attribute.Ctor);
                }
                else
                    code = Template4
                        .Replace("{Ctor})", _attribute.Ctor)
                        .Replace("{Propertys}", _attribute.Propertys.Join(","));
            }

            return code;
        }

        public override string ToFormatCode()
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
