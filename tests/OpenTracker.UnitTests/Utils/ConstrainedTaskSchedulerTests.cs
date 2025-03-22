using Autofac;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Utils
{
    public class ConstrainedTaskSchedulerTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ConstrainedTaskScheduler>();
            
            Assert.NotNull(sut);
        }

        [Fact]
        public void AutofacSingleInstanceTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var value1 = scope.Resolve<ConstrainedTaskScheduler>();
            var value2 = scope.Resolve<ConstrainedTaskScheduler>();
            
            Assert.Equal(value1, value2);
        }
    }
}