using CZGL.CodeAnalysis.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SharedTests
{
    public class ConstantTableTests
    {
        ITestOutputHelper _tempOutput;
        public ConstantTableTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        [Fact]
        public void Test()
        {
            var output = ConstantTable.GetBaseTypeName<Int32>();
            Assert.Equal("int",output);

            output = ConstantTable.GetBaseTypeName(666);
            Assert.Equal("int", output);

            output = ConstantTable.GetBaseTypeName(new object());
            Assert.Equal("object", output);

            output = ConstantTable.GetBaseTypeName(typeof(object));
            Assert.Equal("object", output);

            output = ConstantTable.GetBaseTypeName(typeof(int));
            Assert.Equal("int", output);
        }

    }
}
