using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Single;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

public class AutoTrackFlagBoolTests
{
    private readonly IMemoryFlag _memoryFlag = Substitute.For<IMemoryFlag>();
        
    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(0, false, 0)]
    [InlineData(0, true, 0)]
    [InlineData(null, null, 1)]
    [InlineData(0, false, 1)]
    [InlineData(1, true, 1)]
    [InlineData(null, null, 2)]
    [InlineData(0, false, 2)]
    [InlineData(2, true, 2)]
    public void CurrentValue_ShouldEqualExpected(int? expected, bool? memoryFlagStatus, int trueValue)
    {
        _memoryFlag.Status.Returns(memoryFlagStatus);
        var sut = new AutoTrackFlagBool(_memoryFlag, trueValue);

        Assert.Equal(expected, sut.CurrentValue);
    }

    [Fact]
    public void FlagChanged_ShouldRaisePropertyChanged()
    {
        _memoryFlag.Status.Returns((bool?)null);
        var sut = new AutoTrackFlagBool(_memoryFlag, 1);
        _memoryFlag.Status.Returns(false);
            
        Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
            () => _memoryFlag.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _memoryFlag, new PropertyChangedEventArgs(nameof(IMemoryFlag.Status))));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IAutoTrackFlagBool.Factory>();
        var sut = factory(_memoryFlag, 1);
            
        Assert.NotNull(sut as AutoTrackFlagBool);
    }
}