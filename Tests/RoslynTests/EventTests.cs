using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class EventTests
    {
         //public delegate void T();
         //public static void AAA() { }
         //public event T t1 = AAA;

        ITestOutputHelper _tempOutput;
        public EventTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void 事件_T1()
        {
            EventBuilder builder = CodeSyntax.CreateEvent("t1")
                .WithAccess(MemberAccess.Public)
                .WithType("T")
                .WithInit("AAA");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public event T t1 = AAA;", result.WithUnixEOL());
        }

        [Fact]
        public void 事件_T2_初始化值()
        {
            EventBuilder builder = CodeSyntax.CreateEvent("t1")
                .WithType("T")
                .WithAccess(MemberAccess.Public)
                .WithName("t1")
                .WithInit("AAA");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public event T t1 = AAA;", result.WithUnixEOL());
        }


        [Fact]
        public void 事件_T3_特性注解()
        {
            EventBuilder builder = CodeSyntax.CreateEvent("t1")
                .WithAttributes(new string[] { @"[Display(Name = ""a"")]", @"[Key]" })
                .WithAccess(MemberAccess.Public)
                .WithType("T")
                .WithName("t1")
                .WithInit("AAA");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
[Key]
public event T t1 = AAA;", result.WithUnixEOL());
        }


        [Fact]
        public void 事件_T4_通过字符串生成()
        {
            EventBuilder builder = EventBuilder.FromCode(@"[Display(Name = ""a"")]
[Key]
public event T t1 = AAA;");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
[Key]
public event T t1 = AAA;", result.WithUnixEOL());
        }


    }
}
