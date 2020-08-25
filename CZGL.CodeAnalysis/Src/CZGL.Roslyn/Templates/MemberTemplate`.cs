using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class MemberTemplate<TBuilder> where TBuilder : MemberTemplate<TBuilder>
    {
        protected internal string Visibility = string.Empty;
        protected internal string Qualifier = string.Empty;
        protected internal readonly List<string> MemberAttrs = new List<string>();
        protected internal string MemberName = "YourName";

        protected internal TBuilder _TBuilder;

        /// <summary>
        /// 添加一些特性
        /// <para>会清除已存在的特性</para>
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public virtual TBuilder SetAttributeLists(string[] attrs = null)
        {
            MemberAttrs.Clear();
            MemberAttrs.AddRange(attrs);
            return _TBuilder;
        }



        /// <summary>
        /// 添加一个特性
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public virtual TBuilder AddAttribute(string attr)
        {
            MemberAttrs.Add(attr);
            return _TBuilder;
        }

        /// <summary>
        /// 访问权限
        /// </summary>
        /// <param name="visibilityType"></param>
        /// <returns></returns>
        public virtual TBuilder SetVisibility(MemberVisibilityType visibilityType = MemberVisibilityType.Internal)
        {
            Visibility = RoslynHelper.GetName(visibilityType);
            return _TBuilder;
        }

        /// <summary>
        /// 访问权限
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetVisibility(string str = null)
        {
            Visibility =  str;
            return _TBuilder;
        }


        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder SetName(string name)
        {
            MemberName = name;
            return _TBuilder;
        }

        /// <summary>
        /// 随机设置一个名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder SetRondomName()
        {
            var b = Guid.NewGuid().ToByteArray();
            b[3] |= 0xF0;
            MemberName = new Guid(b).ToString("N");
            return _TBuilder;
        }
    }
}
