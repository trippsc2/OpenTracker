using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.UndoRedo.Prize;
using Xunit;

namespace OpenTracker.UnitTests.Models.PrizePlacements
{
    public class PrizePlacementFactoryTests
    {
        private static readonly IPrizeDictionary Prizes =
            new PrizeDictionary((_, _) => Substitute.For<IItem>());
        private static readonly IPrizePlacement.Factory Factory =
            startingPrize => new PrizePlacement(
                Prizes, _ => Substitute.For<IChangePrize>(), startingPrize);

        private static readonly Dictionary<PrizePlacementID, ExpectedObject> ExpectedValues = new();

        private readonly PrizePlacementFactory _sut = new(Prizes, Factory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (PrizePlacementID id in Enum.GetValues(typeof(PrizePlacementID)))
            {
                var expected = (id switch
                {
                    PrizePlacementID.ATPrize => Factory(Prizes[PrizeType.Aga1]),
                    PrizePlacementID.GTPrize => Factory(Prizes[PrizeType.Aga2]),
                    _ => Factory()
                }).ToExpectedObject();
                
                ExpectedValues.Add(id, expected);
            }
        }

        [Theory]
        [MemberData(nameof(GetPrizePlacement_ShouldReturnExpectedData))]
        public void GetPrizePlacement_ShouldReturnExpected(ExpectedObject expected, PrizePlacementID id)
        {
            var prizePlacement = _sut.GetPrizePlacement(id);
            
            expected.ShouldEqual(prizePlacement);
        }

        public static IEnumerable<object[]> GetPrizePlacement_ShouldReturnExpectedData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IPrizePlacementFactory.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as PrizePlacementFactory);
        }
    }
}