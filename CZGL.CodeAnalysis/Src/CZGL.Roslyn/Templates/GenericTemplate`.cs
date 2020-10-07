using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class GenericTemplate<TBuilder> where TBuilder : GenericTemplate<TBuilder>
    {
        protected internal HashSet<GenericState> _generic = new HashSet<GenericState>();
        protected internal string ParamCode;
        protected internal string WhereCode;

        protected internal TBuilder _TBuilder;
        protected internal GenericState _this;

        // 为哪个对象构建泛型
        protected internal string ObjectName;

        /// <summary>
        /// 创建一个泛型参数
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithCreate(string name)
        {
            _this = new GenericState();
            _this.Name = name;
            _generic.Add(_this);
            return _TBuilder;
        }

        /// <summary>
        /// 结束一个泛型参数的设计
        /// </summary>
        /// <returns></returns>
        public virtual GenericTemplate<TBuilder> WithEnd()
        {
            _this = null;
            return this;
        }

        /// <summary>
        /// 通过代码直接生成泛型参数列表
        /// <code>
        /// string Code = @"T1,T2,T3,T4";
        /// </code>
        /// </summary>
        /// <param name="Code">泛型参数代码</param>
        /// <returns></returns>
        public virtual GenericTemplate<TBuilder> WithTransformParam(string Code)
        {
            ParamCode = Code;
            return this;
        }

        /// <summary>
        /// 通过代码直接生成泛型约束列表
        /// <para>
        /// <example>
        /// <code>
        /// string Code = @"
        ///         where T1 : struct
        ///         where T2 : class
        ///         where T3 : notnull
        ///         where T4 : unmanaged
        ///         where T5 : new()
        ///         where T6 : Model_泛型类4
        ///         where T7 : IEnumerable`int
        ///         where T8 : T2
        /// "; 
        /// .WithConstraintTransform(Code)
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        /// <param name="Code">泛型参数代码</param>
        /// <returns></returns>
        public virtual GenericTemplate<TBuilder> WithTransformConstraint(string Code)
        {
            WhereCode = Code;
            return this;
        }

        /// <summary>
        /// 获取构建器
        /// </summary>
        /// <returns></returns>
        internal virtual TBuilder GetBuilder()
        {
            return _TBuilder;
        }

        /// <summary>
        /// 完整输出代码
        /// <para>不会对代码进行检查，直接输出当前已经定义的代码</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFullCode();

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFormatCode();
    }
}
