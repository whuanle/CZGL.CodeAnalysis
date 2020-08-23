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
        public class T2:T1
        {
            public T2() { Console.WriteLine("666"); }
            public T2(string a):this() { }
            public T2(string a,string b):base(a) { }
        }

        [Fact]
        public void 构造函数_T1()
        {
            CtorBuilder methodBuilder = new CtorBuilder();
            var method = methodBuilder.SetVisibility(MemberVisibilityType.Public)
                .SetName("T2")
                .SetBlock(@"Console.WriteLine(""666"");")
                .Build();
            var result= method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public T2()
{
    Console.WriteLine(""666"");
}",result);
        }

        [Fact]
        public void 构造函数_T2()
        {
            CtorBuilder methodBuilder = new CtorBuilder();
            var method = methodBuilder.SetVisibility(MemberVisibilityType.Public)
                .SetName("T2")
                .SetParams("string a")
                .SetThisCtor("this()")
                .Build();
            var result = method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public T2(string a): this()
{
}", result);

        }

        [Fact]
        public void 构造函数_T3()
        {
            CtorBuilder methodBuilder = new CtorBuilder();
            var method = methodBuilder.SetVisibility(MemberVisibilityType.Public)
                .SetName("T2")
                .SetParams("string a,string b")
                .SetBaseCtor("base(a)")
                .Build();
            var result = method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public T2(string a, string b): base(a)
{
}", result);
        }


    }
}
