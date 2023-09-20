using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Prize;
using Xunit;

namespace OpenTracker.UnitTests.Models.PrizePlacements;

[ExcludeFromCodeCoverage]
public sealed class PrizePlacementTests
{
    private readonly IPrizeDictionary _prizes = new PrizeDictionary(
        (_, _) => Substitute.For<IItem>());

    private readonly IChangePrize.Factory _changePrizeFactory = prizePlacement => new ChangePrize(prizePlacement);

    private readonly IItem _startingPrize = Substitute.For<IItem>();

    [Fact]
    public void Prize_ShouldRaisePropertyChanged()
    {
        var sut = new PrizePlacement(_prizes, _changePrizeFactory);
            
        Assert.PropertyChanged(sut, nameof(IPrizePlacement.Prize), () => sut.Cycle());
    }

    [Fact]
    public void CanCycle_ShouldReturnTrue_WhenStartingPrizeIsNull()
    {
        var sut = new PrizePlacement(_prizes, _changePrizeFactory);
            
        Assert.True(sut.CanCycle());
    }

    [Fact]
    public void CanCycle_ShouldReturnFalse_WhenStartingPrizeIsNotNull()
    {
        var sut = new PrizePlacement(_prizes, _changePrizeFactory, _startingPrize);
            
        Assert.False(sut.CanCycle());
    }

    [Fact]
    public void Cycle_ShouldThrowException_WhenPrizeTypeIsUnexpected()
    {
        var startingPrize = _prizes[PrizeType.Aga1];
        var sut = new PrizePlacement(_prizes, _changePrizeFactory, startingPrize);

        Assert.Throws<Exception>(() => sut.Cycle());
    }

    [Theory]
    [InlineData(PrizeType.Crystal, null, false)]
    [InlineData(PrizeType.GreenPendant, null, true)]
    [InlineData(PrizeType.RedCrystal, PrizeType.Crystal, false)]
    [InlineData(null, PrizeType.Crystal, true)]
    [InlineData(PrizeType.Pendant, PrizeType.RedCrystal, false)]
    [InlineData(PrizeType.Crystal, PrizeType.RedCrystal, true)]
    [InlineData(PrizeType.GreenPendant, PrizeType.Pendant, false)]
    [InlineData(PrizeType.RedCrystal, PrizeType.Pendant, true)]
    [InlineData(null, PrizeType.GreenPendant, false)]
    [InlineData(PrizeType.Pendant, PrizeType.GreenPendant, true)]
    public void Cycle_ShouldSetPrizeToExpected(PrizeType? expected, PrizeType? startingPrize, bool reverse)
    {
        var startingPrizeItem = startingPrize is null ? null : _prizes[startingPrize.Value];
        var expectedPrizeItem = expected is null ? null : _prizes[expected.Value];

        var sut = new PrizePlacement(_prizes, _changePrizeFactory, startingPrizeItem);
            
        sut.Cycle(reverse);
            
        Assert.Equal(expectedPrizeItem, sut.Prize);
    }

    [Fact]
    public void CreateChangePrizeAction_ShouldReturnNewAction()
    {
        var sut = new PrizePlacement(_prizes, _changePrizeFactory);

        var action = sut.CreateChangePrizeAction();
            
        Assert.NotNull(action as ChangePrize);
    }

    [Fact]
    public void Reset_ShouldSetPrizeToStartingValue()
    {
        var startingPrize = _prizes[PrizeType.Crystal];

        var sut = new PrizePlacement(_prizes, _changePrizeFactory, startingPrize);
            
        sut.Cycle();
        sut.Reset();
            
        Assert.Equal(startingPrize, sut.Prize);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(PrizeType.Aga1)]
    [InlineData(PrizeType.Aga2)]
    [InlineData(PrizeType.Crystal)]
    [InlineData(PrizeType.RedCrystal)]
    [InlineData(PrizeType.Pendant)]
    [InlineData(PrizeType.GreenPendant)]
    public void Save_ShouldSetPrizeToExpected(PrizeType? expected)
    {
        var startingPrize = expected is null ? null : _prizes[expected.Value];
        var sut = new PrizePlacement(_prizes, _changePrizeFactory, startingPrize);
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Prize);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        var sut = new PrizePlacement(_prizes, _changePrizeFactory, _startingPrize);
            
        sut.Load(null);
            
        Assert.Equal(_startingPrize, sut.Prize);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(PrizeType.Aga1)]
    [InlineData(PrizeType.Aga2)]
    [InlineData(PrizeType.Crystal)]
    [InlineData(PrizeType.RedCrystal)]
    [InlineData(PrizeType.Pendant)]
    [InlineData(PrizeType.GreenPendant)]
    public void Load_ShouldSetPrizeToExpected(PrizeType? expected)
    {
        var expectedPrize = expected is null ? null : _prizes[expected.Value];
            
        var sut = new PrizePlacement(_prizes, _changePrizeFactory);
        var saveData = new PrizePlacementSaveData
        {
            Prize = expected
        };
            
        sut.Load(saveData);
            
        Assert.Equal(expectedPrize, sut.Prize);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IPrizePlacement.Factory>();
        var sut = factory(_startingPrize);
            
        Assert.NotNull(sut as PrizePlacement);
    }
}