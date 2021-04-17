using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations.Map
{
    public class MapLocationTests
    {
        private readonly ILocation _location = Substitute.For<ILocation>();
        private readonly IRequirement _requirement = Substitute.For<IRequirement>();

        [Fact]
        public void Ctor_ShouldSetMapID()
        {
            const MapID map = MapID.DarkWorld;

            var sut = new MapLocation(map, 0.0, 0.0, _location);
            
            Assert.Equal(map, sut.Map);
        }

        [Fact]
        public void Ctor_ShouldSetX()
        {
            const double x = 1.0;
            
            var sut = new MapLocation(MapID.LightWorld, x, 0.0, _location);
            
            Assert.Equal(x, sut.X);
        }

        [Fact]
        public void Ctor_ShouldSetY()
        {
            const double y = 1.0;
            
            var sut = new MapLocation(MapID.LightWorld, 0.0, y, _location);
            
            Assert.Equal(y, sut.Y);
        }

        [Fact]
        public void Ctor_ShouldSetLocation()
        {
            var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location);
            
            Assert.Equal(_location, sut.Location);
        }

        [Fact]
        public void RequirementMet_ShouldRaisePropertyChanged()
        {
            var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location, _requirement);

            Assert.PropertyChanged(sut, nameof(IMapLocation.RequirementMet), () =>
                _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
        }

        [Fact]
        public void RequirementMet_ShouldAlwaysReturnTrue_WhenRequirementIsNull()
        {
            var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location);
            
            Assert.True(sut.RequirementMet);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void RequirementMet_ShouldReturnExpected(bool expected, bool met)
        {
            _requirement.Met.Returns(met);
            var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location, _requirement);
            
            Assert.Equal(expected, sut.RequirementMet);
        }

        [Fact]
        public void Visible_ShouldRaisePropertyChanged()
        {
            var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location, _requirement);

            Assert.PropertyChanged(sut, nameof(IMapLocation.Visible), () =>
                _location.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _location, new PropertyChangedEventArgs(nameof(ILocation.Visible))));
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Visible_ShouldReturnExpected(bool expected, bool visible)
        {
            _location.Visible.Returns(visible);
            var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location);
            
            Assert.Equal(expected, sut.Visible);
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMapLocation.Factory>();
            var sut = factory(MapID.LightWorld, 0.0, 0.0, _location, _requirement);
            
            Assert.NotNull(sut as MapLocation);
        }
    }
}