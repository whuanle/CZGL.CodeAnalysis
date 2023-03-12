using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCode.Reflect
{
	/// <summary>
	/// 特性定义信息。
	/// </summary>
	[CLSCompliant(true)]
	public class AttributeDefine
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="attributeType"></param>
		/// <param name="constructParams"></param>
		/// <param name="propertyParams"></param>
		public AttributeDefine(string name,
			Type attributeType,
			IReadOnlyList<CustomAttributeTypedArgument>? constructParams = null,
			IReadOnlyDictionary<MemberInfo, CustomAttributeTypedArgument>? propertyParams = null)
		{
			Name = name;
			AttributeType = attributeType;
			ConstructorArguments = constructParams;
			NamedArguments = propertyParams;
		}

		/// <summary>
		/// 特性的名称，不带 Attribute 后缀
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 特性类型
		/// </summary>
		public Type AttributeType { get; private set; }

		/// <summary>
		/// 特性构造函数中的值
		/// <para>ArgumentType、Value 属性分别表示构造函数中参数和参数值</para>
		/// </summary>
		public IReadOnlyList<CustomAttributeTypedArgument>? ConstructorArguments { get; private set; }

		/// <summary>
		/// 特性的属性或者字段
		/// </summary>
		public IReadOnlyDictionary<MemberInfo, CustomAttributeTypedArgument>? NamedArguments { get; private set; }

		/// <summary>
		/// 以字符串代码形式输出
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.View();
		}
	}
}
