using System;

namespace CCode
{
	/// <summary>
	/// 结构体修饰符。
	/// </summary>
	[CLSCompliant(true)]
	public enum StructKeyword : int
	{
		/// <summary>
		/// None
		/// </summary>
		[DefineName(Name = "")]
		Default = 0,

		/// <summary>
		/// readonly
		/// </summary>
		[DefineName(Name = "readonly")]
		Readonly = 1,

		/// <summary>
		/// ref
		/// </summary>
		[DefineName(Name = "ref")]
		Ref = 1 << 1,

		/// <summary>
		/// readonly ref
		/// </summary>
		[DefineName(Name = "readonly ref")]
		ReadonlyRef = Readonly | Ref,

		/// <summary>
		/// record
		/// </summary>
		[DefineName(Name = "record")]
		Record = 1 << 3,

		/// <summary>
		/// readonly record
		/// </summary>
		[DefineName(Name = "readonly record")]
		ReadonlyRecord = 1 << 4,
	}
}
