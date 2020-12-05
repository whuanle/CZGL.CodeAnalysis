using CZGL.Reflect;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using CZGL.CodeAnalysis.Shared;

namespace ReflectTests
{
    // 能够直接在命名空间中声明的
    public class T1 { }
    public delegate void T2();
    public enum T3 { }
    public interface T4 { }
    public static class T5 { }
    public struct T6 { }


    public class 判断类型
    {
        ITestOutputHelper _tempOutput;
        public 判断类型(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]

        public void 判断各种类型1()
        {
            Assert.Equal(MemberType.Class, TypeAnalysis.GetMemberType(typeof(T1)));

            Assert.Equal(MemberType.Delegate, TypeAnalysis.GetMemberType(typeof(T2)));

            Assert.Equal(MemberType.Enum, TypeAnalysis.GetMemberType(typeof(T3)));

            Assert.Equal(MemberType.Interface, TypeAnalysis.GetMemberType(typeof(T4)));

            Assert.Equal(MemberType.Class, TypeAnalysis.GetMemberType(typeof(T5)));

            Assert.Equal(MemberType.Struct, TypeAnalysis.GetMemberType(typeof(T6)));

            Assert.Equal(MemberType.BaseValue, TypeAnalysis.GetMemberType(typeof(int)));
        }

        [Fact]
        public void 判断各种类型2()
        {
            Assert.Equal(MemberType.Class, typeof(T1).GetMemberType());

            Assert.Equal(MemberType.Delegate, typeof(T2).GetMemberType());

            Assert.Equal(MemberType.Enum, typeof(T3).GetMemberType());

            Assert.Equal(MemberType.Interface, typeof(T4).GetMemberType());

            Assert.Equal(MemberType.Class, typeof(T5).GetMemberType());

            Assert.Equal(MemberType.Struct, typeof(T6).GetMemberType());

            Assert.Equal(MemberType.BaseValue, typeof(int).GetMemberType());
        }
    }
}
