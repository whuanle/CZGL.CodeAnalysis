using CZGL.CodeAnalysis;
using CZGL.Reflect;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;

namespace CZGL.Roslyn.Extensions
{
    public static class FieldBuilderExtension
    {
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

        public static FieldDeclarationSyntax Create(this FieldBuilder builder,MemberInfo info)
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
