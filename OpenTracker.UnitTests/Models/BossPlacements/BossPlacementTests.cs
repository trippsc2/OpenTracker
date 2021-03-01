using Autofac;
using OpenTracker.Models.BossPlacements;
using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.BossPlacements
{
    public class BossPlacementTests
    {
        [Theory]
        [MemberData(nameof(BossPlacementData))]
        public void Factory_Tests(
            BossPlacementID id, BossType? expectedDefaultBoss, BossType? expectedBoss)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacementFactory>();
            var bossPlacement = factory.GetBossPlacement(id);
                
            Assert.Equal(expectedDefaultBoss, bossPlacement.DefaultBoss);
            Assert.Equal(expectedBoss, bossPlacement.Boss);
        }

        [Theory]
        [MemberData(nameof(BossPlacementData))]
        public void Dictionary_Tests(
            BossPlacementID id, BossType? expectedDefaultBoss, BossType? expectedBoss)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var bossPlacementDictionary = scope.Resolve<IBossPlacementDictionary>();
            var bossPlacement = bossPlacementDictionary[id];

            Assert.Equal(expectedDefaultBoss, bossPlacement.DefaultBoss);
            Assert.Equal(expectedBoss, bossPlacement.Boss);
        }

        public static IEnumerable<object[]> BossPlacementData =>
            new List<object[]>
            {
                new object[]
                {
                    BossPlacementID.ATBoss,
                    BossType.Aga,
                    BossType.Aga
                },
                new object[]
                {
                    BossPlacementID.EPBoss,
                    BossType.Armos,
                    null
                },
                new object[]
                {
                    BossPlacementID.DPBoss,
                    BossType.Lanmolas,
                    null
                },
                new object[]
                {
                    BossPlacementID.ToHBoss,
                    BossType.Moldorm,
                    null
                },
                new object[]
                {
                    BossPlacementID.PoDBoss,
                    BossType.HelmasaurKing,
                    null
                },
                new object[]
                {
                    BossPlacementID.SPBoss,
                    BossType.Arrghus,
                    null
                },
                new object[]
                {
                    BossPlacementID.SWBoss,
                    BossType.Mothula,
                    null
                },
                new object[]
                {
                    BossPlacementID.TTBoss,
                    BossType.Blind,
                    null
                },
                new object[]
                {
                    BossPlacementID.IPBoss,
                    BossType.Kholdstare,
                    null
                },
                new object[]
                {
                    BossPlacementID.MMBoss,
                    BossType.Vitreous,
                    null
                },
                new object[]
                {
                    BossPlacementID.TRBoss,
                    BossType.Trinexx,
                    null
                },
                new object[]
                {
                    BossPlacementID.GTBoss1,
                    BossType.Armos,
                    null
                },
                new object[]
                {
                    BossPlacementID.GTBoss2,
                    BossType.Lanmolas,
                    null
                },
                new object[]
                {
                    BossPlacementID.GTBoss3,
                    BossType.Moldorm,
                    null
                },
                new object[]
                {
                    BossPlacementID.GTFinalBoss,
                    BossType.Aga,
                    BossType.Aga
                }
            };

        [Theory]
        [MemberData(nameof(GetCurrentBossData))]
        public void GetCurrentBoss_Tests(
            BossPlacementID id, BossType? expectedNoBossShuffle)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var bossPlacementDictionary = scope.Resolve<IBossPlacementDictionary>();
            var bossPlacement = bossPlacementDictionary[id];
            var mode = scope.Resolve<IMode>();
            
            Assert.Equal(expectedNoBossShuffle, bossPlacement.GetCurrentBoss());

            mode.BossShuffle = true;
            
            Assert.Null( bossPlacement.GetCurrentBoss());
        }

        public static IEnumerable<object[]> GetCurrentBossData =>
            new List<object[]>
            {
                new object[]
                {
                    BossPlacementID.EPBoss,
                    BossType.Armos
                },
                new object[]
                {
                    BossPlacementID.DPBoss,
                    BossType.Lanmolas
                },
                new object[]
                {
                    BossPlacementID.ToHBoss,
                    BossType.Moldorm
                },
                new object[]
                {
                    BossPlacementID.PoDBoss,
                    BossType.HelmasaurKing
                },
                new object[]
                {
                    BossPlacementID.SPBoss,
                    BossType.Arrghus
                },
                new object[]
                {
                    BossPlacementID.SWBoss,
                    BossType.Mothula
                },
                new object[]
                {
                    BossPlacementID.TTBoss,
                    BossType.Blind
                },
                new object[]
                {
                    BossPlacementID.IPBoss,
                    BossType.Kholdstare
                },
                new object[]
                {
                    BossPlacementID.MMBoss,
                    BossType.Vitreous
                },
                new object[]
                {
                    BossPlacementID.TRBoss,
                    BossType.Trinexx
                },
                new object[]
                {
                    BossPlacementID.GTBoss1,
                    BossType.Armos
                },
                new object[]
                {
                    BossPlacementID.GTBoss2,
                    BossType.Lanmolas
                },
                new object[]
                {
                    BossPlacementID.GTBoss3,
                    BossType.Moldorm
                }
            };

        [Fact]
        public void PropertyChanged_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacement.Factory>();
            var bossPlacement = factory(BossType.Test);
            
            Assert.PropertyChanged(
                bossPlacement, nameof(IBossPlacement.Boss),
                () => { bossPlacement.Boss = BossType.Armos; });
        }

        [Fact]
        public void Reset_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacement.Factory>();
            var bossPlacement = factory(BossType.Test);

            bossPlacement.Boss = BossType.Test;
            
            Assert.Equal(BossType.Test, bossPlacement.Boss);
            
            bossPlacement.Reset();
            
            Assert.Null(bossPlacement.Boss);
        }

        [Fact]
        public void Load_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacement.Factory>();
            var bossPlacement = factory(BossType.Test);

            var saveData = new BossPlacementSaveData()
            {
                Boss = BossType.Test
            };
            
            Assert.Null(bossPlacement.Boss);
            
            bossPlacement.Load(null);
            
            Assert.Null(bossPlacement.Boss);

            bossPlacement.Load(saveData);
            
            Assert.Equal(BossType.Test, bossPlacement.Boss);
        }

        [Fact]
        public void Save_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacement.Factory>();
            var bossPlacement = factory(BossType.Test);

            var saveData = bossPlacement.Save();
            
            Assert.Null(saveData.Boss);

            bossPlacement.Boss = BossType.Test;
            saveData = bossPlacement.Save();
            
            Assert.Equal(BossType.Test, saveData.Boss);
        }
    }
}
