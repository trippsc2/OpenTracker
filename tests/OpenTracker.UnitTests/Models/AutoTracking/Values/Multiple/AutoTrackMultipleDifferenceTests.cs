using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Multiple;

public class AutoTrackMultipleDifferenceTests
{
    private readonly IAutoTrackValue _value1 = Substitute.For<IAutoTrackValue>();
    private readonly IAutoTrackValue _value2 = Substitute.For<IAutoTrackValue>();
    private readonly AutoTrackMultipleDifference _sut;

    public AutoTrackMultipleDifferenceTests()
    {
        _sut = new AutoTrackMultipleDifference(_value1, _value2);
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData(0, 0, null)]
    [InlineData(1, 1, null)]
    [InlineData(2, 2, null)]
    [InlineData(null, null, 0)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 1, 0)]
    [InlineData(2, 2, 0)]
    [InlineData(null, null, 1)]
    [InlineData(0, 0, 1)]
    [InlineData(0, 1, 1)]
    [InlineData(1, 2, 1)]
    [InlineData(null, null, 2)]
    [InlineData(0, 0, 2)]
    [InlineData(0, 1, 2)]
    [InlineData(0, 2, 2)]
    public void CurrentValue_ShouldEqualExpected(int? expected, int? value1, int? value2)
    {
        _value1.CurrentValue.Returns(value1);
        _value2.CurrentValue.Returns(value2);
            
        Assert.Equal(expected, _sut.CurrentValue);
    }

    [Fact]
    public void ValueChanged_ShouldRaisePropertyChanged()
    {
        Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
            () => _value1.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _value1, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));
        Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
            () => _value2.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _value2, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IAutoTrackMultipleDifference.Factory>();
        var sut = factory(_value1, _value2);
            
        Assert.NotNull(sut as AutoTrackMultipleDifference);
    }
}