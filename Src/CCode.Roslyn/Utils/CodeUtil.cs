using System;
using System.Runtime.CompilerServices;

namespace CCode.Roslyn
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
