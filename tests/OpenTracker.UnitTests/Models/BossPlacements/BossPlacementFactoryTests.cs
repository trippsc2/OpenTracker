using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements;

[ExcludeFromCodeCoverage]
public sealed class BossPlacementFactoryTests
{
    private static readonly IMode Mode = Substitute.For<IMode>();

    private static readonly IChangeBoss.Factory ChangeBossFactory = (_, _) => Substitute.For<IChangeBoss>();
    private readonly IBossPlacement.Factory _factory = boss => new BossPlacement(Mode, ChangeBossFactory, boss);
        
    private readonly IBossPlacementFactory _sut;

    public BossPlacementFactoryTests()
    {
        _sut = new BossPlacementFactory(_factory);
    }
        
    [Theory]
    [InlineData(BossType.Armos, BossPlacementID.EPBoss)]
    [InlineData(BossType.Lanmolas, BossPlacementID.DPBoss)]
    [InlineData(BossType.Moldorm, BossPlacementID.ToHBoss)]
    [InlineData(BossType.Aga, BossPlacementID.ATBoss)]
    [InlineData(BossType.HelmasaurKing, BossPlacementID.PoDBoss)]
    [InlineData(BossType.Arrghus, BossPlacementID.SPBoss)]
    [InlineData(BossType.Mothula, BossPlacementID.SWBoss)]
    [InlineData(BossType.Blind, BossPlacementID.TTBoss)]
    [InlineData(BossType.Kholdstare, BossPlacementID.IPBoss)]
    [InlineData(BossType.Vitreous, BossPlacementID.MMBoss)]
    [InlineData(BossType.Trinexx, BossPlacementID.TRBoss)]
    [InlineData(BossType.Armos, BossPlacementID.GTBoss1)]
    [InlineData(BossType.Lanmolas, BossPlacementID.GTBoss2)]
    [InlineData(BossType.Moldorm, BossPlacementID.GTBoss3)]
    [InlineData(BossType.Aga, BossPlacementID.GTFinalBoss)]
    public void GetBossPlacement_ShouldSetDefaultBossToExpected(BossType expected, BossPlacementID id)
    {
        var bossPlacement = _sut.GetBossPlacement(id);

        bossPlacement.DefaultBoss.Should().Be(expected);
    }

    [Fact]
    public void GetBossPlacement_ShouldThrowException_WhenIDIsOutsideExpected()
    {
        _sut.Invoking(x => x.GetBossPlacement((BossPlacementID)int.MaxValue))
            .Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToSingleInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<IBossPlacementFactory>();

        sut1.Should().BeOfType<BossPlacementFactory>();
        
        var sut2 = scope.Resolve<IBossPlacementFactory>();
        
        sut1.Should().BeSameAs(sut2);
    }
}