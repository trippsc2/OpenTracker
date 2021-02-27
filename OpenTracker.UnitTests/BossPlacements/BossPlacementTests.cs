using Autofac;
using Autofac.Extras.Moq;
using OpenTracker.Models.BossPlacements;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.BossPlacements
{
    [Collection("Tests")]
    public class BossPlacementTests : TestBase
    {
        [Theory]
        [MemberData(nameof(BossPlacementData))]
        public void Factory_Tests(
            BossPlacementID id, BossType? expectedDefaultBoss, BossType? expectedBoss)
        {
            var container = Configure();

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
            var container = Configure();

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

        [Fact]
        public void PropertyChanged_Tests()
        {
            var container = Configure();

            using var scope = container.BeginLifetimeScope();
            var factory = scope.Resolve<IBossPlacement.Factory>();
            var bossPlacement = factory(BossType.Test);
            
            Assert.PropertyChanged(
                bossPlacement, nameof(IBossPlacement.Boss),
                () => { bossPlacement.Boss = BossType.Armos; });
        }
    }
}
