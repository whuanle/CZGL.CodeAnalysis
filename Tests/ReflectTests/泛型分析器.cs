using CZGL.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ReflectTests
{
    public class 泛型分析器
    {
        ITestOutputHelper _tempOutput;
        public 泛型分析器(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void 泛型类原名称()
        {
            Type type = typeof(Model_泛型1<,,>);
            Assert.Equal("Model_泛型1", GenericeAnalysis.GetOriginName(type));
            Assert.Equal("Model_泛型1", GenericeAnalysis.WipeOutName(type.Name));
        }

        public class Test
        {
            public void A<T>() { }
        }

        [Fact]
        public void 泛型方法原名称()
        {
            Type type = typeof(Test);
            MethodInfo methodInfo = type.GetMethod("A");
            Assert.Equal("A", GenericeAnalysis.WipeOutName(methodInfo.Name));
        }


    }
}
