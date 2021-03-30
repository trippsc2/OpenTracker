using Autofac;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns
{
    public class DropdownFactoryTests
    {
        private readonly IRequirementDictionary _requirements = Substitute.For<IRequirementDictionary>();
        private IRequirement? _requirement;

        private readonly DropdownFactory _sut;

        public DropdownFactoryTests()
        {
            var entranceAllInsanityRequirement = Substitute.For<IRequirement>();
            var entranceInsanityRequirement = Substitute.For<IRequirement>();
            _requirements[RequirementType.EntranceShuffleAllInsanity].Returns(entranceAllInsanityRequirement);
            _requirements[RequirementType.EntranceShuffleInsanity].Returns(entranceInsanityRequirement);

            IDropdown Factory(IRequirement requirement)
            {
                _requirement = requirement;
                return Substitute.For<IDropdown>();
            }

            _sut = new DropdownFactory(_requirements, Factory);
        }

        [Theory]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.LumberjackCave)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.ForestHideout)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.CastleSecret)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.TheWell)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.MagicBat)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.SanctuaryGrave)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.HoulihanHole)]
        [InlineData(RequirementType.EntranceShuffleAllInsanity, DropdownID.GanonHole)]
        [InlineData(RequirementType.EntranceShuffleInsanity, DropdownID.SWNEHole)]
        [InlineData(RequirementType.EntranceShuffleInsanity, DropdownID.SWNWHole)]
        [InlineData(RequirementType.EntranceShuffleInsanity, DropdownID.SWSEHole)]
        [InlineData(RequirementType.EntranceShuffleInsanity, DropdownID.SWSWHole)]
        public void GetDropdown_ShouldSetRequirementToExpected(RequirementType expected, DropdownID id)
        {
            _ = _sut.GetDropdown(id);
            
            Assert.Equal(_requirements[expected], _requirement);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDropdownFactory.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as DropdownFactory);
        }
    }
}