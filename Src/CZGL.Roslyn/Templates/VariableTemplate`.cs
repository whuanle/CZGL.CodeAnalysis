using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{

    /// <summary>
    /// 类或结构体成员模板，如字段或属性、事件
    /// <para>实现者有 <see cref="FieldBuilder"/>、<see cref="PropertyBuilder"/>、<see cref="EventBuilder"/></para>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class VariableTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : VariableTemplate<TBuilder>
    {
        /// <summary>
        /// 变量状态机
        /// </summary>
        protected internal readonly VariableState _variable = new VariableState();

        /// <summary>
        /// 设置字段的关键字，如 static，readonly 等
        /// <para>注意，关键字拼写错误，会导致代码出现严重错误</para>
        /// </summary>
        /// <param name="keyword">static... </param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(string keyword = "")
        {
            _variable.Keyword = keyword;
            return _TBuilder;
        }

        /// <summary>
        /// 定义类型
        /// </summary>
        /// <param name="typeName">不能为空</param>
        /// <returns></returns>
        public virtual TBuilder WithType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException(nameof(typeName));
            _variable.MemberType = typeName;
            return _TBuilder;
        }



        /// <summary>
        /// 初始化器
        /// <para>
        /// <code>
        ///     int a = 666;<br />
        ///     int b {get;set;} = 666;<br />
        ///     666 为初始化部分<br />
        /// </code>
        /// </para>
        /// <para>
        /// <code>
        ///     int a = int.Parse("666");<br />
        ///     int b {get;set;} = int.Parse("666");<br />
        ///     int.Parse("666") 为初始化部分<br />
        /// </code>
        /// </para>
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithInit(string initCode)
        {
            if (string.IsNullOrEmpty(initCode))
                throw new ArgumentNullException(nameof(initCode));

            _variable.InitCode = initCode;
            return _TBuilder;
        }
    }
}
