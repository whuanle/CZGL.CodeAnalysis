using System;

namespace CCode
{
	/// <summary>
	/// 为枚举定义注释别名。
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	internal class DefineNameAttribute : Attribute
	{
		public DefineNameAttribute() { }
		public DefineNameAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
