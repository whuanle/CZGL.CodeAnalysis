using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{

    /// <summary>
    /// 属性构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class PropertyTemplate<TBuilder> : VariableTemplate<TBuilder> where TBuilder : PropertyTemplate<TBuilder>
    {
        protected internal readonly PropertyState _property = new PropertyState();

        #region 关键字

        /// <summary>
        /// 设置字段的关键字，如 static，readonly 等
        /// </summary>
        /// <param name="keyword">字段修饰符</param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(PropertyKeyword keyword = PropertyKeyword.Default)
        {
            _variable.Keyword = EnumCache.GetValue(keyword);
            return _TBuilder;
        }

        #endregion

        #region 构造器

        private const string GetBlock = "get;";
        private const string SetBlock = "set;";

        /// <summary>
        /// 将构造器设置为 { get; set; }
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithDefault()
        {
            _property.GetBlock = GetBlock;
            _property.SetBlock = SetBlock;
            return _TBuilder;
        }

#nullable enable

        /// <summary>
        /// 设置属性的 get 和 set 代码，不能两者都设置为空
        /// <para>请不要设置为 null，可以设置为 string.Empty ；</para>
        /// </summary>
        /// <param name="getCode"></param>
        /// <param name="setCode"></param>
        /// <returns></returns>
        public virtual TBuilder WithGetSet(string getCode = "get;", string setCode = "set;")
        {
            if (string.IsNullOrEmpty(getCode) && string.IsNullOrEmpty(setCode))
                throw new ArgumentNullException(nameof(getCode) + "," + nameof(setCode));

            _property.GetBlock = getCode;
            _property.SetBlock = setCode;
            return _TBuilder;
        }

#nullable enable

        #endregion

    }
}
