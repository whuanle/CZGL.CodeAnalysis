using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    public static class CodeExtensions
    {
        /// <summary>
        /// Windows 下 使用 \n，Linux 下时，需要使用 \r\n 才算换行
        /// <para>或者使用 Environment.NewLine </para>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string WithUnixEOL(this string source) => source.Replace("\r\n", "\n");

        /// <summary>
        /// 将集合中的元素以某个字符结尾组合成字符串
        /// </summary>
        /// <param name="source">集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> source, string separator) => string.Join(separator, source);

        /// <summary>
        /// 将集合中的元素以 \n 字符结尾组合成字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Join(this HashSet<string> list)=> string.Join("\n", list);

        /// <summary>
        /// 代码换行；如果源字符串为空，则不改变
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CodeNewLine(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            return source + "\n";
        }
    }
}
