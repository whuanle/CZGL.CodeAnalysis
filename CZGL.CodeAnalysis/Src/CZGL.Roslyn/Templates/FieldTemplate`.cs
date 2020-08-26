using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn
{

    public abstract class FieldTemplate<TBuilder> : VariableTemplate<TBuilder> where TBuilder : FieldTemplate<TBuilder>
    {
        /// <summary>
        /// 设置修饰符，是否为常量，是否为静态成员，是否只读
        /// <para> MemberQualifierType.Abstract 对字段无效</para>
        /// </summary>
        /// <param name="qualifierType"></param>
        /// <returns></returns>
        public virtual TBuilder SetQualifier(MemberQualifierType qualifierType = MemberQualifierType.Default)
        {
            MemberQualifier = RoslynHelper.GetName(qualifierType);
            return _TBuilder;
        }


        /// <summary>
        /// 设置修饰符，是否为常量，是否为静态成员，是否只读
        /// </summary>
        /// <param name="str">static... </param>
        /// <returns></returns>
        public virtual TBuilder SetQualifier(string str = "")
        {
            MemberQualifier = str;
            return _TBuilder;
        }

    }
}
