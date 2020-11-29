using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using CZGL.Reflect;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CZGL.Reflect.Units
{
    /// <summary>
    /// 获取成员修饰符
    /// </summary>
    public class KeywordAnalysis
    {
        /// <summary>
        /// 获取字段修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static FieldKeyword GetFieldKeyword(FieldInfo info)
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
        /// 获取字段修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetFieldKeywordCode(FieldInfo info)
        {
            return EnumCache.GetValue(GetFieldKeyword(info));
        }


        /// <summary>
        /// 判断方法的访问修饰符
        /// <para>暂不支持 new static</para>
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static MethodKeyword GetMethodKeyword(MethodInfo info)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            // 函数是否在本类型中 实现
            bool isLocalDefinition = MethodInfoAnalysis.IsNew(info);
            // 是否使用了 new
            bool isHasNewSlot = ((info.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.NewSlot);

            MethodAttributes attributes = info.Attributes;

            MethodKeyword methodKeyword = MethodKeyword.Default;

            // abstract
            if ((attributes | MethodAttributes.Abstract) == attributes)
                return MethodKeyword.Abstract;


            if (info.IsStatic)
            {
                // static
                methodKeyword = methodKeyword | MethodKeyword.Static;
                // new static
                if (isHasNewSlot)
                    methodKeyword = methodKeyword | MethodKeyword.New;
                return methodKeyword;
            }

            // 继承接口的实现、virtual、new virtual
            if ((attributes | MethodAttributes.VtableLayoutMask) == attributes)
            {
                // 继承的接口实现
                if ((attributes | MethodAttributes.Final) == attributes)
                    return MethodKeyword.Default;

                if (isLocalDefinition)
                    return MethodKeyword.NewVirtual;

                return MethodKeyword.Virtual;

            }

            // override、sealed override
            if (info.IsVirtual)
            {
                // override
                if (info.IsFinal)
                    return MethodKeyword.Override;
                // new override
                else
                    return MethodKeyword.SealedOverride;
            }

            if (isLocalDefinition)
                return MethodKeyword.New;

            return MethodKeyword.Default;
        }

        /// <summary>
        /// 获取属性修饰符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static PropertyKeyword GetPropertyKeyword(PropertyInfo info)
        {
            MethodInfo method = info.GetGetMethod();
            if (method == null)
                method = info.GetSetMethod() ?? throw new NullReferenceException("无法获取属性的信息");

            return EnumCache.ToPropertyKeyword(GetMethodKeyword(method));
        }

        /// <summary>
        /// 获取类修饰符关键字
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ClassKeyword GetClassKeyword(Type type)
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

            return ClassAnalysis.IsNew(type) ? ClassKeyword.New : ClassKeyword.Default;
        }

        public StructKeyword GetStructKeyword(Type type)
        {
            return type.GetCustomAttributes()
                .Any(x => x.GetType().Name.Equals("IsReadOnlyAttribute"))
                ? StructKeyword.Readonly : StructKeyword.Default;
        }
    }
}