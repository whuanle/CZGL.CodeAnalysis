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
    public class MethodTests
    {
        ITestOutputHelper _tempOutput;
        public MethodTests(ITestOutputHelper tempOutput)
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
            MethodBuilder builder = CodeSyntax.CreateMethod("T1")
                .WithAccess(MemberAccess.Public)
                .WithBlock(@"Console.WriteLine(""666"");");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public void T1()
{
    Console.WriteLine(""666"");
}", result.WithUnixEOL());
        }

        [Fact]
        public void 方法_T2_带返回值()
        {
            MethodBuilder builder = CodeSyntax.CreateMethod("T2")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("string")
                .WithBlock(@"return ""666"";");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public string T2()
{
    return ""666"";
}", result.WithUnixEOL());

        }

        [Fact]
        public void 方法_T3_长泛型()
        {
            MethodBuilder builder = CodeSyntax.CreateMethod("T3")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>")
                .WithBlock(@"return new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> T3()
{
    return new List<Dictionary<int, Dictionary<string, List<FieldInfo>>>>();
}", result.WithUnixEOL());

        }

        [Fact]
        public void 方法_T4_参数()
        {
            MethodBuilder builder = CodeSyntax.CreateMethod("T4")
                .WithAccess(MemberAccess.Public)
                .WithParams("int a, int b, string c")
                .WithDefaultBlock();

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public void T4(int a, int b, string c)
{
}", result.WithUnixEOL());
        }

        [Fact]
        public void 方法_T4_关键字参数()
        {
            MethodBuilder builder = CodeSyntax.CreateMethod("T4")
                .WithAccess(MemberAccess.Public)
                .WithParams("ref int a, out int b, param string[] c")
                .WithDefaultBlock();

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public void T4(ref int a, out int b, param string[] c)
{
}", result.WithUnixEOL());
        }

        [Fact]
        public void 方法_T5()
        {
            MethodBuilder builder = CodeSyntax.CreateMethod("T5")
                .WithAccess(MemberAccess.Public)
                .WithParams("List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> a")
                .WithDefaultBlock();

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public void T5(List<Dictionary<int, Dictionary<string, List<FieldInfo>>>> a)
{
}", result.WithUnixEOL());
        }

        public T1 Test<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 t1, T2 t2, T3 t3)
        where T1 : struct
        where T2 : class
        where T3 : notnull
        where T4 : unmanaged
        where T5 : new()
        where T6 : IEnumerable<Dictionary<int, int>>
        where T7 : IEnumerable<int>
        where T8 : T2
        {
            return t1;
        }


        [Fact]
        public void 方法_T6_泛型参数()
        {
            MethodBuilder builder = CodeSyntax.CreateMethod("Test")
                .WithAccess(MemberAccess.Public)
                .WithReturnType("T1")
                .WithGeneric(builder=>
                {
                    builder.WithTransformParam("T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11")
                    .WithTransformConstraint(@"
        where T1 : struct
        where T2 : class
        where T3 : notnull
        where T4 : unmanaged
        where T5 : new()
        where T6 : IEnumerable<Dictionary<int, int>>
        where T7 : IEnumerable<int>
        where T8 : T2");
                })
                .WithParams("T1 t1, T2 t2, T3 t3")
                .WithBlock("return t1;");

            var result = builder.ToFormatCode();
#if Log
            _tempOutput.WriteLine(result.WithUnixEOL());
#endif
            Assert.Equal(@"public T1 Test<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 t1, T2 t2, T3 t3)
    where T1 : struct where T2 : class where T3 : notnull where T4 : unmanaged where T5 : new()
    where T6 : IEnumerable<Dictionary<int, int>> where T7 : IEnumerable<int> where T8 : T2
{
    return t1;
}", result.WithUnixEOL());
        }
    }
}
