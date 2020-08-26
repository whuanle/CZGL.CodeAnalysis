using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class EventTemplate<TBuilder> : VariableTemplate<TBuilder> where TBuilder : EventTemplate<TBuilder>
    {
        /// <summary>
        /// 定义事件的委托类型
        /// </summary>
        /// <param name="typeName">不能为空</param>
        /// <returns></returns>
        public virtual TBuilder SetDelegateType(string typeName)
        {
            MemberType = typeName;
            return _TBuilder;
        }

    }
}
