
using CZGL.Reflect;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using CZGL.CodeAnalysis.Shared;
using System.ComponentModel.DataAnnotations;

namespace ReflectTests
{

    public class 特性解析
    {
        ITestOutputHelper _tempOutput;
        public 特性解析(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }


        public class Test
        {
            [Display(Name = "测试", AutoGenerateField = true)]
            public int A { get; set; }
        }


        [Fact]
        public void 特性解析器()
        {
            // AttributeDefine 对应一个特性
            AttributeDefine[] attributeDefines = AttributeAnalysis.GetDefine(typeof(Test));
            string[] str = AttributeAnalysis.GetAttributes(typeof(Test));
        }
    }
}
