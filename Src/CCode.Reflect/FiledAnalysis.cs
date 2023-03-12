using System;
using System.Reflection;

namespace CCode.Reflect
{
	/// <summary>
	/// 字段分析器
	/// </summary>
	[CLSCompliant(true)]
    public static class FiledAnalysis
    {
        /// <summary>
        /// 获取字段的访问权限修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(this FieldInfo info)
        {
            return AccessAnalysis.GetFieldAccess(info);
        }

        /// <summary>
        /// 获取修饰符
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns></returns>
        public static FieldKeyword GetKeyword(this FieldInfo info)
        {
            return KeywordAnalysis.GetKeyword(info);
        }

		///// <summary>
		///// 给一个字段设置值。
		///// <para>FieldInfo.SetValue() 无法更改原结构体的值，因为其不是引用类型。此 API 可以通过引用更改结构体的值</para>
		///// </summary>
		///// <param name="obj"></param>
		///// <param name="field"></param>
		///// <param name="value"></param>
		//public static void SetValue<T>(T obj, FieldInfo field, object value)
		//{
		//	TypedReference tr = __makeref(obj);
		//	field.SetValueDirect(tr, value);
		//}

		///// <summary>
		///// 可以避免装箱拆箱
		///// </summary>
		///// <typeparam name="TType"></typeparam>
		///// <typeparam name="TValue"></typeparam>
		///// <param name="obj"></param>
		///// <param name="field"></param>
		///// <param name="value"></param>
		//public static void SetValue<TType, TValue>(TType obj, FieldInfo field, TValue value)
		//{
		//	{ __refvalue(__makeref(obj), TValue) = value; }
		//}
	}
}
