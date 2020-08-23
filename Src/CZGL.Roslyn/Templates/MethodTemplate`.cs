using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class MethodTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : MethodTemplate<TBuilder>
    {
        protected internal string ReturnType = "void";
        protected internal string Params;
        protected internal string BlockCode;

        /// <summary>
        /// 设置返回返回类型
        /// </summary>
        /// <example>int</example>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetReturnType(string str = "void")
        {
            ReturnType = str;
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
            Params = paramsStr;
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
            Params = string.Join(",",@params);
            return _TBuilder;
        }

    }
}
