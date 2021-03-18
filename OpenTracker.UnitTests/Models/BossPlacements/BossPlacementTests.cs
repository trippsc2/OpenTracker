using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements
{
    public class BossPlacementTests
    {
        [Fact]
        public void Boss_ShouldRaisePropertyChanged()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = null
            };
            
            Assert.PropertyChanged(sut, nameof(IBossPlacement.Boss), () => sut.Boss = BossType.Test);
        }
        
        [Fact]
        public void GetCurrentBoss_ShouldReturnDefaultBossProperty_WhenBossShuffleFalse()
        {
            const BossType bossType = BossType.Test;
            var sut = new BossPlacement(new Mode()
            {
                BossShuffle = false
            }, bossType)
            {
                Boss = null
            };
            
            Assert.Equal(bossType, sut.GetCurrentBoss());

            sut.Boss = BossType.Armos;
            
            Assert.Equal(bossType, sut.GetCurrentBoss());
        }

        [Fact]
        public void GetCurrentBoss_ShouldReturnBossProperty_WhenBossShuffleTrue()
        {
            const BossType bossType = BossType.Test;
            var sut = new BossPlacement(new Mode()
            {
                BossShuffle = true
            }, bossType)
            {
                Boss = bossType
            };
            
            Assert.Equal(bossType, sut.GetCurrentBoss());

            sut.Boss = BossType.Armos;
            
            Assert.Equal(BossType.Armos, sut.GetCurrentBoss());
        }

        [Fact]
        public void Reset_BossPropertyShouldEqualNullAfterReset_WhenDefaultBossNotAga()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = BossType.Test
            };
            
            sut.Reset();
            
            Assert.Null(sut.Boss);
        }

        [Fact]
        public void Reset_BossPropertyShouldEqualAgaAfterReset_WhenDefaultBossAga()
        {
            var sut = new BossPlacement(new Mode(), BossType.Aga)
            {
                Boss = BossType.Test
            };
            
            sut.Reset();
            
            Assert.Equal(BossType.Aga, sut.Boss);
        }

        [Fact]
        public void Load_BossPropertyShouldEqualSaveDataBossProperty()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
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
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = BossType.Test
            };

            sut.Load(null);
            
            Assert.Equal(BossType.Test, sut.Boss);
        }

        [Fact]
        public void Save_ShouldReturnSaveDataWithBossPropertyEqualToBossProperty()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = BossType.Test
            };

            var saveData = sut.Save();
            
            Assert.Equal(BossType.Test, saveData.Boss);
        }

        [Fact]
        public void Save_ShouldReturnSaveDataWithNullBossProperty_WhenBossPropertyIsNull()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = null
            };

            var saveData = sut.Save();
            
            Assert.Null(saveData.Boss);
        }
    }
}
