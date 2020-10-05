using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 泛型构建器，
    /// 可为类、委托、方法等构建泛型参数以及约束
    /// </summary>
    public sealed class GenericBuilder : GenericTemplate<GenericBuilder>
    {

        public GenericBuilder()
        {
            _TBuilder = this;
        }



        #region 泛型约束

        /// <summary>
        /// 添加一个泛型参数
        /// </summary>
        /// <param name="name">泛型参数名称</param>
        /// <returns></returns>
        public GenericBuilder With(string name)
        {
            _this.Name = name;
            return this;
        }

        /// <summary>
        /// 为泛型参数添加结构体约束
        /// <para>不能与其它约束同时使用</para>
        /// </summary>
        /// <returns></returns>
        public GenericTemplate<GenericBuilder> WithStruct()
        {
            const string Template = "struct";
            _this.Constraints.Add(Template);
            return this;
        }

        /// <summary>
        /// 为泛型参数添加非托管资源约束
        /// <para>不能与其它约束同时使用</para>
        /// </summary>
        /// <returns></returns>
        public GenericTemplate<GenericBuilder> WithUnmanaged()
        {
            const string Template = "unmanaged";
            _this.Constraints.Add(Template);
            return this;
        }

        /// <summary>
        /// 为泛型参数添加 Class 约束
        /// <para>必须放在开头，不能与 notnull、{基类} 约束一起使用</para>
        /// </summary>
        /// <returns></returns>
        public GenericBuilder WithClass()
        {
            const string Template = "class";
            _this.Constraints.Add(Template);
            return this;
        }

        /// <summary>
        /// 为泛型参数添加 notnull 约束
        /// <para>必须放在开头，不能与 class、{基类} 约束一起使用</para>
        /// </summary>
        /// <returns></returns>
        public GenericBuilder WitNotnull()
        {
            const string Template = "notnull";
            _this.Constraints.Add(Template);
            return this;
        }

        /// <summary>
        /// 为泛型参数添加 基类 约束
        /// <para>必须放在开头，不能与 class、notnull 约束一起使用</para>
        /// </summary>
        /// <returns></returns>
        public GenericBuilder WithBase(string baseName)
        {
            if (string.IsNullOrEmpty(baseName))
                throw new ArgumentNullException(nameof(baseName));

            _this.Constraints.Add(baseName);
            return this;
        }

        /// <summary>
        /// 为泛型参数添加接口约束
        /// <para>位置不限，个数不限</para>
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <returns></returns>
        public GenericBuilder WithInterface(string interfaceName)
        {
            if (string.IsNullOrEmpty(interfaceName))
                throw new ArgumentNullException(nameof(interfaceName));

            _this.Constraints.Add(interfaceName);
            return this;
        }

        /// <summary>
        /// 为泛型参数添加接口约束
        /// <para>位置不限，个数不限</para>
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <returns></returns>
        public GenericBuilder WithInterface(params string[] interfaceNames)
        {
            if (interfaceNames is null)
                throw new ArgumentNullException(nameof(interfaceNames));
            _ = interfaceNames.SelectMany(str => { _this.Constraints.Add(str); return str; });

            return this;
        }

        /// <summary>
        /// 为泛型参数继承参数约束,即 T:U 约束
        /// <para>位置不限，个数不限</para>
        /// </summary>
        /// <param name="u">前面已经出现的泛型参数</param>
        /// <returns></returns>
        public GenericBuilder WithTo(string u)
        {
            if (string.IsNullOrEmpty(u))
                throw new ArgumentNullException(nameof(u));

            _this.Constraints.Add(u);
            return this;
        }

        /// <summary>
        /// 添加 new() 约束
        /// </summary>
        /// <returns></returns>
        public GenericTemplate<GenericBuilder> WithNew()
        {
            const string Template = "new()";
            _this.Constraints.Add(Template);
            return this;
        }

        #endregion

        /// <summary>
        /// 构建泛型参数列表
        /// </summary>
        /// <returns></returns>
        public TypeParameterListSyntax BuildTypeParameterListSyntax()
        {
            // syntax 写法太长
            //List<SyntaxNodeOrToken> tokens = new List<SyntaxNodeOrToken>();
            //var list = _generic.ToArray();
            //for (int i = 0; i < list.Length; i++)
            //{
            //    tokens.Add(SyntaxFactory.TypeParameter
            //            (
            //                SyntaxFactory.Identifier(list[i].Name)
            //            ));
            //    if (i < list.Length - 1)
            //        tokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            //}

            //return
            //SyntaxFactory.TypeParameterList
            //(
            //    SyntaxFactory.SeparatedList<TypeParameterSyntax>
            //    (
            //        tokens.ToArray()
            //    )
            //);

            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode()).GetRoot().DescendantNodes();
            TypeParameterListSyntax memberDeclaration = syntaxNodes
                .OfType<TypeParameterListSyntax>()
                .FirstOrDefault();
            return memberDeclaration;
        }

        /// <summary>
        /// 将当前构建的泛型参数约束生成 Syntax
        /// </summary>
        /// <returns></returns>
        public TypeParameterConstraintClauseSyntax ToConstraintSyntax()
        {
            if (_this is null)
                throw new ArgumentNullException("当前没有正在构建的泛型参数约束");

            const string Template = "where {Name} : {Constraints}";
            if (_this.Constraints.Count == 0)
                return null;

            var code = Template
                .Replace("{Name}", _this.Name)
                .Replace("{Constraints}", _this.Constraints.Join(","));

            var syntaxNodes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes();
            TypeParameterConstraintClauseSyntax memberDeclaration = syntaxNodes
                .OfType<TypeParameterConstraintClauseSyntax>()
                .FirstOrDefault();
            return memberDeclaration;
        }

        /// <summary>
        /// 生成完整的泛型约束列表
        /// </summary>
        /// <returns></returns>
        public SyntaxList<TypeParameterConstraintClauseSyntax> ToConstraintListSyntax()
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode()).GetRoot().DescendantNodes();
            SyntaxList<TypeParameterConstraintClauseSyntax> memberDeclaration = syntaxNodes
                .OfType<SyntaxList<TypeParameterConstraintClauseSyntax>>()
                .FirstOrDefault();

            return memberDeclaration;
        }

        public override string ToFullCode()
        {
            const string Template = "<{Params}> {where}";
            ParamCode = _generic.Select(x => x.Name).Join(",");
            WhereCode = string.Join("\n", _generic.Where(x => x.Constraints.Count != 0)
               .SelectMany(gen =>
               {
                   return Template
                   .Replace("{Name}", gen.Name)
                   .Replace("{Constraints}", gen.Constraints.Join(","));
               }));

            var code = Template
                  .Replace("{Params}", ParamCode)
                  .Replace("{where}", WhereCode);

            return code;
        }

        public override string ToFormatCode()
        {
            var node = CSharpSyntaxTree.ParseText(ToFullCode()).GetRoot().DescendantNodes().FirstOrDefault();
            if (node is null)
                return default;

            return node.NormalizeWhitespace().ToFullString();
        }


        internal static GenericBuilder WithFromCode(string paramList, string constraintList)
        {
            return new GenericBuilder()
                .WithTransformParam(paramList)
                .WithTransformConstraint(constraintList)
                .GetBuilder();
        }
    }
}
