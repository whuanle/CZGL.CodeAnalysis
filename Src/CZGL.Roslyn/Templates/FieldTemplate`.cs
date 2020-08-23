using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn
{
    public abstract class FieldTemplate<TBuilder> : QualifierTemplate<TBuilder> where TBuilder : FieldTemplate<TBuilder>
    {
        protected internal string MemberType;
        protected internal string MemberInit;

        /// <summary>
        /// 定义类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetType(string str = null)
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
