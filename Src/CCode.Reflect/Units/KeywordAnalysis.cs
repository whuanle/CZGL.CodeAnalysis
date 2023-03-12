using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CCode.Reflect
{
    /// <summary>
    /// 获取成员修饰符。
    /// </summary>
    [CLSCompliant(true)]
    public class KeywordAnalysis
    {
        /// <summary>
        /// 获取字段修饰符。
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns>字段修饰符</returns>
        public static FieldKeyword GetKeyword(FieldInfo info)
        {
            if (info.IsLiteral)
                return FieldKeyword.Const;

            if (info.IsStatic && info.IsInitOnly)
                return FieldKeyword.StaticReadonly;

            bool isVolatile = info.GetRequiredCustomModifiers().Any(x => x == typeof(IsVolatile));

            if (info.IsStatic)
            {
                if (isVolatile) return FieldKeyword.VolatileStatic;
                return FieldKeyword.Static;
            }

            if (isVolatile) return FieldKeyword.Volatile;

            if (info.IsInitOnly)
                return FieldKeyword.Readonly;

            return FieldKeyword.Default;
        }

        /// <summary>
        /// 获取字段修饰符。
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns>修饰符名称</returns>
        public static string View(FieldInfo info)=> EnumCache.View<FieldKeyword>(GetKeyword(info));


        /// <summary>
        /// 判断方法的访问修饰符。
        /// </summary>
        /// <param name="info">字段</param>
        /// <returns><see cref="MethodKeyword"/></returns>
        public static MethodKeyword GetKeyword(MethodInfo info)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            MethodAttributes attributes = info.Attributes;

            // extern 方法
            if ((attributes | MethodAttributes.PinvokeImpl) == attributes)
            {
                return MethodKeyword.StaticExtern;
            }

            if (info.IsStatic)
            {
                // static
                return MethodKeyword.Static;
            }

            // abstract
            if ((attributes | MethodAttributes.Abstract) == attributes)
                return MethodKeyword.Abstract;

            // 继承接口的实现、virtual、new virtual
            if ((attributes | MethodAttributes.VtableLayoutMask) == attributes)
            {
                // 继承的接口实现
                if ((attributes | MethodAttributes.Final) == attributes)
                    return MethodKeyword.Default;

                return MethodKeyword.Virtual;
            }

            // override、sealed override
            if (info.IsVirtual)
            {
                // override
                if (info.IsFinal)
                    return MethodKeyword.SealedOverride;
                // new override
                else
                    return MethodKeyword.Override;
            }

            return MethodKeyword.Default;
        }

        /// <summary>
        /// 获取属性修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns><see cref="PropertyKeyword"/></returns>
        public static PropertyKeyword GetKeyword(PropertyInfo info)
        {
            MethodInfo method = info.GetMethod ?? throw new NullReferenceException("无法获取属性的信息");

            return GetKeyword(method).ToPropertyKeyword();
        }

        /// <summary>
        /// 获取类修饰符关键字
        /// </summary>
        /// <param name="type"></param>
        /// <returns><see cref="ClassKeyword"/></returns>
        public static ClassKeyword GetKeyword(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (type == typeof(Delegate))
                return ClassKeyword.Default;

            bool isSealed = type.IsSealed;
            bool isAbstract = type.IsAbstract;
            if (isSealed && !isAbstract)
                return ClassKeyword.Sealed;

            if (!isSealed && isAbstract)
                return ClassKeyword.Abstract;

            if (isSealed && isAbstract)
                return ClassKeyword.Static;

            return  ClassKeyword.Default;
        }

        /// <summary>
        /// 获取结构体的修饰符
        /// </summary>
        /// <param name="type">结构体类型</param>
        /// <returns><see cref="StructKeyword"/></returns>
        public StructKeyword GetStructKeyword(Type type)
        {
            return type.GetCustomAttributes()
                .Any(x => x.GetType().Name.Equals("IsReadOnlyAttribute"))
                ? StructKeyword.Readonly : StructKeyword.Default;
        }
    }
}