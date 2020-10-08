using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

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

        #region 名称

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new virtual TBuilder WithName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            base.WithName(name);
            return _TBuilder;
        }

        /// <summary>
        /// 随机生成一个名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new virtual TBuilder WithRondomName()
        {
            base.WithRondomName();
            return _TBuilder;
        }

        #endregion

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
            if (string.IsNullOrEmpty(usingName))
                throw new ArgumentNullException(nameof(usingName));

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
            _ = usingNames.Execute(cl => _namespace.Usings.Add($"using {cl};"));
            return _TBuilder;
        }

        #region 创建成员

        /// <summary>
        /// 创建枚举
        /// </summary>
        /// <returns></returns>
        public virtual EnumBuilder CreateEnum()
        {
            var builder = new EnumBuilder();
            _namespace.Enums.Add(builder);
            return builder;
        }

        /// <summary>
        /// 创建枚举
        /// </summary>
        /// <param name="name">枚举名称</param>
        /// <returns></returns>
        public virtual EnumBuilder CreateEnum(string name)
        {
            var builder = new EnumBuilder(name);
            _namespace.Enums.Add(builder);
            return builder;
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

        /// <summary>
        /// 加入枚举
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder With(EnumBuilder builder)
        {
            _namespace.Enums.Add(builder);
            return _TBuilder;
        }

        /// <summary>
        /// 加入枚举
        /// </summary>
        /// <param name="name">枚举名称</param>
        /// <param name="action">构建枚举</param>
        /// <returns></returns>
        public virtual TBuilder With(string name, Action<EnumBuilder> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            EnumBuilder builder = new EnumBuilder(name);
            action.Invoke(builder);
            _namespace.Enums.Add(builder);
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
            if (_base.UseCode)
                return _base.Code;

            /*
             * {usings}
             * {enums}
             * {structs}
             * {interfaces}
             * {classes}
             * {deletages}
             * {events}
            */

            const string Template = @"{usings}namespace {name}
{
{interfaces}
{enums}
{structs}
{classes}
{deletages}
{events}
}";

            var code = Template
                .Replace("{usings}", _namespace.Usings.Join("\n").CodeNewLine())
                .Replace("{name}", _base.Name)
                .Replace("{interfaces}", _namespace.Interfaces.Select(x => x.ToFullCode()).Join("\n"))
                .Replace("{enums}", _namespace.Enums.Select(x => x.ToFullCode()).Join("\n"))
                .Replace("{structs}", _namespace.Structs.Select(x => x.ToFullCode()).Join("\n"))
                .Replace("{classes}", _namespace.Classes.Select(x => x.ToFullCode()).Join("\n"))
                .Replace("{deletages}", _namespace.Delegates.Select(x => x.ToFullCode()).Join("\n"))
                .Replace("{events}", _namespace.Events.Select(x => x.ToFullCode()).Join("\n"))
                ;

            return code;
        }

        /// <summary>
        /// 使用语法树格式化代码
        /// <para>命名空间的格式化代码不包括 using 部分</para>
        /// </summary>
        /// <returns></returns>
        public override string ToFormatCode()
        {
            var code = BuildSyntax().NormalizeWhitespace().ToFullString();
            return code;
        }


        public abstract NamespaceDeclarationSyntax BuildSyntax();
    }
}
