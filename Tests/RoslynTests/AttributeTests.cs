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
            AttributeBuilder builder = new AttributeBuilder();
            var field = builder
                .SetName("Key")
                .Build();

            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("Key", result);
        }

        // [Display(666)]

        [Fact]
        public void 特性_T2()
        {
            AttributeBuilder builder = new AttributeBuilder();
            var field = builder
                .SetName("DisplayName")
                .SetCtor("666")
                .Build();

            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("DisplayName(666)", result);
        }

        // [Display(Name = "a")]

        [Fact]
        public void 特性_T3()
        {
            AttributeBuilder builder = new AttributeBuilder();
            var field = builder
                .SetName("DisplayName")
                .SetProperty(new string[] { "Name = \"a\""})
                .Build();

            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"DisplayName(Name = ""a"")", result);
        }

        // [Display(666,Name = "a")]
        [Fact]
        public void 特性_T4()
        {
            AttributeBuilder builder = new AttributeBuilder();
            var field = builder
                .SetName("DisplayName")
                .SetCtor("666")
                .SetProperty(new string[] { "Name = \"a\"" })
                .Build();

            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"DisplayName(666, Name = ""a"")", result);
        }
    }
}
