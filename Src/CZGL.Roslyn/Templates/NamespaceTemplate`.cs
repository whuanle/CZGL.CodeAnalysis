using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Linq;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 命名空间模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class NamespaceTemplate<TBuilder> : BaseTemplate where TBuilder : NamespaceTemplate<TBuilder>
    {
        private readonly NamespaceState _namespace = new NamespaceState();
        
        /// <summary>
        /// 构建器
        /// </summary>
        protected internal TBuilder _TBuilder;

        /// <summary>
        /// 命名空间模板
        /// </summary>
        protected NamespaceTemplate() { }

        /// <summary>
        /// 命名空间模板
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        protected NamespaceTemplate(string namespaceName)
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
        /// <returns></returns>
        public virtual TBuilder WithRondomName()
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
        /// <param name="usingNames"></param>
        /// <returns></returns>
        public virtual TBuilder WithUsing(params string[] usingNames)
        {
            _ = usingNames.Execute(cl => _namespace.Usings.Add($"using {cl};"));
            return _TBuilder;
        }

        /// <summary>
        /// 自动引用此类型相同的命名空间
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual TBuilder WithAutoUsing(Type type)
        {
            _ = DependencyContext.Default.CompileLibraries.Execute(cl => _namespace.Usings.Add($"using {cl.Name};"));
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

        /// <summary>
        /// 创建一个结构体
        /// </summary>
        /// <returns></returns>
        public virtual StructBuilder CreateStruct()
        {
            var member = new StructBuilder();
            _namespace.Structs.Add(member);
            return member;
        }

        /// <summary>
        /// 创建一个接口
        /// </summary>
        /// <returns></returns>
        public virtual InterfaceBuilder CreateInterface()
        {
            var member = new InterfaceBuilder();
            _namespace.Interfaces.Add(member);
            return member;
        }

        /// <summary>
        /// 创建一个类
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual ClassBuilder CreateClass(ClassBuilder builder)
        {
            var member = new ClassBuilder();
            _namespace.Classes.Add(member);
            return member;
        }

        /// <summary>
        /// 创建一个委托
        /// </summary>
        /// <returns></returns>
        public virtual DelegateBuilder CreateDelegate()
        {
            var member = new DelegateBuilder();
            _namespace.Delegates.Add(member);
            return member;
        }

        /// <summary>
        /// 创建一个事件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual EventBuilder CreateEvent(EventBuilder builder)
        {
            var member = new EventBuilder();
            _namespace.Events.Add(member);
            return member;
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

        /// <summary>
        /// 加入成员
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder With(StructBuilder builder)
        {
            return _TBuilder;
        }

        /// <summary>
        /// 加入成员
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder With(InterfaceBuilder builder)
        {
            return _TBuilder;
        }

        /// <summary>
        /// 加入成员
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder With(ClassBuilder builder)
        {
            return _TBuilder;
        }

        /// <summary>
        /// 加入成员
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder With(DelegateBuilder builder)
        {
            return _TBuilder;
        }

        /// <summary>
        /// 加入成员
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder With(EventBuilder builder)
        {
            return _TBuilder;
        }

        #endregion

        internal TBuilder WithFromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            _base.UseCode = true;
            _base.Code = code;

            return _TBuilder;
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

        /// <summary>
        /// 生成命名空间语法树
        /// </summary>
        /// <returns></returns>
        public abstract NamespaceDeclarationSyntax BuildSyntax();
    }
}
