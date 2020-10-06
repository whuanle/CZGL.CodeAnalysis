//using CZGL.CodeAnalysis.Shared;
//using CZGL.Roslyn;
//using Microsoft.CodeAnalysis;
//using System;
//using Xunit;
//using Xunit.Abstractions;

//namespace RoslynTests
//{
//    public class CtorTestTests
//    {
//        ITestOutputHelper _tempOutput;
//        public CtorTestTests(ITestOutputHelper tempOutput)
//        {
//            _tempOutput = tempOutput;
//        }

//        public class T1
//        {
//            public T1() { }
//            public T1(string a) { }

//        }
//        public class T2:T1
//        {
//            public T2() { Console.WriteLine("666"); }
//            public T2(string a):this() { }
//            public T2(string a,string b):base(a) { }
//        }

//        [Fact]
//        public void 构造函数_T1()
//        {
//            CtorBuilder methodBuilder = new CtorBuilder();
//            var method = methodBuilder.WithAccess(MemberAccess.Public)
//                .WithName("T2")
//                .WithBlock(@"Console.WriteLine(""666"");")
//                .Build();
//            var result= method.NormalizeWhitespace().ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal(@"public T2()
//{
//    Console.WriteLine(""666"");
//}",result);
//        }

//        [Fact]
//        public void 构造函数_T2()
//        {
//            CtorBuilder methodBuilder = new CtorBuilder();
//            var method = methodBuilder.WithAccess(MemberAccess.Public)
//                .WithName("T2")
//                .WithParams("string a")
//                .SetThisCtor("this()")
//                .Build();
//            var result = method.NormalizeWhitespace().ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal(@"public T2(string a): this()
//{
//}", result);

//        }

//        [Fact]
//        public void 构造函数_T3()
//        {
//            CtorBuilder methodBuilder = new CtorBuilder();
//            var method = methodBuilder.WithAccess(MemberAccess.Public)
//                .WithName("T2")
//                .WithParams("string a,string b")
//                .WithBaseCtor("base(a)")
//                .Build();
//            var result = method.NormalizeWhitespace().ToFullString();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal(@"public T2(string a, string b): base(a)
//{
//}", result);
//        }


//    }
//}
