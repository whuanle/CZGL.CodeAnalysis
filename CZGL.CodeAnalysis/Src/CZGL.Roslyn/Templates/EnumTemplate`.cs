using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 枚举创建模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class EnumTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : EnumTemplate<TBuilder>
    {
        protected internal EnumState _enum = new EnumState();

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="visibilityType">标记</param>
        /// <returns></returns>
        public virtual TBuilder WithAccess(NamespaceAccess access = NamespaceAccess.Internal)
        {
            _member.Access = RoslynHelper.GetName(access);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个枚举字段
        /// <para>也可同时为其添加特性</para>
        /// <code>
        /// [Display]
        /// First = 1
        /// </code>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual TBuilder WithField(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            _enum.Fields.Add(code);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个枚举字段，比为字段添加特性
        /// <code>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="func">特性</param>
        /// <returns></returns>
        public virtual TBuilder WithField(string code, Action<AttributeBuilder> func)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            AttributeBuilder builder = new AttributeBuilder();
            func.Invoke(builder);

            _enum.Fields.Add($"{builder.ToFullCode()}\n{code}");
            return _TBuilder;
        }
    }
}
