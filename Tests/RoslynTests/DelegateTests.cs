using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class DelegateTests
    {
        ITestOutputHelper _tempOutput;
        public DelegateTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        private class TestAttribute : Attribute
        {
            public TestAttribute() { }
            public TestAttribute(string a, string b) { }

            public string A { get; set; }
            public string B { get; set; }
        }

        public delegate void T1();
        public delegate string T2();
        public delegate string T3(string a);

        [Test("1", "2", A = "3", B = "4")]
        public delegate void T4();


        [Fact]
        public void 委托_T1_命名空间()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T1")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("void")
                .WithName("T1");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate void T1();", result);
        }

        [Fact]
        public void 委托_T1_类中()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T1")
                .WithAccess(NamespaceAccess.Public)
                .WithReturnType("void");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate void T1();", result);
        }

        [Fact]
        public void 委托_T2_返回值()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T2")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("string");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate string T2();", result);
        }

        [Fact]
        public void 委托_T3_参数和返回值()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T3")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("string")
                .WithParams("string a");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate string T3(string a);", result);
        }

        [Fact]
        public void 委托_T4_特性()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T4")
                .WithAttributes(new string[] { @"[Test(""1"", ""2"", A = ""3"", B = ""4"")]" })
                .WithAccess(MemberAccess.Public);

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T4();", result);
        }

        [Fact]
        public void 委托_T5_通过字符串生成()
        {
            DelegateBuilder builder = DelegateBuilder.FromCode(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T5();");

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T5();", result);
        }
    }
}
