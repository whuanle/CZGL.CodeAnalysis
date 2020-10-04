﻿using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 命名空间内的成员共有的操作
    /// <para>
    /// * {枚举}
    /// * {委托}
    /// * {事件}
    /// * {结构体}
    /// * {接口}
    /// * {类}
    /// *   -{字段}
    /// *   -{属性}
    /// *   -{方法}
    /// *   -{其它成员}
    /// </para>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class MemberTemplate<TBuilder> : BaseTemplate where TBuilder : MemberTemplate<TBuilder>
    {
        /// <summary>
        /// 命名空间成员共有属性
        /// </summary>
        protected internal MemberState _member = new MemberState();

        protected internal TBuilder _TBuilder;

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
                _ = attrs.SelectMany(str => { _member.Atributes.Add(str); return str; });
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


        #endregion

        #region 访问权限

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="visibilityType">标记</param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(MemberVisibilityType visibilityType = MemberVisibilityType.Internal)
        {
            _member.Access = RoslynHelper.GetName(visibilityType);
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
        public virtual TBuilder WithName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            _base.Name = name;
            return _TBuilder;
        }

        /// <summary>
        /// 随机生成一个名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder WithRondomName()
        {
            _base.Name = "N" + Guid.NewGuid().ToString("N");
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
    }
}
