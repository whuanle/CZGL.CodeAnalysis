//using CZGL.Roslyn;
//using Microsoft.CodeAnalysis;
//using Xunit;
//using Xunit.Abstractions;

//namespace RoslynTests
//{
//   public class ABCZH
//    {
//        private protected void Test() { }
//    }
//    public class ClassTests
//    {
//        ITestOutputHelper _tempOutput;
//        public ClassTests(ITestOutputHelper tempOutput)
//        {
//            _tempOutput = tempOutput;
//        }
        
//        [Fact]
//        public void 只命名()
//        {
//            ClassBuilder classBuilder = new ClassBuilder();
//            var type = classBuilder.WithName("TestClass");

//            var result = type.FullCode();
//#if Log
//            _tempOutput.WriteLine(result);
//#endif
//            Assert.Equal(@"class TestClass
//{
//}", result);

//        }
//    }
//}
