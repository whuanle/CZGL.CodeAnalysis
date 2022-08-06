﻿using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public abstract class MemberTemplate<TBuilder> : BaseTemplate where TBuilder : MemberTemplate<TBuilder>
    {
        /// <summary>
        /// 命名空间成员共有属性
        /// </summary>
        protected internal MemberState _member = new MemberState();

        /// <summary>
        /// 表示当前子类构建器
        /// </summary>
        protected internal TBuilder _TBuilder;

        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name { get { return _base.Name; } }

        #region 特性注解

        /// <summary>
        /// 设置成员的特性注解
        /// <para>
        /// <code>
        /// string[] codes = new string[]{"[Key]","[Display( Name = \"YouName\")]"};
        /// .SetAttributeLists(code);
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public virtual TBuilder WithAttributes(params string[] attrs)
        {
            if (attrs != null)
                _ = attrs.Execute(str => _member.Atributes.Add(str));
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
        public virtual TBuilder WithAttribute(string attr)
        {
            if (string.IsNullOrEmpty(attr))
                throw new ArgumentNullException(nameof(attr));
            _member.Atributes.Add(attr);
            return _TBuilder;
        }

        /// <summary>
        /// 获取当前以定义代码的特性列表
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<AttributeSyntax> GetAttributes()
        {
            string code;
            if (_base.UseCode)
                code = _base.Code;
            else code = ToFullCode();

            var syntaxNodes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes();
            var memberDeclarations = syntaxNodes
                .OfType<AttributeSyntax>()
                .ToList();

            return memberDeclarations;
        }


        #endregion

        #region 访问权限

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="access">标记</param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(MemberAccess access = MemberAccess.Default)
        {
            _member.Access = EnumCache.View<MemberAccess>(access);
            return _TBuilder;
        }

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// <para><b>注意，如果填写不正确，将导致代码错误</b></para>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            _member.Access = code;
            return _TBuilder;
        }


        #endregion

        #region 名称

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new virtual TBuilder WithName(string name)
        {
            base.WithName(name);
            return _TBuilder;
        }

        /// <summary>
        /// 随机生成一个名称
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithRondomName()
        {
            base.WithRondomName();
            return _TBuilder;
        }

        #endregion

        #region clear

        /// <summary>
        /// 清除已经添加的特性
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder ClearAttribute()
        {
            _member.Atributes.Clear();
            return _TBuilder;
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

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code">字符串代码</param>
        /// <returns></returns>
        internal TBuilder WithFromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            _base.UseCode = true;
            _base.Code = code;

            return _TBuilder;
        }
    }
}
