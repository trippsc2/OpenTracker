using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.PrizePlacements;

[ExcludeFromCodeCoverage]
public sealed class PrizePlacementDictionaryTests
{
    private readonly PrizePlacementDictionary _sut = new(() => Substitute.For<IPrizePlacementFactory>());

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var prizePlacement1 = _sut[PrizePlacementID.ATPrize];
        var prizePlacement2 = _sut[PrizePlacementID.ATPrize];
            
        Assert.Equal(prizePlacement1, prizePlacement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var prizePlacement1 = _sut[PrizePlacementID.ATPrize];
        var prizePlacement2 = _sut[PrizePlacementID.EPPrize];
            
        Assert.NotEqual(prizePlacement1, prizePlacement2);
    }

    [Fact]
    public void Reset_ShouldCallResetOnBossPlacements()
    {
        var prizePlacement = _sut[PrizePlacementID.ATPrize];
        _sut.Reset();
            
        prizePlacement.Received().Reset();
    }

    [Fact]
    public void Save_ShouldCallSaveOnBossPlacements()
    {
        var prizePlacement = _sut[PrizePlacementID.ATPrize];
        _ = _sut.Save();
            
        prizePlacement.Received().Save();
    }

    [Fact]
    public void Save_ShouldReturnDictionaryOfSaveData()
    {
        var prizePlacement = _sut[PrizePlacementID.ATPrize];
        var bossPlacementSaveData = new PrizePlacementSaveData();
        prizePlacement.Save().Returns(bossPlacementSaveData);
        var saveData = _sut.Save();

        Assert.Equal(bossPlacementSaveData, saveData[PrizePlacementID.ATPrize]);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        var bossPlacement = _sut[PrizePlacementID.ATPrize];
        _sut.Load(null);
            
        bossPlacement.DidNotReceive().Load(Arg.Any<PrizePlacementSaveData>());
    }

    [Fact]
    public void Load_ShouldCallLoadOnBossPlacements()
    {
        var bossPlacement = _sut[PrizePlacementID.ATPrize];
        var saveData = new Dictionary<PrizePlacementID, PrizePlacementSaveData>
        {
            { PrizePlacementID.ATPrize, new PrizePlacementSaveData() }
        };
        _sut.Load(saveData);
            
        bossPlacement.Received().Load(Arg.Any<PrizePlacementSaveData>());
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IPrizePlacementDictionary>();
            
        Assert.NotNull(sut as PrizePlacementDictionary);
    }
}