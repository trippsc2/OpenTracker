using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.UnitTests.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

[ExcludeFromCodeCoverage]
public sealed class AutoTrackItemValueTests
{
    private readonly MockItem _item = new();
    private readonly AutoTrackItemValue _sut;

    public AutoTrackItemValueTests()
    {
        _sut = new AutoTrackItemValue(_item);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void CurrentValue_ShouldReturnExpected(int? expected, int itemCurrent)
    {
        _item.Current = itemCurrent;

        _sut.CurrentValue.Should().Be(expected);
    }

    [Fact]
    public void ItemChanged_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        _item.Current = 1;
        
        monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
    }
}