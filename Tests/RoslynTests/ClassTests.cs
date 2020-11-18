using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class ABCZH
    {
        private protected void Test() { }
    }

    public class ClassTests
    {
        ITestOutputHelper _tempOutput;
        public ClassTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void 只命名()
        {
            ClassBuilder builder = CodeSyntax.CreateClass("Test");


            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"class Test
{
}", result.WithUnixEOL());

        }

        public class Test<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
            where T1 : struct
            where T2 : class
            where T3 : notnull
            where T4 : unmanaged
            where T5 : new()
            where T6 : Enum
            where T7 : IEnumerable<int>
            where T8 : T2
            where T9 : class, new()
            where T10 : IEnumerator<int>, IEnumerable<int>, new()
        {
            private readonly string str;
            public string Get { get { return str; } }

            public string Method()
            {
                return "";
            }
        }

        [Fact]
        public void 完整的测试()
        {
            ClassBuilder builder = CodeSyntax.CreateClass("Test")
                .WithAccess(NamespaceAccess.Public)
                .WithGeneric(b=>
                {
                    b.WithCreate("T1").WithStruct()
                    .WithCreate("T2").WithClass()
                    .WithCreate("T3").WithNotnull()
                    .WithCreate("T4").WithUnmanaged()
                    .WithCreate("T5").WithNotnull()
                    .WithCreate("T6").WithBase("Enum")
                    .WithCreate("T7").WithBase("IEnumerable<int>")
                    .WithCreate("T8").WithTo("T2")
                    .WithCreate("T9").WithClass().WithNew()
                    .WithCreate("T10").WithInterface("IEnumerator<int>", "IEnumerable<int>").WithNew();
                })
                .WithField("str",b=>
                {
                    b.WithAccess(MemberAccess.Public)
                    .WithKeyword(FieldKeyword.Readonly)
                    .WithType("string");
                })
                .WithProperty("Get", b=>
                {
                    b.WithAccess(MemberAccess.Public)
                    .WithType("string")
                    .WithGetInit("get { return str; }")
                    .WithNullSet();
                })
                .WithMethod("Method",b=>
                {
                    b.WithAccess(MemberAccess.Public)
                    .WithReturnType("string")
                    .WithBlock(@"return """";");
                });


            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public class Test<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    where T1 : struct where T2 : class where T3 : notnull where T4 : unmanaged where T5 : notnull where T6 : Enum where T7 : IEnumerable<int> where T8 : T2 where T9 : class, new()
    where T10 : IEnumerator<int>, IEnumerable<int>, new()
{
    public readonly string str;
    public string Get
    {
        get
        {
            return str;
        }
    }

    public string Method()
    {
        return """";
    }
}", result.WithUnixEOL());

        }


    }
}
