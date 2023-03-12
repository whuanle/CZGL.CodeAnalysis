using CCode;
using CCode.Reflect;
using Xunit;
using Xunit.Abstractions;

namespace CCode.Reflect.Tests
{
	// 能够直接在命名空间中声明的
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

		public void 判断各种类型1()
		{
			Assert.Equal(MemberType.Class, typeof(T1).GetMemberType());

			Assert.Equal(MemberType.Delegate, typeof(T2).GetMemberType());

			Assert.Equal(MemberType.Enum, typeof(T3).GetMemberType());

			Assert.Equal(MemberType.Interface, typeof(T4).GetMemberType());

			Assert.Equal(MemberType.Class, typeof(T5).GetMemberType());

			Assert.Equal(MemberType.Struct, typeof(T6).GetMemberType());

			Assert.Equal(MemberType.BaseType, typeof(int).GetMemberType());
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

			Assert.Equal(MemberType.BaseType, typeof(int).GetMemberType());
		}
	}
}
