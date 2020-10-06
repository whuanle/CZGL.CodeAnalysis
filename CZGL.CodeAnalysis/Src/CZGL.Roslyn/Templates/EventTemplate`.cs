﻿using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 事件构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class EventTemplate<TBuilder> : VariableTemplate<TBuilder> where TBuilder : EventTemplate<TBuilder>
    {
        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="access"><see cref="NamespaceAccess"/></param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(NamespaceAccess access = NamespaceAccess.Internal)
        {
            _member.Access = RoslynHelper.GetName(access);
            return _TBuilder;
        }

        /// <summary>
        /// 事件关键字
        /// </summary>
        /// <param name="keyword"><see cref="EventKeyword"/></param>
        /// <returns></returns>
        public TBuilder WithKeyword(EventKeyword keyword)
        {
            _variable.Keyword = RoslynHelper.GetName(keyword);
            return _TBuilder;
        }

    }
}
