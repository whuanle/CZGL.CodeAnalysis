using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    /// <summary>
    /// 命名空间测试
    /// </summary>
    public class NamespaceTests
    {
        ITestOutputHelper _tempOutput;
        public NamespaceTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void 设置名字()
        {
            const string constCode = @"namespace MySpace
{
}";

            string name = "MySpace";
            var builder = CodeSyntax.CreateNamespace(name);

            var code = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(code);
#endif
            Assert.Equal(constCode, code.WithUnixEOL());



            builder = CodeSyntax.CreateNamespace(name);
            code = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(code);
#endif
            Assert.Equal(constCode, code);

        }



        [Fact]
        public void 添加命名空间()
        {
            const string constCode = @"namespace MySpace
{
}";

            var builder = CodeSyntax.CreateNamespace("MySpace");
            builder
                .WithUsing("System")
                .WithUsing(
                "System.Collections.Generic",
                "System.ComponentModel",
                "System.Linq",
                "System.Security.Cryptography",
                "System.Threading");

            var code = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(code);
#endif
            Assert.Equal(constCode, code.WithUnixEOL());
        }
    }
}
