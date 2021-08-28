using CZGL.CodeAnalysis.Shared;
using CZGL.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ReflectTests
{
    public class 访问修饰符
    {
        ITestOutputHelper _tempOutput;
        public 访问修饰符(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        public class Test { }

        [Fact]

        public void 判断各种类型1()
        {
            Assert.Equal(MemberAccess.Public, AccessAnalysis.GetAccess(typeof(Test)));
        }

    }
}
