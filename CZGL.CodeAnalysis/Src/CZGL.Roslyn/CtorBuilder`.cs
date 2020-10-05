using CZGL.Roslyn.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 类构造函数构建器
    /// </summary>
    /// <typeparam name="TBuiler"></typeparam>
    public sealed class ClassCtorBuiler<TClass> : CtorBuilder where TClass : ClassTemplate<ClassBuilder>
    {
         private ClassTemplate<ClassBuilder> _classBuilder;
        internal ClassCtorBuiler(TClass builer):base(builer.Name)
        {
            _classBuilder = builer;
        }

        public ClassBuilder WithEnd()
        {
            return _classBuilder.GetBuilder();
        }
    }

    /// <summary>
    /// 结构体构造函数构建器
    /// </summary>
    /// <typeparam name="TBuiler"></typeparam>
    public sealed class StructCtorBuiler<TStruct> : CtorBuilder where TStruct : StructBuilder
    {
        private StructBuilder _structBuilder;
        internal StructCtorBuiler(TStruct builer):base(builer.Name)
        {
            _structBuilder = builer;
        }

        public ClassBuilder WithEnd()
        {
            return _structBuilder.GetBuilder();
        }
    }

}
