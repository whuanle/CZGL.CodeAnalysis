using CZGL.Roslyn;
using CZGL.Reflect;
using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System;

namespace CZGL.Roslyn.Extensions
{
    /// <summary>
    /// 字段构建器拓展方法
    /// </summary>
    public static class FieldBuilderExtension
    {

        /// <summary>
        /// 克隆一个字段
        /// </summary>
        /// <param name="field"></param>
        /// <param name="copyAttribute">是否克隆特性注解，默认不克隆</param>
        /// <param name="copyName">是否连名称一起克隆，默认不克隆</param>
        /// <returns></returns>
        public static FieldBuilder WithCopy(this FieldBuilder fieldBuilder, FieldInfo field, bool copyAttribute = false, bool copyName = false)
        {
            if (field is null)
                throw new ArgumentNullException(nameof(field));


            fieldBuilder.WithAccess(field.GetAccess())
            .WithType(field.DeclaringType.Name)
            .WithKeyword(field.GetKeyword());

            if (copyAttribute)
            {
                fieldBuilder.WithAttributes(field.GetAttributes());
            }

            if (copyName)
            {
                fieldBuilder.WithName(field.Name);
            }

            return fieldBuilder;
        }

        public static FieldBuilder WithType<T>(this FieldBuilder field)
        {
            return field.WithType(field.Name);
        }

        //public static FieldBuilder WithType<T>(this FieldBuilder fieldBuilder)
        //{
        //    fieldBuilder.WithType(GenericeAnalysis.Analysis());
        //    return fieldBuilder;
        //}

        //public static ClassBuilder WithField(this ClassBuilder classBuilder,FieldInfo field)
        //{
        //    var analysis = new FiledAnalysis(field);

        //    var fieldBuild = CodeSyntax.CreateField(field.Name)
        //        .WithAccess(analysis.Access);

        //    classBuilder.WithField(fieldBuild);

        //    return classBuilder;
        //}

        //public FieldBuilder SetType(Type type)
        //{


        //    return this;
        //}

        public static FieldDeclarationSyntax Create(this FieldBuilder builder, FieldInfo info)
        {
            //
            return builder.BuildSyntax();
        }

        public static FieldDeclarationSyntax Create(this FieldBuilder builder, MemberInfo info)
        {

            return builder.BuildSyntax();
        }

        ///// <summary>
        ///// 设置初始化值
        ///// </summary>
        ///// <returns></returns>
        //public FieldBuilder SetInitializer<TType>(TType value)
        //{
        //    return this;
        //}
    }
}
