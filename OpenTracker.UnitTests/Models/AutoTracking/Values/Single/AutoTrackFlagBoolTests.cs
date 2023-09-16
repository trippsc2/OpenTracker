using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.UnitTests.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

public class AutoTrackFlagBoolTests
{
    private readonly MockMemoryFlag _memoryFlag = new();
        
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
        _memoryFlag.Status = memoryFlagStatus;
        var sut = new AutoTrackFlagBool(_memoryFlag, trueValue);

        Assert.Equal(expected, sut.CurrentValue);
    }

    [Fact]
    public void FlagChanged_ShouldRaisePropertyChanged()
    {
        _memoryFlag.Status = null;
        var sut = new AutoTrackFlagBool(_memoryFlag, 1);
            
        Assert.PropertyChanged(
            sut,
            nameof(IAutoTrackValue.CurrentValue),
            () => _memoryFlag.Status = false);
    }
}