using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class EventTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : EventTemplate<TBuilder>
    {
        protected internal string MemberType;
        protected internal string MemberInit;

        /// <summary>
        /// 定义类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetDelegateType(string str = null)
        {
            MemberType = str;
            return _TBuilder;
        }

        /// <summary>
        /// 初始化器
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder Initializer(string initString = null)
        {
            MemberInit = initString;
            return _TBuilder;
        }

    }
}
