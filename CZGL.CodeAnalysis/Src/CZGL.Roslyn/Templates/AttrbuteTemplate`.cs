using System;

namespace CZGL.Roslyn.Templates
{
    public abstract class AttrbuteTemplate<TBuilder> where TBuilder : AttrbuteTemplate<TBuilder>
    {
        protected internal string MemberName;
        protected internal string MemberCtor;
        protected internal string MemberPropertys;

        protected internal TBuilder _TBuilder;


        /// <summary>
        /// 要使用的特性类型
        /// <para>需要保证此特性已经存在</para>
        /// </summary>
        /// <param name="attrType">名称后缀不需要带上 Attribute</param>
        public virtual TBuilder SetName(string attrType)
        {
            MemberName = attrType;
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
            MemberName = attrType.Name;
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
        public virtual TBuilder SetCtor(string[] @params)
        {
            MemberCtor = string.Join(",", @params);
            return _TBuilder;
        }

        /// <summary>
        /// 设置构造函数中的参数
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual TBuilder SetCtor(string paramsStr)
        {
            MemberCtor = paramsStr;
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
        public virtual TBuilder SetProperty(params string[] propertys)
        {
            MemberPropertys = string.Join(",", propertys);
            return _TBuilder;
        }


        /// <summary>
        /// 获得格式化代码
        /// </summary>
        /// <returns></returns>
        public abstract string FullCode();

    }
}
