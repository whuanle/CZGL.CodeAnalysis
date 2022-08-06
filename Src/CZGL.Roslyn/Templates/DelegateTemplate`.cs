using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 委托构建器模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class DelegateTemplate<TBuilder> : FuncTemplate<TBuilder>
        where TBuilder : DelegateTemplate<TBuilder>
    {

        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="access">标记</param>
        /// <returns></returns>
        public TBuilder WithAccess(NamespaceAccess access = NamespaceAccess.Internal)
        {
            _member.Access = EnumCache.View<NamespaceAccess>(access);
            return _TBuilder;
        }
    }
}
