using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements;

[ExcludeFromCodeCoverage]
public sealed class BossPlacementDictionaryTests
{
    private readonly BossPlacementDictionary _sut = new(() => Substitute.For<IBossPlacementFactory>());

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var bossPlacement1 = _sut[BossPlacementID.ATBoss];
        var bossPlacement2 = _sut[BossPlacementID.ATBoss];

        bossPlacement1.Should().Be(bossPlacement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var bossPlacement1 = _sut[BossPlacementID.ATBoss];
        var bossPlacement2 = _sut[BossPlacementID.EPBoss];

        bossPlacement1.Should().NotBe(bossPlacement2);
    }

    [Fact]
    public void Reset_ShouldCallResetOnBossPlacements()
    {
        var bossPlacement = _sut[BossPlacementID.ATBoss];
        _sut.Reset();
            
        bossPlacement.Received().Reset();
    }

    [Fact]
    public void Save_ShouldCallSaveOnBossPlacements()
    {
        var bossPlacement = _sut[BossPlacementID.ATBoss];
        _ = _sut.Save();
            
        bossPlacement.Received().Save();
    }

    [Fact]
    public void Save_ShouldReturnDictionaryOfSaveData()
    {
        const BossPlacementID id = BossPlacementID.ATBoss;
        
        var bossPlacement = _sut[id];
        var bossPlacementSaveData = new BossPlacementSaveData();
        bossPlacement.Save().Returns(bossPlacementSaveData);
        var saveData = _sut.Save();

        saveData[id].Should().Be(bossPlacementSaveData);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        var bossPlacement = _sut[BossPlacementID.ATBoss];
        _sut.Load(null);
            
        bossPlacement.DidNotReceive().Load(Arg.Any<BossPlacementSaveData>());
    }

    [Fact]
    public void Load_ShouldCallLoadOnBossPlacements()
    {
        var bossPlacement = _sut[BossPlacementID.ATBoss];
        var saveData = new Dictionary<BossPlacementID, BossPlacementSaveData>
        {
            { BossPlacementID.ATBoss, new BossPlacementSaveData() }
        };
        _sut.Load(saveData);
            
        bossPlacement.Received().Load(Arg.Any<BossPlacementSaveData>());
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToSingleInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<IBossPlacementDictionary>();

        sut1.Should().BeOfType<BossPlacementDictionary>();
        
        var sut2 = scope.Resolve<IBossPlacementDictionary>();

        sut1.Should().BeSameAs(sut2);
    }
}