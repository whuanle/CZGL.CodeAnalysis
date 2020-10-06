using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace CZGL.Roslyn
{

    /// <summary>
    /// 类构建器
    /// </summary>
    public sealed class ClassBuilder : ClassTemplate<ClassBuilder>
    {
        internal ClassBuilder()
        {
            _TBuilder = this;
        }

        internal ClassBuilder(string name) : this()
        {
            base.WithName(name);
        }

        /// <summary>
        /// 通过字符串代码生成类
        /// </summary>
        /// <param name="Code">字符串代码</param>
        /// <param name="attrs">类型的特性注解</param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Build(string Code, string[] attrs = null)
        {
            ClassDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Single();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }


        public ClassDeclarationSyntax BuildSyntax()
        {
            ClassDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(ToFullCode())
               .GetRoot()
               .DescendantNodes()
               .OfType<ClassDeclarationSyntax>()
               .Single();

            //if (_member.Atributes.Count != 0)
            //    memberDeclaration = memberDeclaration
            //        .WithAttributeLists(CodeSyntax.CreateAttributeList(_member.Atributes.ToArray()));


            return memberDeclaration;

        }

        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        public override string ToFullCode()
        {
            const string Template = @"{Attributes}{Access} {Keyword} class {Name} {:}{BaseClass}{,}{Interfaces}
{
{Fields}

{Properties}

{Delegates}

{Events}

{Methods}

{Others}
}";

            var code = Template
                .Replace("{Attributes}", _member.Atributes.Join("\n").CodeNewAfter("\n"))
                .Replace("{Access}", _member.Access)
                .Replace("{Keyword}", _class.Keyword)
                .Replace("{Name}", _base.Name)
                .Replace("{:}", string.IsNullOrEmpty(_class.BaseClass) && _class.Interfaces.Count == 0 ? "" : ":")
                .Replace("{BaseClass}", _class.BaseClass)
                .Replace("{,}", (string.IsNullOrEmpty(_class.BaseClass) || _class.Interfaces.Count == 0) ? "" : ",")
                .Replace("{Interfaces}", _class.Interfaces.Join(","))
                .Replace("{Fields}", _class.Fields.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Properties}", _class.Propertys.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Delegates}", _class.Delegates.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Events}", _class.Events.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Methods}", _class.Methods.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Others}", _class.Others.OfType<BaseTemplate>().Select(item => item.ToFullCode()).Join("\n"));

            return code;
        }
    }
}
