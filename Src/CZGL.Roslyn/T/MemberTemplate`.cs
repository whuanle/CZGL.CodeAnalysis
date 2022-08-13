using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 命名空间内的成员模板。
    /// <para>
    /// * {枚举}<br />
    /// * {委托}<br />
    /// * {事件}<br />
    /// * {结构体}<br />
    /// * {接口}<br />
    /// * {类}<br />
    /// *   -{字段}<br />
    /// *   -{属性}<br />
    /// *   -{方法}<br />
    /// *   -{其它成员}<br />
    /// </para>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class MemberTemplate<TBuilder> : BaseTemplate<TBuilder>
        where TBuilder : MemberTemplate<TBuilder>
    {
        #region 属性

        /// <summary>
        /// 可访问性
        /// </summary>
        protected string _access = "";

        /// <summary>
        /// 特性列表
        /// </summary>
        protected string? _atribute;

        public TBuilder WithAccess(MemberAccess memberAccess)
        {
            // SyntaxToken PublicKeyword public
            var a = new SyntaxToken();
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AttributeSyntax> AttributeSytaxList
        {
            get
            {
                if (string.IsNullOrEmpty(_atribute)) return Enumerable.Empty<AttributeSyntax>();
                return CSharpSyntaxTree.ParseText(_atribute!).GetRoot().DescendantNodes()
                    .OfType<AttributeSyntax>()
                .ToList();
            }
        }

        #endregion


        /// <summary>
        /// 设置成员的特性注解
        /// </summary>
        /// <param name="attrList">特性注解字符串列表</param>
        /// <returns></returns>
        public virtual TBuilder WithAttributes(string attrList)
        {
            _atribute = attrList;
            return (TBuilder)this;
        }


        /// <summary>
        /// 设置访问修饰符(Access Modifiers)。
        /// <para>如果修饰符设置错误，将导致代码错误，例如命名空间中的成员不支持 private。</para>
        /// </summary>
        /// <param name="access">标记</param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(MemberAccess access = MemberAccess.Default)
        {
            _access = EnumCache.View<MemberAccess>(access);
            return (TBuilder)this;
        }

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// <para><b>注意，如果填写不正确，将导致代码错误</b></para>
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(string access)
        {
            _access = access;
            return (TBuilder)this;
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static class MemberTemplateExtensions
    {
        /// <summary>
        /// 设置特性注解
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <param name="tbuilder"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static TBuilder WithAttributes<TBuilder>(this MemberTemplate<TBuilder> tbuilder, AttributeBuilder builder) where TBuilder : MemberTemplate<TBuilder>
        {
            tbuilder.WithAttributes(builder.ToFormatCode());
            return (TBuilder)tbuilder;
        }
    }
}
