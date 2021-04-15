using CZGL.CodeAnalysis.Shared;
using CZGL.Reflect.Units;
using System;
using System.Reflection;

namespace CZGL.Reflect
{
    /// <summary>
    /// 字段分析
    /// </summary>
    public static class FiledAnalysis
    {
        /// <summary>
        /// 获取字段的访问权限修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(this FieldInfo info)
        {
            return AccessAnalysis.GetAccess(info);
        }

        /// <summary>
        /// 获取修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static FieldKeyword GetKeyword(this FieldInfo info)
        {
            return KeywordAnalysis.GetFieldKeyword(info);
        }

        /// <summary>
        /// 获取特性列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetAttributes(this FieldInfo info)
        {
            return AttributeAnalysis.GetAttributes(info.GetCustomAttributesData());
        }

        /// <summary>
        /// 给一个字段设置值。
        /// <para>如果此字段是结构体字段，常规的 FieldInfo.SetValue() 无法更改原结构体的值，因为其不是引用类型。此 API 可以通过引用更改结构体的值</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(T obj,FieldInfo field,object value)
        {
            TypedReference tr = __makeref(obj);
            field.SetValueDirect(tr,obj);
        }

        ///// <summary>
        ///// 可以避免装箱拆箱
        ///// </summary>
        ///// <typeparam name="TType"></typeparam>
        ///// <typeparam name="TValue"></typeparam>
        ///// <param name="obj"></param>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        //public static void SetValue<TType,TValue>(TargetException obj,FieldInfo field,TValue value)
        //{
        //    if (obj.GetType() == typeof(int))
        //    {
        //        { __refvalue(__makeref(obj), int) = (int)666; }
        //    }
        //}
    }
}
