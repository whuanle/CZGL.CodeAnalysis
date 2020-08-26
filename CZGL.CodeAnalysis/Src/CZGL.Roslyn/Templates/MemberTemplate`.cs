using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class MemberTemplate<TBuilder> where TBuilder : MemberTemplate<TBuilder>
    {
        protected internal string MemberVisibility;
        protected internal string MemberQualifier;

        protected internal readonly List<string> MemberAttrs = new List<string>();
        protected internal readonly List<AttributeSyntax> MemberAttrSyntaxs = new List<AttributeSyntax>();

        protected internal string MemberName = string.Empty;

        protected internal TBuilder _TBuilder;

        #region 特性

        /// <summary>
        /// 设置成员的特性注解
        /// <para>
        /// <code>
        /// string[] codes = new string[]{"[Key]","[Display( Name = \"YouName\")]"};
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public virtual TBuilder SetAttributeLists(string[] attrs = null)
        {
            MemberAttrs.Clear();
            if (attrs != null)
                MemberAttrs.AddRange(attrs);
            return _TBuilder;
        }


        /// <summary>
        /// 添加一个特性
        /// <para>
        /// <code>
        /// string attr = "[Key]";
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="attr">attr 不能为空</param>
        /// <returns></returns>
        public virtual TBuilder AddAttribute(string attr)
        {
            if (string.IsNullOrEmpty(attr))
                throw new ArgumentNullException(nameof(attr));
            MemberAttrs.Add(attr);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个特性
        /// </summary>
        /// <param name="builder">特性构建器</param>
        /// <returns></returns>
        public virtual TBuilder AddAttribute(Action<AttrbuteTemplate<AttributeBuilder>> builder)
        {
            AttributeBuilder attributeBuilder = new AttributeBuilder();
            builder.Invoke(attributeBuilder);
            MemberAttrSyntaxs.Add(attributeBuilder.Build());
            return _TBuilder;
        }

        #endregion

        #region 访问权限

        /// <summary>
        /// 设置成员的访问权限,public 、private
        /// </summary>
        /// <param name="visibilityType">标记</param>
        /// <returns></returns>
        public virtual TBuilder SetVisibility(MemberVisibilityType visibilityType = MemberVisibilityType.Internal)
        {
            MemberVisibility = RoslynHelper.GetName(visibilityType);
            return _TBuilder;
        }

        /// <summary>
        /// 设置成员的访问权限,public 、private
        /// </summary>
        /// <param name="code">不能为空</param>
        /// <returns></returns>
        public virtual TBuilder SetVisibility(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            MemberVisibility = code;
            return _TBuilder;
        }

        #endregion

        #region 名称

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            MemberName = name;
            return _TBuilder;
        }

        /// <summary>
        /// 随机设置一个名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder SetRondomName()
        {
            MemberName = "N" + new Guid().ToString("N");
            return _TBuilder;
        }



        #endregion 
    }
}
