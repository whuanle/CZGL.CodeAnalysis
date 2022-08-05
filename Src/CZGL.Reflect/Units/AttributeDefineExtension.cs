using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CZGL.Reflect
{
    /// <summary>
    /// AttributeDefine 扩展
    /// </summary>
    public static class AttributeDefineExtension
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="define"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToCode(this AttributeDefine define)
        {
            if (define.ConstructorParams is { } && define.PropertyParams is { })
                return $"[{define.Name}]";
            var comma = define.ConstructorParams.Any() && define.PropertyParams.Any() ? "," : "";
            return $"[{define.Name}({string.Join(",", define.ConstructorParams.Select(c => c.Value.ToString()))}{comma}{string.Join(",", define.PropertyParams.Select(c => c.Value.ToString()))})]";
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="defines"></param>
        /// <returns></returns>
        public static string ToCode(this IEnumerable<AttributeDefine> defines)
        {
            return string.Join(Constants.NewLine, defines.Select(d => ToCode(d)));
        }
    }
}
