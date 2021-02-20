using CZGL.Roslyn;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace RoslynExtensionsTests
{
    /// <summary>
    /// 字段构建器拓展测试
    /// </summary>
    public class FiledBuilderExtensionsTests
    {
        private readonly FieldBuilder builder = CodeSyntax.CreateField("T1");
        ITestOutputHelper _tempOutput;
        public FiledBuilderExtensionsTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        /// <summary>
        /// WithType 测试
        /// </summary>
        [Fact]
        public void 类型定义1()
        {
            builder.WithType<int>();
            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("int T1;", result.WithUnixEOL());
        }

        /// <summary>
        /// WithType 测试
        /// </summary>
        [Fact]
        public void 类型定义2()
        {
            builder.WithType(typeof(int));
            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("int T1;", result.WithUnixEOL());
        }

        private static readonly List<int> a;

        /// <summary>
        /// WithType 测试
        /// </summary>
        [Fact]
        public void 字段复制()
        {
            builder.WithCopy(typeof(FiledBuilderExtensionsTests).GetField("a"));
            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("int T1;", result.WithUnixEOL());
        }

    }
}
