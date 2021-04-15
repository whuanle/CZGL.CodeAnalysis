using CZGL.Roslyn;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynExtensionsTests
{
    /// <summary>
    /// �ֶι�������չ����
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
        /// WithType ����
        /// </summary>
        [Fact]
        public void ���Ͷ���1()
        {
            builder.WithType<int>();
            var result = builder.ToFormatCode();
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("int T1;", result.WithUnixEOL());
        }

        /// <summary>
        /// WithType ����
        /// </summary>
        [Fact]
        public void ���Ͷ���2()
        {
            builder.WithType(typeof(int));
            var result = builder.ToFormatCode();
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("int T1;", result.WithUnixEOL());
        }
        private static readonly int a;
        private static readonly List<int> b;

        /// <summary>
        /// WithType ����
        /// </summary>
        [Fact]
        public void �ֶθ���()
        {
            builder.WithCopy(typeof(FiledExtensionsTests).GetField("a", BindingFlags.NonPublic | BindingFlags.Static));
            var result = builder.ToFormatCode();
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("private static readonly int a;", result.WithUnixEOL());
        }
        /// <summary>
        /// WithType ����
        /// </summary>
        [Fact]
        public void ���������ֶθ���()
        {
            builder.WithCopy(typeof(FiledExtensionsTests).GetField("b", BindingFlags.NonPublic | BindingFlags.Static));
            var result = builder.ToFormatCode();
            _tempOutput.WriteLine(result.WithUnixEOL());
            Assert.Equal("private static readonly List<int> b;", result.WithUnixEOL());
        }
    }
}
