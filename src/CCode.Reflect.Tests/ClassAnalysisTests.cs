using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CCode.Reflect.Tests
{
	internal class InternalClass { }
	public class ClassAnalysisTests
	{
		ITestOutputHelper _tempOutput;
		public ClassAnalysisTests(ITestOutputHelper tempOutput)
		{
			_tempOutput = tempOutput;
		}

		protected class ProtectedClass { }
		private class PrivateClass { }
		protected internal class PIClass { }
		private protected class PPClass { }

		[Fact]
		public void Access()
		{
			Assert.Equal(MemberAccess.Public, ClassAnalysis.GetAccess(typeof(ClassAnalysisTests)));
			Assert.Equal(MemberAccess.Internal, ClassAnalysis.GetAccess(typeof(InternalClass)));
			Assert.Equal(MemberAccess.Protected, ClassAnalysis.GetAccess(typeof(ProtectedClass)));
			Assert.Equal(MemberAccess.Private, ClassAnalysis.GetAccess(typeof(PrivateClass)));
			Assert.Equal(MemberAccess.ProtectedInternal, ClassAnalysis.GetAccess(typeof(PIClass)));
			Assert.Equal(MemberAccess.PrivateProtected, ClassAnalysis.GetAccess(typeof(PPClass)));
		}

		public sealed class SealedClass { }
		public static class StaticClass { }
		public abstract class AbstractClass { }

		[Fact]
		public void Keyword()
		{
			Assert.Equal(ClassKeyword.Default, ClassAnalysis.GetKeyword(typeof(ClassAnalysisTests)));
			Assert.Equal(ClassKeyword.Sealed, ClassAnalysis.GetKeyword(typeof(SealedClass)));
			Assert.Equal(ClassKeyword.Static, ClassAnalysis.GetKeyword(typeof(StaticClass)));
			Assert.Equal(ClassKeyword.Abstract, ClassAnalysis.GetKeyword(typeof(AbstractClass)));
		}

		[Fact]
		public void CanInherited()
		{
			Assert.True(ClassAnalysis.IsCanInherited(typeof(ClassAnalysisTests)));
			Assert.False(ClassAnalysis.IsCanInherited(typeof(SealedClass)));
			Assert.False(ClassAnalysis.IsCanInherited(typeof(StaticClass)));
			Assert.True(ClassAnalysis.IsCanInherited(typeof(AbstractClass)));
		}

		public class Model_泛型1<T1, T2, T3>
		{

		}

		public class Model_泛型2<T1, T2, T3> : Model_泛型1<T1, T2, T3> where T1 : struct
		{

		}

		public class Model_泛型3 : Model_泛型1<int, double, int>
		{

		}


		public class Model_泛型类4 { }
		public class Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
			where T1 : struct
			where T2 : class
			where T3 : notnull
			where T4 : unmanaged
			where T5 : new()
			where T6 : Model_泛型类4
			where T7 : IEnumerable<int>
			where T8 : T2
			// 组合条件
			where T9 : class, new()
			where T10 : Model_泛型类4, IEnumerable<int>, new()
		{
		}

		[Fact]
		public void Generice()
		{
			var gs = ClassAnalysis.GetGenericeParam(typeof(Model_泛型1<,,>));
			var ks = gs.Keys.ToArray();
			Assert.Equal("T1", ks[0]);
			Assert.Equal("T2", ks[1]);
			Assert.Equal("T3", ks[2]);
		}
	}
}
