using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class PropertyTemplate<TBuilder> : FieldTemplate<TBuilder> where TBuilder : PropertyTemplate<TBuilder>
    {
        protected internal string SetBlock = "get;";
        protected internal string GetBlock = "set;";

        /// <summary>
        /// 设置为 set 构造器为 set;        
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder DefaultSet()
        {
            GetBlock = "set;";
            return _TBuilder;
        }

        /// <summary>
        /// 设置为 get 构造器为 get;
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder DefaultGet()
        {
            SetBlock = "get;";
            return _TBuilder;
        }


        /// <summary>
        /// 属性 set 构造器
        /// <para>示例 set{z=value;}</para>
        /// </summary>
        /// <param name="blockCode"></param>
        /// <returns></returns>
        public virtual TBuilder SetInitializer(string blockCode)
        {
            SetBlock = blockCode;
            return _TBuilder;
        }

        /// <summary>
        /// 属性 get 构造器
        /// <para>示例 get{return z;}</para>
        /// </summary>
        /// <param name="blockCode"></param>
        /// <returns></returns>
        public virtual TBuilder GetInitializer(string blockCode)
        {
            GetBlock = blockCode;
            return _TBuilder;
        }

        /// <summary>
        /// 删除 set 构造器
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder DeleteSet()
        {
            SetBlock = string.Empty;
            return _TBuilder;
        }


        /// <summary>
        /// 删除 get 构造器
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder DeleteGet()
        {
            GetBlock = string.Empty;
            return _TBuilder;
        }
    }
}
