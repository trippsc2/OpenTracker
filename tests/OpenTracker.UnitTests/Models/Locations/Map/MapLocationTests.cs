using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Locations.Map;

[ExcludeFromCodeCoverage]
public sealed class MapLocationTests
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
    public void IsActive_ShouldRaisePropertyChanged()
    {
        var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location, _requirement);

        _requirement.Met.Returns(true);
        _location.IsActive.Returns(true);

        Assert.PropertyChanged(sut, nameof(IMapLocation.IsActive), () =>
            _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void IsActive_ShouldReturnExpected_WhenRequirementIsNull(bool expected, bool locationIsActive)
    {
        _location.IsActive.Returns(locationIsActive);
            
        var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location);

        Assert.Equal(expected, sut.IsActive);
    }

    [Theory]
    [InlineData(false, false, false)]
    [InlineData(false, false, true)]
    [InlineData(false, true, false)]
    [InlineData(true, true, true)]
    public void IsActive_ShouldReturnExpected(bool expected, bool metRequirement, bool locationIsActive)
    {
        _location.IsActive.Returns(locationIsActive);
        _requirement.Met.Returns(metRequirement);
        var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location, _requirement);
            
        Assert.Equal(expected, sut.IsActive);
    }

    [Fact]
    public void ShouldBeDisplayed_ShouldRaisePropertyChanged()
    {
        var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location, _requirement);

        Assert.PropertyChanged(sut, nameof(IMapLocation.ShouldBeDisplayed), () =>
            _location.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _location, new PropertyChangedEventArgs(nameof(ILocation.ShouldBeDisplayed))));
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ShouldBeDisplayed_ShouldReturnExpected(bool expected, bool visible)
    {
        _location.ShouldBeDisplayed.Returns(visible);
        var sut = new MapLocation(MapID.LightWorld, 0.0, 0.0, _location);
            
        Assert.Equal(expected, sut.ShouldBeDisplayed);
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