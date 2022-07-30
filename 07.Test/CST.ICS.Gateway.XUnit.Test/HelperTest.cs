using CST.ICS.Gateway.Common;
using Xunit;
using Xunit.Abstractions;

namespace CST.ICS.Gateway.XUnit.Test
{
    public class HelperTest
    {
        private readonly ITestOutputHelper _output;
        public HelperTest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void AssemblyHelperTest()
        {
            var libs = ReflectionHelper.GetAllAssemblies();
            foreach (var lib in libs)
            {
                _output.WriteLine(lib.FullName);
            }   
            Assert.True(true);
        }
    }
}