using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory;

[ExcludeFromCodeCoverage]
public sealed class MemoryAddressTests
{
    private readonly MemoryAddress _sut = new();

    [Fact]
    public void Value_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        _sut.Value = 1;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Value);
    }

    [Fact]
    public void Reset_ShouldSetValueToNull()
    {
        _sut.Value = 1;
        _sut.Reset();

        _sut.Value.Should().BeNull();
    }
}