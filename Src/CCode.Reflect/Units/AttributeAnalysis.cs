using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CCode.Reflect
{
    /// <summary>
    /// 特性解析器。
    /// </summary>
    [CLSCompliant(true)]
    public static class AttributeAnalysis
    {
        /// <summary>
        /// 获得在当前类型上定义的特性注解定义。
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>特性定义</returns>
        public static IReadOnlyList<AttributeDefine> GetDefine(Type type)
        {
            IList<CustomAttributeData> attrs = type.GetCustomAttributesData();
            return ToDefines(attrs);
        }


        /// <summary>
        /// 获得在当前成员上定义的特性注解定义。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info">成员</param>
        /// <returns></returns>
        public static IReadOnlyList<AttributeDefine> GetDefine<T>(T info) where T : MemberInfo
        {
            IList<CustomAttributeData> attrs = info.GetCustomAttributesData();
            return ToDefines(attrs);
        }


        /// <summary>
        /// <see cref="CustomAttributeData"/> To <see cref="AttributeDefine"/>
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<AttributeDefine> ToDefines(IEnumerable<CustomAttributeData> attrs)
        {
            List<AttributeDefine> attDeifnes = new List<AttributeDefine>();
            foreach (var item in attrs)
            {
                var define = ToDefine(item);
                attDeifnes.Add(define);
            }
            return attDeifnes.ToArray();
        }

        /// <summary>
        /// <see cref="CustomAttributeData"/> To <see cref="AttributeDefine"/>
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AttributeDefine ToDefine(CustomAttributeData attr)
        {
            var nameLen = attr.AttributeType.Name.Length - nameof(Attribute).Length;
            var name = attr.AttributeType.Name.Remove(nameLen, nameof(Attribute).Length);
            AttributeDefine info = new AttributeDefine(name, attr.AttributeType);

            Debug.Assert(!info.Name.EndsWith(nameof(Attribute)));

            // 构造函数中的参数
            IList<CustomAttributeTypedArgument> constructors = attr.ConstructorArguments;
            // 具名参数
            IList<CustomAttributeNamedArgument> arguments = attr.NamedArguments;

            if (constructors.Count != 0)
            {
                info.ConstructorArguments = constructors.ToList();
            }
            if (arguments.Count != 0)
            {
                info.NamedArguments = arguments.ToDictionary(a => a.MemberInfo, a => a.TypedValue);
            }

            return info;
        }
    }
}
