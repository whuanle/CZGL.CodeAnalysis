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
    /// �๹����
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
        /// ͨ���ַ�������������
        /// </summary>
        /// <param name="Code">�ַ�������</param>
        /// <returns></returns>
        public static ClassDeclarationSyntax BuildSyntax(string Code)
        {
            ClassDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(Code)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();

            if (memberDeclaration is null)
                throw new InvalidOperationException("δ�ܹ����࣬��������﷨�Ƿ��д���");

            return memberDeclaration;
        }

        /// <summary>
        /// �������﷨��
        /// </summary>
        /// <returns></returns>
        public ClassDeclarationSyntax BuildSyntax()
        {
            ClassDeclarationSyntax memberDeclaration;
            memberDeclaration = CSharpSyntaxTree.ParseText(ToFullCode())
               .GetRoot()
               .DescendantNodes()
               .OfType<ClassDeclarationSyntax>()
               .FirstOrDefault();

            //if (_member.Atributes.Count != 0)
            //    memberDeclaration = memberDeclaration
            //        .WithAttributeLists(CodeSyntax.CreateAttributeList(_member.Atributes.ToArray()));

            if (memberDeclaration is null)
                throw new InvalidOperationException("δ�ܹ����࣬��������﷨�Ƿ��д���");

            return memberDeclaration;
        }

        /// <summary>
        /// ͨ������ֱ������
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ClassBuilder FromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            return new ClassBuilder().WithFromCode(code);
        }

        /// <summary>
        /// ���������ʽ������
        /// <para>��Դ�������﷨���������������Ƿ������⡣��������⣬�ٸ�ʽ���������</para>
        /// </summary>
        /// <returns>���� <see cref="string"/></returns>
        public override string ToFormatCode()
        {
            return BuildSyntax().NormalizeWhitespace().ToFullString();
        }

        /// <summary>
        /// �����������
        /// <para>����Դ�����м�飬ֱ�������ǰ�Ѿ�����Ĵ���</para>
        /// </summary>
        /// <returns>���� <see cref="string"/></returns>
        public override string ToFullCode()
        {
            if (_base.UseCode)
                return _base.Code;

            const string Template = @"{Attributes}{Access} {Keyword} class {Name}{GenericParams} {:}{BaseClass}{,}{Interfaces}{GenericList}
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
                .Replace("{GenericParams}", _member.GenericParams.GetParamCode().CodeNewBefore("<").CodeNewAfter(">"))
                .Replace("{:}", string.IsNullOrEmpty(_class.BaseClass) && _objectState.Interfaces.Count == 0 ? "" : ":")
                .Replace("{BaseClass}", _class.BaseClass)
                .Replace("{,}", (string.IsNullOrEmpty(_class.BaseClass) || _objectState.Interfaces.Count == 0) ? "" : ",")
                .Replace("{Interfaces}", _objectState.Interfaces.Join(","))
                .Replace("{GenericList}", _member.GenericParams.GetWhereCode(true).CodeNewBefore("\n"))
                .Replace("{Ctors}", _typeState.Ctors.Select(item => item.ToFullCode()).Join("\n"))
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
