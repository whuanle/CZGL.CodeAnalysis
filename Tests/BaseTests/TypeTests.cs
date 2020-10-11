using CZGL.Reflect;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using CZGL.CodeAnalysis.Shared;

namespace BaseTests
{
    public class T1 { }
    public delegate void T2();
    public enum T3 { }
    public interface T4 { }
    public static class T5 { }
    public struct T6 { }


    public class TypeTests
    {
        ITestOutputHelper _tempOutput;
        public TypeTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]

        public void 判断类型()
        {
            Assert.Equal(MemberType.Class, TypeAnalysis.GetMemberType(typeof(T1)));

            Assert.Equal(MemberType.Delegate, TypeAnalysis.GetMemberType(typeof(T2)));

            Assert.Equal(MemberType.Enum, TypeAnalysis.GetMemberType(typeof(T3)));

            Assert.Equal(MemberType.Interface, TypeAnalysis.GetMemberType(typeof(T4)));

            Assert.Equal(MemberType.Class, TypeAnalysis.GetMemberType(typeof(T5)));

            Assert.Equal(MemberType.Struct, TypeAnalysis.GetMemberType(typeof(T6)));

            Assert.Equal(MemberType.BaseValue, TypeAnalysis.GetMemberType(typeof(int)));
        }
    }
}
