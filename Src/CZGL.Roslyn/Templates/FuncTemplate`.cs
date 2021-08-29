﻿using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 构建器模板 <see cref="DelegateBuilder"/>、<see cref="MethodTemplate{TBuilder}"/>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class FuncTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : FuncTemplate<TBuilder>
    {
        /// <summary>
        /// 函数状态机
        /// </summary>
        protected internal FuncState _func = new FuncState();
        private const string ReturnType = "void";


        #region 返回值

        /// <summary>
        /// 函数返回类型
        /// <para>构造函数没有返回值</para>
        /// </summary>
        /// <example>int</example>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual TBuilder WithReturnType(string code = "void")
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            _func.ReturnType = code;
            return _TBuilder;
        }

        /// <summary>
        /// 函数返回类型
        /// <para>构造函数没有返回值</para>
        /// </summary>
        /// <example>int</example>
        /// <returns></returns>
        public virtual TBuilder WithDefaultReturnType()
        {
            _func.ReturnType = ReturnType;
            return _TBuilder;
        }

        #endregion


        #region 参数

        /// <summary>
        /// 为方法添加一个参数
        /// </summary>
        /// <param name="paramCode">参数内容</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// WithParam("int a")
        /// </code>
        /// </example>
        public virtual TBuilder WithParam(string paramCode)
        {
            if (string.IsNullOrWhiteSpace(paramCode))
                throw new ArgumentNullException(nameof(paramCode));

            _func.Params.Add(paramCode);
            return _TBuilder;
        }

        /// <summary>
        /// 为方法添加一串参数
        /// </summary>
        /// <param name="paramsCode">参数内容</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// WithParams("int a,,int b,int c = 0")
        /// </code>
        /// </example>
        public virtual TBuilder WithParams(string paramsCode) => WithParam(paramsCode);


        /// <summary>
        /// 为方法添加一串参数
        /// </summary>
        /// <param name="paramsCode">参数内容</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// WithParams("int a","int b","int c = 0")
        /// </code>
        /// </example>
        public virtual TBuilder WithParams(params string[] paramsCode)
        {
            _ = paramsCode.Execute(cl =>_func.Params.Add(cl));

            return _TBuilder;
        }

        #endregion

    }
}
