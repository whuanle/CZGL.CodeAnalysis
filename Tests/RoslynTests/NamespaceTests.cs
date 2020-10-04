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
            const string constCode = @"
namespace MySpace
{
}";

            string name = "MySpace";
            var builder = CodeSyntax.CreateNamespace();
            builder.WithName(name);

            var code = builder.ToFullCode();
#if Log
            _tempOutput.WriteLine(code);
#endif
            Assert.Equal(constCode, code);



            builder = new NamespaceBuilder(name);
            code = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(code);
#endif
            Assert.Equal(constCode, code);

        }



        [Fact]
        public void 添加命名空间()
        {
            const string constCode = @"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace MySpace
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

            var code = builder.ToFullCode();

#if Log
            _tempOutput.WriteLine(code);
#endif
            Assert.Equal(constCode, code);
        }
    }
}
