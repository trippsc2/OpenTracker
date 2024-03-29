using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values.Single;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

[ExcludeFromCodeCoverage]
public sealed class AutoTrackAddressValueTests
{
    private readonly MemoryAddress _memoryAddress = new();
        
    [Theory]
    [InlineData(null, null, 0, 0)]
    [InlineData(0, (byte)0, 0, 0)]
    [InlineData(null, (byte)1, 0, 0)]
    [InlineData(null, null, 0, 1)]
    [InlineData(null, (byte)0, 0, 1)]
    [InlineData(null, null, 1, 0)]
    [InlineData(0, (byte)0, 1, 0)]
    [InlineData(1, (byte)1, 1, 0)]
    [InlineData(null, (byte)2, 1, 0)]
    [InlineData(null, null, 1, 1)]
    [InlineData(1, (byte)0, 1, 1)]
    [InlineData(null, (byte)1, 1, 1)]
    [InlineData(null, null, 1, 2)]
    [InlineData(null, (byte)0, 1, 2)]
    [InlineData(null, null, 2, 0)]
    [InlineData(0, (byte)0, 2, 0)]
    [InlineData(1, (byte)1, 2, 0)]
    [InlineData(2, (byte)2, 2, 0)]
    [InlineData(null, (byte)3, 2, 0)]
    [InlineData(null, null, 2, 1)]
    [InlineData(1, (byte)0, 2, 1)]
    [InlineData(2, (byte)1, 2, 1)]
    [InlineData(null, (byte)2, 2, 1)]
    [InlineData(2, (byte)0, 2, 2)]
    [InlineData(null, (byte)1, 2, 2)]
    [InlineData(null, null, 2, 3)]
    [InlineData(null, (byte)0, 2, 3)]
    public void CurrentValue_ShouldEqualExpected(int? expected, byte? memoryAddressValue, byte maximum, int adjustment)
    {
        _memoryAddress.Value = memoryAddressValue;
        var sut = new AutoTrackAddressValue(_memoryAddress, maximum, adjustment);

        sut.CurrentValue.Should().Be(expected);
    }

    [Fact]
    public void CurrentValue_ShouldRaisePropertyChanged()
    {
        _memoryAddress.Value = null;
            
        var sut = new AutoTrackAddressValue(_memoryAddress, 255);

        using var monitor = sut.Monitor();
        
        _memoryAddress.Value = 1;
        
        monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
    }
}