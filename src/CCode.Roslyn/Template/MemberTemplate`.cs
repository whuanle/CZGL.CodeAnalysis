using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Roslyn
{
	/// <summary>
	/// 命名空间内的成员共有的结构和操作
	/// <para>
	/// * {枚举}<br />
	/// * {委托}<br />
	/// * {事件}<br />
	/// * {结构体}<br />
	/// * {接口}<br />
	/// * {类}<br />
	/// *   -{字段}<br />
	/// *   -{属性}<br />
	/// *   -{方法}<br />
	/// *   -{其它成员}<br />
	/// </para>
	/// </summary>
	/// <typeparam name="TBuilder"></typeparam>
	public abstract class MemberTemplate<TBuilder> : BaseTemplate<TBuilder>
		where TBuilder : MemberTemplate<TBuilder>
	{
		/// <summary>
		/// 成员修饰符
		/// </summary>
		protected SyntaxToken? _accessToken;

		/// <summary>
		/// 设置成员访问修饰符
		/// </summary>
		/// <param name="access"></param>
		/// <returns></returns>
		public virtual TBuilder WithAccess(MemberAccess access = MemberAccess.Default)
		{
			SyntaxKind kind;
			switch (access)
			{
				case MemberAccess.Public: kind = SyntaxKind.PublicKeyword; break;
				case MemberAccess.Protected: kind = SyntaxKind.ProtectedKeyword; break;
				case MemberAccess.Internal: kind = SyntaxKind.InternalKeyword; break;
				case MemberAccess.Private: kind = SyntaxKind.PrivateKeyword; break;
				case MemberAccess.PrivateProtected: kind = SyntaxKind.PrivateKeyword; break;
				case MemberAccess.ProtectedInternal: kind = SyntaxKind.ProtectedKeyword; break;
				default: return (TBuilder)this;
			}
			_accessToken = SyntaxFactory.Token(kind);
			return (TBuilder)this;
		}

		/// <summary>
		/// 设置成员访问修饰符
		/// </summary>
		/// <param name="access"></param>
		/// <returns></returns>
		public virtual TBuilder WithAccess(string access = "private")
		{
			var token = SyntaxFactory.ParseToken(access);
			if (!token.IsKeyword()) throw new ArgumentException($"[ {access} ] 并非有效访问修饰符", nameof(access));
			_accessToken = token;
			return (TBuilder)this;
		}
	}
}
