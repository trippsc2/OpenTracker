using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Single;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

public class AutoTrackAddressBoolTests
{
    private readonly IMemoryAddress _memoryAddress = Substitute.For<IMemoryAddress>();
        
    [Theory]
    [InlineData(null, null, 0, 1)]
    [InlineData(null, null, 0, 2)]
    [InlineData(0, (byte)0, 0, 1)]
    [InlineData(0, (byte)0, 0, 2)]
    [InlineData(1, (byte)1, 0, 1)]
    [InlineData(2, (byte)1, 0, 2)]
    [InlineData(null, null, 1, 1)]
    [InlineData(null, null, 1, 2)]
    [InlineData(0, (byte)0, 1, 1)]
    [InlineData(0, (byte)0, 1, 2)]
    [InlineData(0, (byte)1, 1, 1)]
    [InlineData(0, (byte)1, 1, 2)]
    [InlineData(1, (byte)2, 1, 1)]
    [InlineData(2, (byte)2, 1, 2)]
    public void CurrentValue_ShouldEqualExpected(
        int? expected, byte? memoryAddressValue, byte comparison, int trueValue)
    {
        _memoryAddress.Value.Returns(memoryAddressValue);
        var sut = new AutoTrackAddressBool(_memoryAddress, comparison, trueValue);

        Assert.Equal(expected, sut.CurrentValue);
    }

    [Fact]
    public void MemoryAddressChanged_RaisesPropertyChanged()
    {
        var memoryAddress = new MemoryAddress();
        var sut = new AutoTrackAddressBool(memoryAddress, 0, 1);
            
        Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
            () => memoryAddress.Value = 1);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IAutoTrackAddressBool.Factory>();
        var sut = factory(_memoryAddress, 1, 1);
            
        Assert.NotNull(sut as AutoTrackAddressBool);
    }
}