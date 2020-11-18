using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class CtorTestTests
    {
        ITestOutputHelper _tempOutput;
        public CtorTestTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        public class T1
        {
            public T1() { }
            public T1(string a) { }

        }
        public class T2 : T1
        {
            public T2() { Console.WriteLine("666"); }
            public T2(string a) : this() { }
            public T2(string a, string b) : base(a) { }
        }

        [Fact]
        public void 构造函数_T1()
        {
            CtorBuilder builder = CodeSyntax.CreateCtor("T2")
                .WithAccess(MemberAccess.Public)
                .WithBlock(@"Console.WriteLine(""666"");");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public T2()
{
    Console.WriteLine(""666"");
}", result.WithUnixEOL());
        }

        [Fact]
        public void 构造函数_T2_本类其它构造函数()
        {
            CtorBuilder builder = CodeSyntax.CreateCtor("T2")
                .WithAccess(MemberAccess.Public)
                .WithParams("string a")
                .WithThis("this()");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public T2(string a): this()
{
}", result.WithUnixEOL());

        }

        [Fact]
        public void 构造函数_T3_父类构造函数()
        {
            CtorBuilder builder = CodeSyntax.CreateCtor("T2")
                .WithAccess(MemberAccess.Public)
                .WithParams("string a,string b")
                .WithBase("base(a)");

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public T2(string a, string b): base(a)
{
}", result.WithUnixEOL());
        }


    }
}
