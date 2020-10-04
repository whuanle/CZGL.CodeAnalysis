using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    /// <summary>
    /// 解析泛型约束的标记
    /// <para>你也可以 <see href="https://www.cnblogs.com/whuanle/p/12252754.html#113-泛型的参数名称和泛型限定">查看具体的约束说明</see></para>
    /// </summary>
    public enum ConstraintScheme
    {
        /// <summary>
        /// 红色，不能与其它约束同时使用
        /// </summary>
        Red = 0,

        /// <summary>
        /// 黄色，必须放在首位；多个黄色块无法同时使用
        /// </summary>
        Yellow = 1,

        /// <summary>
        /// 蓝色，不限定
        /// </summary>
        Blue = 2,

        /// <summary>
        /// 橙色，必须放在最后
        /// </summary>
        Orange = 3,

        /// <summary>
        /// 没有任何约束
        /// </summary>
        None = 4
    }
}
