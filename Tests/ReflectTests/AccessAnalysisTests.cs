using CZGL.CodeAnalysis.Shared;
using CZGL.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ReflectTests
{
    public class AccessAnalysisTests
    {
        ITestOutputHelper _tempOutput;
        public AccessAnalysisTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        private class Test
        {
            public void A() { }
            public int B;
            public int C { get; }
        }

        private static readonly Type type = typeof(Test);

        [Fact]
        public void 识别成员访问权限()
        {
            var t = typeof(AccessAnalysisTests);
            var access = AccessAnalysis.GetAccess<Type>(t);

            Assert.Equal(MemberAccess.Public, access);
        }

        [Fact]
        public void 命名空间访问权限()
        {
            var t = typeof(AccessAnalysisTests);
            var access = AccessAnalysis.GetTypeAccess(t);
            Assert.Equal(MemberAccess.Public,access);

            var str = AccessAnalysis.GetTypeAccessCode(t);
            Assert.Equal("public", str);
        }

        [Fact]
        public void 嵌套类型成员访问权限()
        {
            var access = AccessAnalysis.GetNestedTypeAccess(type);
            Assert.Equal(MemberAccess.Private, access);

            var str = AccessAnalysis.GetNestedAccessCode(type);
            Assert.Equal("private", str);
        }

        [Fact]
        public void 方法访问权限()
        {
            var method = type.GetMethod("A");
            var access = AccessAnalysis.GetMethodAccess(method);
            Assert.Equal(MemberAccess.Public, access);

            var str = AccessAnalysis.GetMethodAccessCode(method);
            Assert.Equal("public", str);
        }

        [Fact]
        public void 字段访问权限()
        {
            var access = AccessAnalysis.GetFieldAccess(type.GetField("B"));
            Assert.Equal(MemberAccess.Public, access);

            var str = AccessAnalysis.GetFieldAccessCode(type.GetField("B"));
            Assert.Equal("public", str);
        }

        [Fact]
        public void 属性访问权限()
        {
            var access = AccessAnalysis.GetPropertyAccess(type.GetProperty("C"));
            Assert.Equal(MemberAccess.Public, access);

            var str = AccessAnalysis.GetPropertyAccessCode(type.GetProperty("C"));
            Assert.Equal("public", str);
        }
    }
}
