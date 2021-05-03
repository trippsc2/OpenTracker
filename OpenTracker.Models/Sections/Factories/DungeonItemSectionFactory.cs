using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This class contains the creation logic for dungeon item sections.
    /// </summary>
    public class DungeonItemSectionFactory : IDungeonItemSectionFactory
    {
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        
        private readonly IMarking.Factory _markingFactory;
        
        private readonly IDungeonItemSection.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="smallKeyShuffleRequirements">
        ///     The small key shuffle requirement dictionary.
        /// </param>
        /// <param name="markingFactory">
        ///     An Autofac factory for creating new markings.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new dungeon item sections.
        /// </param>
        public DungeonItemSectionFactory(
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IMarking.Factory markingFactory,
            IDungeonItemSection.Factory factory)
        {
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;
            _markingFactory = markingFactory;
            _factory = factory;
        }

        /// <summary>
        ///     Returns a new dungeon item section for the specified location ID.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The accessibility provider.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     A new dungeon item section.
        /// </returns>
        public IDungeonItemSection GetDungeonItemSection(
            IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue,
            LocationID id)
        {
            return _factory(dungeon, accessibilityProvider, autoTrackValue, GetMarking(id), GetRequirement(id));
        }

        /// <summary>
        ///     Returns the marking for the section.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The marking for the section.
        /// </returns>
        private IMarking? GetMarking(LocationID id)
        {
            return id is LocationID.DesertPalace or LocationID.GanonsTower
                ? _markingFactory() : null;
        }

        /// <summary>
        ///     Returns the requirement for the section to be active.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The requirement for the section to be active.
        /// </returns>
        private IRequirement? GetRequirement(LocationID id)
        {
            return id == LocationID.AgahnimTower ? _smallKeyShuffleRequirements[true] : null;
        }
    }
}