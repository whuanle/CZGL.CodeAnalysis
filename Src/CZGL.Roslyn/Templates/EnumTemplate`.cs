using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 枚举创建模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class EnumTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : EnumTemplate<TBuilder>
    {
        /// <summary>
        /// 枚举的字段
        /// </summary>
        /// <summary>
        /// 成员使用到的命名空间
        /// </summary>
        protected readonly HashSet<string> _fields = new HashSet<string>();

        /// <summary>
        /// 成员使用到的命名空间
        /// </summary>
        public IReadOnlyList<string> Fields => _fields.ToList();

        /// <summary>
        /// 通过代码形式添加一个枚举字段
        /// <para>也可同时为其添加特性</para>
        /// <code>
        /// [Display]
        /// First = 1
        /// </code>
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public virtual TBuilder WithField(string field)
        {
            _fields.Add(field);
            return (TBuilder)this;
        }

        /// <summary>
        /// 添加一个枚举字段，比如为字段添加特性
        /// </summary>
        /// <param name="field"></param>
        /// <param name="builder">特性</param>
        /// <returns></returns>
        public virtual TBuilder WithField(string field, Action<AttributeBuilder> builder)
        {
            AttributeBuilder _builder = new AttributeBuilder();
            builder.Invoke(_builder);

            _fields.Add($"{builder.ToFullCode()}\n{code}");
            return (TBuilder)this;
        }
    }
}
