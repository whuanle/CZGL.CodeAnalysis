using CZGL.Roslyn.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Extensions
{
    public static class VariableExtensions
    {
        public static VariableTemplate<TBuilder> WithType<TBuilder>(this VariableTemplate<TBuilder> builder,Type type)where TBuilder: VariableTemplate<TBuilder>
        {

            return builder;
        }
    }
}
