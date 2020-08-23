
using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        public delegate void A1();
        public event A1 a1;
        static void Main(string[] args)
        {
            EventBuilder builder = new EventBuilder();
            var result = builder
                .SetVisibility(MemberVisibilityType.Public)
                .SetDelegateType("T")
                .SetName("t1")
                .Initializer("AAA")
                .Build();


            Console.WriteLine(result);
            Console.ReadKey();

        }

    }
}
