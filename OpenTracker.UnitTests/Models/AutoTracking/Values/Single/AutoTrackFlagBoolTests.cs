using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.UnitTests.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

[ExcludeFromCodeCoverage]
public sealed class AutoTrackFlagBoolTests
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

        sut.CurrentValue.Should().Be(expected);
    }

    [Fact]
    public void FlagChanged_ShouldRaisePropertyChanged()
    {
        _memoryFlag.Status = null;
        var sut = new AutoTrackFlagBool(_memoryFlag, 1);
        
        using var monitor = sut.Monitor();
        
        _memoryFlag.Status = false;
        
        monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
    }
}