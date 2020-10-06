using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 字段构建模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class FieldTemplate<TBuilder> : VariableTemplate<TBuilder> where TBuilder : FieldTemplate<TBuilder>
    {
        protected internal ClassBuilder _this;


        /// <summary>
        /// 设置字段的关键字，如 static，readonly 等
        /// </summary>
        /// <param name="keyword">字段修饰符</param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(FieldKeyword keyword = FieldKeyword.Default)
        {
            _variable.Keyword = RoslynHelper.GetName(keyword);
            return _TBuilder;
        }
    }
}
