using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CZGL.CodeAnalysis
{
    public class MethodInfoAnalysis
    {
        private readonly MethodInfo _methodInfo;
        public MethodInfoAnalysis(MethodInfo methodInfo)
        {
            if (methodInfo is null)
                throw new ArgumentNullException(paramName: nameof(methodInfo), message: $"");
            _methodInfo = methodInfo;
        }

        /// <summary>
        /// 是否属于 runtime 的 MethodInfo
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPropertyOfAttr(MethodInfo method)
        {
            return method.GetCustomAttributes().Any(x => x.GetType() == typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute));
        }

        /// <summary>
        /// 去除属于 runtime 的 MethodInfo
        /// <para>编译器在编译时会加入很多 runtime 方法，这些不是程序员编写的代码</para>
        /// </summary>
        /// <param name="methodInfos"></param>
        /// <returns></returns>
        public static MethodInfo[] ExcludeSystemMethod(IEnumerable<MethodInfo> methodInfos)
        {
            HashSet<MethodInfo> list = new HashSet<MethodInfo>();
            foreach (var item in methodInfos)
            {
                if (IsPropertyOfAttr(item))
                    continue;
                list.Add(item);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 识别方法的访问权限
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public string GetVisibility(MethodInfo method)
        {
            return
                method.IsPublic ? "public" :
                method.IsPrivate ? "private" :
                method.IsAssembly ? "internal" :
                method.IsFamily ? "protected" :
                method.IsFamilyOrAssembly ? "protected internal" : throw new ArgumentNullException($"未能识别当前类型({method.Name})的访问权限");
        }

        /// <summary>
        /// 获取方法的修饰符
        /// <para>virtual override  abstract new</para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string Qualifier()
        {
            Type type = _methodInfo.DeclaringType;
            // 没有相应的信息，说明没有使用以上关键字修饰
            if (!_methodInfo.IsHideBySig)
                return string.Empty;

            // 是否抽象方法
            if (_methodInfo.IsAbstract)
                return "abstract";

            // virtual、override、实现接口的方法
            if (_methodInfo.IsVirtual)
            {
                // 实现接口的方法
                if (_methodInfo.IsFinal)
                    return string.Empty;
                // 没有被重写，则为 virtual
                if (_methodInfo.Equals(_methodInfo.GetBaseDefinition()))
                    return "virtual";
                else
                    return "override";
            }
            // new
            else
            {
                // 如果是当前类型中定义的方法，则只是一个普通的方法
                if (type == _methodInfo.DeclaringType)
                    return string.Empty;

                return "new";
            }
        }

        /// <summary>
        /// 是否异步方法
        /// </summary>
        public bool IsAsync()
        {
            return _methodInfo.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) == null ? false : true;
        }

        /// <summary>
        /// 获取返回类型
        /// </summary>
        /// <returns></returns>
        public string GetReturn()
        {
            Type returnType = _methodInfo.ReturnType;
            ParameterInfo returnParam = _methodInfo.ReturnParameter;

            if (returnType == typeof(void))
                return "void";
            if (returnType.IsValueType)
            {
                // 判断是否 (type1,type2) 这样的返回
                if (returnParam.ParameterType.IsGenericType)
                {
                    Type[] types = returnParam.ParameterType.GetGenericArguments();
                    string str = "(";
                    for (int i = 0; i < types.Length; i++)
                    {
                        str += types[i].Name;
                        if (i < types.Length - 1)
                            str += ",";
                    }
                    str += ")";
                    return str;
                }
                // 泛型
                else if (returnType.IsGenericType)
                {
                    return GenericeAnalysis.Instance.Analysis(returnType);
                }
                return returnType.Name;
            }
            // 暂时不处理数组
            // 泛型
            if (returnType.IsGenericType)
            {
                return GenericeAnalysis.Instance.Analysis(returnType);
            }
            return returnType.Name;
        }

        /// <summary>
        /// 是否为泛型方法
        /// </summary>
        /// <returns></returns>
        public bool IsIsGenericMethod()
        {
            return _methodInfo.IsGenericMethod;
        }

        /// <summary>
        /// 判断方法是否为泛型方法，并且返回泛型名称
        /// </summary>
        /// <returns></returns>
        public string GetMethodName()
        {
            if (!_methodInfo.IsGenericMethod)
                return _methodInfo.Name;
            Type[] types = _methodInfo.GetGenericArguments();
            string str = _methodInfo.Name + "<";
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
        // 
        public  string GetParams(MethodInfo method)
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

        public  string InRefOut(ParameterInfo parameter)
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

        // 获取类型
        public  string GetParamType(ParameterInfo parameter)
        {
            string typeName = parameter.ParameterType.Name;
            if (typeName.EndsWith("&"))
                typeName = typeName.Substring(0, typeName.Length - 1);
            return typeName;
        }

        // 是否为可选参数，是否有默认值
        public  string HasValue(ParameterInfo parameter)
        {
            if (!parameter.IsOptional)
                return string.Empty;
            object value = parameter.DefaultValue;
            return " = " + value.ToString();
        }

        #endregion
    }
}
