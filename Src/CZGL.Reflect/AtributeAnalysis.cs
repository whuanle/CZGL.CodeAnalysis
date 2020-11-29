using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CZGL.Dynamic
{
    public class AtributeAnalysis
    {
        /// <summary>
        /// 获取枚举携带的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetName<T>(T t) where T : Enum
        {
            Type type = t.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, t));
            if (field == null) return null;
            return GetDisplayNameValue(field.GetCustomAttributesData());
        }

        /// <summary>
        /// 获取 [Display] 特性的属性 Name 的值
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        private static string GetDisplayNameValue(IList<CustomAttributeData> attrs)
        {
            if (attrs == null || attrs.Count == 0)
                return null;
            var argument = attrs.FirstOrDefault(x => x.AttributeType.Name == nameof(MemberDefineNameAttribute)).NamedArguments;
            return argument.FirstOrDefault(x => x.MemberName == nameof(MemberDefineNameAttribute.Name)).TypedValue.Value.ToString();
        }
    }
}
