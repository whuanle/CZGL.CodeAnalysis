using Xunit;

namespace CZGL.CodeAnalysis.Shared.Tests
{
    public class ClassKeywordTests
    {

        class DefaultKeywordClass { }
        [Fact]
        public void DefaultKeyword()
        {
            var value = EnumCache.View(ClassKeyword.Default);
            Assert.Equal("", value);
        }

        sealed class SealedKeywordClass { }
        [Fact]
        public void SealedKeyword()
        {
            var value = EnumCache.View(ClassKeyword.Sealed);
            Assert.Equal("sealed", value);
        }

        static class StaticKeywordClass { }
        [Fact]
        public void StaticKeyword()
        {
            var value = EnumCache.View(ClassKeyword.Static);
            Assert.Equal("static", value);
        }

        abstract class AstractKeywordClass { }
        [Fact]
        public void AstractKeyword()
        {
            var value = EnumCache.View(ClassKeyword.Abstract);
            Assert.Equal("abstract", value);
        }
    }
}