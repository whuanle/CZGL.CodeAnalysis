using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class NamespaceTemplate<TBuilder> : BaseTemplate where TBuilder : NamespaceTemplate<TBuilder>
    {
        private readonly NamespaceState _namespace = new NamespaceState();
        protected internal TBuilder _TBuilder;

        public NamespaceTemplate() { }
        public NamespaceTemplate(string namespaceName)
        {
            _base.Name = namespaceName;
        }

        /// <summary>
        /// 设置命名空间的名称
        /// </summary>
        /// <param name="namespaceName">命名空间的名称</param>
        /// <returns></returns>
        public virtual TBuilder WithName(string namespaceName)
        {
            _base.Name = namespaceName;
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个命名空间引用
        /// <para>
        /// <code>
        /// .WithUsing("System")
        /// .WithUsing("System.Collections.Generic")
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="usingName"></param>
        /// <returns></returns>
        public virtual TBuilder WithUsing(string usingName)
        {
            _namespace.Usings.Add($"using {usingName};");
            return _TBuilder;
        }

        /// <summary>
        /// 添加 N 个命名空间引用
        /// </summary>
        /// <param name="usingName"></param>
        /// <returns></returns>
        public virtual TBuilder WithUsing(params string[] usingNames)
        {
            _ = usingNames.SelectMany(cl => { _namespace.Usings.Add($"using {cl};"); return cl; });
            return _TBuilder;
        }

        #region 创建成员

        public virtual EnumBuilder CreateEnum()
        {
            return new EnumBuilder();
        }

        public virtual StructBuilder CreateStruct()
        {
            return new StructBuilder();
        }

        public virtual InterfaceBuilder CreateInterface()
        {
            return new InterfaceBuilder();
        }

        public virtual ClassBuilder CreateClass(ClassBuilder builder)
        {
            return new ClassBuilder();
        }
        public virtual DelegateBuilder CreateDelegate()
        {
            return new DelegateBuilder();
        }
        public virtual EventBuilder CreateEvent(EventBuilder builder)
        {
            return new EventBuilder();
        }


        #endregion

        #region 引入成员

        public virtual TBuilder With(EnumBuilder builder)
        {
            return _TBuilder;
        }

        public virtual TBuilder With(StructBuilder builder)
        {
            return _TBuilder;
        }

        public virtual TBuilder With(InterfaceBuilder builder)
        {
            return _TBuilder;
        }

        public virtual TBuilder With(ClassBuilder builder)
        {
            return _TBuilder;
        }
        public virtual TBuilder With(DelegateBuilder builder)
        {
            return _TBuilder;
        }
        public virtual TBuilder With(EventBuilder builder)
        {
            return _TBuilder;
        }

        #endregion

        public override string ToFullCode()
        {
            /*
             * {usings}
             * {enums}
             * {structs}
             * {interfaces}
             * {classes}
             * {deletages}
             * {events}
            */

            const string Template = @"
{usings}namespace {name}
{

}";
            var code = Template
                .Replace("{usings}", _namespace.Usings.Join().CodeNewLine())
                .Replace("{name}", _base.Name)
                ;

            return code;
        }

        public override string ToFormatCode()
        {
            var code = Build().NormalizeWhitespace().ToFullString();
            return code;
        }


        internal abstract NamespaceDeclarationSyntax Build();
    }
}
