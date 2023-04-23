using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace CCode.Roslyn
{
	/// <summary>
	/// 基础类型模板，接口、类、结构体、枚举
	/// <para>定义能够被左式的类型，如 XXX x = ... ...</para>
	/// </summary>
	/// <typeparam name="TBuilder"></typeparam>
	public abstract class ObjectTemplate<TBuilder> : MemberTemplate<TBuilder>
		where TBuilder : ObjectTemplate<TBuilder>
	{
		/// <summary>
		/// 构建器模板
		/// </summary>
		protected readonly List<BaseTemplate> _baseTemplates = new List<BaseTemplate>();

		/// <summary>
		/// 设置访问修饰符(Access Modifiers)
		/// </summary>
		/// <param name="access">标记</param>
		/// <returns></returns>
		public virtual TBuilder WithAccess(NamespaceAccess access = NamespaceAccess.Internal)
		{
			switch (access)
			{
				case NamespaceAccess.Default:
					break;
				case NamespaceAccess.Internal:
					_accessToken = SyntaxFactory.Token(SyntaxKind.InternalKeyword);
					break;
				case NamespaceAccess.Public:
					_accessToken = SyntaxFactory.Token(SyntaxKind.PublicKeyword);
					break;
			}
			return (TBuilder)this;
		}

		/// <summary>
		/// 添加属性，方法，字段，事件，委托等定义
		/// </summary>
		/// <param name="template"></param>
		/// <returns></returns>
		public virtual TBuilder With(BaseTemplate template)
		{
			_baseTemplates.Add(template);
			return (TBuilder)this;
		}

		// TODO:通过字符串添加定义
	}
}
