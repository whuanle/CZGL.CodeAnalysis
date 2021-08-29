using CZGL.Roslyn.States;
using System;
using System.Linq;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 特性构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class AttrbuteTemplate<TBuilder> : BaseTemplate where TBuilder : AttrbuteTemplate<TBuilder>
    {
        /// <summary>
        /// 特性状态机
        /// </summary>
        protected internal readonly AttributeState _attribute = new AttributeState();

        /// <summary>
        /// 模板构建器
        /// </summary>
        protected internal TBuilder _TBuilder;


        /// <summary>
        /// 要使用的特性类型
        /// <para>需要保证此特性已经存在</para>
        /// </summary>
        /// <param name="name">名称后缀不需要带上 Attribute</param>
        public new virtual TBuilder WithName(string name)
        {
            base.WithName(name);
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
        /// <param name="paramsCode"></param>
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
            _ = propertys.Execute(str => _attribute.Propertys.Add(str));

            return _TBuilder;
        }
    }
}
