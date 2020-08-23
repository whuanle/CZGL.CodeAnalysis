using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class PropertyTests
    {

        ITestOutputHelper _tempOutput;
        public PropertyTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        /*
protected internal static readonly int i
{
    get;
    set;
}

= int.Parse("1");
         */

        /*
protected internal static readonly int i
{
    get
    {
        return tmp + 1;
    }

    set
    {
        tmp += 1;
    }
}

= int.Parse("1");
         */



        [Fact]
        public void 属性_T1()
        {
            PropertyBuilder builder = new PropertyBuilder();
            var field = builder
                .SetVisibility(MemberVisibilityType.ProtectedInternal)
                .SetQualifier(MemberQualifierType.Static | MemberQualifierType.Readonly)
                .SetType("int")
                .SetName("i")
                .Initializer("int.Parse(\"1\")")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();

#if Log
            _tempOutput.WriteLine(result);
#endif

            Assert.Equal(@"protected internal static readonly int i
{
    set;
    get;
}

= int.Parse(""1"");", result);


        }


        [Fact]
        public void 属性_T2()
        {
            PropertyBuilder builder = new PropertyBuilder();
            var field = builder
                .SetVisibility(MemberVisibilityType.ProtectedInternal)
                .SetQualifier(MemberQualifierType.Static | MemberQualifierType.Readonly)
                .SetType("int")
                .SetName("i")
                .GetInitializer("get{return tmp+1;}")
                .SetInitializer("set{tmp+=1;}")
                .Initializer("int.Parse(\"1\")")
                .Build();
            var result = field.NormalizeWhitespace().ToFullString();

#if Log
            _tempOutput.WriteLine(result);
#endif


            Assert.Equal(@"protected internal static readonly int i
{
    get
    {
        return tmp + 1;
    }

    set
    {
        tmp += 1;
    }
}

= int.Parse(""1"");", result);
        }

    }
}
