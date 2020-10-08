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
    public class EnumTests
    {
        ITestOutputHelper _tempOutput;
        public EnumTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        public enum Test
        {
            A,
            B,
            C
        }

        [Fact]
        public void 枚举()
        {
            EnumBuilder builder = CodeSyntax.CreateEnum("Test")
                .WithAccess(NamespaceAccess.Public)
                .WithField("A")
                .WithField("B")
                .WithField("C");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public enum Test
{
    A,
    B,
    C
}", result);
        }
    }
}
