using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCode.Roslyn
{
	/// <summary>
	/// 枚举创建模板
	/// </summary>
	/// <typeparam name="TBuilder"></typeparam>
	public abstract class EnumTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : EnumTemplate<TBuilder>
	{
		/// <summary>
		/// 枚举的字段
		/// </summary>
		/// <summary>
		/// 成员使用到的命名空间
		/// </summary>
		protected readonly HashSet<string> _fields = new HashSet<string>();

		/// <summary>
		/// 成员使用到的命名空间
		/// </summary>
		public IReadOnlyList<string> Fields => _fields.ToList();

		/// <summary>
		/// 通过代码形式添加一个枚举字段
		/// <para>也可同时为其添加特性</para>
		/// <code>
		/// [Display]
		/// First = 1
		/// </code>
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public virtual TBuilder WithField(string code)
		{
			_fields.Add(field);
			return (TBuilder)this;
		}

	}


	/// <summary>
	/// 方法构建模板
	/// </summary>
	/// <typeparam name="TBuilder"></typeparam>
	public abstract class MethodTemplate<TBuilder> : MethodBaseTemplate<TBuilder> where TBuilder : MethodTemplate<TBuilder>
	{
		/// <summary>
		/// 函数修饰符
		/// </summary>
		protected readonly HashSet<SyntaxToken> _methodTokens = new HashSet<SyntaxToken>();

		/// <summary>
		/// 设置方法的关键字修饰符
		/// </summary>
		/// <param name="keyword">关键字</param>
		/// <returns></returns>
		public virtual TBuilder WithKeyword(MethodKeyword keyword = MethodKeyword.Default)
		{
			_methodTokens.Clear();
			switch (keyword)
			{
				case MethodKeyword.Virtual:
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.VirtualKeyword));
					break;
				case MethodKeyword.Static:
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
					break;
				case MethodKeyword.Abstract:
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.AbstractKeyword));
					break;
				case MethodKeyword.Override:
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
					break;
				case MethodKeyword.SealedOverride:
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword));
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
					break;
				case MethodKeyword.StaticExtern:
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
					_methodTokens.Add(SyntaxFactory.Token(SyntaxKind.ExternKeyword));
					break;
				default: return (TBuilder)this;
			}

			return (TBuilder)this;
		}

		/// <summary>
		/// 关键字
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public virtual TBuilder WithKeyword(string code)
		{
			_methodTokens.Clear();
			var tokens = SyntaxFactory.ParseTokens(code);

			foreach (var token in tokens)
			{
				if (token.IsKeyword())
					_methodTokens.Add(token);
			}
			return (TBuilder)this;
		}
	}
}
