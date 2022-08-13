using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 拼接代码拓展
    /// </summary>
    public static class CodeExtensions
    {
        /// <summary>
        /// Windows 下 使用 r\n，Linux 下时，需要使用 \n
        /// <para>或者使用 Environment.NewLine </para>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string WithUnixEOL(this string source) => source.Replace("\r\n", Environment.NewLine);

        /// <summary>
        /// 将集合中的元素以某个字符结尾组合成字符串
        /// </summary>
        /// <param name="source">集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> source, string separator) => string.Join(separator, source);

        /// <summary>
        /// 将集合中的元素以 <see cref="Environment.NewLine"/> 字符结尾组合成字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Join(this HashSet<string> list) => string.Join(Environment.NewLine, list);

        /// <summary>
        /// 代码换行；如果源字符串为空，则不改变
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CodeNewLine(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            return source + Environment.NewLine;
        }

        /// <summary>
        /// 代码加一个空格；如果源字符串为空，则不改变
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CodeNewSpace(this string? source)
        {
            if (string.IsNullOrEmpty(source))
                return "";
            return source + " ";
        }

        /// <summary>
        /// 如果源字符串为空，则不改变，如果不为空，则再前面加上符号
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string CodeNewBefore(this string? source, string separator = " ")
        {
            if (string.IsNullOrEmpty(source))
                return "";
            return separator + source;
        }

        /// <summary>
        /// 如果源字符串为空，则不改变，如果不为空，则再后面面加上符号
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string CodeNewAfter(this string? source, string separator = " ")
        {
            if (string.IsNullOrEmpty(source))
                return "";
            return source + separator;
        }

        /// <summary>
        /// 对每个成员执行一个操作
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector">要执行的任务</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Execute<TSource>(this IEnumerable<TSource> source, Action<TSource> selector)
        {
            var tor = source.GetEnumerator();
            while (tor.MoveNext())
            {
                selector.Invoke(tor.Current);
            }
            return source;
        }
    }
}
