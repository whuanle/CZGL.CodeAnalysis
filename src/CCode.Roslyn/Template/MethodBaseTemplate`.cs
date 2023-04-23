using CCode.Roslyn.Template;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CCode.Roslyn
{
    /// <summary>
    /// 函数定义构建器模板 <see cref="DelegateTemplate"/>、<see cref="MethodTemplate{TBuilder}"/>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class MethodBaseTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : MethodBaseTemplate<TBuilder>
	{
		/// <summary>
		/// 返回类型
		/// </summary>
		protected TypeSyntax? _returnType;

		/// <summary>
		/// 返回类型
		/// </summary>
		public TypeSyntax? ReturnType => _returnType;

        /// <summary>
        /// 函数参数
        /// </summary>
        protected ParameterListSyntax? _parameters;

		/// <summary>
		/// 函数参数
		/// </summary>
		public ParameterListSyntax? Parameters => _parameters;

        /// <summary>
        /// 方法体代码
        /// </summary>
        protected BlockSyntax? _blockCode;

		/// <summary>
		/// 设置返回参数
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public virtual TBuilder WithReturn(string code)
		{
			_returnType = SyntaxFactory.ParseTypeName(code);
			return (TBuilder)this;
		}

		/// <summary>
		/// 设置函数参数
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public virtual TBuilder WithParameter(string code)
		{
			_parameters = SyntaxFactory.ParseParameterList(code);
			return (TBuilder)this;
		}

		/// <summary>
		/// 方法体中的代码
		/// </summary>
		/// <param name="blockCode">方法体中的代码</param>
		/// <returns></returns>
		public TBuilder WithBlock(string blockCode)
		{
            var newTokens = SyntaxFactory.ParseTokens(blockCode);
            var newBody = SyntaxFactory.Block();
            newBody = newBody.InsertTokensAfter(
              newBody.OpenBraceToken,
              newTokens
            );
            _blockCode = newBody;
			return (TBuilder)this;
		}
	}
}
