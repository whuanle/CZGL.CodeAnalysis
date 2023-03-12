using System;

namespace CCode.Reflect.Tests
{
	public class AttributeTestObject
	{
		#region Attribute

		[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
		public class Test1_Attribute : Attribute
		{
			public Test1_Attribute() { }
			public Test1_Attribute(string name)
			{
				Name = name;
			}
			public string Name { get; set; }
			public string Value { get; set; }
		}

		[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
		public class Test2_Attribute : Attribute
		{
			public Test2_Attribute() { }
			public Test2_Attribute(string name)
			{
				Name = name;
			}
			public string Name { get; set; }
			public string Value { get; set; }
		}

		[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
		public class Test3_Attribute : Attribute
		{
			public Test3_Attribute() { }
			public Test3_Attribute(string name)
			{
				Name = name;
			}
			public string Name { get; set; }
			public string Value { get; set; }
		}


		#endregion

		#region test1

		[Test1_]
		public class Test1_1 { }

		[Test1_("aaa")]
		public class Test1_2 { }

		[Test1_("aaa", Value = "666")]
		public class Test1_3 { }

		[Test1_(Name = "aaa", Value = "666")]
		public class Test1_4 { }

		#endregion

		#region test2

		[Test2_("aaa")]
		[Test2_(Name = "bbb")]
		[Test2_(Value = "ccc")]
		[Test2_(Name = "ccc", Value = "ddd")]
		public class Test2_1 { }

		#endregion

		#region test3

		[Test3_(Name = "666")]
		public class Test3_1 { }

		public class Test3_2 : Test3_1 { }

		[Test3_(Name = "666")]
		public class Test3_3 { }

		[Test3_(Value = "666")]
		public class Test3_4 : Test3_3 { }

		#endregion
	}
}
