using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.GuaranteedBossItems
{
    public class GuaranteedBossItemsRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new GuaranteedBossItemsRequirement(_mode, true);
            _mode.GuaranteedBossItems.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.GuaranteedBossItems)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool guaranteedBossItems, bool requirement)
        {
            _mode.GuaranteedBossItems.Returns(guaranteedBossItems);
            var sut = new GuaranteedBossItemsRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IGuaranteedBossItemsRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as GuaranteedBossItemsRequirement);
        }
    }
}