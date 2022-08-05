using Xunit;

namespace CZGL.CodeAnalysis.Shared.Tests
{
    public class ClassKeywordTests
    {

        class DefaultKeywordClass { }
        [Fact]
        public void DefaultKeyword()
        {
            var value = EnumCache.GetClassKeyword(ClassKeyword.Default);
            Assert.Equal("", value);
        }

        sealed class SealedKeywordClass { }
        [Fact]
        public void SealedKeyword()
        {
            var value = EnumCache.GetClassKeyword(ClassKeyword.Sealed);
            Assert.Equal("sealed", value);
        }

        static class StaticKeywordClass { }
        [Fact]
        public void StaticKeyword()
        {
            var value = EnumCache.GetClassKeyword(ClassKeyword.Static);
            Assert.Equal("static", value);
        }

        abstract class AstractKeywordClass { }
        [Fact]
        public void AstractKeyword()
        {
            var value = EnumCache.GetClassKeyword(ClassKeyword.Abstract);
            Assert.Equal("abstract", value);
        }

#pragma warning disable CS0109
        new class NewKeywordClass { }
#pragma warning restore CS0109
        [Fact]
        public void NewKeyword()
        {
            var value = EnumCache.GetClassKeyword(ClassKeyword.New);
            Assert.Equal("new", value);
        }
    }
}