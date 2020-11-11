using System;
using System.ComponentModel.DataAnnotations;
using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;

namespace ConsoleApp
{
    class Program
    {
        [Display]
        public readonly static int MyField = int.Parse("666");
        static void Main(string[] args)
        {
            PropertyBuilder builder = CodeSyntax.CreateProperty("i");
            var field = builder
                .WithAccess(MemberAccess.ProtectedInternal)
                .WithKeyword(PropertyKeyword.Static | PropertyKeyword.Readonly)
                .WithType("int")
                .WithName("i")
                .WithDefaultGet()
                .WithNullSet();

            Console.ReadKey();
        }
    }
}
