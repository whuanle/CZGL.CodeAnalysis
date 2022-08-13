using Microsoft.CodeAnalysis.CSharp;
using System;

namespace CCode.Roslyn
{
    /// <summary>
    /// 代码上下文
    /// </summary>
    public sealed class CodeContext
    {
        private readonly CSharpParseOptions _csharpParseOptions = new CSharpParseOptions();

        /// <summary>
        /// C# 代码配置
        /// </summary>
        public CSharpParseOptions CSharpParseOptions => _csharpParseOptions;

        /// <summary>
        /// 设置当前编译环境的 C# 语言解析配置
        /// </summary>
        /// <param name="options">C# 分析器配置</param>
        public CodeContext(Action<CSharpParseOptions> options)
        {
            options.Invoke(_csharpParseOptions);
        }

        /// <summary>
        /// 缩进字符长度，一般为三/四个空格或者 TAB
        /// </summary>
        public string Indentation { get; set; } = "    ";

        /// <summary>
        /// 换行符
        /// </summary>
        public string Eol { get; set; } = "\r\n";

        /// <summary>
        /// 处理元素之间的空白，如空格、空行等
        /// </summary>
        public bool ElasticTrivia { get; set; } = false;
    }
}
