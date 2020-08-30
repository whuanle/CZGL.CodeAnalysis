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
        public void 委托_T1()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .SetVisibility(MemberVisibilityType.Public)
                .SetReturnType("void")
                .SetName("T1").Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate void T1();", result);
        }

        [Fact]
        public void 委托_T2()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .SetVisibility(MemberVisibilityType.Public)
                .SetReturnType("string")
                .SetName("T2").Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate string T2();", result);
        }

        [Fact]
        public void 委托_T3()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .SetVisibility(MemberVisibilityType.Public)
                .SetReturnType("string")
                .SetParams("string a")
                .SetName("T3").Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate string T3(string a);", result);
        }

        [Fact]
        public void 委托_T4()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .SetAttributeLists(new string[] { @"[Test(""1"", ""2"", A = ""3"", B = ""4"")]" })
                .SetVisibility(MemberVisibilityType.Public)
                .SetReturnType("void")
                .SetName("T4").Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T4();", result);
        }


    }
}
