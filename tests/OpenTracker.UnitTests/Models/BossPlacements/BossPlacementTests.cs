using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements;

[ExcludeFromCodeCoverage]
public sealed class BossPlacementTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    private readonly IChangeBoss.Factory _changeBossFactory = (_, _) => Substitute.For<IChangeBoss>(); 

    [Fact]
    public void Boss_ShouldRaisePropertyChanged()
    {
        var sut = new BossPlacement(_mode, _changeBossFactory, BossType.Test) { Boss = null };

        using var monitor = sut.Monitor();
        
        sut.Boss = BossType.Test;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Boss);
    }

    [Fact]
    public void Reset_BossPropertyShouldEqualNullAfterReset_WhenDefaultBossNotAga()
    {
        var sut = new BossPlacement(_mode, _changeBossFactory, BossType.Test) { Boss = BossType.Test };
            
        sut.Reset();

        sut.Boss.Should().BeNull();
    }

    [Fact]
    public void Reset_BossPropertyShouldEqualAgaAfterReset_WhenDefaultBossAga()
    {
        const BossType expected = BossType.Aga;
        
        var sut = new BossPlacement(_mode, _changeBossFactory, expected) { Boss = BossType.Test };
            
        sut.Reset();

        sut.Boss.Should().Be(expected);
    }

    [Fact]
    public void Load_BossPropertyShouldEqualSaveDataBossProperty()
    {
        const BossType expected = BossType.Test;
        
        var sut = new BossPlacement(_mode, _changeBossFactory, BossType.Armos) { Boss = null };

        var saveData = new BossPlacementSaveData { Boss = expected };
            
        sut.Load(saveData);

        sut.Boss.Should().Be(expected);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        const BossType expected = BossType.Test;
        
        var sut = new BossPlacement(_mode, _changeBossFactory, BossType.Armos) { Boss = expected };

        sut.Load(null);

        sut.Boss.Should().Be(expected);
    }

    [Fact]
    public void Save_ShouldReturnSaveDataWithBossPropertyEqualToBossProperty()
    {
        const BossType expected = BossType.Test;
        
        var sut = new BossPlacement(_mode, _changeBossFactory, BossType.Armos) { Boss = expected };

        var saveData = sut.Save();
            
        saveData.Boss.Should().Be(expected);
    }

    [Fact]
    public void Save_ShouldReturnSaveDataWithNullBossProperty_WhenBossPropertyIsNull()
    {
        var sut = new BossPlacement(_mode, _changeBossFactory, BossType.Test) { Boss = null };

        var saveData = sut.Save();
            
        saveData.Boss.Should().BeNull();
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBossPlacement.Factory>();
        var sut1 = factory(BossType.Test);

        sut1.Should().BeOfType<BossPlacement>();
        
        var sut2 = factory(BossType.Test);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}