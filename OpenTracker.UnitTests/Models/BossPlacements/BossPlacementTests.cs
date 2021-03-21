using Autofac;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements
{
    public class BossPlacementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly IUndoRedoManager _undoRedoManager = Substitute.For<IUndoRedoManager>();

        [Fact]
        public void Boss_ShouldRaisePropertyChanged()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = null
            };
            
            Assert.PropertyChanged(sut, nameof(IBossPlacement.Boss), () => sut.Boss = BossType.Test);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData(BossType.Armos)]
        public void GetCurrentBoss_ShouldReturnDefaultBossProperty_WhenBossShuffleEqualsFalse(BossType? boss)
        {
            const BossType bossType = BossType.Test;
            _mode.BossShuffle.Returns(false);
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                bossType)
            {
                Boss = boss
            };
            
            Assert.Equal(bossType, sut.GetCurrentBoss());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(BossType.Test, BossType.Test)]
        [InlineData(BossType.Armos, BossType.Armos)]
        public void GetCurrentBoss_ShouldReturnBossProperty_WhenBossShuffleTrue(BossType? expected, BossType? boss)
        {
            _mode.BossShuffle.Returns(true);
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = boss
            };
            
            Assert.Equal(expected, sut.GetCurrentBoss());
        }

        [Fact]
        public void Reset_BossPropertyShouldEqualNullAfterReset_WhenDefaultBossNotAga()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = BossType.Test
            };
            
            sut.Reset();
            
            Assert.Null(sut.Boss);
        }

        [Fact]
        public void Reset_BossPropertyShouldEqualAgaAfterReset_WhenDefaultBossAga()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Aga)
            {
                Boss = BossType.Test
            };
            
            sut.Reset();
            
            Assert.Equal(BossType.Aga, sut.Boss);
        }

        [Fact]
        public void Load_BossPropertyShouldEqualSaveDataBossProperty()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = null
            };

            var saveData = new BossPlacementSaveData()
            {
                Boss = BossType.Test
            };
            
            sut.Load(saveData);
            
            Assert.Equal(BossType.Test, sut.Boss);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = BossType.Test
            };

            sut.Load(null);
            
            Assert.Equal(BossType.Test, sut.Boss);
        }

        [Fact]
        public void Save_ShouldReturnSaveDataWithBossPropertyEqualToBossProperty()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = BossType.Test
            };

            var saveData = sut.Save();
            
            Assert.Equal(BossType.Test, saveData.Boss);
        }

        [Fact]
        public void Save_ShouldReturnSaveDataWithNullBossProperty_WhenBossPropertyIsNull()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test)
            {
                Boss = null
            };

            var saveData = sut.Save();
            
            Assert.Null(saveData.Boss);
        }

        [Fact]
        public void ChangeBoss_ShouldCallNewAction()
        {
            var sut = new BossPlacement(
                _mode, _undoRedoManager, (bossPlacement, newValue) => Substitute.For<IChangeBoss>(),
                BossType.Test);
            sut.ChangeBoss(BossType.Test);
            
            _undoRedoManager.Received().NewAction(Arg.Any<IUndoable>());
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacement.Factory>();
            var sut = factory(BossType.Test);
            
            Assert.NotNull(sut as BossPlacement);
        }
    }
}
