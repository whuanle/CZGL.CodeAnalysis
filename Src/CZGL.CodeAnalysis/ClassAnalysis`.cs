using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis
{
    /// <summary>
    /// 解析一个类型
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public class ClassAnalysis<TClass>: ClassAnalysis
    {
        public ClassAnalysis():base(typeof(TClass))
        {
        }
    }
}
