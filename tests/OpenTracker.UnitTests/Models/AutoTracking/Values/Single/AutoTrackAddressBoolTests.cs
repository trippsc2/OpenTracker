using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values.Single;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

[ExcludeFromCodeCoverage]
public sealed class AutoTrackAddressBoolTests
{
    private readonly MemoryAddress _memoryAddress = new();
        
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
        int? expected,
        byte? memoryAddressValue,
        byte comparison,
        int trueValue)
    {
        _memoryAddress.Value = memoryAddressValue;
        var sut = new AutoTrackAddressBool(_memoryAddress, comparison, trueValue);

        sut.CurrentValue.Should().Be(expected);
    }

    [Fact]
    public void CurrentValue_ShouldRaisePropertyChanged()
    {
        var memoryAddress = new MemoryAddress();
        var sut = new AutoTrackAddressBool(memoryAddress, 0, 1);

        using var monitor = sut.Monitor();

        memoryAddress.Value = 1;
        
        monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
    }
}