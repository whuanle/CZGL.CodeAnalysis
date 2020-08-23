using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class QualifierTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : QualifierTemplate<TBuilder>
    {
        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="qualifierType"></param>
        /// <returns></returns>
        public virtual TBuilder SetQualifier(MemberQualifierType qualifierType)
        {
            Qualifier = RoslynHelper.GetName(qualifierType);
            return _TBuilder;
        }

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="str">static... </param>
        /// <returns></returns>
        public virtual TBuilder SetQualifier(string str = "")
        {
            Qualifier = str;
            return _TBuilder;
        }
    }
}
