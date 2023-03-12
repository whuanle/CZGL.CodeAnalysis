using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CCode.Reflect
{
	/// <summary>
	/// 专门用于解析泛型<br />
	/// </summary>
	/// <remarks>
	/// <para>支持:<br />
	///     解析泛型类型的泛型参数、泛型约束；<br />
	///     方法的泛型参数和泛型约束；<br />
	///     解析一个泛型类型；</para>
	/// </remarks>
	[CLSCompliant(true)]
	public static class GenericeAnalysis
	{

		#region 名称

		/// <summary>
		/// 获取泛型定义字符串，包括约束
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetGenericeCode(this Type type)
		{
			var define = GetGenriceDefine(type);
			var contrain = GetConstrainCode(type, true);
			if (string.IsNullOrEmpty(contrain)) return define;
			return $"{define}\r\n{contrain}";
		}

		/// <summary>
		/// 获取泛型类原本的名称，即不包含泛型部分的名称。
		/// </summary>
		/// <remarks>例如 List`1 为 List </remarks>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetOriginName(this Type type)
		{
			if (!type.IsGenericType)
				return type.Name;

			return WipeOutName(type.Name);
		}

		/// <summary>
		/// 获取泛型方法原本的名称
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static string GetOriginName(this MethodInfo info)
		{
			if (!info.IsGenericMethod)
				return info.Name;

			return WipeOutName(info.Name);
		}

		#endregion

		/// <summary>
		/// 去除泛型名称中的特殊符号，然后输出正常定义的名称
		/// </summary>
		/// <param name="name">必须是泛型名称，如 List`1 </param>
		/// <returns></returns>
		private static string WipeOutName(string name)
		{
			return name
				.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '&')
				.TrimEnd('`');
		}

		/// <summary>
		/// 获取泛型类型的完整定义
		/// <para>获取示例<c> Test&lt;in T1, out T2, List&lt;T3&gt;&gt; </c></para>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetGenriceDefine(this Type type)
		{
			if (!type.IsGenericType)
				return type.Name;

			// 要识别出为定义泛型和已定义泛型
			// List<>、List<int>

			StringBuilder code = new StringBuilder($"{WipeOutName(type.Name)}<");
			List<string> vs = new List<string>();

			// 未定义泛型
			if (type.IsGenericTypeDefinition)
			{
				var gType = type.GetGenericTypeDefinition();
				code.Append(string.Join(", ", gType.GetGenericArguments().Select(x => GetInOut(x) + x.Name)));
			}
			else
			{
				foreach (var item in type.GetGenericArguments())
				{
					if (item.IsGenericType)
						vs.Add(GetInOut(item) + GetGenriceDefine(item));
					else
						vs.Add(GetInOut(item) + TypeAliasName.GetName(item));
				}
				code.Append(string.Join(", ", vs));
			}


			return code.Append('>').ToString();

			// 解析协变逆变
			string GetInOut(Type type1)
			{
				if (!type1.IsGenericParameter)
					return string.Empty;
				switch (type1.GenericParameterAttributes)
				{
					case GenericParameterAttributes.Contravariant: return "in";
					case GenericParameterAttributes.Covariant: return "out";
					default: return string.Empty;
				}
			}
		}

		/// <summary>
		/// 解析当前泛型类型的约束字符串
		/// </summary>
		/// <remarks>如：where T1: Enum,where T2 class,new() </remarks>
		/// <param name="type"></param>
		/// <param name="lineFeed">是否换行</param>
		/// <returns></returns>
		public static string GetConstrainCode(this Type type, bool lineFeed = false)
		{
			if (!type.IsGenericType)
				return string.Empty;

			Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
			Dictionary<string, IEnumerable<GenericeConstraintInfo>> dic = GetConstrainParams(gType);

			string lineChar = lineFeed ? " \r\n" : " ";

			StringBuilder stringBuilder = new StringBuilder();
			var data = dic.ToArray();
			var count = data.Count();
			for (int i = 0; i < count; i++)
			{
				if (data[i].Value.Count() == 0)
					continue;

				stringBuilder.Append("where" + $" {data[i].Key} : {string.Join(",", data[i].Value.Select(z => z.Value).ToArray())}");
				if (i < count)
					stringBuilder.Append(lineChar);
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// 解析一个泛型类型的所有泛型参数约束
		/// </summary>
		/// <remarks>返回每个参数对应的所有约束方式</remarks>
		/// <param name="type"></param>
		/// <returns>参数对应的约束条件</returns>
		public static Dictionary<string, IEnumerable<GenericeConstraintInfo>> GetConstrainParams(this Type type)
		{
			Dictionary<string, IEnumerable<GenericeConstraintInfo>> dic = new Dictionary<string, IEnumerable<GenericeConstraintInfo>>();
			if (!type.IsGenericType)
				return dic;
			Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
			var arguments = gType.GetGenericArguments();
			foreach (var argument in arguments)
			{
				dic.Add(argument.Name, GetConstrain(argument));
			}
			return dic;
		}

		#region 泛型约束解析器

		/// <summary>
		/// 解析泛型 一个参数的约束信息为字符串
		/// </summary>
		/// <remarks>如：public class Service&lt;T&gt; where T : class, new()<br /> 
		/// 获取： class, new()
		/// </remarks>
		/// <param name="infos"></param>
		/// <returns></returns>
		public static string GetWhereCode(this IEnumerable<GenericeConstraintInfo> infos)
		{
			// 只能唯一的
			if (infos.FirstOrDefault(x => x.Location == ConstraintLocation.Only) is var info)
				return info.Value;

			// "{First} {;1} {Any} {;2} {End}"
			const string Template = "{0}{1}{2}{3}{4}";
			// b
			var first = infos.FirstOrDefault(x => x.Location == ConstraintLocation.First);
			var end = infos.FirstOrDefault(x => x.Location == ConstraintLocation.End);
			var any = infos.Where(x => x.Location == ConstraintLocation.Any).ToArray() ?? Array.Empty<GenericeConstraintInfo>();
			var code = string.Format(Template,
				new object[]
				{
					WithSpace(first?.Value),
					(first == null || any.Length == 0) ? "" : ", ",
					WithSpace(string.Join(", ", any.Select(x => x.Value))),
					end != null && (first != null || end != null) ? ", " : "",
					WithSpace(end.Value)
				}
				);

			return code;
			string WithSpace(string? value)
			{
				if (string.IsNullOrEmpty(value)) return string.Empty;
				return " " + value;
			}
		}

		/// <summary>
		/// 获取一个泛型类型中，指定参数的约束定义
		/// </summary>
		/// <param name="type"></param>
		/// <param name="argumentName">泛型参数名称</param>
		/// <returns></returns>
		public static IEnumerable<GenericeConstraintInfo> GetConstrain(this Type type, string argumentName)
		{
			Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
			var argument = gType.GetGenericArguments().FirstOrDefault(x => x.Name.Equals(argumentName));

			return GetConstrain(gType);
		}

		/// <summary>
		/// 获取一个泛型类型中，指定参数的约束定义
		/// </summary>
		/// <param name="argumentType">泛型参数</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<GenericeConstraintInfo> GetConstrain(Type argumentType)
		{
			// IsGenericParameter    指明是不是泛型参数模板，如 T1、T2
			if (!argumentType.IsGenericParameter)
				return Array.Empty<GenericeConstraintInfo>();

			return GetOneGenericParameterConstrains(argumentType);
		}

		/// <summary>
		/// 解析一个泛型参数的约束
		/// <para>要获得泛型参数，可以使用 Type.GetGenericArguments()</para>
		/// </summary>
		/// <param name="argument">此类型必须是泛型参数的参数类型</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<GenericeConstraintInfo> GetOneGenericParameterConstrains(Type argument)
		{
			// 获取泛型参数约束列表
			var constraints = argument.GetGenericParameterConstraints();
			var target = argument.GenericParameterAttributes;
			var attrs = argument.GetCustomAttributes();
			// 不具有任何约束
			if (!constraints.Any()
				&& argument.GenericParameterAttributes == GenericParameterAttributes.None
				)
			{
				return Array.Empty<GenericeConstraintInfo>();
			}
			return GetOneConstrain(argument, constraints.AsEnumerable(), argument.GenericParameterAttributes);

		}


		/// <summary>
		/// 解析一个约束。必须配合两个参数才能正确解析约束类型以及值
		/// <para>要获取约束 Type，可以使用 Type.GetGenericParameterConstraints</para>
		/// </summary>
		/// <param name="paramter"></param>
		/// <param name="constraintType">此类型必须是泛型参数的约束列表</param>
		/// <param name="attributes">泛型参数约束枚举</param>
		/// <returns>Keyword 约束类型，Location 摆放位置、是否可组合，Value 代表值</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<GenericeConstraintInfo> GetOneConstrain(Type paramter, IEnumerable<Type> constraintType, GenericParameterAttributes attributes)
		{
			// struct unmanaged
			var only = IsStructOrUnmanaged(constraintType.ToArray(), attributes);
			if (only.IsThis) return new GenericeConstraintInfo[] { new GenericeConstraintInfo(only.Keyword, only.Location, only.Value) };

			List<GenericeConstraintInfo> list = new List<GenericeConstraintInfo>();
			var types = constraintType.GetEnumerator();

			#region 第一位置

			// classs
			if ((attributes | GenericParameterAttributes.ReferenceTypeConstraint) == attributes)
			{
				list.Add(new GenericeConstraintInfo(GenericKeyword.Class, ConstraintLocation.First, "class"));
				attributes = attributes ^ GenericParameterAttributes.ReferenceTypeConstraint;
			}

			// <baseclass>
			else if (constraintType.FirstOrDefault(x => !x.IsInterface && x != typeof(object)) is var baseClassType && baseClassType != null)
			{
				list.Add(new GenericeConstraintInfo(GenericKeyword.BaseClass, ConstraintLocation.First, GenericeAnalysis.GetGenriceDefine(baseClassType)));
				types.MoveNext();
			}

			// notnull
			else if ((attributes | GenericParameterAttributes.NotNullableValueTypeConstraint) == attributes)
			{
				list.Add(new GenericeConstraintInfo(GenericKeyword.Notnull, ConstraintLocation.First, "notnull"));
			}

			#endregion

			#region 中间位置

			if (constraintType.Any())
			{
				while (types.MoveNext())
				{
					var item = types.Current;
					list.Add(
						new GenericeConstraintInfo(item.IsInterface ? GenericKeyword.Interface : GenericKeyword.TU,
						ConstraintLocation.Any,
						GenericeAnalysis.GetGenriceDefine(item))
						);
				}
			}

			#endregion

			// new
			if ((attributes | GenericParameterAttributes.DefaultConstructorConstraint) == attributes)
			{
				list.Add(new GenericeConstraintInfo(GenericKeyword.New, ConstraintLocation.End, "new()"));
			}

			return list;
		}

		/// <summary>
		/// 判断是struct还是unmanaaged
		/// <para>进入条件：约束类型(constraintType)有且只有一个</para>
		/// </summary>
		/// <param name="constraintType"></param>
		/// <param name="attributes"></param>
		/// <returns>如果是这两种约束，则为true，并返回解析数据</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static (bool IsThis, GenericKeyword Keyword, ConstraintLocation Location, string Value) IsStructOrUnmanaged(IEnumerable<Type> constraintType, GenericParameterAttributes attributes)
		{
			// struct 8,16
			// unmanaged 8,16
			if (attributes == (GenericParameterAttributes.NotNullableValueTypeConstraint | GenericParameterAttributes.DefaultConstructorConstraint))
			{
				// 枚举值为 8,16 时
				// 判断是 struct 还是 unamaged

				if (constraintType.First().IsSecurityCritical)
					return (true, GenericKeyword.Struct, ConstraintLocation.Only, "struct");
				return (true, GenericKeyword.Struct, ConstraintLocation.Only, "unmanaged");
			}

			return (false, GenericKeyword.NoConstrant, ConstraintLocation.None, string.Empty);
		}
		#endregion
	}
}
