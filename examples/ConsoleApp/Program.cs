using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
// using CZGL.CodeAnalysis.Shared;
// using CZGL.Roslyn;

namespace ConsoleApp
{
    public class A
    {
        private readonly int a = 111;
        private int b = 222;
        public ref readonly int AAA()
        {
            return ref a;
        }
        public ref int BBB()
        {
            return ref b;
        }
    }

    public class Test
    {
        public void A() { }
    }

    public class Test2 : Test
    {
        public new void A() { }
    }
    class Program
    {
        [Display]
        public readonly static int MyField = int.Parse("666");
        static void Main(string[] args)
        {
            Type a = typeof(A);
            var aaa = a.GetMethod("AAA").ReturnParameter;
            foreach (var item in typeof(ParameterInfo).GetProperties())
            {
                Console.WriteLine($"{item.Name} {item.GetValue(aaa)}");
            }
            var bbb = a.GetMethod("BBB").ReturnParameter;
            foreach (var item in typeof(ParameterInfo).GetProperties())
            {
                Console.WriteLine($"{item.Name} {item.GetValue(bbb)}");
            }
            // PropertyBuilder builder = CodeSyntax.CreateProperty("i");
            // var field = builder
            //     .WithAccess(MemberAccess.ProtectedInternal)
            //     .WithKeyword(PropertyKeyword.Static)
            //     .WithType("int")
            //     .WithName("i")
            //     .WithDefaultGet()
            //     .WithNullSet();

            Console.ReadKey();
        }
    }
}
