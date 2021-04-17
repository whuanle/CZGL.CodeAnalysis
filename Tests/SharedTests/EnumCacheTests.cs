using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SharedTests
{
    public class EnumCacheTests
    {
        ITestOutputHelper _tempOutput;
        public EnumCacheTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void 类关键字()
        {
            ClassKeyword classKeyword = ClassKeyword.Abstract;
            var output = EnumCache.GetClassKeyword(classKeyword);
            Assert.Equal("abstract", output);

            output = EnumCache.GetValue(classKeyword);
            Assert.Equal("abstract", output);
        }

        [Fact]
        public void 结构体关键字()
        {
            StructKeyword structKeyword = StructKeyword.ReadonlyRef;
            var output = EnumCache.GetStructKword(structKeyword);
            Assert.Equal("readonly ref", output);

            output = EnumCache.GetValue(structKeyword);
            Assert.Equal("readonly ref", output);
        }

        [Fact]
        public void 事件关键字()
        {
            EventKeyword eventKeyword = EventKeyword.Virtual;
            var output = EnumCache.GetEventKeyword(eventKeyword);
            Assert.Equal("virtual", output);

            output = EnumCache.GetValue(eventKeyword);
            Assert.Equal("virtual", output);
        }

        [Fact]
        public void 泛型关键字()
        {
            GenericKeyword genericKeyword = GenericKeyword.Unmanaged;
            var output = EnumCache.GetGenericKeyword(genericKeyword);
            Assert.Equal("unmanaged", output);

            output = EnumCache.GetValue(genericKeyword);
            Assert.Equal("unmanaged", output);
        }

        [Fact]
        public void 字段关键字()
        {
            FieldKeyword fieldKeyword = FieldKeyword.StaticReadonly;
            var output = EnumCache.GetFieldKeyword(fieldKeyword);
            Assert.Equal("static readonly", output);

            output = EnumCache.GetValue(fieldKeyword);
            Assert.Equal("static readonly", output);
        }

        [Fact]
        public void 成员访问修饰符()
        {
            MemberAccess access = MemberAccess.Protected;
            var output = EnumCache.GetMemberAccess(access);
            Assert.Equal("protected", output);

            output = EnumCache.GetValue(access);
            Assert.Equal("protected", output);
        }

        [Fact]
        public void 成员类型()
        {
            MemberType type = MemberType.BaseType;
            var output = EnumCache.GetMemberType(type);
            Assert.Equal("BaseType", output);

            output = EnumCache.GetValue(type);
            Assert.Equal("BaseType", output);
        }

        [Fact]
        public void 方法关键字()
        {
            MethodKeyword methodKeyword = MethodKeyword.Abstract;
            var output = EnumCache.GetMethodKeyword(methodKeyword);
            Assert.Equal("abstract", output);

            output = EnumCache.GetValue(methodKeyword);
            Assert.Equal("abstract", output);
        }

        [Fact]
        public void 命名空间访问修饰符()
        {
            NamespaceAccess access = NamespaceAccess.Internal;
            var output = EnumCache.GetNamespaceAccess(access);
            Assert.Equal("internal", output);

            output = EnumCache.GetValue(access);
            Assert.Equal("internal", output);
        }

        [Fact]
        public void 属性关键字()
        {
            PropertyKeyword propertyKeyword = PropertyKeyword.Abstract;
            var output = EnumCache.GetPropertyKeyword(propertyKeyword);
            Assert.Equal("abstract", output);

            output = EnumCache.GetValue(propertyKeyword);
            Assert.Equal("abstract", output);
        }

        [Fact]
        public void 方法关键字转换属性关键字()
        {
            MethodKeyword methodKeyword = MethodKeyword.Abstract;
            PropertyKeyword propertyKeyword = EnumCache.ToPropertyKeyword(methodKeyword);
            Assert.Equal(PropertyKeyword.Abstract, propertyKeyword);
        }
    }
}
