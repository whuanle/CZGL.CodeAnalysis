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
        protected internal readonly CtorState _ctor = new CtorState();



        /// <summary>
        /// 调用父类构造函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// base("test")
        /// </code>
        /// </example>
        public virtual TBuilder WithBaseCtor(string Code)
        {
            _ctor.BaseCtor = Code;
            return _TBuilder;
        }

        /// <summary>
        /// 调用本身的其它构造函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder WithThisCtor(string Code)
        {
            _ctor.ThisCtor = Code;
            return _TBuilder;
        }
    }
}
