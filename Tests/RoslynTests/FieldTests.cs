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
            FieldBuilder builder = new FieldBuilder();
            var field = builder
                .SetType("int")
                .SetName("i").Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("int i;", result);
        }

        [Fact]
        public void 定义字段_T2_初始化值()
        {
            FieldBuilder builder = new FieldBuilder();
            var field = builder.SetType("int")
                .SetName("i")
                .Initializer("0")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("int i = 0;", result);
        }


        [Fact]
        public void 定义字段_T3_表达式初始化值()
        {
            FieldBuilder builder = new FieldBuilder();
            var field = builder.SetType("int")
                .SetName("i")
                .Initializer("int.Parse(\"1\")")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("int i = int.Parse(\"1\");", result);
        }


        [Fact]
        public void 定义字段_T4_访问修饰符()
        {
            var field1 = new FieldBuilder()
                .SetVisibility(MemberVisibilityType.Public)
                .SetType("int")
                .SetName("i").Build();
            var result = field1.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public int i;", result);

             var field2 = new FieldBuilder()
                .SetVisibility("public")
                .SetType("int")
                .SetName("i").Build();
             result = field2.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif

            Assert.Equal("public int i;", result);
        }

        [Fact]
        public void 定义字段_T5_访问修饰符和限定修饰符()
        {
            FieldBuilder builder = new FieldBuilder();
            var field = builder
                .SetVisibility(MemberVisibilityType.ProtectedInternal)
                .SetQualifier(MemberQualifierType.Static|MemberQualifierType.Readonly)
                .SetType("int")
                .SetName("i")
                .Initializer("int.Parse(\"1\")")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("protected internal static readonly int i = int.Parse(\"1\");", result);
        }


        [Fact]
        public void 定义字段_T6_超长的泛型()
        {
            FieldBuilder builder = new FieldBuilder();
            var field = builder
                .SetType("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>")
                .SetName("i")
                .Initializer("new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>()")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> i = new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();", result);
        }


        [Fact]
        public void 定义字段_T7_字符串整体生成()
        {
            var field = FieldBuilder
                .Build("int i;");
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("int i;", result);
        }


        [Fact]
        public void 定义字段_T8_字符串整体生成()
        {
            var field = FieldBuilder
                .Build("public int i;");
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public int i;", result);
        }


        [Fact]
        public void 定义字段_T9_特性注解()
        {
            FieldBuilder builder = new FieldBuilder();
            var field = builder
                .SetAttributeLists(new string[] { @"[Display(Name = ""a"")]", @"[Key]" })
                .SetVisibility(MemberVisibilityType.Public)
                .SetType("int")
                .SetName("i")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"[Display(Name = ""a"")]
[Key]
public int i;", result);
        }
    }
}
