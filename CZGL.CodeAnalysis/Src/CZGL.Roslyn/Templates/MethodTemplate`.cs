using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class MethodTemplate<TBuilder> : FuncTemplate<TBuilder> where TBuilder : MethodTemplate<TBuilder>
    {
        protected internal string BlockCode;

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="qualifierType"></param>
        /// <returns></returns>
        public TBuilder SetQualifier(MemberQualifierType qualifierType)
        {
            MemberQualifier = RoslynHelper.GetName(qualifierType);
            return _TBuilder;
        }

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="str">static... </param>
        /// <returns></returns>
        public TBuilder SetQualifier(string str = "")
        {
            MemberQualifier = str;
            return _TBuilder;
        }


        /// <summary>
        /// 方法体中的代码
        /// </summary>
        /// <param name="block">方法体中的代码</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// int a = 0;
        /// Console.WriteLine(a);
        /// </code>
        /// </example>
        public TBuilder SetBlock(string blockCode = null)
        {
            BlockCode = blockCode;
            return _TBuilder;
        }

    }
}