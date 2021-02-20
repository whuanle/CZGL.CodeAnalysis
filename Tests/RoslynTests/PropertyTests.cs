using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class PropertyTests
    {

        ITestOutputHelper _tempOutput;
        public PropertyTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        /*
protected internal static readonly int i
{
    get;
    set;
}

= int.Parse("1");
         */

        /*
protected internal static readonly int i
{
    get
    {
        return tmp + 1;
    }

    set
    {
        tmp += 1;
    }
}

= int.Parse("1");
         */

        [Fact]
        public void 定义属性_T1_简单型()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithType("int");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"int i
{
    get;
    set;
}", result.WithUnixEOL());
        }

        [Fact]
        public void 定义属性_T2_常量初始化值()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithType("int")
                .WithInit("0");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"int i
{
    get;
    set;
}

= 0;", result.WithUnixEOL());
        }


        [Fact]
        public void 定义属性_T3_表达式初始化值()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithType("int")
                .WithInit("int.Parse(\"1\")");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"int i
{
    get;
    set;
}

= int.Parse(""1"");", result.WithUnixEOL());
        }


        [Fact]
        public void 定义属性_T4_访问修饰符()
        {
            var field1 = CodeSyntax.CreateProperty("i")
                .WithAccess(MemberAccess.Public)
                .WithType("int")
                .WithName("i");

            var result = field1.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public int i
{
    get;
    set;
}", result.WithUnixEOL());
        }

        [Fact]
        public void 定义属性_T5_修饰符()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithAccess(MemberAccess.ProtectedInternal)
                .WithKeyword(PropertyKeyword.Static)
                .WithType("int");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"protected internal static int i
{
    get;
    set;
}", result.WithUnixEOL());
        }


        [Fact]
        public void 定义属性_T6_超长的泛型()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithType("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>")
                .WithName("i")
                .WithInit("new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>()");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> i
{
    get;
    set;
}

= new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();", result.WithUnixEOL());
        }

        [Fact]
        public void 定义属性_T7_字符串整体生成()
        {
            var builder = PropertyBuilder.FromCode(@"[Display(Name = ""a"")]
public int a{get;set;}");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
public int a
{
    get;
    set;
}", result.WithUnixEOL());
        }


        [Fact]
        public void 定义属性_T8_特性注解()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithAttributes(new string[] { @"[Display(Name = ""a"")]", @"[Key]" })
                .WithAccess(MemberAccess.Public)
                .WithType("int");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
[Key]
public int i
{
    get;
    set;
}", result.WithUnixEOL());
        }


        [Fact]
        public void 属性_T1()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i")
                .WithAccess(MemberAccess.ProtectedInternal)
                .WithKeyword(PropertyKeyword.Static)
                .WithType("int")
                .WithName("i")
                .WithInit("int.Parse(\"1\")");

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif

            Assert.Equal(@"protected internal static int i
{
    get;
    set;
}

= int.Parse(""1"");", result.WithUnixEOL());


        }


        [Fact]
        public void 属性_T2()
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i");
            var field = builder
                .WithAccess(MemberAccess.ProtectedInternal)
                .WithKeyword(PropertyKeyword.Static)
                .WithType("int")
                .WithName("i")
                .WithGetSet("get{return tmp+1;}", "set{tmp+=1;}")
                .WithInit("int.Parse(\"1\")");

            var result = builder.ToFormatCode();

#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif


            Assert.Equal(@"protected internal static int i
{
    get
    {
        return tmp + 1;
    }

    set
    {
        tmp += 1;
    }
}

= int.Parse(""1"");", result.WithUnixEOL());
        }

    }
}
