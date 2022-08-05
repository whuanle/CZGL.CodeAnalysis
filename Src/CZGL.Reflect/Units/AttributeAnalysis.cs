using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CZGL.Reflect
{
    /// <summary>
    /// 特性解析器。
    /// </summary>
    [CLSCompliant(true)]
    public static class AttributeAnalysis
    {
        /// <summary>
        /// 获取当前类型的每个特性的定义。
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>特性定义</returns>
        public static IReadOnlyList<AttributeDefine> GetDefine(Type type)
        {
            IList<CustomAttributeData> attrs = type.GetCustomAttributesData();
            return ToDefines(attrs);
        }


        /// <summary>
        /// 获取当前成员的每个特性的定义。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info">成员</param>
        /// <returns></returns>
        public static IReadOnlyList<AttributeDefine> Getfine<T>(T info) where T : MemberInfo
        {
            IList<CustomAttributeData> attrs = info.GetCustomAttributesData();
            return ToDefines(attrs);
        }


        // CustomAttributeData to AttributeDefine
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReadOnlyList<AttributeDefine> ToDefines(IEnumerable<CustomAttributeData> attrs)
        {
            List<AttributeDefine> attDeifnes = new List<AttributeDefine>();
            foreach (var item in attrs)
            {
                var define = ToDefine(item);
                attDeifnes.Add(define);
            }
            return attDeifnes.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AttributeDefine ToDefine(CustomAttributeData attr)
        {
            Type attrType = attr.AttributeType;
            AttributeDefine info = new AttributeDefine(attrType.Name, attrType);

            Debug.Assert(!attrType.Name.EndsWith(nameof(Attribute)));

            // 构造函数中的参数
            IList<CustomAttributeTypedArgument> constructors = attr.ConstructorArguments;
            // 具名参数
            IList<CustomAttributeNamedArgument> arguments = attr.NamedArguments;

            if (constructors.Count != 0)
            {
                info.ConstructorParams = constructors.ToList();
            }
            if (arguments.Count != 0)
            {
                info.PropertyParams = arguments.Select(x => x.TypedValue).ToList();
            }

            return info;
        }
    }
}
