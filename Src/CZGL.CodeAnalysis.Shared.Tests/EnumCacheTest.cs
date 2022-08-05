using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CZGL.CodeAnalysis.Shared.Tests
{
    public class EnumCacheTest
    {
        [Fact]
        public void GetCaches()
        {
            Assert.Equal("sealed", EnumCache.GetClassKeyword(ClassKeyword.Sealed));
            Assert.Equal("sealed", EnumCache.GetValue(ClassKeyword.Sealed));

            Assert.Equal("sealed", EnumCache.GetRecordKeyword(RecordKeyword.Sealed));
            Assert.Equal("sealed", EnumCache.GetValue(RecordKeyword.Sealed));

            Assert.Equal("readonly record", EnumCache.GetStructKword(StructKeyword.ReadonlyRecord));
            Assert.Equal("readonly record", EnumCache.GetValue(StructKeyword.ReadonlyRecord));

            Assert.Equal("static", EnumCache.GetEventKeyword(EventKeyword.Static));
            Assert.Equal("static", EnumCache.GetValue(EventKeyword.Static));

            Assert.Equal("new", EnumCache.GetGenericKeyword(GenericKeyword.New));
            Assert.Equal("new", EnumCache.GetValue(GenericKeyword.New));

            Assert.Equal("static", EnumCache.GetFieldKeyword(FieldKeyword.Static));
            Assert.Equal("static", EnumCache.GetValue(FieldKeyword.Static));

            Assert.Equal("internal", EnumCache.GetMemberAccess(MemberAccess.Internal));
            Assert.Equal("internal", EnumCache.GetValue(MemberAccess.Internal));

            Assert.Equal("property", EnumCache.GetMemberType(MemberType.Property));
            Assert.Equal("property", EnumCache.GetValue(MemberType.Property));

            Assert.Equal("static", EnumCache.GetMethodKeyword(MethodKeyword.Static));
            Assert.Equal("static", EnumCache.GetValue(MethodKeyword.Static));

            Assert.Equal("internal", EnumCache.GetNamespaceAccess(NamespaceAccess.Internal));
            Assert.Equal("internal", EnumCache.GetValue(NamespaceAccess.Internal));

            Assert.Equal("static", EnumCache.GetPropertyKeyword(PropertyKeyword.Static));
            Assert.Equal("static", EnumCache.GetValue(PropertyKeyword.Static));
        }

        [Fact]
        public void ToPropertyKeyword()
        {
            Assert.Equal(PropertyKeyword.Static, EnumCache.ToPropertyKeyword(MethodKeyword.Static));
            Assert.Equal(MethodKeyword.Static, EnumCache.ToMethodKeyword(PropertyKeyword.Static));
        }
    }
}
