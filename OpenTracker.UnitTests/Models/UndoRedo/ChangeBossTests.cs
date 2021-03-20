using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class ChangeBossTests
    {
        private readonly IBossPlacement _bossPlacement = Substitute.For<IBossPlacement>();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeBoss(_bossPlacement, null);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(BossType.Armos, BossType.Armos)]
        [InlineData(BossType.Lanmolas, BossType.Lanmolas)]
        [InlineData(BossType.Moldorm, BossType.Moldorm)]
        [InlineData(BossType.HelmasaurKing, BossType.HelmasaurKing)]
        [InlineData(BossType.Arrghus, BossType.Arrghus)]
        [InlineData(BossType.Mothula, BossType.Mothula)]
        [InlineData(BossType.Blind, BossType.Blind)]
        [InlineData(BossType.Kholdstare, BossType.Kholdstare)]
        [InlineData(BossType.Vitreous, BossType.Vitreous)]
        [InlineData(BossType.Trinexx, BossType.Trinexx)]
        public void ExecuteDo_ShouldSetBossToNewValue(BossType? expected, BossType? newValue)
        {
            var sut = new ChangeBoss(_bossPlacement, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _bossPlacement.Boss);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(BossType.Armos, BossType.Armos)]
        [InlineData(BossType.Lanmolas, BossType.Lanmolas)]
        [InlineData(BossType.Moldorm, BossType.Moldorm)]
        [InlineData(BossType.HelmasaurKing, BossType.HelmasaurKing)]
        [InlineData(BossType.Arrghus, BossType.Arrghus)]
        [InlineData(BossType.Mothula, BossType.Mothula)]
        [InlineData(BossType.Blind, BossType.Blind)]
        [InlineData(BossType.Kholdstare, BossType.Kholdstare)]
        [InlineData(BossType.Vitreous, BossType.Vitreous)]
        [InlineData(BossType.Trinexx, BossType.Trinexx)]
        public void ExecuteUndo_ShouldSetBossToPreviousValue(BossType? expected, BossType? previousValue)
        {
            _bossPlacement.Boss = previousValue;
            var sut = new ChangeBoss(_bossPlacement, null);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _bossPlacement.Boss);
        }
    }
}