using System;

namespace CCode
{
	/// <summary>
	/// 事件关键字
	/// </summary>
	[CLSCompliant(true)]
	public enum EventKeyword : int
	{
		/// <summary>
		/// 无
		/// </summary>
		[DefineName(Name = "")]
		Default = 0,

		/// <summary>
		/// 静态事件
		/// </summary>
		[DefineName(Name = "static")]
		Static = 1 << 1,

		/// <summary>
		/// virtual事件
		/// </summary>
		[DefineName(Name = "virtual")]
		Virtual = 1 << 2,

		/// <summary>
		/// 抽象事件
		/// </summary>
		[DefineName(Name = "abstract")]
		Abstract = 1 << 3,

		/// <summary>
		/// 密封事件
		/// </summary>
		[DefineName(Name = "sealed")]
		Sealed = 1 << 4
	}
}
