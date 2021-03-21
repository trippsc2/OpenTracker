using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements
{
    public class BossPlacementDictionaryTests
    {
        private readonly BossPlacementDictionary _sut;

        public BossPlacementDictionaryTests()
        {
            _sut = new BossPlacementDictionary(() => Substitute.For<IBossPlacementFactory>());
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
            var bossPlacement = _sut[BossPlacementID.ATBoss];
            var bossPlacementSaveData = new BossPlacementSaveData();
            bossPlacement.Save().Returns(bossPlacementSaveData);
            var saveData = _sut.Save();

            Assert.Equal(bossPlacementSaveData, saveData[BossPlacementID.ATBoss]);
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
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossPlacementDictionary>();
            
            Assert.NotNull(sut as BossPlacementDictionary);
        }
    }
}