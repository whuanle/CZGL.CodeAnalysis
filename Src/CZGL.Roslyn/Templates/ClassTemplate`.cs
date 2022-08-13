using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 类构建器模板
    /// </summary>
    /// <typeparam name="TBuilder"><see cref="ClassBuilder"/></typeparam>
    public abstract class ClassTemplate<TBuilder> : ObjectTypeTemplate<TBuilder> where TBuilder : ClassTemplate<TBuilder>
    {
        /// <summary>
        /// 继承基类
        /// </summary>
        public string BaseClass { get; set; }

        /// <summary>
        /// 接口列表
        /// </summary>
        public HashSet<string> Interfaces { get; } = new HashSet<string>();

        #region 修饰符

        /// <summary>
        /// 类的修饰符关键字，如 static，sealed
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(ClassKeyword keyword)
        {
            _typeState.Keyword = EnumCache.View<ClassKeyword>(keyword);
            return (TBuilder)this;
        }

        #endregion


        #region 继承


        /// <summary>
        /// 继承基类
        /// </summary>
        /// <param name="baseName">基类名称</param>
        /// <returns></returns>
        public virtual TBuilder WithBaseClass(string baseName)
        {
            BaseClass = baseName;
            return (TBuilder)this;
        }

        #endregion
    }
}