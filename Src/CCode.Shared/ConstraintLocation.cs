using System;
using System.Collections.Generic;
using System.Text;

namespace CCode.Shared
{
    /// <summary>
    /// 此泛型必须排序的位置，解析泛型约束的标记。
    /// <para>你也可以 <see href="https://www.cnblogs.com/whuanle/p/12252754.html#113-泛型的参数名称和泛型限定">查看具体的约束说明</see></para>
    /// </summary>
    [CLSCompliant(true)]
    public enum ConstraintLocation : int
    {
        /// <summary>
        /// 此约束不能与其它约束同时使用
        /// </summary>
        Only = 1,

        /// <summary>
        /// 此约束必须放在首位
        /// </summary>
        First = 1 << 1,

        /// <summary>
        /// 不限定位置
        /// </summary>
        Any = 1 << 2,

        /// <summary>
        /// 此约束必须放在最后
        /// </summary>
        End = 1 << 3,

        /// <summary>
        /// 没有任何约束
        /// </summary>
        None = 0
    }
}
