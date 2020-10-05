using CZGL.CodeAnalysis.Models;
using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.CodeAnalysis
{
    /// <summary>
    /// 解析一个类型
    /// </summary>
    public class ClassAnalysis
    {
        protected GenericeAnalysis _genericeAnalysis;

        protected static readonly ConcurrentDictionary<Type, ClassDefineResult> _ClassInfos = new ConcurrentDictionary<Type, ClassDefineResult>();

        protected readonly ClassDefineResult _thisTypeInfo;

        /// <summary>
        /// 已经分析完成的类信息
        /// </summary>
        public ClassDefine ClassInfo => _thisTypeInfo;
        public ClassAnalysis(Type type)
        {
            bool isContainKey = _ClassInfos.TryGetValue(type, out _thisTypeInfo);
            if (!isContainKey)
            {
                _thisTypeInfo = new ClassDefineResult { ClassType = type };
                _ClassInfos.TryAdd(type, _thisTypeInfo);
            }
            _genericeAnalysis = GenericeAnalysis.Instance;
        }

        /// <summary>
        /// 初始化解析类型
        /// </summary>
        private void InitInfo()
        {
            Task.Factory.StartNew(() =>
            {
                if (_thisTypeInfo.ClassType.IsSealed && _thisTypeInfo.ClassType.IsAbstract)
                    _thisTypeInfo.IsStatic = true;
                else if (_thisTypeInfo.ClassType.IsSealed && !_thisTypeInfo.ClassType.IsAbstract)
                    _thisTypeInfo.IsSealed = true;
                else if (!_thisTypeInfo.ClassType.IsSealed && _thisTypeInfo.ClassType.IsAbstract)
                    _thisTypeInfo.IsAbstract = true;

                // 处理名称
                _thisTypeInfo.ClassName = _genericeAnalysis.GetGenericeName(_thisTypeInfo.ClassType);

                // 是否有继承
                _thisTypeInfo.BaseType = _thisTypeInfo.ClassType.BaseType == typeof(object) ? null : _thisTypeInfo.ClassType.BaseType;
                _thisTypeInfo.Interfaces = _thisTypeInfo.ClassType.GetInterfaces().Length == 0 ? null : _thisTypeInfo.ClassType.GetInterfaces();
                _thisTypeInfo.IsInherit = _thisTypeInfo.BaseType != null && _thisTypeInfo.Interfaces != null ? true : false;
                if (_thisTypeInfo.IsGenericType = _thisTypeInfo.ClassType.IsGenericType)
                {
                    _thisTypeInfo.GenericeParamters = _genericeAnalysis.GenericeParamterAnalysis(_thisTypeInfo.ClassType);
                }
            });
        }

        /// <summary>
        /// 是否密封类
        /// </summary>
        public bool IsSealed { get { return _thisTypeInfo.IsSealed; } }
        /// <summary>
        /// 是否抽象类
        /// </summary>
        public bool IsAbstract { get { return _thisTypeInfo.IsAbstract; } }
        /// <summary>
        /// 是否静态类
        /// </summary>
        public bool IsStatic { get { return _thisTypeInfo.IsStatic; } }

        /// <summary>
        /// 当前类型是否为嵌套类
        /// </summary>
        public bool IsNestedClass { get { return _thisTypeInfo.ClassType.IsNested; } }

        /// <summary>
        /// 解析类、接口等的定义
        /// <para>例如：public class Model_泛型3 : Model_泛型1<int,double,int></para>
        /// </summary>
        /// <param name="isGenericeFullName">解析的时候，如果此类型是泛型，则是否详细给出泛型参数详细的{命名空间.类}形式</param>
        /// <returns></returns>
        public string Definition(bool isGenericeFullName = false)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // 检查类型修饰符
            stringBuilder.Append(ClassVisibility() + " class ");

            // 检查是否为密封类、抽象类、静态类
            string qualifier = Qualifier();
            stringBuilder.Append(qualifier);

            // 检查是否为泛型
            stringBuilder.Append(_genericeAnalysis.AnalysisName(_thisTypeInfo.ClassType, isGenericeFullName));

            // 父类或接口

            bool isHasBase = _thisTypeInfo.ClassType.BaseType != typeof(object);
            if (isHasBase)
                stringBuilder.Append($":{_thisTypeInfo.ClassType.BaseType.Name}" + _genericeAnalysis.Analysis(_thisTypeInfo.ClassType.BaseType, isGenericeFullName));

            var interfaces = _thisTypeInfo.ClassType.GetInterfaces();

            if (!isHasBase && interfaces.Length != 0) stringBuilder.Append(":");
            else if (isHasBase && interfaces.Length != 0)
            {
                stringBuilder.Append(",");

                for (int i = 0; i < interfaces.Length; i++)
                {
                    stringBuilder.Append($"{interfaces[i].Name}{_genericeAnalysis.Analysis(interfaces[i])}");
                    if (i < interfaces.Length - 1)
                        stringBuilder.Append(",");
                }
            }

            // 泛型约束
            stringBuilder.Append(_genericeAnalysis.GetGenericConstraintString(_thisTypeInfo.ClassType));

            return stringBuilder.ToString();
        }

        #region 类访问权限 第一步

        /// <summary>
        /// 检查当前类的访问权限
        /// <para>可以识别嵌套类的访问权限</para>
        /// </summary>
        /// <returns></returns>
        public string ClassVisibility()
        {
            if (_thisTypeInfo.ClassType.IsNested)
                return NestedClassVisibility();
            return _thisTypeInfo.ClassType.IsPublic ? "public" :
                _thisTypeInfo.ClassType.IsNested ? NestedClassVisibility() : "internal";
        }


        /// <summary>
        /// 检查当前类的访问权限
        /// <para>不能识别嵌套类的访问权限，使用前请先检查</para>
        /// </summary>
        /// <returns></returns>
        public ClassAccess ClassVisibilityShared()
        {
            if (_thisTypeInfo.ClassType.IsNested)
                throw new ArgumentNullException($"当前类型 {_thisTypeInfo.ClassType.Name} 为嵌套类");

            return _thisTypeInfo.ClassType.IsPublic ? ClassAccess.Public : ClassAccess.Internal;
        }


        /// <summary>
        /// 嵌套类的访问权限
        /// </summary>
        /// <returns></returns>
        public string NestedClassVisibility()
        {
            return
                _thisTypeInfo.ClassType.IsNestedPublic ? "public" :
                _thisTypeInfo.ClassType.IsNestedPrivate ? "private" :
                _thisTypeInfo.ClassType.IsNestedAssembly ? "internal" :
                _thisTypeInfo.ClassType.IsNestedFamily ? "protected" :
                _thisTypeInfo.ClassType.IsNestedFamORAssem ? "protected internal" : throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }

        /// <summary>
        /// 嵌套类的访问权限
        /// </summary>
        /// <returns></returns>
        public MemberAccess NestedClassVisibilityShared()
        {
            if (!_thisTypeInfo.ClassType.IsNested)
                throw new ArgumentNullException($"当前类型 {_thisTypeInfo.ClassType.Name} 不是嵌套类");
            return
                _thisTypeInfo.ClassType.IsNestedPublic ? MemberAccess.Public :
                _thisTypeInfo.ClassType.IsNestedPrivate ? MemberAccess.Private :
                _thisTypeInfo.ClassType.IsNestedAssembly ? MemberAccess.Internal :
                _thisTypeInfo.ClassType.IsNestedFamily ? MemberAccess.Protected :
                _thisTypeInfo.ClassType.IsNestedFamORAssem ? MemberAccess.ProtectedInternal : throw new ArgumentNullException($"未能识别当前类型的访问权限");
        }


        #endregion

        #region 第二步

        /// <summary>
        /// 检查类是否为密封类、抽象类、静态类、接口
        /// </summary>
        /// <returns>如果都不是，则返回 string.Empty </returns>
        public string Qualifier()
        {
            return
                _thisTypeInfo.IsSealed ? "sealed" :
                _thisTypeInfo.IsAbstract ? "abstract" :
                _thisTypeInfo.IsStatic ? "static" : string.Empty;
        }

        /// <summary>
        /// 检查类是否为密封类、抽象类、静态类、接口
        /// </summary>
        /// <returns>如果都不是，则返回 string.Empty </returns>
        public ClassKeyword QualifierShared()
        {
            return
                _thisTypeInfo.IsSealed ? ClassKeyword.Sealed :
                _thisTypeInfo.IsAbstract ? ClassKeyword.Abstract :
                _thisTypeInfo.IsStatic ? ClassKeyword.Static : ClassKeyword.Default;
        }

        #endregion

        #region 第三步

        /// <summary>
        /// 当前类型是否泛型
        /// </summary>
        public bool IsGenerice { get { return _thisTypeInfo.ClassType.IsGenericType; } }

        /// <summary>
        /// 当前类型是否为已定义参数的泛型
        /// </summary>
        public bool IsHasefineGenerice { get { return !_thisTypeInfo.ClassType.IsGenericTypeDefinition; } }

        /// <summary>
        /// 获取类的名称
        /// <para>会自动处理泛型类的特殊符号，返回定义时正常的名称</para>
        /// </summary>
        public string ClassName { get { return _thisTypeInfo.ClassName; } }

        /// <summary>
        /// 获取当前类型所有的泛型参数信息
        /// </summary>
        /// <returns>如果当前类型不是泛型，会返回(false,null)</returns>
        public (bool, GenericeParamterInfo[]) GetGenericeParamter()
        {
            if (_thisTypeInfo.ClassType.IsGenericType)
                return default;

            return (true, _thisTypeInfo.GenericeParamters);
        }

        #endregion


        #region 检查是否有继承基类或接口

        /// <summary>
        /// 当前类型是否有继承基类或实现接口
        /// </summary>
        public bool IsInherit { get { return _thisTypeInfo.IsInherit; } }

        /// <summary>
        /// 是否有继承基类(object属于默认，不算在此)
        /// </summary>
        public bool IsInheritBaseType => _thisTypeInfo.BaseType == null ? false : true;

        /// <summary>
        /// 是否有继承接口
        /// </summary>
        public bool IsInherInterface => _thisTypeInfo.Interfaces == null ? false : true;

        #endregion

        #region 特性

        /// <summary>
        /// 获取当前类型的特性信息列表
        /// </summary>
        /// <returns></returns>
        public AttributeDefine[] GetAttribute()
        {
            IList<CustomAttributeData> attrs = _thisTypeInfo.ClassType.GetCustomAttributesData();
            AttributeAnalysisInfo[] infos = GetAttributesParams(attrs);
            return infos;

        }

        #endregion

        #region 字段

        #endregion

        #region 属性

        #endregion

        #region 方法

        #endregion

        /// <summary>
        /// 识别类成员的访问权限
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string Visibility(FieldInfo field)
        {
            return
                field.IsPublic ? "public" :
                field.IsPrivate ? "private" :
                field.IsAssembly ? "internal" :
                field.IsFamily ? "protected" :
                field.IsFamilyOrAssembly ? "protected internal" : throw new ArgumentNullException($"未能识别当前类型({field.Name})的访问权限");
        }



        /// <summary>
        /// 识别类属性的访问权限
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public string Visibility(PropertyInfo propertyInfo)
        {
            MethodInfo method = propertyInfo.GetAccessors()[0];
            return
                method.IsPublic ? "public" :
                method.IsPrivate ? "private" :
                method.IsAssembly ? "internal" :
                method.IsFamily ? "protected" :
                method.IsFamilyOrAssembly ? "protected internal" : throw new ArgumentNullException($"未能识别当前类型({method.Name})的访问权限");
        }

        #region 特性

        /// <summary>
        /// 获取一个字段的特性信息
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public AttributeAnalysisInfo[] AttributeAnalyses(FieldInfo field)
        {
            return GetAttributesParams(field.GetCustomAttributesData());
        }

        /// <summary>
        /// 获取一个属性的特性信息
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public AttributeAnalysisInfo[] AttributeAnalyses(PropertyInfo property)
        {
            return GetAttributesParams(property.GetCustomAttributesData());
        }

        /// <summary>
        /// 获取一个方法的特性信息
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public AttributeAnalysisInfo[] AttributeAnalyses(MethodInfo method)
        {
            return GetAttributesParams(method.GetCustomAttributesData());
        }



        /// <summary>
        /// 解析输出类型、方法、属性、字段等特性，生成字符串
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        private string[] GetAttributeToString(IList<CustomAttributeData> attrs)
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
        private AttributeAnalysisInfo[] GetAttributesParams(IList<CustomAttributeData> attrs)
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

        #endregion
    }
}
