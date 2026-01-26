using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single;

public class AutoTrackItemValueTests
{
    private readonly IItem _item = Substitute.For<IItem>();
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
    public void ItemChanged_CurrentValueShouldEqualExpected(int? expected, int itemCurrent)
    {
        _item.Current.Returns(itemCurrent);
            
        Assert.Equal(expected, _sut.CurrentValue);
    }

    [Fact]
    public void ItemChanged_ShouldRaisePropertyChanged()
    {
        Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
            () => _item.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _item, new PropertyChangedEventArgs(nameof(IItem.Current))));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IAutoTrackItemValue.Factory>();
        var sut = factory(_item);
            
        Assert.NotNull(sut as AutoTrackItemValue);
    }
}