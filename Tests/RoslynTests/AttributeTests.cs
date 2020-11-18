using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class AttributeTests
    {
        ITestOutputHelper _tempOutput;
        public AttributeTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }


        // [Key]

        [Fact]
        public void 特性_T1()
        {
            AttributeBuilder builder = CodeSyntax.CreateAttribute("Key");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("Key", result.WithUnixEOL());
        }

        // [Display(666)]

        [Fact]
        public void 特性_T2()
        {
            AttributeBuilder builder = CodeSyntax.CreateAttribute("DisplayName")
                .WithCtor("666");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal("DisplayName(666)", result.WithUnixEOL());
        }

        // [Display(Name = "a")]

        [Fact]
        public void 特性_T3()
        {
            AttributeBuilder builder = CodeSyntax.CreateAttribute("DisplayName")
                .WithProperty(new string[] { "Name = \"a\"" });

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"DisplayName(Name = ""a"")", result.WithUnixEOL());
        }

        // [Display(666,Name = "a")]
        [Fact]
        public void 特性_T4()
        {
            AttributeBuilder builder = CodeSyntax.CreateAttribute("DisplayName")
                .WithCtor("666")
                .WithProperty(new string[] { "Name = \"a\"" });

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"DisplayName(666, Name = ""a"")", result.WithUnixEOL());
        }
    }
}
