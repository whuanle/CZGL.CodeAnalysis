using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 构造函数和方法构建模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class MethodBaseTemplate<TBuilder> : FuncTemplate<TBuilder> where TBuilder : MethodBaseTemplate<TBuilder>
    {
        /// <summary>
        /// 方法关键字
        /// </summary>
        protected string? _keyword;

        /// <summary>
        /// 方法关键字
        /// </summary>
        public string? Keyword => _keyword;

        /// <summary>
        /// 方法是否具有代码体，例如抽象方法就没有代码体
        /// </summary>
        public bool IsHasBlockCode => !string.IsNullOrEmpty(_blockCode);

        /// <summary>
        /// 方法的代码块
        /// </summary>
        protected string? _blockCode;

        /// <summary>
        /// 设置方法的关键字修饰符
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public TBuilder WithKeyword(MethodKeyword keyword = MethodKeyword.Default)
        {
            _keyword = EnumCache.View<MethodKeyword>(keyword);
            return (TBuilder)this;
        }


        /// <summary>
        /// 设置方法的关键字修饰符，如 static，virtual、override
        /// <para>拼错关键字代码可能会出现严重错误</para>
        /// </summary>
        /// <param name="keyword">static... </param>
        /// <returns></returns>
        public TBuilder WithKeyword(string keyword = "")
        {
            _keyword = keyword;
            return (TBuilder)this;
        }


        /// <summary>
        /// 方法体中的代码
        /// </summary>
        /// <param name="blockCode">方法体中的代码</param>
        /// <returns></returns>
        public TBuilder WithBlock(string? blockCode = null)
        {
            _blockCode = blockCode;
            return (TBuilder)this;
        }
    }
}