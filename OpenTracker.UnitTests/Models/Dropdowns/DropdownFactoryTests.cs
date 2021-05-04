using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns
{
    public class DropdownFactoryTests
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements =
            Substitute.For<IAggregateRequirementDictionary>();
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements =
            Substitute.For<IEntranceShuffleRequirementDictionary>();

        private IRequirement? _requirement;

        private readonly DropdownFactory _sut;

        public DropdownFactoryTests()
        {
            var entranceAllInsanityRequirement = Substitute.For<IRequirement>();
            var entranceAllRequirement = Substitute.For<IRequirement>();
            var entranceInsanityRequirement = Substitute.For<IRequirement>();

            _entranceShuffleRequirements[EntranceShuffle.All].Returns(entranceAllRequirement);
            _entranceShuffleRequirements[EntranceShuffle.Insanity].Returns(entranceInsanityRequirement);
            
            _aggregateRequirements[Arg.Any<HashSet<IRequirement>>()].Returns(entranceAllInsanityRequirement);
            
            IDropdown Factory(IRequirement requirement)
            {
                _requirement = requirement;
                return Substitute.For<IDropdown>();
            }

            _sut = new DropdownFactory(_aggregateRequirements, _entranceShuffleRequirements, Factory);
        }

        [Theory]
        [InlineData(EntranceShuffle.All, DropdownID.LumberjackCave)]
        [InlineData(EntranceShuffle.All, DropdownID.ForestHideout)]
        [InlineData(EntranceShuffle.All, DropdownID.CastleSecret)]
        [InlineData(EntranceShuffle.All, DropdownID.TheWell)]
        [InlineData(EntranceShuffle.All, DropdownID.MagicBat)]
        [InlineData(EntranceShuffle.All, DropdownID.SanctuaryGrave)]
        [InlineData(EntranceShuffle.All, DropdownID.HoulihanHole)]
        [InlineData(EntranceShuffle.All, DropdownID.GanonHole)]
        [InlineData(EntranceShuffle.Insanity, DropdownID.SWNEHole)]
        [InlineData(EntranceShuffle.Insanity, DropdownID.SWNWHole)]
        [InlineData(EntranceShuffle.Insanity, DropdownID.SWSEHole)]
        [InlineData(EntranceShuffle.Insanity, DropdownID.SWSWHole)]
        public void GetDropdown_ShouldSetRequirementToExpected(EntranceShuffle minimumEntranceShuffle, DropdownID id)
        {
            _ = _sut.GetDropdown(id);

            var expected = minimumEntranceShuffle switch
            {
                EntranceShuffle.All => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _entranceShuffleRequirements[EntranceShuffle.All],
                    _entranceShuffleRequirements[EntranceShuffle.Insanity]
                }],
                EntranceShuffle.Insanity =>  _entranceShuffleRequirements[EntranceShuffle.Insanity],
                _ => throw new ArgumentOutOfRangeException(nameof(minimumEntranceShuffle), minimumEntranceShuffle, null)
            };
            
            Assert.Equal(expected, _requirement);
        }

        [Fact]
        public void GetDropdown_ShouldThrowException_WhenIDIsOutsideExpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _sut.GetDropdown((DropdownID)int.MaxValue));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDropdownFactory.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as DropdownFactory);
        }

        [Fact]
        public void AutofacSingleInstanceTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDropdownFactory.Factory>();
            var value1 = factory();
            var value2 = factory();
            
            Assert.Equal(value1, value2);
        }
    }
}