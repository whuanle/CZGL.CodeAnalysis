using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CCode
{
	/// <summary>
	/// 枚举信息缓存。
	/// </summary>
	[CLSCompliant(true)]
	public static class EnumCache
	{
		#region 缓存
		private static readonly Cache _Cache = new Cache();

		private class Cache
		{
			private readonly IReadOnlyDictionary<string, string> _Cache;

			public string this[Enum e]
			{
				get
				{
					var name = Enum.GetName(e.GetType(), e);
					return _Cache[$"{e.GetType().Name}.{name}"];
				}
			}

			public Cache()
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				var types = typeof(EnumCache).Assembly.GetTypes().Where(x => x.IsEnum && !x.IsNested);

				foreach (var type in types)
				{
					var name = type.Name;
					foreach (var item in type.GetFields(BindingFlags.Static | BindingFlags.Public))
					{
						var attr = item.GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(DefineNameAttribute)) as DefineNameAttribute;
						if (attr is null) continue;
						dictionary.Add($"{name}.{item.Name}", attr!.Name);
					}
				}
				_Cache = dictionary;
			}
		}

		#endregion

		/// <summary>
		/// 获取 <see cref="ClassKeyword"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>类修饰符</returns>
		public static string View(ClassKeyword value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="RecordKeyword"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>类修饰符</returns>
		public static string View(RecordKeyword value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="StructKeyword"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>结构体修饰符</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(StructKeyword value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="EventKeyword"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>事件类型</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(EventKeyword value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="GenericKeyword"/>  枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>泛型类型</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(GenericKeyword value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="FieldKeyword"/>  枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>字段</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(FieldKeyword value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="MemberAccess"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>成员访问修饰符</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(MemberAccess value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="MemberType"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>成员类型</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(MemberType value) => _Cache[value];

		/// <summary>
		/// 获取 <see cref="MethodKeyword"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>方法修饰符</returns>
		public static string View(MethodKeyword value) => _Cache[value];


		/// <summary>
		/// 获取 <see cref="NamespaceAccess"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(NamespaceAccess value) => _Cache[value];


		/// <summary>
		/// 获取 <see cref="PropertyKeyword"/> 枚举代表的信息。
		/// </summary>
		/// <param name="value">枚举</param>
		/// <returns>属性修饰符</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string View(PropertyKeyword value) => _Cache[value];




		/// <summary>
		/// 获取某个枚举的代表的值。
		/// </summary>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns>修饰符或关键字</returns>
		public static string View<T>(T value) where T : Enum
		{
			return _Cache[value]; ;
		}

		/// <summary>
		/// 转换标识符。
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public static PropertyKeyword ToPropertyKeyword(this MethodKeyword keyword)
		{
			return (PropertyKeyword)((int)keyword);
		}

		/// <summary>
		/// 转换标识符。
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public static MethodKeyword ToMethodKeyword(this PropertyKeyword keyword)
		{
			return (MethodKeyword)((int)keyword);
		}

	}
}
