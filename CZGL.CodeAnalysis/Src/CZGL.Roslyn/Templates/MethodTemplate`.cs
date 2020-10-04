using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class MethodTemplate<TBuilder> : FuncTemplate<TBuilder> where TBuilder : MethodTemplate<TBuilder>
    {
        protected internal MethodState _method = new MethodState();

        /// <summary>
        /// 设置方法的关键字修饰符，如 static，virtual、override
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public TBuilder WithKeyword(MethodKeyword keyword=MethodKeyword.Default)
        {
            _method.Keyword = RoslynHelper.GetName(keyword);
            return _TBuilder;
        }


        /// <summary>
        /// 设置方法的关键字修饰符，如 static，virtual、override
        /// <para>拼错关键字代码可能会出现严重错误</para>
        /// </summary>
        /// <param name="keyword">static... </param>
        /// <returns></returns>
        public TBuilder WithKeyword(string keyword = "")
        {
            _method.Keyword = keyword;
            return _TBuilder;
        }


        /// <summary>
        /// 方法体中的代码
        /// <example>
        /// <code>
        /// int a = 0;
        /// Console.WriteLine(a);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="blockCode">方法体中的代码</param>
        /// <returns></returns>
        public TBuilder WithBlock(string blockCode = null)
        {
            _method.BlockCode = blockCode;
            return _TBuilder;
        }

        /// <summary>
        /// 定义此方法没有代码体，例如抽象方法
        /// </summary>
        /// <returns></returns>
        public TBuilder WithNullBlock()
        {
            _method.IsHasBlockCode = false;
            _method.BlockCode = null;
            return _TBuilder;
        }
    }
}