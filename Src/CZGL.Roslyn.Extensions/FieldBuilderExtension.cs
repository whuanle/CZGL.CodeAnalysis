using CZGL.Roslyn;
using CZGL.Reflect;
using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 字段构建器拓展方法
    /// </summary>
    public static class FieldBuilderExtension
    {

        /// <summary>
        /// 克隆一个字段
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="field"></param>
        /// <param name="copyAttribute">是否克隆特性注解，默认克隆</param>
        /// <returns></returns>
        public static FieldBuilder WithCopy(this FieldBuilder builder, FieldInfo field, bool copyAttribute = true)
        {
            if (field is null)
                throw new ArgumentNullException(nameof(field));

            builder.WithAccess(field.GetAccess())
            .WithType(field.FieldType)
            .WithKeyword(field.GetKeyword());

            if (copyAttribute)
            {
                builder.WithAttributes(field.GetAttributes());
            }
            builder.WithName(field.Name);

            builder.WithNamespace(field.FieldType.Namespace);

            return builder;
        }

#nullable enable

        /// <summary>
        /// 定义字段类型
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static FieldBuilder WithType(this FieldBuilder builder, Type fieldType)
        {
            if (fieldType == null)
                throw new ArgumentNullException(nameof(fieldType));

            var type = fieldType;
            string typeName;

            if (type.IsGenericType)
                typeName = GenericeAnalysis.GetGenriceName(type);
            else typeName = type.Name;

            return builder.WithType(typeName);
        }

#nullable enable

        /// <summary>
        /// 为字段定义类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FieldBuilder WithType<T>(this FieldBuilder builder)
        {
            Type type = typeof(T);
            if (!string.IsNullOrEmpty(type.Namespace))
                builder.WithNamespace(type.Namespace);

            return WithType(builder, type);
        }

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
