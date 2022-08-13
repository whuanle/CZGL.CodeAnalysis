using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 构建器模板 <see cref="DelegateBuilder"/>、<see cref="MethodBaseTemplate{TBuilder}"/>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class FuncTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : FuncTemplate<TBuilder>
    {
        /// <summary>
        /// 返回值
        /// </summary>
        protected string _returnArgmunt  = "void";

        /// <summary>
        /// 参数列表
        /// </summary>
        protected readonly HashSet<string> _inputParams = new HashSet<string>();


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
            _returnArgmunt = code;
            return (TBuilder)this;
        }

        #endregion


        #region 参数

        /// <summary>
        /// 为方法添加一个参数
        /// </summary>
        /// <param name="paramCode">参数内容</param>
        /// <returns></returns>
        public virtual TBuilder WithParam(string paramCode)
        {
            if (string.IsNullOrWhiteSpace(paramCode))
                throw new ArgumentNullException(nameof(paramCode));

            _inputParams.Add(paramCode);
            return (TBuilder)this;
        }

        #endregion

    }
}
