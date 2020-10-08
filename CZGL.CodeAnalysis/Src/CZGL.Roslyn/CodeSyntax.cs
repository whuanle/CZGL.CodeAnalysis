using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 代码工具
    /// </summary>
    public static class CodeSyntax
    {

        #region 命名空间

        /// <summary>
        /// 创建一个命名空间
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        /// <returns></returns>
        public static NamespaceBuilder CreateNamespace(string namespaceName)
        {
            return new NamespaceBuilder(namespaceName);
        }

        public static NamespaceBuilder NamespaceTransform()
        {
            return null;
        }

        #endregion


        #region 特性

        /// <summary>
        /// 字符串生成特性列表
        /// <para>
        /// <example>
        /// <code>
        /// string[] code = new string[]
        /// {
        ///     "[Display(Name = \"a\")]",
        ///     "[Display(Name = \"b\")]"
        /// };
        /// Cbuilder.reateAttributeList(code);
        /// </code>
        /// </example>
        /// </para>
        /// </summary>
        /// <param name="attrCode"></param>
        /// <returns></returns>
        public static SyntaxList<AttributeListSyntax> CreateAttributeList(params string[] attrsCode)
        {

            List<AttributeListSyntax> syntaxes = new List<AttributeListSyntax>();

            foreach (var item in attrsCode)
            {
                var tmp = CreateCodeAttribute(item);
                syntaxes.Add(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(tmp)));
            }

            return SyntaxFactory.List<AttributeListSyntax>(syntaxes.ToArray());
        }

        /// <summary>
        /// 生成特性列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static SyntaxList<AttributeListSyntax> CreateAttributeList(IEnumerable<AttributeListSyntax> list)
        {
            return SyntaxFactory.List<AttributeListSyntax>(list.ToArray());
        }


        /// <summary>
        /// 字符串生成特性
        /// </summary>
        /// <param name="attrCode"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// "[Display(Name = \"a\")]"
        /// </code>
        /// </example>
        public static AttributeListSyntax CreateAttributeList(string attrCode)
        {
            var result = CreateCodeAttribute(attrCode);

            return SyntaxFactory.AttributeList(
                  SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(result));
        }

        /// <summary>
        /// 字符串生成特性
        /// </summary>
        /// <param name="attrbuteSyntax"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// "[Display(Name = \"a\")]"
        /// </code>
        /// </example>
        public static AttributeListSyntax CreateAttributeList(AttributeSyntax attrbuteSyntax)
        {
            return SyntaxFactory.AttributeList(
                  SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(attrbuteSyntax));
        }

        /// <summary>
        /// 字符串生成特性
        /// </summary>
        /// <param name="attrCode"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// "[Display(Name = \"a\")]"
        /// </code>
        /// </example>
        public static AttributeSyntax CreateCodeAttribute(string attrCode)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(attrCode).GetRoot().DescendantNodes();
            _ = syntaxNodes.Execute(item =>
            item.NormalizeWhitespace().ToFullString()
            );
            var member = syntaxNodes.OfType<AttributeSyntax>().FirstOrDefault();
            return member;
        }

        #endregion


        /// <summary>
        /// 创建枚举
        /// </summary>
        /// <param name="name">枚举名称</param>
        /// <returns></returns>
        public static EnumBuilder CreateEnum(string name)
        {
            return new EnumBuilder(name);
        }


        /// <summary>
        /// 创建泛型构建器
        /// </summary>
        /// <returns></returns>
        public static GenericBuilder CreateGeneric()
        {
            return new GenericBuilder();
        }

        /// <summary>
        /// 通过代码构建泛型参数以及约束
        /// <para>
        /// string paramList = @"T1,T2,T3,T4";
        /// string constraintList = @"
        ///         where T1 : struct
        ///         where T2 : class
        ///         where T3 : notnull
        ///         where T4 : unmanaged
        ///         where T5 : new()
        ///         where T6 : Model_泛型类4
        ///         where T7 : IEnumerable<int />
        ///         where T8 : T2
        /// "; 
        /// </para>
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="constraintList"></param>
        /// <returns></returns>
        public static GenericBuilder CreateGeneric(string paramList, string constraintList)
        {
            return GenericBuilder.WithFromCode(paramList, constraintList);
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FieldBuilder CreateField(string name)
        {
            return new FieldBuilder(name);
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyBuilder CreateProperty(string name)
        {
            return new PropertyBuilder(name);
        }

        /// <summary>
        /// 创建委托
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DelegateBuilder CreateDelegate(string name)
        {
            return new DelegateBuilder(name);
        }

        /// <summary>
        /// 创建事件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EventBuilder CreateEvent(string name)
        {
            return new EventBuilder(name);
        }

        /// <summary>
        /// 创建方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodBuilder CreateMethod(string name)
        {
            return new MethodBuilder(name);
        }

        /// <summary>
        /// 创建构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CtorBuilder CreateCtor(string name)
        {
            return new CtorBuilder(name);
        }

        /// <summary>
        /// 创建特性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AttributeBuilder CreateAttribute(string name)
        {
            return new AttributeBuilder(name);
        }

        /// <summary>
        /// 构建类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ClassBuilder CreateClass(string name)
        {
            return new ClassBuilder(name);
        }

        /// <summary>
        /// 构建类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static StructBuilder CreateStruct(string name)
        {
            return new StructBuilder(name);
        }

        /// <summary>
        /// 构建接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterfaceBuilder CreateInterface(string name)
        {
            return new InterfaceBuilder(name);
        }

        /// <summary>
        /// 生成编译器
        /// </summary>
        /// <returns></returns>
        public static CompilationBuilder CreateCompilation(string assemblyName)
        {
            return new CompilationBuilder(assemblyName);
        }
    }
}
