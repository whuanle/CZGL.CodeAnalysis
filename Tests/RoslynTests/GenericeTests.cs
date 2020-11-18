using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{

    //public class Model_泛型1<T1, T2, T3>
    //{

    //}

    //public class Model_泛型2<T1, T2, T3> : Model_泛型1<T1, T2, T3> where T1 : struct
    //{

    //}

    //public class Model_泛型3 : Model_泛型1<int, double, int>
    //{

    //}


    //public class Model_泛型类4 { }
    //public class Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    //    where T1 : struct
    //    where T2 : class
    //    where T3 : notnull
    //    where T4 : unmanaged
    //    where T5 : new()
    //    where T6 : Model_泛型类4
    //    where T7 : IEnumerable<int>
    //    where T8 : T2
    //    // 组合条件
    //    where T9 : class, new()
    //    where T10 : Model_泛型类4, IEnumerable<int>, new()
    //{
    //}


    public class GenericeTests
    {
        ITestOutputHelper _tempOutput;
        public GenericeTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        // 约束 
        // 泛型类
        // 泛型方法
        // 泛型类泛型约束
        // 泛型方法泛型约束


        [Fact]
        public void 约束()
        {
            DelegateBuilder builder = CodeSyntax.CreateDelegate("Test")
                .WithGeneric(b=>
                {
                    b.WithCreate("T1").WithStruct()
                    .WithCreate("T2").WithClass()
                    .WithCreate("T3").WithNotnull()
                    .WithCreate("T4").WithUnmanaged()
                    .WithCreate("T5").WithNotnull()
                    .WithCreate("T6").WithBase("Enum")
                    .WithCreate("T7").WithBase("IEnumerable<int>")
                    .WithCreate("T8").WithTo("T2")
                    .WithCreate("T9").WithClass().WithNew();
                });
            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif

            Assert.Equal(@"delegate void Test<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    where T1 : struct where T2 : class where T3 : notnull where T4 : unmanaged where T5 : notnull where T6 : Enum where T7 : IEnumerable<int> where T8 : T2 where T9 : class, new();", result.WithUnixEOL());
        }

        //[Fact]
        //public void 泛型类_T1()
        //{
        //    GenericBuilder generic = new GenericBuilder();
        //    generic.AddConstarint(new GenericScheme("T1", GenericConstraintsType.Struct));
        //    var tmp = generic.Build();
        //    _tempOutput.WriteLine(tmp.ToFullString());
        //    Assert.True(true);
        //}

    }
}
