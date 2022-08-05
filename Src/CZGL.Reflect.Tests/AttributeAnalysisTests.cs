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
            AttributeDefine[] attributeDefines = AttributeAnalysis.GetDefine(typeof(AttributeTestObject.Test1_1));


        }
    }
}
