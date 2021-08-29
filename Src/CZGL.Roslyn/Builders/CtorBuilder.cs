using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 构造函数构建器
    /// </summary>
    public class CtorBuilder : CtorTemplate<CtorBuilder>
    {
        internal CtorBuilder(string name)
        {
            _base.Name = name;
            _TBuilder = this;
        }

        /// <summary>
        /// 生成语法树
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax BuildSyntax(string Code, string[] attrs = null)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(Code).GetRoot().DescendantNodes();
            ConstructorDeclarationSyntax memberDeclaration = syntaxNodes
                .OfType<ConstructorDeclarationSyntax>()
                .FirstOrDefault();

            if (attrs != null)
                memberDeclaration = memberDeclaration
                    .WithAttributeLists(CodeSyntax.CreateAttributeList(attrs));

            return memberDeclaration;
        }

        /// <summary>
        /// 通过代码直接生成构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CtorBuilder FromCode(string code)
        {
            var ctor = new CtorBuilder(null);
            ctor.WithFromCode(code);
            return ctor;
        }

        /// <summary>
        /// 生成语法树
        /// </summary>
        /// <returns></returns>
        public ConstructorDeclarationSyntax BuildSyntax()
        {
            var code = $@"public class {Name}
{{
                {ToFullCode()}
}}";

            ConstructorDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(code)
                .GetRoot()
                .DescendantNodes();


            memberDeclaration = syntaxNodes
           .OfType<ConstructorDeclarationSyntax>().FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("未能构建构造函数，请检查代码是否有语法错误！");

            // 添加特性
            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
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
            if (_func.UseCode)
                return _func.Code;

            const string Template = @"{Access} {Name}({Params}) {BaseOrThis}
{
{BlockCode}
}";
            var code = Template
                .Replace("{Access}", _member.Access)
                .Replace("{Name}", _base.Name)
                .Replace("{Params}", _func.Params.Join(","))
                .Replace("{BaseOrThis}",_method.BaseOrThis.CodeNewBefore(":"))
                .Replace("{BlockCode}", _method.BlockCode);
            return code;
        }



    }
}
