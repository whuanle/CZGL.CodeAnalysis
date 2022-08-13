using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 构造函数构建器
    /// </summary>
    public class CtorBuilder : CtorTemplate<CtorBuilder>
    {
        internal CtorBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 通过代码生成语法树
        /// </summary>
        /// <param name="code">构造函数代码</param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax BuildSyntax(string code)
        {
            var newCode = $"class N___{{{code}}}";
            var syntaxNodes = CSharpSyntaxTree.ParseText(newCode).GetRoot().DescendantNodes();
            var ctorSyntax = syntaxNodes
                .OfType<ConstructorDeclarationSyntax>()
                .SingleOrDefault();

            return ctorSyntax;
        }

        /// <summary>
        /// 通过代码直接生成构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CtorBuilder GetFromCode(string code)
        {
            var ctor = new CtorBuilder("");
            ctor.WithFromCode(code);
            return ctor;
        }

        /// <summary>
        /// 生成语法树
        /// </summary>
        /// <returns></returns>
        public ConstructorDeclarationSyntax BuildSyntax()
        {
            var attr = string.Join(Environment.NewLine, _atributes);
            var code = $@"public class N___
{{
                {ToFullCode()}
}}";

            var ctorSyntax = CSharpSyntaxTree.ParseText(code)
                .GetRoot()
                .DescendantNodes()
                .OfType<ConstructorDeclarationSyntax>()
                .SingleOrDefault();

            if (ctorSyntax is null)
                throw new InvalidOperationException("未能构建构造函数，请检查代码是否有语法错误！");

            return ctorSyntax;
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
            if (_useCode)
                return _code!;

            var access = _access;
            var name = _name;
            var @params = _inputParams.Join(", ");
            var invoke = _invokeBase;

            string code = @$"{access} {name}({@params}) {invoke.CodeNewBefore(" : ")}
{{
{_blockCode}
}}";
            return code;
        }
    }
}
