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

namespace CZGL.Reflect.Tests
{
    public delegate void D1(int a, int b);

    /// <summary>
    /// 访问修饰符解析器测试
    /// </summary>
    public class AccessAnalysisTests
    {
        ITestOutputHelper _tempOutput;
        public AccessAnalysisTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        private class Test
        {
            public string F1;
            protected string F2;
            internal string F3;
            private string F4;
            protected internal string F5;
            private protected string F6;

            public string P1 { get; set; }
            protected string P2 { get; set; }
            internal string P3 { get; set; }
            private string P4 { get; set; }
            protected internal string P5 { get; set; }
            private protected string P6 { get; set; }

            public void M1() { }
            protected void M2() { }
            internal void M3() { }
            private void M4() { }
            protected internal void M5() { }
            private protected void M6() { }
        }


        [Fact]
        public void Namespace_Access()
        {
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetAccess(typeof(AccessAnalysisTests)));
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetAccess(typeof(D1)));
        }

        [Fact]
        public void Type_Access()
        {
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetAccess(typeof(AccessAnalysisTests)));
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetTypeAccess(typeof(AccessAnalysisTests)));
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetNestedTypeAccess(typeof(AccessAnalysisTests)));

            Assert.Equal("public", AccessAnalysis.GetTypeAccessString(typeof(AccessAnalysisTests)));
            Assert.Equal("public", AccessAnalysis.GetAccessString(typeof(AccessAnalysisTests)));
            Assert.Equal("public", AccessAnalysis.GetNestedTypeAccessString(typeof(AccessAnalysisTests)));
        }

        [Fact]
        public void NestedType_Access()
        {
            var access = AccessAnalysis.GetNestedTypeAccess(typeof(Test));
            Assert.Equal(MemberAccess.Private, access);

            var str = AccessAnalysis.GetNestedTypeAccessString(typeof(Test));
            Assert.Equal("private", str);
        }

        [Fact]
        public void Method_Access()
        {
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetMethodAccess(typeof(Test).GetMethod("M1")));
            Assert.Equal(MemberAccess.Protected, AccessAnalysis.GetMethodAccess(typeof(Test).GetMethod("M2", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.Internal, AccessAnalysis.GetMethodAccess(typeof(Test).GetMethod("M3", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.Private, AccessAnalysis.GetMethodAccess(typeof(Test).GetMethod("M4", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.ProtectedInternal, AccessAnalysis.GetMethodAccess(typeof(Test).GetMethod("M5", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.PrivateProtected, AccessAnalysis.GetMethodAccess(typeof(Test).GetMethod("M6", BindingFlags.NonPublic | BindingFlags.Instance)));

            Assert.Equal("public", AccessAnalysis.GetMethodAccessString(typeof(Test).GetMethod("M1")));
            Assert.Equal("protected", AccessAnalysis.GetMethodAccessString(typeof(Test).GetMethod("M2", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("internal", AccessAnalysis.GetMethodAccessString(typeof(Test).GetMethod("M3", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("private", AccessAnalysis.GetMethodAccessString(typeof(Test).GetMethod("M4", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("protected internal", AccessAnalysis.GetMethodAccessString(typeof(Test).GetMethod("M5", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("private protected", AccessAnalysis.GetMethodAccessString(typeof(Test).GetMethod("M6", BindingFlags.NonPublic | BindingFlags.Instance)));
        }

        [Fact]
        public void Field_Access()
        {
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetFieldAccess(typeof(Test).GetField("F1")));
            Assert.Equal(MemberAccess.Protected, AccessAnalysis.GetFieldAccess(typeof(Test).GetField("F2", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.Internal, AccessAnalysis.GetFieldAccess(typeof(Test).GetField("F3", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.Private, AccessAnalysis.GetFieldAccess(typeof(Test).GetField("F4", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.ProtectedInternal, AccessAnalysis.GetFieldAccess(typeof(Test).GetField("F5", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.PrivateProtected, AccessAnalysis.GetFieldAccess(typeof(Test).GetField("F6", BindingFlags.NonPublic | BindingFlags.Instance)));

            Assert.Equal("public", AccessAnalysis.GetFieldAccessString(typeof(Test).GetField("F1")));
            Assert.Equal("protected", AccessAnalysis.GetFieldAccessString(typeof(Test).GetField("F2", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("internal", AccessAnalysis.GetFieldAccessString(typeof(Test).GetField("F3", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("private", AccessAnalysis.GetFieldAccessString(typeof(Test).GetField("F4", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("protected internal", AccessAnalysis.GetFieldAccessString(typeof(Test).GetField("F5", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("private protected", AccessAnalysis.GetFieldAccessString(typeof(Test).GetField("F6", BindingFlags.NonPublic | BindingFlags.Instance)));
        }

        [Fact]
        public void Property_Access()
        {
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetPropertyAccess(typeof(Test).GetProperty("P1")));
            Assert.Equal(MemberAccess.Protected, AccessAnalysis.GetPropertyAccess(typeof(Test).GetProperty("P2", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.Internal, AccessAnalysis.GetPropertyAccess(typeof(Test).GetProperty("P3", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.Private, AccessAnalysis.GetPropertyAccess(typeof(Test).GetProperty("P4", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.ProtectedInternal, AccessAnalysis.GetPropertyAccess(typeof(Test).GetProperty("P5", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal(MemberAccess.PrivateProtected, AccessAnalysis.GetPropertyAccess(typeof(Test).GetProperty("P6", BindingFlags.NonPublic | BindingFlags.Instance)));

            Assert.Equal("public", AccessAnalysis.GetPropertyAccessString(typeof(Test).GetProperty("P1")));
            Assert.Equal("protected", AccessAnalysis.GetPropertyAccessString(typeof(Test).GetProperty("P2", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("internal", AccessAnalysis.GetPropertyAccessString(typeof(Test).GetProperty("P3", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("private", AccessAnalysis.GetPropertyAccessString(typeof(Test).GetProperty("P4", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("protected internal", AccessAnalysis.GetPropertyAccessString(typeof(Test).GetProperty("P5", BindingFlags.NonPublic | BindingFlags.Instance)));
            Assert.Equal("private protected", AccessAnalysis.GetPropertyAccessString(typeof(Test).GetProperty("P6", BindingFlags.NonPublic | BindingFlags.Instance)));
        }
    }
}
