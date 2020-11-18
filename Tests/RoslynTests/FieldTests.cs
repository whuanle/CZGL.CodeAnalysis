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


    /// <summary>
    /// 字段生成测试
    /// </summary>
    public class FieldTests
    {
        ITestOutputHelper _tempOutput;
        public FieldTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }


        // int i;
        // int i = 0;
        // int i = int.Parse("1");
        // public int i;
        // protected internel static readonly int i = int.Parse("1");
        // List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> i = new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();

        [Fact]
        public void 定义字段_T1_简单型()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("int");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("int i;", result.WithUnixEOL());
        }

        [Fact]
        public void 定义字段_T2_常量初始化值()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("int")
                .WithInit("0");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("int i = 0;", result.WithUnixEOL());
        }


        [Fact]
        public void 定义字段_T3_表达式初始化值()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("int")
                .WithInit("int.Parse(\"1\")");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("int i = int.Parse(\"1\");", result.WithUnixEOL());
        }


        [Fact]
        public void 定义字段_T4_访问修饰符()
        {
            var field1 = CodeSyntax.CreateField("i")
                .WithAccess(MemberAccess.Public)
                .WithType("int")
                .WithName("i");

            var result = field1.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("public int i;", result.WithUnixEOL());
        }

        [Fact]
        public void 定义字段_T5_修饰符()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithAccess(MemberAccess.ProtectedInternal)
                .WithKeyword(FieldKeyword.Static)
                .WithType("int");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("protected internal static int i;", result.WithUnixEOL());
        }


        [Fact]
        public void 定义字段_T6_超长的泛型()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>")
                .WithName("i")
                .WithInit("new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>()");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> i = new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();", result.WithUnixEOL());
        }

        [Fact]
        public void 定义字段_T7_字符串整体生成()
        {
            var builder = FieldBuilder.FromCode(@"[Display(Name = ""a"")]
public int a;");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
public int a;", result.WithUnixEOL());
        }


        [Fact]
        public void 定义字段_T8_特性注解()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithAttributes(new string[] { @"[Display(Name = ""a"")]", @"[Key]" })
                .WithAccess(MemberAccess.Public)
                .WithType("int");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
[Key]
public int i;", result.WithUnixEOL());
        }
    }
}
