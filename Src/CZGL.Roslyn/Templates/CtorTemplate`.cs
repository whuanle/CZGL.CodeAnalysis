using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 构造函数构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class CtorTemplate<TBuilder> : MethodTemplate<TBuilder> where TBuilder : CtorTemplate<TBuilder>
    {
        /// <summary>
        /// 构造函数状态机
        /// </summary>
        protected internal readonly CtorState _ctor = new CtorState();

        /// <summary>
        /// 调用父类构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// base("test")
        /// </code>
        /// </example>
        public virtual TBuilder WithBase(string code)
        {
            _method.BaseOrThis = code;
            return _TBuilder;
        }

        /// <summary>
        /// 调用本身的其它构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual TBuilder WithThis(string code)
        {
            _method.BaseOrThis = code;
            return _TBuilder;
        }

    }
}
