using CZGL.Roslyn;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynExtensionsTests
{
    /// <summary>
    /// 字段构建器拓展测试
    /// </summary>
    public class FiledExtensionsTests
    {
        private readonly FieldBuilder builder = CodeSyntax.CreateField("T1");
        ITestOutputHelper _tempOutput;
        public FiledExtensionsTests(ITestOutputHelper tempOutput)
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
            _tempOutput.WriteLine(result.WithUnixEOL());
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
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("int T1;", result.WithUnixEOL());
        }
        private static readonly int a;
        private static readonly List<int> b;

        /// <summary>
        /// WithType 测试
        /// </summary>
        [Fact]
        public void 字段复制()
        {
            builder.WithCopy(typeof(FiledExtensionsTests).GetField("a", BindingFlags.NonPublic | BindingFlags.Static));
            var result = builder.ToFormatCode();
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("private static readonly int a;", result.WithUnixEOL());
        }
        /// <summary>
        /// WithType 测试
        /// </summary>
        [Fact]
        public void 泛型类型字段复制()
        {
            builder.WithCopy(typeof(FiledExtensionsTests).GetField("b", BindingFlags.NonPublic | BindingFlags.Static));
            var result = builder.ToFormatCode();
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("private static readonly List<int> b;", result.WithUnixEOL());
        }
    }
}
