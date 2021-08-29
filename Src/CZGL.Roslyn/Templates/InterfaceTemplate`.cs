using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 接口构建器模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class InterfaceTemplate<TBuilder> : ObjectTemplate<TBuilder>
        where TBuilder : InterfaceTemplate<TBuilder>
    {

    }
}
