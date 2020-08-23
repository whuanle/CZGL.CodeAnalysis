using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class CtorTemplate<TBuilder> : MethodTemplate<TBuilder> where TBuilder : CtorTemplate<TBuilder>
    {
        protected internal string BaseCtor;
        protected internal string ThisCtor;

        /// <summary>
        /// 此设置对构造函数无效
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [Obsolete]
        public override TBuilder SetReturnType(string str = "void")
        {
            return _TBuilder;
        }

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
        public virtual TBuilder SetBaseCtor(string Code)
        {
            BaseCtor = Code;
            return _TBuilder;
        }

        /// <summary>
        /// 调用本身的其它构造函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetThisCtor(string Code)
        {
            ThisCtor = Code;
            return _TBuilder;
        }

        /// <summary>
        /// 方法体中的代码
        /// </summary>
        /// <param name="block">方法体中的代码</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// int a = 0;
        /// Console.WriteLine(a);
        /// </code>
        /// </example>
        public virtual TBuilder SetBlock(string blockCode = null)
        {
            BlockCode = blockCode;
            return _TBuilder;
        }
    }
}
