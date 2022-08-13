using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 结构体
    /// </summary>
    public sealed class StructBuilder : StructTemplate<StructBuilder>
    {

        internal StructBuilder(string name) 
        {
            _name = name;
        }

        /// <summary>
        /// 设置为 readonly struct
        /// </summary>
        /// <returns></returns>
        public StructBuilder WithReadonly(bool isReadonly = true)
        {
            if (isReadonly)
                _typeState.Keyword = "readonly";

            return _TBuilder;
        }



        /// <summary>
        /// 通过字符串代码生成类
        /// </summary>
        /// <param name="Code">字符串代码</param>
        /// <returns></returns>
        public static StructDeclarationSyntax BuildSyntax(string Code)
        {
            StructDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<StructDeclarationSyntax>()
                .FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建结构体，请检查代码语法是否有错误！");

            return memberDeclaration;
        }


        /// <summary>
        /// 生成语法树
        /// </summary>
        /// <returns></returns>
        public StructDeclarationSyntax BuildSyntax()
        {
            StructDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(ToFullCode())
               .GetRoot()
               .DescendantNodes()
               .OfType<StructDeclarationSyntax>()
               .FirstOrDefault();

            //if (_member.Atributes.Count != 0)
            //    memberDeclaration = memberDeclaration
            //        .WithAttributeLists(CodeSyntax.CreateAttributeList(_member.Atributes.ToArray()));

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建结构体，请检查代码语法是否有错误！");

            return memberDeclaration;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static StructBuilder FromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            return new StructBuilder().WithFromCode(code);
        }

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        /// <summary>
        /// 完整输出代码
        /// <para>不会对代码进行检查，直接输出当前已经定义的代码</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public override string ToFullCode()
        {
            if (_base.UseCode)
                return _base.Code;

            const string Template = @"{Attributes}{Access} {Keyword} struct {Name}{GenericParams} {:} {Interfaces}{GenericList}
{

{Ctors}

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
                .Replace("{Keyword}", _typeState.Keyword)
                .Replace("{Name}", _base.Name)
                .Replace("{:}", _objectState.Interfaces.Count()==0?string.Empty:":")
                .Replace("{Interfaces}", _objectState.Interfaces.Join(","))
                .Replace("{GenericParams}", _member.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{GenericList}", _member.GenericParams.GetWhereCode(true).CodeNewBefore("\n"))
                .Replace("{Ctors}", _typeState.Ctors.Select(item=>item.ToFullCode()).Join("\n"))
                .Replace("{Fields}", _typeState.Fields.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Properties}", _objectState.Propertys.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Delegates}", _typeState.Delegates.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Events}", _typeState.Events.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Methods}", _objectState.Methods.Select(item => item.ToFullCode()).Join("\n"))
                .Replace("{Others}", _typeState.Others.OfType<BaseTemplate>().Select(item => item.ToFullCode()).Join("\n"));

            return code;
        }
    }
}
