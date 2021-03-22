using Autofac;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements
{
    public class BossPlacementFactoryTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly IUndoRedoManager _undoRedoManager = Substitute.For<IUndoRedoManager>();
        private readonly IBossPlacementFactory _sut;

        public BossPlacementFactoryTests()
        {
            _sut = new BossPlacementFactory(boss => new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                boss));
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
            
            Assert.Equal(expected, bossPlacement.DefaultBoss);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossPlacementFactory>();
            
            Assert.NotNull(sut as BossPlacementFactory);
        }
    }
}