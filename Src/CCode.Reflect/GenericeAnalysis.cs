using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CCode.Reflect
{
    /// <summary>
    /// 专门用于解析泛型
    /// <para>支持解析泛型类型的泛型参数、泛型约束；方法的泛型参数和泛型约束；解析一个泛型类型；</para>
    /// </summary>
    [CLSCompliant(true)]
    public static class GenericeAnalysis
    {

        #region 名称

        /// <summary>
        /// 获取泛型类原本的名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOriginName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            return WipeOutName(type.Name);
        }

        /// <summary>
        /// 获取泛型方法原本的名称
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetOriginName(this MethodInfo info)
        {
            if (!info.IsGenericMethod)
                return info.Name;

            return WipeOutName(info.Name);
        }

        #endregion

        /// <summary>
        /// 去除泛型名称中的特殊符号，然后输出正常定义的名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string WipeOutName(string name)
        {
            return name
                .TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '&')
                .TrimEnd('`');
        }

        /// <summary>
        /// 获取泛型类型中的参数列表，如果泛型参数未定义，则列表数为 0
        /// <para>注意，只支持一层泛型</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IReadOnlyList<string> GetGenriceParams(this Type type)
        {
            if (!type.IsGenericType)
                return new string[0];

#if NETSTANDARD2_0
            return type.GetGenericArguments().Select(x =>
                                                    x.IsGenericParameter ? x.Name : TypeAliasName.GetName(x))
                                             .ToList();
#else
            return type.GetGenericArguments().Select(x =>
                                                    x.IsGenericTypeParameter ? x.Name : TypeAliasName.GetName(x))
                                             .ToList();
#endif
        }

        // Test<int,int>，支持递归深入下一层

        /// <summary>
        /// 获取类型的泛型定义。
        /// <para>获取示例 Test{int,int,List{int}}></para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenriceName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            // 要识别出为定义泛型和已定义泛型
            // List<>、List<int>

            StringBuilder code = new StringBuilder($"{WipeOutName(type.Name)}<");
            List<string> vs = new List<string>();

            // 未定义泛型
            if (type.IsGenericTypeDefinition)
            {
                var gType = type.GetGenericTypeDefinition();
                code.Append(string.Join(", ", gType.GetGenericArguments().Select(x => GetInOut(x) + x.Name)));
            }
            else
            {
                foreach (var item in type.GetGenericArguments())
                {
                    if (item.IsGenericType)
                        vs.Add(GetInOut(item) + GetGenriceName(item));
                    else
                        vs.Add(GetInOut(item) + TypeAliasName.GetName(item));
                }
                code.Append(string.Join(", ", vs));
            }


            return code.Append('>').ToString();

            // 解析协变逆变
            string GetInOut(Type type1)
            {
                if (!type1.IsGenericParameter)
                    return string.Empty;
                switch (type1.GenericParameterAttributes)
                {
                    case GenericParameterAttributes.Contravariant: return "in";
                    case GenericParameterAttributes.Covariant: return "out";
                    default: return string.Empty;
                }
            }
        }

        /// <summary>
        /// 解析当前泛型类型的约束字符串
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lineFeed">是否换行</param>
        /// <returns></returns>
        public static string GetConstrainCode(this Type type, bool lineFeed = false)
        {
            if (!type.IsGenericType)
                return string.Empty;

            Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
            Dictionary<string, IEnumerable<GenericeConstraintInfo>> dic = GetConstrains(gType);

            string lineChar = lineFeed ? " \r\n" : " ";

            StringBuilder stringBuilder = new StringBuilder();
            var data = dic.ToArray();
            var count = data.Count();
            for (int i = 0; i < count; i++)
            {
                if (data[i].Value.Count() == 0)
                    continue;

                stringBuilder.Append("where" + $" {data[i].Key} : {string.Join(",", data[i].Value.Select(z => z.Value).ToArray())}");
                if (i < count)
                    stringBuilder.Append(lineChar);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 解析一个泛型类型的所有泛型参数约束
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, IEnumerable<GenericeConstraintInfo>> GetConstrains(this Type type)
        {
            Dictionary<string, IEnumerable<GenericeConstraintInfo>> dic = new Dictionary<string, IEnumerable<GenericeConstraintInfo>>();
            if (!type.IsGenericType)
                return dic;
            Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
            var arguments = gType.GetGenericArguments();
            foreach (var argument in arguments)
            {
                dic.Add(argument.Name, GetConstrain(gType, argument));
            }
            return dic;
        }

#region 泛型约束解析器

        /// <summary>
        /// 解析泛型约束信息为字符串
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        public static string GetString(this IEnumerable<GenericeConstraintInfo> infos)
        {
            // 只能唯一的
            if (infos.FirstOrDefault(x => x.Location == ConstraintLocation.Only) is var info)
                return info.Value;

            const string Template = "{First} {;1} {Any} {;2} {End}";


            var first = infos.FirstOrDefault(x => x.Location == ConstraintLocation.First);
            var end = infos.FirstOrDefault(x => x.Location == ConstraintLocation.End);
            var any = infos.Where(x => x.Location == ConstraintLocation.Any).ToArray();
            var code = Template.Replace("{First}", first.Value)
                               .Replace("{Any}", string.Join(",", any.Select(x => x.Value).ToArray()))
                               .Replace("{End}", end.Value)
                               .Replace("{;1}", (first != null && any != null) ? "," : "")
                               .Replace("{;2}", (end != null && (first != null || any.Length != 0)) ? ";" : "");

            return code;
        }

        /// <summary>
        /// 获取一个泛型类型中，指定参数的约束以及其位置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argumentName">泛型参数名称</param>
        /// <returns></returns>
        public static IEnumerable<GenericeConstraintInfo> GetConstrain(this Type type, string argumentName)
        {
            Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
            var argument = gType.GetGenericArguments().FirstOrDefault(x => x.Name.Equals(argumentName));

            return GetConstrain(gType, argument);
        }

        /// <summary>
        /// 泛型约束解析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argumentType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<GenericeConstraintInfo> GetConstrain(Type type, Type argumentType)
        {
            // IsGenericParameter    指明是不是泛型参数模板，如 T1、T2
            if (!argumentType.IsGenericParameter)
                return new GenericeConstraintInfo[0];

            return GetOneGenericParameterConstrains(argumentType);
        }

        /// <summary>
        /// 解析一个泛型参数的约束
        /// <para>要获得泛型参数，可以使用 Type.GetGenericArguments()</para>
        /// </summary>
        /// <param name="argument">此类型必须是泛型参数的参数类型</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<GenericeConstraintInfo> GetOneGenericParameterConstrains(Type argument)
        {

            // 获取泛型参数约束列表
            var constraints = argument.GetGenericParameterConstraints();

            // 不具有任何约束
            if (!constraints.Any()
                && argument.GenericParameterAttributes == GenericParameterAttributes.None
                && !argument.GetCustomAttributes().Any())
                return new GenericeConstraintInfo[0];

            return GetOneConstrain(argument, constraints.AsEnumerable(), argument.GenericParameterAttributes);

        }


        /// <summary>
        /// 解析一个约束。必须配合两个参数才能正确解析约束类型以及值
        /// <para>要获取约束 Type，可以使用 Type.GetGenericParameterConstraints</para>
        /// </summary>
        /// <param name="paramter"></param>
        /// <param name="constraintType">此类型必须是泛型参数的约束列表</param>
        /// <param name="attributes">泛型参数约束枚举</param>
        /// <returns>Keyword 约束类型，Location 摆放位置、是否可组合，Value 代表值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<GenericeConstraintInfo> GetOneConstrain(Type paramter, IEnumerable<Type> constraintType, GenericParameterAttributes attributes)
        {
            // struct unmanaged
            var only = IsStructOrUnmanaged(constraintType.ToArray(), attributes);
            if (only.IsThis) return new GenericeConstraintInfo[] { new GenericeConstraintInfo(only.Keyword, only.Location, only.Value) };

            List<GenericeConstraintInfo> list = new List<GenericeConstraintInfo>();
            var types = constraintType.GetEnumerator();

#region 第一位置

            // classs
            if ((attributes | GenericParameterAttributes.ReferenceTypeConstraint) == attributes)
            {
                list.Add(new GenericeConstraintInfo(GenericKeyword.Class, ConstraintLocation.First, "class"));
                attributes = attributes ^ GenericParameterAttributes.ReferenceTypeConstraint;
            }

            // notnull
            else if (!constraintType.Any() && attributes == GenericParameterAttributes.None)
            {
                // notnull
                // System.Runtime.CompilerServices.NullableAttribute
                if (paramter.GetCustomAttributes().Any(x => x.GetType().Name.CompareTo("NullableAttribute") == 0))
                {
                    list.Add(new GenericeConstraintInfo(GenericKeyword.Notnull, ConstraintLocation.First, "notnull"));
                }
                else return new GenericeConstraintInfo[] { new GenericeConstraintInfo(GenericKeyword.NoConstrant, ConstraintLocation.None, string.Empty) };
            }

            // <baseclass>
            else if (constraintType.FirstOrDefault(x => !x.IsInterface && x != typeof(object)) is var baseClassType && baseClassType != null)
            {
                list.Add(new GenericeConstraintInfo(GenericKeyword.BaseClass, ConstraintLocation.First, GenericeAnalysis.GetGenriceName(baseClassType)));
                types.MoveNext();
            }

#endregion

#region 中间位置

            if (constraintType.Any())
            {
                while (types.MoveNext())
                {
                    var item = types.Current;
                    list.Add(
                        new GenericeConstraintInfo(item.IsInterface ? GenericKeyword.Interface : GenericKeyword.TU,
                        ConstraintLocation.Any,
                        GenericeAnalysis.GetGenriceName(item))
                        );
                }
            }

#endregion

            // new
            if ((attributes | GenericParameterAttributes.DefaultConstructorConstraint) == attributes)
            {
                list.Add(new GenericeConstraintInfo(GenericKeyword.New, ConstraintLocation.End, "new()"));
            }

            return list;
        }

        /// <summary>
        /// 判断是struct还是unmanaaged
        /// <para>进入条件：约束类型(constraintType)有且只有一个</para>
        /// </summary>
        /// <param name="constraintType"></param>
        /// <param name="attributes"></param>
        /// <returns>如果是这两种约束，则为true，并返回解析数据</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (bool IsThis, GenericKeyword Keyword, ConstraintLocation Location, string Value) IsStructOrUnmanaged(IEnumerable<Type> constraintType, GenericParameterAttributes attributes)
        {
            // struct 8,16
            // unmanaged 8,16
            if (attributes == (GenericParameterAttributes.NotNullableValueTypeConstraint | GenericParameterAttributes.DefaultConstructorConstraint))
            {
                // 枚举值为 8,16 时
                // 判断是 struct 还是 unamaged

                if (constraintType.First().IsSecurityCritical)
                    return (true, GenericKeyword.Struct, ConstraintLocation.Only, "struct");
                return (true, GenericKeyword.Struct, ConstraintLocation.Only, "unmanaged");
            }

            return (false, GenericKeyword.NoConstrant, ConstraintLocation.None, string.Empty);
        }
#endregion
    }
}
