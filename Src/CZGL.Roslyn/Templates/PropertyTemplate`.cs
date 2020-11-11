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
            _variable.Keyword = CodeHelper.GetName(keyword);
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

        /// <summary>
        /// 将 set 构造器设置为 set;        
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithDefaultSet()
        {
            _property.SetBlock = SetBlock;
            return _TBuilder;
        }

        /// <summary>
        /// 将 get 构造器设置为 get;
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithDefaultGet()
        {
            _property.GetBlock = GetBlock;
            return _TBuilder;
        }


        /// <summary>
        /// 属性 set 构造器
        /// <para>示例 set{z=value;}</para>
        /// </summary>
        /// <param name="blockCode"></param>
        /// <returns></returns>
        public virtual TBuilder WithSetInit(string blockCode)
        {
            if (string.IsNullOrWhiteSpace(blockCode))
                throw new ArgumentNullException(nameof(blockCode));
            _property.SetBlock = blockCode;
            return _TBuilder;
        }

        /// <summary>
        /// 属性 get 构造器
        /// <para>示例 get{return z;}</para>
        /// </summary>
        /// <param name="blockCode"></param>
        /// <returns></returns>
        public virtual TBuilder WithGetInit(string blockCode)
        {
            if (string.IsNullOrWhiteSpace(blockCode))
                throw new ArgumentNullException(nameof(blockCode));

            _property.GetBlock = blockCode;
            return _TBuilder;
        }

        /// <summary>
        /// 不设置 Set 构造器
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithNullSet()
        {
            _property.SetBlock = string.Empty;
            return _TBuilder;
        }


        /// <summary>
        /// 不设置 Get 构造器
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder WithNullGet()
        {
            _property.GetBlock = string.Empty;
            return _TBuilder;
        }

        #endregion

    }
}
