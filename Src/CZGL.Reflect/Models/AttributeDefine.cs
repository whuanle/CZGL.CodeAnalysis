using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CZGL.Reflect
{

    /// <summary>
    /// 特性定义信息
    /// </summary>
    [CLSCompliant(true)]
    public class AttributeDefine
    {
        /// <summary>
        /// 特性的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 特性类型
        /// </summary>
        public Type AttributeType { get; set; }

        /// <summary>
        /// 特性构造函数中的值
        /// <para>ArgumentType、Value 属性分别表示构造函数中参数和参数值</para>
        /// </summary>
        public CustomAttributeTypedArgument[] ConstructParams { get; set; }

        /// <summary>
        /// 特性的属性
        /// </summary>
        public CustomAttributeTypedArgument[] PropertyParams { get; set; }
    }
}
