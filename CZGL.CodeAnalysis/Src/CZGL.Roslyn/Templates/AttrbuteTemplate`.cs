using CZGL.Roslyn.States;
using System;
using System.Linq;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 特性构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class AttrbuteTemplate<TBuilder> where TBuilder : AttrbuteTemplate<TBuilder>
    {
        protected internal AttributeState _attribute;

        protected internal TBuilder _TBuilder;


        /// <summary>
        /// 要使用的特性类型
        /// <para>需要保证此特性已经存在</para>
        /// </summary>
        /// <param name="name">名称后缀不需要带上 Attribute</param>
        public virtual TBuilder WithName(string name)
        {
            _attribute.Name = name;
            return _TBuilder;
        }


        /// <summary>
        /// 设置构造函数中的参数
        /// <para>例如: ["666","false","\"myname\"","new int[1,2,3]"]</para>
        /// <example>
        /// <code>
        /// "666"
        /// false
        /// "myname"
        /// "new int[1,2,3]"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual TBuilder WithCtor(string[] @params)
        {
            _attribute.Ctor = string.Join(",", @params);
            return _TBuilder;
        }

        /// <summary>
        /// 设置构造函数中的参数
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual TBuilder WithCtor(string paramsCode)
        {
            _attribute.Ctor = paramsCode;
            return _TBuilder;
        }

        /// <summary>
        /// 添加属性初始化
        /// <example>
        /// <code>
        /// param1: false
        /// param2:\"myname\"
        /// param3: new int[]{1,2,3}()
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public virtual TBuilder WithProperty(params string[] propertys)
        {
            _ = propertys.SelectMany(str =>
            {
                _attribute.Propertys.Add(str); return str;
            });

            return _TBuilder;
        }

        /// <summary>
        /// 完整输出代码
        /// <para>不会对代码进行检查，直接输出当前已经定义的代码</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFullCode();

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFormatCode();

    }
}
