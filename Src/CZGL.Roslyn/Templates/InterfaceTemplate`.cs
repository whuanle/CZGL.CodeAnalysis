using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.Templates
{
    public abstract class InterfaceTemplate<TBuilder> : ObjectTemplate<TBuilder>
        where TBuilder : InterfaceTemplate<TBuilder>
    {

    }
}
