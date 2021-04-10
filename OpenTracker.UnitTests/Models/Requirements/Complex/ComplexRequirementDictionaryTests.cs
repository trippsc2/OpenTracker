using Autofac;
using OpenTracker.Models.Requirements.Complex;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Complex
{
    public class ComplexRequirementDictionaryTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IComplexRequirementDictionary>();
            
            Assert.NotNull(sut as ComplexRequirementDictionary);
        }
    }
}