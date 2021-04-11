using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.ShopShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.ShopShuffle
{
    public class ShopShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new ShopShuffleRequirement(_mode, true);
            _mode.ShopShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.ShopShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool shopShuffle, bool requirement)
        {
            _mode.ShopShuffle.Returns(shopShuffle);
            var sut = new ShopShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IShopShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as ShopShuffleRequirement);
        }
    }
}