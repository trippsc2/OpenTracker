using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This class contains the creation logic for dungeon sections.
    /// </summary>
    public class DungeonSectionFactory : IDungeonSectionFactory
    {
        private readonly IDungeonDictionary _dungeons;

        private readonly IDungeonAccessibilityProvider.Factory _accessibilityProviderFactory;

        private readonly ISectionAutoTrackingFactory _autoTrackingFactory;
        private readonly IBossSectionFactory _bossSectionFactory;
        private readonly IDungeonItemSectionFactory _dungeonItemSectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeons">
        ///     The <see cref="IDungeonDictionary"/>.
        /// </param>
        /// <param name="accessibilityProviderFactory">
        ///     An Autofac factory for creating new <see cref="IDungeonAccessibilityProvider"/> objects.
        /// </param>
        /// <param name="autoTrackingFactory">
        ///     The <see cref="ISectionAutoTrackingFactory"/>.
        /// </param>
        /// <param name="bossSectionFactory">
        ///     The <see cref="IBossSectionFactory"/>.
        /// </param>
        /// <param name="dungeonItemSectionFactory">
        ///     The <see cref="IDungeonItemSectionFactory"/>.
        /// </param>
        public DungeonSectionFactory(
            IDungeonDictionary dungeons, IDungeonAccessibilityProvider.Factory accessibilityProviderFactory,
            ISectionAutoTrackingFactory autoTrackingFactory, IBossSectionFactory bossSectionFactory,
            IDungeonItemSectionFactory dungeonItemSectionFactory)
        {
            _dungeons = dungeons;
            
            _accessibilityProviderFactory = accessibilityProviderFactory;
            
            _autoTrackingFactory = autoTrackingFactory;
            _bossSectionFactory = bossSectionFactory;
            _dungeonItemSectionFactory = dungeonItemSectionFactory;
        }

        public List<ISection> GetDungeonSections(LocationID id)
        {
            var dungeon = _dungeons[Enum.Parse<DungeonID>(id.ToString())];
            var accessibilityProvider = _accessibilityProviderFactory(dungeon);
            var dungeonItemSection = _dungeonItemSectionFactory.GetDungeonItemSection(
                dungeon, accessibilityProvider, _autoTrackingFactory.GetAutoTrackValue(id), id);
            
            switch (id)
            {
                case LocationID.HyruleCastle:
                    return new List<ISection> {dungeonItemSection};
                case LocationID.GanonsTower:
                    return new List<ISection>
                    {
                        dungeonItemSection,
                        _bossSectionFactory.GetBossSection(
                            accessibilityProvider.BossAccessibilityProviders[0],
                            _autoTrackingFactory.GetAutoTrackValue(id, 1), id),
                        _bossSectionFactory.GetBossSection(
                            accessibilityProvider.BossAccessibilityProviders[1],
                            _autoTrackingFactory.GetAutoTrackValue(id, 2), id, 2),
                        _bossSectionFactory.GetBossSection(
                            accessibilityProvider.BossAccessibilityProviders[2],
                            _autoTrackingFactory.GetAutoTrackValue(id, 3), id, 3),
                        _bossSectionFactory.GetBossSection(
                            accessibilityProvider.BossAccessibilityProviders[3],
                            _autoTrackingFactory.GetAutoTrackValue(id, 4), id, 4)
                    };
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.DesertPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                    return new List<ISection>
                    {
                        dungeonItemSection,
                        _bossSectionFactory.GetBossSection(
                            accessibilityProvider.BossAccessibilityProviders[0],
                            _autoTrackingFactory.GetAutoTrackValue(id, 1), id),
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}