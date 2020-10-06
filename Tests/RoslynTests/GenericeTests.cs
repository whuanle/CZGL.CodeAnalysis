//using CZGL.CodeAnalysis.Shared;
//using CZGL.Roslyn;
//using Microsoft.CodeAnalysis;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Reflection;
//using Xunit;
//using Xunit.Abstractions;

//namespace RoslynTests
//{

//    //public class Model_泛型1<T1, T2, T3>
//    //{

//    //}

//    //public class Model_泛型2<T1, T2, T3> : Model_泛型1<T1, T2, T3> where T1 : struct
//    //{

//    //}

//    //public class Model_泛型3 : Model_泛型1<int, double, int>
//    //{

//    //}


//    //public class Model_泛型类4 { }
//    //public class Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
//    //    where T1 : struct
//    //    where T2 : class
//    //    where T3 : notnull
//    //    where T4 : unmanaged
//    //    where T5 : new()
//    //    where T6 : Model_泛型类4
//    //    where T7 : IEnumerable<int>
//    //    where T8 : T2
//    //    // 组合条件
//    //    where T9 : class, new()
//    //    where T10 : Model_泛型类4, IEnumerable<int>, new()
//    //{
//    //}


//    public class GenericeTests
//    {
//        ITestOutputHelper _tempOutput;
//        public GenericeTests(ITestOutputHelper tempOutput)
//        {
//            _tempOutput = tempOutput;
//        }

//        // 约束 
//        // 泛型类
//        // 泛型方法
//        // 泛型类泛型约束
//        // 泛型方法泛型约束


//        [Fact]
//        public void 约束()
//        {
//            GenericBuilder generic = new GenericBuilder();
//            generic.AddStruct("T1");
//            generic.AddClassConstarint("T2");
//            generic.AddNotNullConstarint("T3");
//            generic.AddUnmanagedConstarint("T4");
//            generic.AddNewNullConstarint("T5");
//            generic.AddBaseClassConstarint("T6","object");
//            generic.AddBaseClassConstarint("T7", "IEnumerable<int>");
//            generic.AddTUConstarint("T8","T2");
//            generic.AddClassConstarint("T9").AddNewNullConstarint("T9");
//            var syntax =  generic.BuildSyntax();
//            var result = syntax.ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif

//            Assert.Equal(@"where T1:struct
//where T2:class
//where T3:notnull
//where T4:unmanaged
//where T5:new()
//where T6:object
//where T7:IEnumerable<int>
//where T8:T2
//where T9:class,new()", result);
//        }

//        //[Fact]
//        //public void 泛型类_T1()
//        //{
//        //    GenericBuilder generic = new GenericBuilder();
//        //    generic.AddConstarint(new GenericScheme("T1", GenericConstraintsType.Struct));
//        //    var tmp = generic.Build();
//        //    _tempOutput.WriteLine(tmp.ToFullString());
//        //    Assert.True(true);
//        //}

//    }
//}
