using CZGL.CodeAnalysis.Shared;
using System;
using Xunit;
using Xunit.Abstractions;

namespace SharedTests
{
    public class ClassKeywordTest
    {
        ITestOutputHelper _tempOutput;
        public ClassKeywordTest(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void Test()
        {
            ClassKeyword classKeyword = ClassKeyword.Abstract;
            Assert.Equal(ClassKeyword.Abstract, classKeyword);
        }
    }
}
