using Microsoft.CodeAnalysis.CSharp;
using System;

namespace CZGL.Roslyn
{
    public class CodeContext
    {

        /// <summary>
        /// 默认配置
        /// </summary>
        private readonly CSharpParseOptions _csharpParseOptions = new CSharpParseOptions();

        /// <summary>
        /// 设置当前编译环境的
        /// </summary>
        /// <param name="options">C# 分析器配置</param>
        public CodeContext(Action<CSharpParseOptions> options)
        {
            options.Invoke(_csharpParseOptions);
        }

        /// <summary>
        /// 缩进字符长度，一般为三/四个空格或者 TAB
        /// </summary>
        public string indentation = "    ";

        /// <summary>
        /// 换行符
        /// </summary>
        public string eol = Environment.NewLine;

        /// <summary>
        /// 其中所有空格和行尾琐事都替换为常规格式的琐事
        /// </summary>
        public bool elasticTrivia = false;
    }
}
