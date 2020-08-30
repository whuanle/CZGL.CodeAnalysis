using CZGL.CodeAnalysis.Shared;
using CZGL.RoslynTool.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 泛型类型构建器
    /// </summary>
    public sealed class GenericBuilder
    {
        private Dictionary<string, DefineGenericInfo> tokens = new Dictionary<string, DefineGenericInfo>();

        /// <summary>
        /// 添加一个泛型参数
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public GenericBuilder AddParam(string paramterName)
        {
            return Add(paramterName, out _);
        }

        /// <summary>
        /// 移除一个泛型参数
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public GenericBuilder RemoveParam(string paramterName)
        {
            return Remove(paramterName);
        }

        /// <summary>
        /// 批量添加泛型参数
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public GenericBuilder AddParam(string[] @params)
        {
            foreach (var item in @params)
            {
                _ = Add(item, out _);
            }

            return this;
        }
        /// <summary>
        /// 添加泛型参数或为泛型参数添加约束,或修改此参数的约束
        /// </summary>
        /// <param name="parmter">参数名称</param>
        /// <param name="scheme">泛型类别</param>
        /// <returns></returns>
        public GenericBuilder AddParam(GenericScheme scheme)
        {
            if (scheme == null)
                throw new ArgumentNullException(nameof(scheme), "参数不能为 null !");

            switch (scheme.TypeId)
            {
                case 0: Add(scheme.Name, out _); break;
                case 1: AddRed(scheme); break;
                case 2: AddYellow(scheme); break;
                case 3: AddBlue(scheme); break;
                case 4: AddYellow(scheme); AddBlue(scheme); break;
                case 5: AddYellow(scheme); AddOrange(scheme); break;
                case 6: AddYellow(scheme); AddBlue(scheme); AddOrange(scheme); break;
                case 7: AddBlue(scheme); AddOrange(scheme); break;
                default:
                    throw new Exception();
            }

            return this;
        }

        #region 8 s种型约束

        /// <summary>
        /// 处理 Red 
        /// </summary>
        /// <returns></returns>
        private void AddRed(GenericScheme scheme)
        {
            if (scheme.Red == GenericConstraintsType.Struct)
                AddStruct(scheme.Name);

            AddUnmanaged(scheme.Name);
        }


        private void AddYellow(GenericScheme scheme)
        {
            switch (scheme.Yellow)
            {
                case GenericConstraintsType.Class: AddClass(scheme.Name); break;
                case GenericConstraintsType.Notnull: AddNotNull(scheme.Name); break;
                case GenericConstraintsType.BaseClass: AddBaseClass(scheme.Name, scheme.BaseType.Name); break;
            }
        }

        private void AddBlue(GenericScheme scheme)
        {
            if (scheme.InterfaceType != null)
                AddInterface(scheme.Name, scheme.InterfaceType.Select(x => x.Name).ToArray());
            if (scheme.TU != null)
                AddTU(scheme.Name, scheme.TU.Select(x => x).ToArray());
        }

        private void AddOrange(GenericScheme scheme)
        {
            AddNewNull(scheme.Name);
        }

        /// <summary>
        /// 添加 struct 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public GenericBuilder AddStruct(string paramterName)
        {
            DefineGenericInfo info = Search(paramterName);

            info.Name = paramterName;
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));
            info.ConstraintClauseSyntax.Add(
                SyntaxFactory.TypeParameterConstraintClause(SyntaxFactory.IdentifierName(paramterName)).WithConstraints(
                    SyntaxFactory.SingletonSeparatedList<TypeParameterConstraintSyntax>(
                    SyntaxFactory.ClassOrStructConstraint(SyntaxKind.StructConstraint))));

            return this;
        }

        /// <summary>
        /// 添加 unmanaged 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public GenericBuilder AddUnmanaged(string paramterName)
        {
            DefineGenericInfo info = Search(paramterName);
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            info.ConstraintClauseSyntax.Add(
               SyntaxFactory.TypeParameterConstraintClause(SyntaxFactory.IdentifierName(paramterName))
            .WithConstraints(
                SyntaxFactory.SingletonSeparatedList<TypeParameterConstraintSyntax>(
                    SyntaxFactory.TypeConstraint(
                        SyntaxFactory.IdentifierName("unmanaged")))));

            return this;
        }

        /// <summary>
        /// 添加 Class 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public GenericBuilder AddClass(string paramterName)
        {
            DefineGenericInfo info = Search(paramterName);
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            info.ConstraintClauseSyntax.Add(
                SyntaxFactory.TypeParameterConstraintClause(
                SyntaxFactory.IdentifierName(paramterName))
            .WithConstraints(
                SyntaxFactory.SingletonSeparatedList<TypeParameterConstraintSyntax>(
                    SyntaxFactory.ClassOrStructConstraint(
                        SyntaxKind.ClassConstraint))));

            return this;
        }


        /// <summary>
        /// 添加 notnull 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public GenericBuilder AddNotNull(string paramterName)
        {
            DefineGenericInfo info = Search(paramterName);
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            info.ConstraintClauseSyntax.Add(
                SyntaxFactory.TypeParameterConstraintClause(
                SyntaxFactory.IdentifierName(paramterName))
            .WithConstraints(
                SyntaxFactory.SingletonSeparatedList<TypeParameterConstraintSyntax>(
                    SyntaxFactory.TypeConstraint(
                        SyntaxFactory.IdentifierName("notnull")))));

            return this;
        }

        /// <summary>
        /// 添加 <基类> 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="baseTypeName"></param>
        /// <returns></returns>
        public GenericBuilder AddBaseClass(string paramterName, string baseTypeName)
        {
            DefineGenericInfo info = Search(paramterName);
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            info.ConstraintClauseSyntax.Add(
                SyntaxFactory.TypeParameterConstraintClause(
                SyntaxFactory.IdentifierName(paramterName))
            .WithConstraints(
                SyntaxFactory.SingletonSeparatedList<TypeParameterConstraintSyntax>(
                    SyntaxFactory.TypeConstraint(
                        SyntaxFactory.IdentifierName(baseTypeName)))));

            return this;
        }

        /// <summary>
        /// 添加接口约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        public GenericBuilder AddInterface(string paramterName, string[] interfaces)
        {
            DefineGenericInfo info = Search(paramterName);
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            SyntaxNodeOrToken[] syntaxes = new SyntaxNodeOrToken[interfaces.Length];

            for (int i = 0; i < interfaces.Length; i++)
            {
                syntaxes[i] = SyntaxFactory.TypeConstraint(
                            SyntaxFactory.IdentifierName(interfaces[i]));
            }

            info.ConstraintClauseSyntax.Add(
                SyntaxFactory.TypeParameterConstraintClause(
                SyntaxFactory.IdentifierName(paramterName))
            .WithConstraints(
                SyntaxFactory.SeparatedList<TypeParameterConstraintSyntax>(syntaxes)));

            return this;
        }

        /// <summary>
        /// 添加 T:U 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        public GenericBuilder AddTU(string paramterName, string[] UName)
        {
            return AddInterface(paramterName, UName);
        }

        /// <summary>
        /// 添加 new() 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        public GenericBuilder AddNewNull(string paramterName)
        {
            DefineGenericInfo info = Search(paramterName);
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            info.ConstraintClauseSyntax.Add(
                 SyntaxFactory.TypeParameterConstraintClause(
                SyntaxFactory.IdentifierName(paramterName))
            .WithConstraints(
                SyntaxFactory.SingletonSeparatedList<TypeParameterConstraintSyntax>(
                    SyntaxFactory.ConstructorConstraint())));

            return this;
        }

        #endregion


        #region Base

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        private DefineGenericInfo Search(string paramterName)
        {
            DefineGenericInfo info = default;

            if (!tokens.ContainsKey(paramterName))
            {
                tokens.Add(paramterName, info);
            }
            else info = tokens[paramterName];
            return info;
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="paramterName">参数名称</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        private GenericBuilder Add(string paramterName, out DefineGenericInfo info)
        {
            if (!tokens.ContainsKey(paramterName))
            {
                info = new DefineGenericInfo();
                tokens.Add(paramterName, info);
            }
            else info = tokens[paramterName];

            info.Name = paramterName;
            info.Parameter = SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(paramterName));

            return this;
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="paramterName">参数名称</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        private GenericBuilder Remove(string paramterName)
        {
            tokens.Remove(paramterName);
            return this;
        }

        #endregion


        /// <summary>
        /// 生成泛型参数
        /// </summary>
        /// <returns></returns>
        public SyntaxNodeOrToken[] BuildTypeParameterListSyntax()
        {
            List<SyntaxNodeOrToken> tokenss = new List<SyntaxNodeOrToken>();
            var syntaxs = tokens.Values.Select(x => x.Parameter).ToArray();

            for (int i = 0; i < syntaxs.Length; i++)
            {
                tokenss.Add(syntaxs[i]);
                if (i < syntaxs.Length - 1)
                    tokenss.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }
            return tokenss.ToArray();
        }

        /// <summary>
        /// 生成泛型约束
        /// </summary>
        /// <returns></returns>
        public TypeParameterConstraintClauseSyntax[] BuildTypeParameterConstraintClauseSyntax()
        {
            List<TypeParameterConstraintClauseSyntax> list = new List<TypeParameterConstraintClauseSyntax>();
            foreach (var item in tokens.Values)
            {
                foreach (var tmp in item.ConstraintClauseSyntax)
                {
                    list.Add(tmp);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 为类生成泛型
        /// </summary>
        /// <returns></returns>
        public ClassDeclarationSyntax Build(ClassDeclarationSyntax _classDeclaration)
        {

            return _classDeclaration.WithTypeParameterList(SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList<TypeParameterSyntax>(BuildTypeParameterListSyntax()))).WithConstraintClauses(SyntaxFactory.List(BuildTypeParameterConstraintClauseSyntax()));
        }

        /// <summary>
        /// 泛型参数列表
        /// </summary>
        /// <returns></returns>
        public TypeParameterSyntax[] GetGenericeParamters()
        {
            return Constarints.Select(x => SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(x.Key))).ToArray();
        }
    }

}
