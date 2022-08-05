using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CZGL.Reflect
{
    /// <summary>
    /// 
    /// </summary>
    public static class MethodInfoAnalysis
    {
        /// <summary>
        /// 判断一个方法是否为 new 方法
        /// <para>不支持 new virtual</para>
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsNew(this MethodInfo method)
        {
            var baseType = method.GetBaseDefinition().DeclaringType;

            // 排除 object
            if (baseType == typeof(object))
                return false;

            // 如果不在此类中定义，则肯定不可能是 new
            if (baseType != method.DeclaringType)
                return false;

            // 方法肯定在此类中定义，判断带不带 new

            BindingFlags flags = BindingFlags.Default;
            if (method.IsStatic)
                flags = flags | BindingFlags.Static;
            else flags = flags | BindingFlags.Instance;

            if (method.DeclaringType.BaseType.GetMethod(method.Name, flags) != null)
                return true;

            return false;
        }

        /// <summary>
        /// 是否属于 runtime 的 MethodInfo
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsRuntime(this MethodInfo method)
        {
            return method.GetCustomAttributes().Any(x => x.GetType() == typeof(CompilerGeneratedAttribute));
        }

        /// <summary>
        /// 去除属于 runtime 的 MethodInfo
        /// <para>编译器在编译时会加入很多 runtime 方法，这些不是程序员编写的代码</para>
        /// </summary>
        /// <param name="methodInfos"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> ExcludeSystemMethod(this IEnumerable<MethodInfo> methodInfos)
        {
            foreach (var item in methodInfos)
            {
                if (IsRuntime(item))
                    continue;
                yield return item;
            }
        }

        /// <summary>
        /// 识别方法的访问权限
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static MemberAccess GetAccess(this MethodInfo method)
        {
            return AccessAnalysis.GetMethodAccess(method);
        }

        /// <summary>
        /// 获取方法关键字
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static MethodKeyword GetKeyword(this MethodInfo method)
        {
            return KeywordAnalysis.GetMethodKeyword(method);
        }

        /// <summary>
        /// 是否异步方法
        /// </summary>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) == null ? false : true;
        }

        /// <summary>
        /// 获取返回类型字符串
        /// </summary>
        /// <returns></returns>
        public static string GetReturn(this MethodInfo method)
        {
            Type returnType = method.ReturnType;
            ParameterInfo returnParam = method.ReturnParameter;

            if (returnType == typeof(void))
                return "void";

            // ref、ref readonly
            string code =
                method.ReturnParameter.Name.EndsWith("&") ?
                (method.GetCustomAttributes().Any(x => x.GetType().Name.Equals("IsReadOnlyAttribute"))
                ? "ref readonly" : "ref")
                : "";

            if (returnType.IsValueType)
            {
                // 判断是否 (type1,type2) 这样的返回
                if (returnParam.ParameterType.IsGenericType)
                {
                    Type[] types = returnParam.ParameterType.GetGenericArguments();
                    code = "(";
                    for (int i = 0; i < types.Length; i++)
                    {
                        code += types[i].Name;
                        if (i < types.Length - 1)
                            code += ",";
                    }
                    code += ")";
                    return code;
                }

                // 泛型
                else if (returnType.IsGenericType)
                {
                    return returnType.GetGenericeName();
                }
                return returnType.Name;
            }

            // 数组
            if (returnType.BaseType == typeof(System.Array))
                return returnType.Name + "[]";

            // 泛型
            if (returnType.IsGenericType)
            {
                return returnType.GetGenericeName();
            }
            return returnType.Name;
        }

        /// <summary>
        /// 判断方法是否为泛型方法，并且返回泛型名称
        /// </summary>
        /// <returns></returns>
        public static string GetGenericeName(this MethodInfo method)
        {
            if (!method.IsGenericMethod)
                return method.Name;
            Type[] types = method.GetGenericArguments();
            string str = method.Name + "<";
            for (int i = 0; i < types.Length; i++)
            {
                str += types[i].Name;
                if (i < types.Length - 1)
                    str += ",";
            }
            str += ">";
            return str;
        }

        #region 解析方法的参数

        /// <summary>
        /// 解析方法的参数并返回字符串
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetParams(this MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length == 0)
                return string.Empty;

            int length = parameters.Length - 1;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= length; i++)
            {
                str.Append(InRefOut(parameters[i]) + " ");
                // 这里不对复杂类型等做处理
                str.Append(GetParamType(parameters[i]) + " ");
                str.Append(parameters[i].Name);
                str.Append(HasValue(parameters[i]) + " ");
                if (i < length)
                    str.Append(",");
            }
            return str.ToString();
        }

        /// <summary>
        /// 判断参数是 in、out、ref、params 哪种修饰符
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string InRefOut(this ParameterInfo parameter)
        {
            // in、ref、out ，类型后面会带有 & 符号
            if (parameter.ParameterType.Name.EndsWith("&"))
            {
                return
                    parameter.IsIn ? "in" :
                    parameter.IsOut ? "out" :
                    "ref";
            }
            if (parameter.GetCustomAttributes().Any(x => x.GetType() == typeof(ParamArrayAttribute)))
                return "params";
            return string.Empty;
        }

        /// <summary>
        /// 获取参数的类型
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetParamType(this ParameterInfo parameter)
        {
            string typeName = parameter.ParameterType.Name;
            if (typeName.EndsWith("&"))
                typeName = typeName.Substring(0, typeName.Length - 1);
            return typeName;
        }

        /// <summary>
        /// 是否为可选参数，是否有默认值
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>为空则不是可选参数， 没有默认值</returns>
        public static string HasValue(this ParameterInfo parameter)
        {
            if (!parameter.IsOptional)
                return string.Empty;
            object value = parameter.DefaultValue;
            return " = " + value.ToString();
        }

        #endregion

        /// <summary>
        /// 是否为 ref readonly 方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsRefReadOnly(this MethodInfo method)
        {
            return method.GetCustomAttributes().Any(x => x.GetType().Name.Equals("IsReadOnlyAttribute"));
        }
    }
}
