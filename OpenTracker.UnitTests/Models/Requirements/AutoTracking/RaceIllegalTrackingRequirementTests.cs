using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.Requirements.AutoTracking;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AutoTracking
{
    public class RaceIllegalTrackingRequirementTests
    {
        private readonly IAutoTracker _autoTracker = Substitute.For<IAutoTracker>();

        private readonly RaceIllegalTrackingRequirement _sut;

        public RaceIllegalTrackingRequirementTests()
        {
            _sut = new RaceIllegalTrackingRequirement(_autoTracker);
        }
        
        [Fact]
        public void AutoTrackerChanged_ShouldUpdateMetValue()
        {
            _autoTracker.RaceIllegalTracking.Returns(true);

            _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));
            
            Assert.True(_sut.Met);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool raceIllegalTracking)
        {
            _autoTracker.RaceIllegalTracking.Returns(raceIllegalTracking);
            
            _autoTracker.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _autoTracker, new PropertyChangedEventArgs(nameof(IAutoTracker.RaceIllegalTracking)));

            Assert.Equal(expected, _sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IRaceIllegalTrackingRequirement>();
            
            Assert.NotNull(sut as RaceIllegalTrackingRequirement);
        }
    }
}