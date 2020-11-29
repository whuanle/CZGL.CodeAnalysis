using CZGL.CodeAnalysis.Models;
using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CZGL.Reflect.Units;

namespace CZGL.Reflect
{

    /// <summary>
    /// 专门用于解析泛型
    /// <para>支持解析泛型类型的泛型参数、泛型约束；方法的泛型参数和泛型约束；解析一个泛型类型；</para>
    /// </summary>
    public static class GenericeAnalysis
    {
        #region 重构的代码
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

        #endregion

        /// <summary>
        /// 获取泛型类型中的参数列表，如果泛型参数未定义，则列表数为 0
        /// <para>注意，只支持一层泛型</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetGenriceParams(this Type type)
        {
            if (!type.IsGenericType)
                return new string[0];

            return type.GetGenericArguments().Select(x => x.Name).ToArray();
        }

        // Test<int,int>，支持递归深入下一层
        /// <summary>
        /// 获取类型的泛型定义
        /// </summary>
        /// <param name="type"></param>
        /// <param name="definition">使用基础泛型类型定义</param>
        /// <returns></returns>
        public static string GetGenriceName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            // 基础泛型定义
            Type gType = type.GetGenericTypeDefinition();
            string code = $"{type.Name}<";

            List<string> vs = new List<string>();

            if (!type.IsGenericTypeDefinition)
                string.Join(",", gType.GetGenericArguments().Select(x=> GetInOut(x)+x.Name));

            foreach (var item in gType.GetGenericArguments())
            {
                if (item.IsGenericType)
                    vs.Add(GetGenriceName(item));
            }

            return code+string.Join(",",vs) + ">";

            // 解析协变逆变
            string GetInOut(Type type1)
            {
                switch (type.GenericParameterAttributes)
                {
                    case GenericParameterAttributes.Contravariant:return "in";
                    case GenericParameterAttributes.Covariant:return "out";
                   default:return string.Empty;
                }
            }
        }

        /// <summary>
        /// 获取一个泛型类型中，指定参数的约束以及其位置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argumentName">泛型参数名称</param>
        /// <param name="definition">使用基础泛型类型定义</param>
        /// <returns></returns>
        public static IEnumerable<GenericeConstraintInfo> GetConstrain(this Type type, string argumentName)
        {
            // IsGenericTypeDefinition 指明是否泛型类

            Type gType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();

            var argument = gType.GetGenericArguments().FirstOrDefault(x => x.Name.Equals(argumentName));

            // IsGenericParameter    指明是不是泛型参数
            if (!argument.IsGenericParameter)
                return new GenericeConstraintInfo[0];

            return GetOneGenericParameterConstrains(argument);
        }

        /// <summary>
        /// 解析一个泛型参数的约束
        /// <para>要获得泛型参数，可以使用 Type.GetGenericArguments()</para>
        /// </summary>
        /// <param name="argument">此类型必须是泛型参数的参数类型</param>
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
        /// <param name="constraintType">此类型必须是泛型参数的约束列表</param>
        /// <param name="attributes">泛型参数约束枚举</param>
        /// <returns>Keyword 约束类型，Location 摆放位置、是否可组合，Value 代表值</returns>
        private static IEnumerable<GenericeConstraintInfo> GetOneConstrain(Type paramter, IEnumerable<Type> constraintType, GenericParameterAttributes attributes)
        {
            // struct unmanaged
            var only = IsStructOrUnmanaged(constraintType.ToArray(), attributes);
            if (only.IsThis) return new GenericeConstraintInfo[] { new GenericeConstraintInfo(only.Keyword, only.Location, only.Value) };

            List<GenericeConstraintInfo> list = new List<GenericeConstraintInfo>();
            var types = constraintType.GetEnumerator();

            //// new()
            //if((attributes|GenericParameterAttributes.DefaultConstructorConstraint)==attributes)
            //    list
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
                while(types.MoveNext())
                {
                    var item = types.Current; 
                    list.Add(
                        new GenericeConstraintInfo(item.IsInterface?GenericKeyword.Interface:GenericKeyword.TU,
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
        private static (bool IsThis, GenericKeyword Keyword,ConstraintLocation Location,string Value) IsStructOrUnmanaged(IEnumerable<Type> constraintType, GenericParameterAttributes attributes)
        {
            // struct 8,16
            // unmanaged 8,16
            if (attributes == (GenericParameterAttributes.NotNullableValueTypeConstraint | GenericParameterAttributes.DefaultConstructorConstraint))
            {
                // 枚举值为 8,16 时
                // 判断是 struct 还是 unamaged

                    if (constraintType.First().IsSecurityCritical)
                        return (true,GenericKeyword.Struct, ConstraintLocation.Only, "struct");
                    return (true, GenericKeyword.Struct, ConstraintLocation.Only, "unmanaged");
            }

            return (false, GenericKeyword.NoConstrant, ConstraintLocation.None, string.Empty);
        }

        #region 旧版本代码
        ///// <summary>
        ///// 获取一个类型的名称，如果此类型是泛型类型，则会输出器泛型参数名称
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="isFullName">是否显示泛型参数完整的命名空间</param>
        ///// <returns></returns>
        //public static string GetTypeName(Type type, bool isFullName = false)
        //{
        //    if (!type.IsGenericType)
        //        return default(string);

        //    if (type.IsGenericTypeDefinition)
        //        return BuilderGenerice(type, true, !isFullName);

        //    return BuilderGenerice(type, true, isFullName);
        //}



        //#region 解析一个类的泛型

        ///// <summary>
        ///// 解析一个类型的泛型，处理泛型参数和约束
        ///// </summary>
        ///// <param name="genericeType"></param>
        ///// <returns></returns>
        //public static GenericeParamterInfo[] GenericeParamterAnalysis(Type genericeType)
        //{
        //    if (!genericeType.IsGenericType)
        //        return default;

        //    bool isHasefineGenerice = !genericeType.IsGenericTypeDefinition;

        //    List<GenericeParamterInfo> paramterInfos = new List<GenericeParamterInfo>();

        //    Type[] types = genericeType.GetGenericArguments();

        //    foreach (var item in types)
        //    {
        //        Type[] constrainTypes;
        //        GenericParameterAttributes genricAttrs;
        //        bool isConstraint = GenericHasConstraint(item, out constrainTypes, out genricAttrs);

        //        paramterInfos.Add(new GenericeParamterInfo
        //        {
        //            IsHasefineGenerice = isHasefineGenerice,
        //            IsConstraint = GenericHasConstraint(item),
        //            Name = item.Name,
        //            FullName = item.Name,
        //            ParamterType = isHasefineGenerice ? item : null,
        //            Constraints = isConstraint ? GetGenericeConstraint(constrainTypes, genricAttrs) : null
        //        });
        //    }

        //    return paramterInfos.AsReadOnly().ToArray();
        //}


        //#region 解析类的泛型详细存储到类中

        ///// <summary>
        ///// 此泛型参数是否具有约束
        ///// </summary>
        ///// <param name="typeParameter">泛型中的参数</param>
        ///// <returns></returns>
        //public static bool GenericHasConstraint(Type typeParameter)
        //{
        //    return GenericHasConstraint(typeParameter, out _, out _);
        //}

        ///// <summary>
        ///// 此泛型参数是否具有约束，并返回约束以及约束的标识信息
        ///// </summary>
        ///// <param name="typeParameter">泛型中的参数</param>
        ///// <param name="constrainTypes">泛型约束，约束的类型</param>
        ///// <param name="genricAttrs">何种约束</param>
        ///// <returns></returns>
        //public static bool GenericHasConstraint(Type typeParameter, out Type[] constrainTypes, out GenericParameterAttributes genricAttrs)
        //{
        //    constrainTypes = typeParameter.GetGenericParameterConstraints();
        //    genricAttrs = typeParameter.GenericParameterAttributes;
        //    // 不具有任何约束
        //    if (constrainTypes.Length == 0 && genricAttrs == GenericParameterAttributes.None && typeParameter.GetCustomAttributes().ToArray().Length == 0)
        //        return false;

        //    return true;
        //}

        ///// <summary>
        ///// 获取一个泛型参数的约束
        ///// <para>调用时需要确保此泛型具有约束</para>
        ///// </summary>
        ///// <param name="typeParameter"></param>
        ///// <returns></returns>
        //public static GenericeConstraint[] GetGenericeConstraint(Type typeParameter, out bool isHasConstraint)
        //{
        //    Type[] constrainTypes;
        //    GenericParameterAttributes genricAttrs;
        //    isHasConstraint = GenericHasConstraint(typeParameter, out constrainTypes, out genricAttrs);

        //    if (isHasConstraint == false)
        //        return default;

        //    return GetGenericeConstraint(constrainTypes, genricAttrs);
        //}

        ///// <summary>
        ///// 获取一个泛型参数的约束
        ///// <para>调用时需要确保此泛型具有约束</para>
        ///// </summary>
        ///// <param name="typeParameter"></param>
        ///// <returns></returns>
        //private static GenericeConstraint[] GetGenericeConstraint(Type[] constrainTypes, GenericParameterAttributes genricAttrs)
        //{
        //    List<GenericeConstraint> cons = new List<GenericeConstraint>();

        //    // 只有一种约束的情况下
        //    // 符号条件有 staruct、unmanaged、<基类名>、<接口名>、T:U
        //    if (constrainTypes.Length == 1)
        //        cons.Add(_GenericConstraint(constrainTypes[0]));

        //    // 没有 Type 约束时
        //    else if (constrainTypes.Length == 0)
        //        cons.AddRange(_GenericConstraint(genricAttrs));

        //    // 类型约束和特殊标识约束一起时，最麻烦了
        //    // 他们有组合关系，又有顺序关系
        //    // 分为三个顺序阶段，黄色，蓝色，橙
        //    else
        //    {
        //        // 黄色
        //        bool a = IsHasYellow(constrainTypes);
        //        bool b = IsHasYellow(genricAttrs);
        //        if (a == true)
        //        {
        //            Type tmp = constrainTypes.FirstOrDefault(x => !x.IsInterface && x.IsSubclassOf(typeof(System.Object)));
        //            cons.Add(new GenericeConstraint(tmp.Name, ConstraintScheme.Yellow, tmp));
        //        }
        //        else if (b == true)
        //            cons.AddRange(_GenericConstraint(genricAttrs));

        //        // 蓝色
        //        cons.AddRange(_GenericConstraint(constrainTypes.Where(x => !(!x.IsInterface && x.IsSubclassOf(typeof(System.Object)))).ToArray()));

        //        // 橙色
        //        if (genricAttrs.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
        //            cons.AddRange(_GenericConstraint(genricAttrs));
        //    }

        //    return cons.AsReadOnly().ToArray();
        //}


        ///// <summary>
        ///// struct,<基类>,<接口>，T:U
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //private static GenericeConstraint _GenericConstraint(Type type)
        //{
        //    if (type.Name == "ValueType")
        //        return new GenericeConstraint
        //        {
        //            Name = "struct",
        //            ConstraintScheme = ConstraintScheme.Red
        //        };
        //    else if (type.IsInterface)
        //        return new GenericeConstraint
        //        {
        //            Name = AnalysisName(type),
        //            ConstraintType = type,
        //            ConstraintScheme = ConstraintScheme.Blue
        //        };
        //    else if (type.IsSubclassOf(typeof(Object)))
        //        return new GenericeConstraint
        //        {
        //            Name = AnalysisName(type),
        //            ConstraintType = type,
        //            ConstraintScheme = ConstraintScheme.Yellow
        //        };
        //    else
        //        return new GenericeConstraint
        //        {
        //            Name = AnalysisName(type),
        //            ConstraintScheme = ConstraintScheme.Blue
        //        };
        //}

        ///// <summary>
        ///// 多种组合条件,struct,<基类>,<接口>，T:U
        ///// </summary>
        ///// <param name="types"></param>
        ///// <returns></returns>
        //private static GenericeConstraint[] _GenericConstraint(Type[] types)
        //{
        //    List<GenericeConstraint> cons = new List<GenericeConstraint>();
        //    int length = types.Length - 1;
        //    for (int i = 0; i <= length; i++)
        //    {
        //        // 约束也可以是泛型，这里不做处理
        //        if (types[i].Name == "ValueType")
        //            cons.Add(new GenericeConstraint("struct", ConstraintScheme.Red));
        //        else
        //            cons.Add(new GenericeConstraint($"{AnalysisName(types[i])}", types[i].IsInterface ? ConstraintScheme.Blue : ConstraintScheme.Yellow)); // 检查类是泛型的话
        //    }
        //    return cons.AsReadOnly().ToArray();
        //}

        ///// <summary>
        ///// 单个或多个组合条件, class、notnull、new()
        ///// </summary>
        ///// <param name="attributes"></param>
        ///// <returns></returns>
        //private static GenericeConstraint[] _GenericConstraint(GenericParameterAttributes attributes)
        //{
        //    /*
        //     * 下面是不同约束的结果
        //     * class：ReferenceTypeConstraint
        //     * notnull：None
        //     * unmanaged：NotNullableValueTypeConstraint, DefaultConstructorConstraint
        //     * new()：DefaultConstructorConstraint
        //     */
        //    switch (attributes)
        //    {
        //        case GenericParameterAttributes.ReferenceTypeConstraint:
        //            return new GenericeConstraint[] { new GenericeConstraint("class", ConstraintScheme.Yellow) };
        //        case GenericParameterAttributes.None:
        //            return new GenericeConstraint[] { new GenericeConstraint("notnull", ConstraintScheme.Yellow) };
        //        case GenericParameterAttributes.DefaultConstructorConstraint:
        //            return new GenericeConstraint[] { new GenericeConstraint("new()", ConstraintScheme.Orange) };
        //        // 多种组合条件时
        //        default:
        //            List<GenericeConstraint> cons = new List<GenericeConstraint>();
        //            if (attributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
        //                cons.Add(new GenericeConstraint("class", ConstraintScheme.Yellow));
        //            if (attributes.HasFlag(GenericParameterAttributes.None))
        //                cons.Add(new GenericeConstraint("notnull", ConstraintScheme.Yellow));
        //            if (attributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
        //                cons.Add(new GenericeConstraint("new()", ConstraintScheme.Orange));
        //            return cons.AsReadOnly().ToArray();
        //    }
        //}


        //#endregion






        ///// <summary>
        ///// 解析泛型的参数
        ///// <para>结果如 <int,int></para>
        ///// </summary>
        ///// <param name="isFullName">参数类型是否显示详细的名称，即显示详细的命名空间，FullName</param>
        ///// <returns></returns>
        //public static string Analysis(Type _genericeType, bool isFullName = false)
        //{
        //    if (!_genericeType.IsGenericType)
        //        return default(string);

        //    if (_genericeType.IsGenericTypeDefinition)
        //        return BuilderGenerice(_genericeType, true, !isFullName);

        //    return BuilderGenerice(_genericeType, true, isFullName);
        //}

        ///// <summary>
        ///// 解析泛型类型，结果带泛型类型的名称，如果不是泛型则返回类型名称
        ///// <para>结果如 Test<int></para>
        ///// </summary>
        ///// <param name="isFullName">参数类型是否显示详细的名称，即显示详细的命名空间，FullName</param>
        ///// <returns></returns>
        //public static string AnalysisName(Type _genericeType, bool isFullName = false)
        //{
        //    if (!_genericeType.IsGenericType)
        //        return isFullName ? _genericeType.FullName : _genericeType.Name;

        //    if (_genericeType.IsGenericTypeDefinition)
        //        return GetGenericeName(_genericeType, isFullName) + BuilderGenerice(_genericeType, true, !isFullName);

        //    return GetGenericeName(_genericeType, isFullName) + BuilderGenerice(_genericeType, true, isFullName);
        //}











        ///// <summary>
        ///// 处理一个泛型里面的参数，生成字符串
        ///// <para></para>
        ///// </summary>
        ///// <param name="genericType">泛型类型</param>
        ///// <param name="first">是否最外层，如果为最外层的话，会忽略类型名称</param>
        ///// <returns></returns>
        //private static string BuilderGenerice(Type genericType, bool first = true, bool isFullName = false)
        //{
        //    string str = (first ? string.Empty : GetGenericeName(genericType, isFullName))
        //        + "<";
        //    var types = genericType.GetGenericArguments();
        //    for (int i = 0; i < types.Length; i++)
        //    {
        //        str += types[i].IsGenericType ? BuilderGenerice(types[i], false, isFullName) : (isFullName ? types[i].FullName : TypeDictionary.FindAlias(types[i]));
        //        if (i < types.Length - 1)
        //            str += ", ";
        //    }
        //    str += ">";
        //    return str;
        //}

        ///// <summary>
        ///// 获取处理泛型的名称
        ///// <para>可以处理泛型类的，获取不带特殊字符的名称</para>
        ///// </summary>
        ///// <param name="_genericeType">泛型</param>
        ///// <param name="isFullName">是否显示命名空间前缀</param>
        ///// <returns></returns>
        //public static string GetGenericeName(Type _genericeType, bool isFullName = false)
        //{
        //    return (isFullName ? _genericeType.GetGenericTypeDefinition().FullName : _genericeType.GetGenericTypeDefinition().Name).TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '&').TrimEnd('`');
        //}



        ///// <summary>
        ///// 解析一个类型的泛型约束
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static string GetGenericConstraintString(Type type)
        //{
        //    if (!type.IsGenericType)
        //        return type.Name;

        //    StringBuilder className = new StringBuilder();
        //    Type[] types = ((System.Reflection.TypeInfo)type).GenericTypeParameters;

        //    foreach (var item in types)
        //    {
        //        string str = "where ";
        //        str += item.Name + " : ";

        //        Type[] genericTypes = item.GetGenericParameterConstraints();
        //        GenericParameterAttributes genricAttrs = item.GenericParameterAttributes;

        //        int length = genericTypes.Length - 1;
        //        // 不具有任何约束
        //        if (genericTypes.Length == 0 && genricAttrs == GenericParameterAttributes.None && item.GetCustomAttributes().ToArray().Length == 0)
        //            str = String.Empty;


        //        // 只有一种约束的情况下
        //        // 符号条件有 staruct、unmanaged、<基类名>、<接口名>、T:U
        //        else if (genericTypes.Length == 1)
        //            str += GetGenericType(genericTypes[0]).Item1;

        //        // 没有 Type 约束时
        //        else if (genericTypes.Length == 0)
        //            str += GetGenericType(genricAttrs).Item1;

        //        // 类型约束和特殊标识约束一起时，最麻烦了
        //        // 他们有组合关系，又有顺序关系
        //        // 分为三个顺序阶段，黄色，蓝色，橙
        //        else
        //        {
        //            List<string> color = new List<string>();


        //            // 黄色
        //            bool a = IsHasYellow(genericTypes);
        //            bool b = IsHasYellow(genricAttrs);
        //            if (a == true)
        //                color.Add(genericTypes.FirstOrDefault(x => !x.IsInterface && x.IsSubclassOf(typeof(System.Object))).Name);
        //            else if (b == true)
        //                color.Add(GetGenericType(genricAttrs).Item1);
        //            else color.Add(string.Empty);

        //            // 蓝色
        //            color.Add(GetGenericType(genericTypes.Where(x => !(!x.IsInterface && x.IsSubclassOf(typeof(System.Object)))).ToArray()));

        //            // 橙色
        //            if (genricAttrs.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
        //                color.Add(GetGenericType(genricAttrs).Item1);

        //            str += string.Join(",", color.ToArray());
        //        }
        //        if (str != string.Empty)
        //            className.AppendLine(str + " ");
        //    }
        //    return className.ToString();
        //}


        //// 枚举值为 8,16 时
        //// 判断是 struct 还是 unamaged
        //// 红色
        //private static string GetStructUnmanaged(Type type)
        //{
        //    if (type.IsSecurityCritical)
        //        return "struct";
        //    return "unmanaged";
        //}

        //// 枚举值为 4 时
        //// 黄色
        //private static string GetClass()
        //{
        //    return "class";
        //}

        ///// <summary>
        ///// 判断是否有黄色
        ///// </summary>
        ///// <returns></returns>
        //private static bool IsHasYellow(Type[] types)
        //{
        //    return types.Any(x => !x.IsInterface && x.IsSubclassOf(typeof(System.Object)));
        //}

        ///// <summary>
        ///// 判断是否有黄色
        ///// </summary>
        ///// <returns></returns>
        //private static bool IsHasYellow(GenericParameterAttributes attributes)
        //{
        //    return
        //        attributes == GenericParameterAttributes.ReferenceTypeConstraint ||
        //        attributes == GenericParameterAttributes.None ?
        //        true : false;
        //}

        ///// <summary>
        ///// struct,<基类>,<接口>，T:U
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //private static (string, ConstraintScheme) GetGenericType(Type type)
        //{
        //    // 约束也可以是泛型，这里不做处理
        //    if (type.Name == "ValueType")
        //        return ("struct", ConstraintScheme.Red);
        //    else if (type.IsInterface)
        //        return (AnalysisName(type), ConstraintScheme.Blue);
        //    else if (type.IsSubclassOf(typeof(Object)))
        //    {
        //        return (AnalysisName(type), ConstraintScheme.Yellow);
        //    }
        //    else
        //        return (AnalysisName(type), ConstraintScheme.Blue);
        //}

        ///// <summary>
        ///// 多种组合条件,struct,<基类>,<接口>，T:U
        ///// </summary>
        ///// <param name="types"></param>
        ///// <returns></returns>
        //private static string GetGenericType(Type[] types)
        //{
        //    int length = types.Length - 1;
        //    string str = "";
        //    for (int i = 0; i <= length; i++)
        //    {
        //        // 约束也可以是泛型，这里不做处理
        //        if (types[i].Name == "ValueType")
        //            str += "struct";
        //        else
        //            str += $"{AnalysisName(types[i])}"; // 检查类是泛型的话

        //        if (i < length)
        //            str += ",";
        //    }
        //    return str;
        //}

        ///// <summary>
        ///// 单个或多个组合条件, class、notnull、new()
        ///// </summary>
        ///// <param name="attributes"></param>
        ///// <returns></returns>
        //private static (string, ConstraintScheme) GetGenericType(GenericParameterAttributes attributes)
        //{
        //    /*
        //     * 下面是不同约束的结果
        //     * class：ReferenceTypeConstraint
        //     * notnull：None
        //     * unmanaged：NotNullableValueTypeConstraint, DefaultConstructorConstraint
        //     * new()：DefaultConstructorConstraint
        //     */
        //    switch (attributes)
        //    {
        //        case GenericParameterAttributes.ReferenceTypeConstraint:
        //            return ("class", ConstraintScheme.Yellow);
        //        case GenericParameterAttributes.None:
        //            return ("notnull", ConstraintScheme.Yellow);
        //        case GenericParameterAttributes.DefaultConstructorConstraint:
        //            return ("new()", ConstraintScheme.Orange);
        //        // 多种组合条件时
        //        default:
        //            string str = "";
        //            if (attributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
        //                str += "class,";
        //            if (attributes.HasFlag(GenericParameterAttributes.None))
        //                str += "notnull,";
        //            if (attributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
        //                str += "new(),";
        //            // 去除最后一个 , 号，
        //            return (str.Substring(0, str.Length - 1), ConstraintScheme.None);
        //    }
        //}
        //#endregion
        #endregion
    }
}
