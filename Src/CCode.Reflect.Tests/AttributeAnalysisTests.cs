using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Abstractions;

namespace CZGL.Reflect.Tests
{
    public class AttributeAnalysisTests
    {
        ITestOutputHelper _tempOutput;
        public AttributeAnalysisTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void Get_Type_Attribute_Define()
        {
            var defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test1_1));
            Assert.Equal(1, defines.Count);
            Assert.Null(defines[0].ConstructorArguments);
            Assert.Null(defines[0].NamedArguments);
        }

        [Fact]
        public void Get_Type_Attribute_Code_Single()
        {
            var defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test1_1));
            var code = defines.View();
            Assert.Equal("[Test1_]", code);

            defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test1_2));
            code = defines[0].View();
            Assert.Equal("[Test1_(\"aaa\")]", code);
            code = defines[0].ToString();
            Assert.Equal("[Test1_(\"aaa\")]", code);
            code = defines.View();
            Assert.Equal("[Test1_(\"aaa\")]", code);

            defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test1_3));
            code = defines.View();
            Assert.Equal("[Test1_(\"aaa\", Value = \"666\")]", code);

            defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test1_4));
            code = defines.View();
            Assert.Equal("[Test1_(Name = \"aaa\", Value = \"666\")]", code);
        }

        [Fact]
        public void Get_Type_Attribute_Code_Multiple()
        {
            string _code = @"[Test2_(""aaa"")]
[Test2_(Name = ""bbb"")]
[Test2_(Value = ""ccc"")]
[Test2_(Name = ""ccc"", Value = ""ddd"")]".ConvertLine();

            var defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test2_1));
            var code = defines.View();
            Assert.Equal(_code, code);
        }

        [Fact]
        public void Get_Type_Attribute_Code_Inherited()
        {
            var defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test3_1));
            var code = defines.View();
            Assert.Equal("[Test3_(Name = \"666\")]", code);

            defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test3_2));
            code = defines.View();
            Assert.Equal("", code);

            defines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test3_4));
            code = defines.View();
            Assert.Equal("[Test3_(Value = \"666\")]", code);
        }

        [Fact]
        public void Get_Member_Attribute_Code_Inherited()
        {
            var defines = AttributeAnalysis.GetDefine<Type>(typeof(AttributeTestObject.Test3_1));
            var code = defines.View();
            Assert.Equal("[Test3_(Name = \"666\")]", code);

            defines = AttributeAnalysis.GetDefine<Type>(typeof(AttributeTestObject.Test3_2));
            code = defines.View();
            Assert.Equal("", code);

            defines = AttributeAnalysis.GetDefine<Type>(typeof(AttributeTestObject.Test3_4));
            code = defines.View();
            Assert.Equal("[Test3_(Value = \"666\")]", code);
        }
    }
}
