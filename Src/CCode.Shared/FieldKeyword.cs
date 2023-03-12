using System;

namespace CCode
{
	/// <summary>
	/// 字段的关键字。
	/// <para>该枚举可组合，但不能随意组合，建议使用枚举中已定义的组合。</para>
	/// </summary>
	[CLSCompliant(true)]
	public enum FieldKeyword : int
	{
		/// <summary>
		/// 无
		/// </summary>
		[DefineName(Name = "")]
		Default = 0,

		/// <summary>
		/// 常量字段
		/// </summary>
		[DefineName(Name = "const")]
		Const = 1,

		/// <summary>
		/// 静态字段
		/// </summary>
		[DefineName(Name = "static")]
		Static = 1 << 1,

		/// <summary>
		/// 只读字段
		/// </summary>
		[DefineName(Name = "readonly")]
		Readonly = 1 << 2,

		/// <summary>
		/// 静态只读字段
		/// </summary>
		[DefineName(Name = "static readonly")]
		StaticReadonly = 1 << 3,

		/// <summary>
		/// volatile 字段
		/// </summary>
		[DefineName(Name = "volatile")]
		Volatile = 1 << 4,

		/// <summary>
		/// volatile static 修饰的字段
		/// </summary>
		[DefineName(Name = "volatile static")]
		VolatileStatic = 1 << 5
	}
}
