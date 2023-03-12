using System.Collections.Generic;
using Xunit;

namespace CCode.Reflect.Tests
{
	public class GenericeTests
	{
		public class Model_泛型1<T1, T2, T3>
		{

		}

		public class Model_泛型2<T1, T2, T3> : Model_泛型1<T1, T2, T3> where T1 : struct
		{

		}

		public class Model_泛型3 : Model_泛型1<int, double, int>
		{

		}


		public class Model_泛型类4 {}
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
		public void GetOriginName()
		{
			Assert.Equal("Model_泛型2", GenericeAnalysis.GetOriginName(typeof(Model_泛型2<,,>)));
			Assert.Equal("Model_泛型类5", GenericeAnalysis.GetOriginName(typeof(Model_泛型类5<,,,,,,,,,,>)));
		}

		[Fact]
		public void GetGenriceDefine()
		{
			Assert.Equal("Model_泛型2<T1, T2, T3>", GenericeAnalysis.GetGenriceDefine(typeof(Model_泛型2<,,>)));
			Assert.Equal("Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>", GenericeAnalysis.GetGenriceDefine(typeof(Model_泛型类5<,,,,,,,,,,>)));
		}

		[Fact]
		public void GetConstrainCode()
		{
			var a = GenericeAnalysis.GetConstrainCode(typeof(Model_泛型类5<,,,,,,,,,,>), true);
			Assert.Equal("where T1 : struct ", GenericeAnalysis.GetConstrainCode(typeof(Model_泛型2<,,>)));
			Assert.Equal("where T1 : struct where T2 : class where T4 : struct where T5 : new() where T6 : Model_泛型类4 where T7 : IEnumerable<int> where T8 : T2 where T9 : class,new() where T10 : Model_泛型类4,IEnumerable<int>,new() ", GenericeAnalysis.GetConstrainCode(typeof(Model_泛型类5<,,,,,,,,,,>)));
			Assert.Equal("where T1 : struct \r\n", GenericeAnalysis.GetConstrainCode(typeof(Model_泛型2<,,>), true));
			Assert.Equal("where T1 : struct \r\nwhere T2 : class \r\nwhere T4 : struct \r\nwhere T5 : new() \r\nwhere T6 : Model_泛型类4 \r\nwhere T7 : IEnumerable<int> \r\nwhere T8 : T2 \r\nwhere T9 : class,new() \r\nwhere T10 : Model_泛型类4,IEnumerable<int>,new() \r\n", GenericeAnalysis.GetConstrainCode(typeof(Model_泛型类5<,,,,,,,,,,>), true));
		}

		[Fact]
		public void GetGenericeCode()
		{
			var a = GenericeAnalysis.GetGenericeCode(typeof(Model_泛型类5<,,,,,,,,,,>));
			Assert.Equal("Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>\r\nwhere T1 : struct \r\nwhere T2 : class \r\nwhere T4 : struct \r\nwhere T5 : new() \r\nwhere T6 : Model_泛型类4 \r\nwhere T7 : IEnumerable<int> \r\nwhere T8 : T2 \r\nwhere T9 : class,new() \r\nwhere T10 : Model_泛型类4,IEnumerable<int>,new() \r\n", GenericeAnalysis.GetGenericeCode(typeof(Model_泛型类5<,,,,,,,,,,>)));
		}
	}
}
