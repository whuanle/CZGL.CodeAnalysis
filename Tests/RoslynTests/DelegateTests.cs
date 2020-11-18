using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public delegate void T1();", result.WithUnixEOL());
        }

        [Fact]
        public void 委托_T1_类中()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T1")
                .WithAccess(NamespaceAccess.Public)
                .WithReturnType("void");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public delegate void T1();", result.WithUnixEOL());
        }

        [Fact]
        public void 委托_T2_返回值()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T2")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("string");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public delegate string T2();", result.WithUnixEOL());
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
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public delegate string T3(string a);", result.WithUnixEOL());
        }

        [Fact]
        public void 委托_T4_特性()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("T4")
                .WithAttributes(new string[] { @"[Test(""1"", ""2"", A = ""3"", B = ""4"")]" })
                .WithAccess(MemberAccess.Public);

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T4();", result.WithUnixEOL());
        }

        [Fact]
        public void 委托_T5_通过字符串生成()
        {
            DelegateBuilder builder = DelegateBuilder.FromCode(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T5();");

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T5();", result.WithUnixEOL());
        }

        public delegate T2 Test<T1, T2, T3, T4, T5>(string a, string b)
                    where T2 : struct
                    where T3 : class
                    where T4 : notnull
                    where T5 : IEnumerable<int>, IQueryable<int>;

        [Fact]
        public void 委托_T5_泛型委托()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("Test")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("T2")
                .WithGeneric(builder =>
                {
                    builder
                    .WithCreate("T1").WithEnd()
                    .WithCreate("T2").WithStruct().WithEnd()
                    .WithCreate("T3").WithClass().WithEnd()
                    .WithCreate("T4").WithNotnull().WithEnd()
                    .WithCreate("T5").WithInterface("IEnumerable<int>", "IQueryable<int>").WithEnd();
                });

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public delegate T2 Test<T1, T2, T3, T4, T5>()
    where T2 : struct where T3 : class where T4 : notnull where T5 : IEnumerable<int>, IQueryable<int>;", result.WithUnixEOL());
        }

        [Fact]
        public void 委托_T5_泛型委托代码生成()
        {
            DelegateBuilder builder = DelegateBuilder.FromCode(@"public delegate T2 Test<T1, T2, T3, T4, T5>(string a, string b)
                    where T2 : struct
                    where T3 : class
                    where T4 : notnull
                    where T5 : IEnumerable<int>, IQueryable<int>;");

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public delegate T2 Test<T1, T2, T3, T4, T5>(string a, string b)
    where T2 : struct where T3 : class where T4 : notnull where T5 : IEnumerable<int>, IQueryable<int>;", result.WithUnixEOL());
        }
    }
}
