using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class AttrbuteTemplate<TBuilder> where TBuilder : AttrbuteTemplate<TBuilder>
    {
        protected internal string Name;
        protected internal string Ctor;
        protected internal string Propertys;

        protected internal TBuilder _TBuilder;


        /// <summary>
        /// 要使用的特性类型
        /// <para>需要保证此特性已经存在</para>
        /// </summary>
        /// <param name="attrType">名称后缀不需要带上 Attribute</param>
        public virtual TBuilder SetName(string attrType)
        {
            Name = attrType;
            return _TBuilder;
        }

        /// <summary>
        /// 要使用的特性的名字
        /// <para>需要保证此特性已经存在</para>
        /// </summary>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public virtual TBuilder SetName(Type attrType)
        {
            Name = attrType.Name;
            return _TBuilder;
        }

        /// <summary>
        /// 设置构造函数中的参数
        /// <para>例如: ["666","false","\"myname\"","new int[1,2,3]"]</para>
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// "666"
        /// false
        /// "myname"
        /// "new int[1,2,3]"
        /// </code>
        /// </example>
        public virtual TBuilder SetCtor(string[] @params)
        {
            Ctor = string.Join(",", @params);
            return _TBuilder;
        }

        /// <summary>
        /// 设置构造函数中的参数
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual TBuilder SetCtor(string paramsStr)
        {
            Ctor = paramsStr;
            return _TBuilder;
        }

        /// <summary>
        /// 添加属性初始化
        /// </summary>
        /// <param name="propertys"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// param1: false
        /// param2:\"myname\"
        /// param3: new int[]{1,2,3}()
        /// </code>
        /// </example>
        public virtual TBuilder SetProperty(params string[] propertys)
        {
            Propertys = string.Join(",",propertys);
            return _TBuilder;
        }

    }
}
