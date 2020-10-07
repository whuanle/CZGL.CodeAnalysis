using BenchmarkDotNet.Attributes;
using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using System;
using System.Collections.Generic;
using System.Text;

namespace RosylnBenchmark
{
    public class FieldBen
    {

        [Benchmark]
        public void 定义字段_T1_简单型()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("int");

            var result = builder.ToFormatCode();
        }

        [Benchmark]
        public void 定义字段_T2_常量初始化值()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("int")
                .WithInit("0");

            var result = builder.ToFormatCode();
        }


        [Benchmark]
        public void 定义字段_T3_表达式初始化值()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("int")
                .WithInit("int.Parse(\"1\")");

            var result = builder.ToFormatCode();
        }


        [Benchmark]
        public void 定义字段_T4_访问修饰符()
        {
            var field1 = CodeSyntax.CreateField("i")
                .WithAccess(MemberAccess.Public)
                .WithType("int")
                .WithName("i");

            var result = field1.ToFormatCode();
        }

        [Benchmark]
        public void 定义字段_T5_修饰符()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithAccess(MemberAccess.ProtectedInternal)
                .WithKeyword(FieldKeyword.Static)
                .WithType("int");

            var result = builder.ToFormatCode();
        }


        [Benchmark]
        public void 定义字段_T6_超长的泛型()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithType("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>")
                .WithName("i")
                .WithInit("new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>()");

            var result = builder.ToFormatCode();
        }

        [Benchmark]
        public void 定义字段_T7_字符串整体生成()
        {
            var builder = FieldBuilder.FromCode(@"[Display(Name = ""a"")]
public int a;");

            var result = builder.ToFormatCode();
        }


        [Benchmark]
        public void 定义字段_T8_特性注解()
        {
            FieldBuilder builder = CodeSyntax.CreateField("i")
                .WithAttributes(new string[] { @"[Display(Name = ""a"")]", @"[Key]" })
                .WithAccess(MemberAccess.Public)
                .WithType("int");

            var result = builder.ToFormatCode();
        }
    }
}
