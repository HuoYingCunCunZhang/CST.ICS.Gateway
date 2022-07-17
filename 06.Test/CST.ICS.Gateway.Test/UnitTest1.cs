using CST.ICS.Gateway.Common.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace CST.ICS.Gateway.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;
        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void Test1()
        {
            var dlls = ReflectionHelper.GetAllAssemblies("CST","Common");
            foreach (var dll in dlls)
            {
                _output.WriteLine(dll.FullName);
                Assert.NotNull(dll.FullName);
            }
        }
    }
}