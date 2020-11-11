using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 构造函数构建器
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class CtorTemplate<TBuilder> : MethodTemplate<TBuilder> where TBuilder : CtorTemplate<TBuilder>
    {
        protected internal readonly CtorState _ctor = new CtorState();
    }
}
