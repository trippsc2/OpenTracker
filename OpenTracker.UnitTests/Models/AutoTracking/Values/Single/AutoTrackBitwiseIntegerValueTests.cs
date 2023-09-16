using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values.Single;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

[ExcludeFromCodeCoverage]
public sealed class AutoTrackBitwiseIntegerValueTests
{
    private readonly MemoryAddress _memoryAddress = new();
        
    [Theory]
    [InlineData(null, null, 0, 0)]
    [InlineData(0, (byte)0, 0, 0)]
    [InlineData(0, (byte)1, 0, 0)]
    [InlineData(0, (byte)255, 0, 0)]
    [InlineData(null, null, 1, 0)]
    [InlineData(0, (byte)0, 1, 0)]
    [InlineData(1, (byte)1, 1, 0)]
    [InlineData(0, (byte)254, 1, 0)]
    [InlineData(1, (byte)255, 1, 0)]
    [InlineData(null, null, 3, 0)]
    [InlineData(0, (byte)0, 3, 0)]
    [InlineData(1, (byte)1, 3, 0)]
    [InlineData(2, (byte)2, 3, 0)]
    [InlineData(3, (byte)3, 3, 0)]
    [InlineData(0, (byte)252, 3, 0)]
    [InlineData(1, (byte)253, 3, 0)]
    [InlineData(2, (byte)254, 3, 0)]
    [InlineData(3, (byte)255, 3, 0)]
    [InlineData(null, null, 0, 1)]
    [InlineData(0, (byte)0, 0, 1)]
    [InlineData(0, (byte)1, 0, 1)]
    [InlineData(0, (byte)255, 0, 1)]
    [InlineData(null, null, 1, 1)]
    [InlineData(0, (byte)0, 1, 1)]
    [InlineData(0, (byte)255, 1, 1)]
    [InlineData(null, null, 3, 1)]
    [InlineData(0, (byte)0, 3, 1)]
    [InlineData(0, (byte)1, 3, 1)]
    [InlineData(1, (byte)2, 3, 1)]
    [InlineData(1, (byte)3, 3, 1)]
    [InlineData(0, (byte)252, 3, 1)]
    [InlineData(0, (byte)253, 3, 1)]
    [InlineData(1, (byte)254, 3, 1)]
    [InlineData(1, (byte)255, 3, 1)]
    public void CurrentValue_ShouldEqualExpected(int? expected, byte? memoryAddressValue, byte mask, int shift)
    {
        _memoryAddress.Value = memoryAddressValue;
        var sut = new AutoTrackBitwiseIntegerValue(_memoryAddress, mask, shift);

        sut.CurrentValue.Should().Be(expected);
    }

    [Fact]
    public void CurrentValue_ShouldRaisePropertyChanged()
    {
        _memoryAddress.Value = null;
            
        var sut = new AutoTrackBitwiseIntegerValue(_memoryAddress, 255, 0);

        using var monitor = sut.Monitor();
        
        _memoryAddress.Value = 1;
        
        monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
    }
}