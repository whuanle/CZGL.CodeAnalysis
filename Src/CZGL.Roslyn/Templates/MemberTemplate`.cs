using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 命名空间内的成员共有的结构和操作
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

        // 委托事件，不具有属性修饰符，如 static

#nullable enable

        /// <summary>
        /// 特性列表
        /// </summary>
        protected readonly List<string> _atributes = new List<string>();

        #endregion


        #region 特性注解

        /// <summary>
        /// 设置成员的特性注解
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public virtual TBuilder WithAttributes(params string[] attrs)
        {
            _atributes.AddRange(attrs);
            return (TBuilder)this;
        }

        /// <summary>
        /// 设置成员的特性注解
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public virtual TBuilder WithAttributes(IEnumerable<string> attrs)
        {
            _atributes.AddRange(attrs);
            return (TBuilder)this;
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
        public virtual TBuilder WithAttribute(string attr)
        {
            _atributes.Add(attr);
            return (TBuilder)this;
        }

        /// <summary>
        /// 获取当前以定义代码的特性列表
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<AttributeSyntax> GetAttributeSytax()
        {
            var code = ToFullCode();
            var syntaxNodes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes();
            var memberDeclarations = syntaxNodes
                .OfType<AttributeSyntax>()
                .ToList();

            return memberDeclarations;
        }


        #endregion

        #region 访问权限

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


        #endregion

        #region 泛型约束


        /// <summary>
        /// 为此函数构建泛型
        /// <para>构造函数无泛型参数</para>
        /// </summary>
        /// <param name="builder">泛型构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(Action<GenericTemplate<GenericBuilder>> builder)
        {
            GenericBuilder generic = new GenericBuilder();
            builder.Invoke(generic);
            _member.GenericParams = generic;
            return _TBuilder;
        }

        /// <summary>
        /// 为此函数构建泛型
        /// <para>构造函数无泛型参数</para>
        /// </summary>
        /// <param name="builder">泛型构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(Func<GenericTemplate<GenericBuilder>, GenericTemplate<GenericBuilder>> builder)
        {
            GenericBuilder generic = new GenericBuilder();

            _member.GenericParams = builder.Invoke(generic).GetBuilder();
            return _TBuilder;
        }

        /// <summary>
        /// 为此函数构建泛型
        /// <para>构造函数无泛型参数</para>
        /// </summary>
        /// <param name="builder">构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(GenericBuilder builder)
        {
            _member.GenericParams = builder;
            return _TBuilder;
        }

        /// <summary>
        /// 为此函数构建泛型
        /// <para>构造函数无泛型参数</para>
        /// </summary>
        /// <param name="paramList">泛型参数</param>
        /// <param name="constraintList">泛型参数约束</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(string paramList, string constraintList)
        {
            _member.GenericParams = GenericBuilder.WithFromCode(paramList, constraintList);
            return _TBuilder;
        }

        /// <summary>
        /// 为此函数构建泛型
        /// <para>构造函数无泛型参数</para>
        /// </summary>
        /// <param name="paramList">泛型参数</param>
        /// <param name="constraintList">泛型参数约束</param>
        /// <param name="builder">构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(string paramList, string constraintList, out GenericBuilder builder)
        {
            var generic = GenericBuilder.WithFromCode(paramList, constraintList);
            _member.GenericParams = generic;
            builder = generic;
            return _TBuilder;
        }


        #endregion
    }
}
