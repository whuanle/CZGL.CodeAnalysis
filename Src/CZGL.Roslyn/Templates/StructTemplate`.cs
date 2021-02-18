using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 结构体构建器模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class StructTemplate<TBuilder> : ObjectTypeTemplate<TBuilder>
        where TBuilder : StructTemplate<TBuilder>
    {

        /// <summary>
        /// 设置修饰符关键字
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public TBuilder WithKeyword(StructKeyword keyword)
        {
            _typeState.Keyword = EnumCache.GetStructKword(keyword);

            return _TBuilder;
        }
    }
}
