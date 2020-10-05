using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public ConstructorDeclarationSyntax BuildSyntax()
        {
            ConstructorDeclarationSyntax memberDeclaration = default;
            var syntaxNodes = CSharpSyntaxTree.ParseText(ToFullCode())
                .GetRoot()
                .DescendantNodes();

            memberDeclaration = syntaxNodes
           .OfType<ConstructorDeclarationSyntax>().Single();

            // 添加特性
            if (_member.Atributes.Count != 0)
            {
                var tmp = CodeSyntax.CreateAttributeList(_member.Atributes.ToArray());
                memberDeclaration = memberDeclaration.WithAttributeLists(tmp);
            }

            return memberDeclaration;
        }



        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        public override string ToFullCode()
        {
            if (_func.UseCode)
                return _func.Code;

            const string Template = @"{Access} {Name}({Params})
{
{BlockCode}
}";
            var code = Template
                .Replace("{Access}", _member.Access)
                .Replace("{Name}", _base.Name)
                .Replace("{{Params}}", _func.Params.Join(","))
                .Replace("{BlockCode}", _method.BlockCode);
            return code;
        }



    }
}
