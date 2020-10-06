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
//    public class EventTests
//    {
//        // public delegate void T();
//        // public static void AAA() { }
//        // public event T t1 = AAA;




//        ITestOutputHelper _tempOutput;
//        public EventTests(ITestOutputHelper tempOutput)
//        {
//            _tempOutput = tempOutput;
//        }

//        [Fact]
//        public void 事件_T1()
//        {
//            EventBuilder builder = new EventBuilder();
//            var field = builder
//                .WithAccess(MemberAccess.Public)
//                .SetDelegateType("T")
//                .WithName("t1")
//                .WithInit("AAA")
//                .Build();

//            var result = field.NormalizeWhitespace().ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal("public event T t1 = AAA;", result);
//        }

//        [Fact]
//        public void 事件_T2()
//        {
//            EventBuilder builder = new EventBuilder();
//            var field = builder
//                .SetDelegateType("T")
//                .WithName("t1")
//                .WithInit("AAA")
//                .Build();

//            var result = field.NormalizeWhitespace().ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal("event T t1 = AAA;", result);
//        }


//        [Fact]
//        public void 事件_T3()
//        {
//            EventBuilder builder = new EventBuilder();
//            var field = builder
//                 .WithAttributes(new string[] { @"[Display(Name = ""a"")]", @"[Key]" })
//                .WithAccess(MemberAccess.Public)
//                .SetDelegateType("T")
//                .WithName("t1")
//                .WithInit("AAA")
//                .Build();

//            var result = field.NormalizeWhitespace().ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal(@"[Display(Name = ""a"")]
//[Key]
//public event T t1 = AAA;", result);
//        }


//    }
//}
