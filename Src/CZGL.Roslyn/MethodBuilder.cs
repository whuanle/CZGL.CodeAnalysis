using CZGL.CodeAnalysis.Shared;
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
    public sealed class MethodBuilder : MethodTemplate<MethodBuilder>
    {
        public MethodBuilder()
        {
            _TBuilder = this;
        }

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="qualifierType"></param>
        /// <returns></returns>
        public  MethodBuilder SetQualifier(MemberQualifierType qualifierType)
        {
            Qualifier = RoslynHelper.GetName(qualifierType);
            return _TBuilder;
        }

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="str">static... </param>
        /// <returns></returns>
        public  MethodBuilder SetQualifier(string str = "")
        {
            Qualifier = str;
            return _TBuilder;
        }


        /// <summary>
        /// 方法体中的代码
        /// </summary>
        /// <param name="block">方法体中的代码</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// int a = 0;
        /// Console.WriteLine(a);
        /// </code>
        /// </example>
        public MethodBuilder SetBlock(string blockCode = null)
        {
            BlockCode = blockCode;
            return _TBuilder;
        }


        /// <summary>
        /// 通过字符串代码直接生成方法
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// public int A 
        /// { 
        ///   get{return B;}
        ///   set{B = value;}
        ///   }
        /// </code>
        /// </example>
        public static MethodDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            MethodDeclarationSyntax memberDeclaration;
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes();
            memberDeclaration = syntaxNodes
            .OfType<MethodDeclarationSyntax>()
            .Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        /// <summary>
        /// 构建方法
        /// </summary>
        /// <returns></returns>
        public MethodDeclarationSyntax Build()
        {
            StringBuilder stringBuilder = new StringBuilder("class Test66666666{");

            stringBuilder.Append(Visibility);

            stringBuilder.Append(" ");
            stringBuilder.Append(Qualifier);

            stringBuilder.Append(" ");
            stringBuilder.Append(ReturnType);

            stringBuilder.Append(" ");
            stringBuilder.Append(MemberName);

            stringBuilder.AppendLine($"({Params})");

            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(BlockCode);
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine("}");
            MethodDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(stringBuilder.ToString())
                .GetRoot()
                .DescendantNodes();

            memberDeclaration = syntaxNodes
           .OfType<MethodDeclarationSyntax>().Single();

            // 添加特性
            if (MemberAttrs.Count != 0)
            {
                var tmp = AttributeBuilder.CreateAttributeList(MemberAttrs.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
        }

    }
}
