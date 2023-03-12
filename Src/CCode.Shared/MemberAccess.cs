using System;

namespace CCode
{

	/// <summary>
	/// 命名空间中所有种类的成员的访问修饰符。
	/// <para>接口，枚举，嵌套类、属性、字段、方法等。</para>
	/// </summary>
	[CLSCompliant(true)]
	public enum MemberAccess : int
	{
		/// <summary>
		/// None
		/// </summary>
		[DefineName(Name = "")]
		Default = 0,

		/// <summary>
		/// internal
		/// </summary>
		[DefineName(Name = "internal")]
		Internal = 1,

		/// <summary>
		/// public
		/// </summary>
		[DefineName(Name = "public")]
		Public = 1 << 1,

		/// <summary>
		/// protected
		/// </summary>
		[DefineName(Name = "protected")]
		Protected = 1 << 2,

		/// <summary>
		/// private
		/// </summary>
		[DefineName(Name = "private")]
		Private = 1 << 3,

		/// <summary>
		/// protected internal
		/// </summary>
		[DefineName(Name = "protected internal")]
		ProtectedInternal = 1 << 4,

		/// <summary>
		/// private protected
		/// <para><b>只允许 C# 7.2 以上项目使用(C# 7.2 新增)。</b></para>
		/// </summary>
		[DefineName(Name = "private protected")]
		PrivateProtected = 1 << 5
	}
}
