using CZGL.Reflect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CZGL.Reflect.Units
{

    /// <summary>
    /// 特性反解器
    /// </summary>
    public static class AttributeAnalysis
    {
        /// <summary>
        /// 获取当前类型的特性信息列表
        /// </summary>
        /// <returns></returns>
        public static AttributeDefine[] GetAttributeDefine(this Type type)
        {
            IList<CustomAttributeData> attrs = type.GetCustomAttributesData();
            AttributeAnalysisInfo[] infos = GetAttributesParams(attrs);
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
            return GetAttributes(info.GetCustomAttributesData());
        }

        /// <summary>
        /// 解析输出类型、方法、属性、字段等特性
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static string[] GetAttributes(this IEnumerable<CustomAttributeData> attrs)
        {
            List<string> attrResult = new List<string>(); ;
            foreach (var item in attrs)
            {
                Type attrType = item.GetType();
                string str = "[";
                str += item.AttributeType.Name;
                // 构造函数中的值
                IList<CustomAttributeTypedArgument> customs = item.ConstructorArguments;
                // 属性的值
                IList<CustomAttributeNamedArgument> arguments = item.NamedArguments;

                // 没有任何值
                if (customs.Count == 0 && arguments.Count == 0)
                {
                    attrResult.Add(str + "]");
                    continue;
                }
                str += "(";
                if (customs.Count != 0)
                {
                    str += string.Join(",", customs.ToArray());
                }
                if (customs.Count != 0 && arguments.Count != 0)
                    str += ",";

                if (arguments.Count != 0)
                {
                    str += string.Join(",", arguments.ToArray());
                }
                str += ")";
                attrResult.Add(str);
            }
            return attrResult.ToArray();
        }

        /// <summary>
        /// 解析输出类型、方法、属性、字段等特性，生成字符串
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        private static AttributeAnalysisInfo[] GetAttributesParams(this IEnumerable<CustomAttributeData> attrs)
        {
            List<AttributeAnalysisInfo> attrResult = new List<AttributeAnalysisInfo>(); ;
            foreach (var item in attrs)
            {
                Type attrType = item.GetType();
                AttributeAnalysisInfo info = new AttributeAnalysisInfo()
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
