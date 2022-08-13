using CZGL.Reflect;
using CZGL.Roslyn.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// <see cref="MemberTemplate{T}"/> 扩展
    /// </summary>
    public static class MemberExtensions
    {
        /// <summary>
        /// 为成员添加特性注解
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <param name="builder"></param>
        /// <param name="attributeDefine"></param>
        /// <returns></returns>
        public static TBuilder WithAttribute<TBuilder>(this TBuilder builder, AttributeDefine attributeDefine) where TBuilder : MemberTemplate<TBuilder>
        {
            return builder.WithAttribute(attributeDefine.ToString());
        }

        /// <summary>
        /// 为成员添加特性注解
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <param name="builder"></param>
        /// <param name="attributeDefines">泛型定义</param>
        /// <returns></returns>
        public static TBuilder WithAttributes<TBuilder>(this TBuilder builder, IEnumerable<AttributeDefine> attributeDefines) where TBuilder : MemberTemplate<TBuilder>
        {
            return builder.WithAttributes(attributeDefines.Select(a => a.ToString()));
        }


        ///// <summary>
        ///// 设置成员的访问权限，如 public 、private
        ///// </summary>
        ///// <typeparam name="TBuilder"></typeparam>
        ///// <param name="builer"></param>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public static MemberTemplate<TBuilder> Add<TBuilder>(this MemberTemplate<TBuilder> builer,string code)where TBuilder:MemberTemplate<TBuilder>
        //{
        //    if (string.IsNullOrEmpty(code))
        //        throw new ArgumentNullException(nameof(code));

        //    builer. = code;
        //    return builer;
        //}
    }
}
