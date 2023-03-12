using CCode.Shared;
using Xunit;

namespace CZGL.CodeAnalysis.Shared.Tests
{
    public class EnumCacheTests
    {
        [Fact]
        public void GetCaches()
        {
            Assert.Equal("sealed", EnumCache.View(ClassKeyword.Sealed));
            Assert.Equal("sealed", EnumCache.View<ClassKeyword>(ClassKeyword.Sealed));

            Assert.Equal("sealed", EnumCache.View(RecordKeyword.Sealed));
            Assert.Equal("sealed", EnumCache.View<RecordKeyword>(RecordKeyword.Sealed));

            Assert.Equal("readonly record", EnumCache.View(StructKeyword.ReadonlyRecord));
            Assert.Equal("readonly record", EnumCache.View<StructKeyword>(StructKeyword.ReadonlyRecord));

            Assert.Equal("static", EnumCache.View(EventKeyword.Static));
            Assert.Equal("static", EnumCache.View<EventKeyword>(EventKeyword.Static));

            Assert.Equal("new", EnumCache.View(GenericKeyword.New));
            Assert.Equal("new", EnumCache.View<GenericKeyword>(GenericKeyword.New));

            Assert.Equal("static", EnumCache.View(FieldKeyword.Static));
            Assert.Equal("static", EnumCache.View<FieldKeyword>(FieldKeyword.Static));

            Assert.Equal("internal", EnumCache.View(MemberAccess.Internal));
            Assert.Equal("internal", EnumCache.View<MemberAccess>(MemberAccess.Internal));

            Assert.Equal("property", EnumCache.View(MemberType.Property));
            Assert.Equal("property", EnumCache.View<MemberType>(MemberType.Property));

            Assert.Equal("static", EnumCache.View(MethodKeyword.Static));
            Assert.Equal("static", EnumCache.View<MethodKeyword>(MethodKeyword.Static));

            Assert.Equal("internal", EnumCache.View(NamespaceAccess.Internal));
            Assert.Equal("internal", EnumCache.View<NamespaceAccess>(NamespaceAccess.Internal));

            Assert.Equal("static", EnumCache.View(PropertyKeyword.Static));
            Assert.Equal("static", EnumCache.View<PropertyKeyword>(PropertyKeyword.Static));
        }

        [Fact]
        public void ToPropertyKeyword()
        {
            Assert.Equal(PropertyKeyword.Static, EnumCache.ToPropertyKeyword(MethodKeyword.Static));
            Assert.Equal(MethodKeyword.Static, EnumCache.ToMethodKeyword(PropertyKeyword.Static));
        }
    }
}
