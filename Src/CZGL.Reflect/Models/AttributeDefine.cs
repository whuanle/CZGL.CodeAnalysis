using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CZGL.Reflect.Models
{

    /// <summary>
    /// 特性信息
    /// </summary>
    public interface AttributeDefine
    {
        /// <summary>
        /// 特性的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 特性类型
        /// </summary>
        Type AttributeType { get; }

        /// <summary>
        /// 特性构造函数中的值
        /// <para>ArgumentType、Value 属性分别表示构造函数中参数和参数值</para>
        /// </summary>
        CustomAttributeTypedArgument[] ConstructParams { get; }

        /// <summary>
        /// 特性的属性
        /// </summary>
        CustomAttributeTypedArgument[] PropertyParams { get; }
    }
}
