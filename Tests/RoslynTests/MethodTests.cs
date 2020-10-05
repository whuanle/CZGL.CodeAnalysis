using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class CtorTest4Tests
    {
        ITestOutputHelper _tempOutput;
        public CtorTest4Tests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        //public void T1() { Console.WriteLine("666"); }
        //public string T2()
        //{
        //    return "666";
        //}

        //public List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> T3()
        //{
        //    return new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();
        //}

        //public void T4(int a, int b, string c) { }

        //public void T5(List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> a) { }

        [Fact]
        public void 方法_T1()
        {
            MethodBuilder methodBuilder = new MethodBuilder();
            var method = methodBuilder.WithAccess(MemberAccess.Public)
                //.SetReturnParam("void")
                .WithName("T1")
                .WithBlock(@"Console.WriteLine(""666"");")
                .Build();
            var result= method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public void T1()
{
    Console.WriteLine(""666"");
}",result);
        }

        [Fact]
        public void 方法_T2()
        {
            MethodBuilder methodBuilder = new MethodBuilder();
            var method = methodBuilder.WithAccess(MemberAccess.Public)
                .WithReturnType("string")
                .WithName("T2")
                .WithBlock(@"return ""666"";")
                .Build();
            var result = method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public string T2()
{
    return ""666"";
}", result);

        }

        [Fact]
        public void 方法_T3()
        {
            MethodBuilder methodBuilder = new MethodBuilder();
            var method = methodBuilder.WithAccess(MemberAccess.Public)
                .WithReturnType("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>")
                .WithName("T3")
                .WithBlock(@"return new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();")
                .Build();
            var result = method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> T3()
{
    return new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();
}", result);

        }

        [Fact]
        public void 方法_T4()
        {
            MethodBuilder methodBuilder = new MethodBuilder();
            var method = methodBuilder.WithAccess(MemberAccess.Public)
                //.SetReturnParam("void")
                .WithName("T4")
                .WithParams("int a, int b, string c")
                .Build();
            var result = method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public void T4(int a, int b, string c)
{
}", result);
        }

        [Fact]
        public void 方法_T5()
        {
            MethodBuilder methodBuilder = new MethodBuilder();
            var method = methodBuilder.WithAccess(MemberAccess.Public)
                //.SetReturnParam("void")
                .WithName("T5")
                .WithParams("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> a")
                .Build();
            var result = method.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"public void T5(List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> a)
{
}", result);
        }

    }
}
