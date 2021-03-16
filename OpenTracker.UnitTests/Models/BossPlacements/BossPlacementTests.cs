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
        public void GetCurrentBoss_NonBossShuffleShouldAlwaysReturnDefault()
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
        public void GetCurrentBoss_BossShuffleShouldReturnValueOfBossProperty()
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
        public void Reset_BossShouldBeNullAfterResetIfNotAga()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = BossType.Test
            };
            
            sut.Reset();
            
            Assert.Null(sut.Boss);
        }

        [Fact]
        public void Reset_BossShouldBeAgaAfterResetIfAgaBefore()
        {
            var sut = new BossPlacement(new Mode(), BossType.Aga)
            {
                Boss = BossType.Test
            };
            
            sut.Reset();
            
            Assert.Equal(BossType.Aga, sut.Boss);
        }

        [Fact]
        public void Load_BossShouldBeEqualToExpectedAfterLoad()
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
        public void Load_LoadingNullDataShouldDoNothing()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = BossType.Test
            };

            sut.Load(null);
            
            Assert.Equal(BossType.Test, sut.Boss);
        }

        [Fact]
        public void Save_SaveDataBossShouldBeEqualToBossProperty()
        {
            var sut = new BossPlacement(new Mode(), BossType.Test)
            {
                Boss = BossType.Test
            };

            var saveData = sut.Save();
            
            Assert.Equal(BossType.Test, saveData.Boss);
        }

        [Fact]
        public void Save_SaveDataBossShouldBeNullIfBossPropertyIsNull()
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
