using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Reflection;

namespace CZGL.Roslyn.Extensions
{
    public static class FieldBuilderExtension
    {

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
