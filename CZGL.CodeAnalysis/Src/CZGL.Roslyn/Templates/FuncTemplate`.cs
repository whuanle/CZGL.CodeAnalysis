using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class FuncTemplate<TBuilder>:MemberTemplate<TBuilder> where TBuilder : FuncTemplate<TBuilder>
    {
        protected internal string FuncReturnType = "void";
        protected internal string FuncParams;

        /// <summary>
        /// 设置返回返回类型
        /// </summary>
        /// <example>int</example>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetReturnType(string str = "void")
        {
            FuncReturnType = str;
            return _TBuilder;
        }

        /// <summary>
        /// 设置方法的参数列表
        /// </summary>
        /// <param name="paramsStr">参数内容</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// SetParams("int a,int b,int c = 0")
        /// </code>
        /// </example>
        public virtual TBuilder SetParams(string paramsStr)
        {
            FuncParams = paramsStr;
            return _TBuilder;
        }

        /// <summary>
        /// 设置方法的参数列表
        /// </summary>
        /// <param name="paramsStr">参数内容</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// SetParams("int a,int b,int c = 0")
        /// </code>
        /// </example>
        public virtual TBuilder SetParams(params string[] @params)
        {
            FuncParams = string.Join(",", @params);
            return _TBuilder;
        }


    }
}
