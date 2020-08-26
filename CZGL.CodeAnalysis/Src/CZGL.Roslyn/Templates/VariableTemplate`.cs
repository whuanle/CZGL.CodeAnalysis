using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class VariableTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : VariableTemplate<TBuilder>
    {
        protected internal string MemberType;
        protected internal string MemberInit;

        /// <summary>
        /// 定义类型
        /// </summary>
        /// <param name="typeName">不能为空</param>
        /// <returns></returns>
        public virtual TBuilder SetType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException(nameof(typeName));
            MemberType = typeName;
            return _TBuilder;
        }

        /// <summary>
        /// 初始化器
        /// <para>
        /// <code>
        ///  int a = 666;
        ///  666 为初始化部分
        /// </code>
        /// </para>
        /// <para>
        /// <code>
        /// int a = int.Parse("666");
        /// int.Parse("666") 为初始化部分
        /// </code>
        /// </para>
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder Initializer(string initString = null)
        {
            MemberInit = initString;
            return _TBuilder;
        }
    }
}
