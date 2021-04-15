using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CZGL.Roslyn.Utils
{
    /// <summary>
    /// 代码生成
    /// </summary>
    public static class CodeUtil
    {
        /// <summary>
        /// 生成随机名称
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CreateRondomName(string prefix = "N")
        {
            var time = DateTime.Now;
            return prefix + time.Ticks.ToString("X2");
        }
    }
}
