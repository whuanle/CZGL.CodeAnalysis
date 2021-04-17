using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CZGL.Reflect
{

    /// <summary>
    /// 特性反解器
    /// </summary>
    [CLSCompliant(true)]
    public static class AttributeAnalysis
    {
        /// <summary>
        /// 获取当前类型的特性信息列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>特性定义</returns>
        public static AttributeDefine[] GetAttributeDefine(this Type type)
        {
            IList<CustomAttributeData> attrs = type.GetCustomAttributesData();
            AttributeDefine[] infos = attrs.GetAttributesParams();
            return infos;
        }

        /// <summary>
        /// 获取成员的特性列表
        /// </summary>
        /// <param name="info"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string[] GetAttributes<T>(this T info) where T : MemberInfo
        {
            return info.GetCustomAttributesData().GetAttributes();
        }

        /// <summary>
        /// 解析输出类型、方法、属性、字段等特性
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static string[] GetAttributes(this IEnumerable<CustomAttributeData> attrs)
        {
            List<string> attrResult = new List<string>();
            foreach (var item in attrs)
            {
                StringBuilder str = new StringBuilder("[");
                str.Append(item.AttributeType.Name);
                // 构造函数中的参数值
                IList<CustomAttributeTypedArgument> customs = item.ConstructorArguments;
                // 属性的参数值
                IList<CustomAttributeNamedArgument> arguments = item.NamedArguments;

                /*
                 * Test[{构造函数参数},{属性参数}]
                 */

                // 没有任何值
                if (customs.Count == 0 && arguments.Count == 0)
                {
                    attrResult.Add(str + "]");
                    continue;
                }

                str.Append('(');
                if (customs.Count != 0)
                    str.Append(string.Join(",", customs.ToArray()));
                // 如果构造函数参数和属性参数都有，则需要使用 , 隔开
                if (customs.Count != 0 && arguments.Count != 0)
                    str.Append(',');

                if (arguments.Count != 0)
                    str.Append(string.Join(",", arguments.ToArray()));

                str.Append(')');
                attrResult.Add(str.ToString());
            }
            return attrResult.ToArray();
        }

        /// <summary>
        /// 解析输出类型、方法、属性、字段等特性，生成字符串
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AttributeDefine[] GetAttributesParams(this IEnumerable<CustomAttributeData> attrs)
        {
            List<AttributeDefine> attrResult = new List<AttributeDefine>(); ;
            foreach (var item in attrs)
            {
                Type attrType = item.GetType();
                AttributeDefine info = new AttributeDefine()
                {
                    AttributeType = item.AttributeType,
                    Name = item.AttributeType.Name
                };

                // 构造函数中的值
                IList<CustomAttributeTypedArgument> customs = item.ConstructorArguments;
                // 属性的值
                IList<CustomAttributeNamedArgument> arguments = item.NamedArguments;

                // 没有任何值
                if (customs.Count == 0 && arguments.Count == 0)
                    continue;

                if (customs.Count != 0)
                {
                    info.ConstructParams = customs.ToArray();
                }
                if (arguments.Count != 0)
                {
                    info.PropertyParams = arguments.Select(x => x.TypedValue).ToArray();
                }
                attrResult.Add(info);
            }
            return attrResult.ToArray();
        }
    }
}
