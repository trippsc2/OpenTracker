using System;
using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.ViewModels.Dropdowns;
using OpenTracker.ViewModels.Dungeons;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.PinnedLocations;

namespace OpenTracker.ViewModels.UIPanels
{
    /// <summary>
    ///     This class contains the creation logic for UI panel controls.
    /// </summary>
    public class UIPanelFactory : IUIPanelFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        
        private readonly IModeSettingsVM _modeSettings;
        private readonly IDungeonPanelVM _dungeons;
        private readonly IPinnedLocationsPanelVM _pinnedLocations;
        private readonly IItemVMFactory _itemFactory;
        private readonly IDropdownVMFactory _dropdownFactory;
        
        private readonly IUIPanelVM.Factory _factory;
        private readonly ILargeItemPanelVM.Factory _largeFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alternativeRequirements">
        ///     The alternative requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="modeSettings">
        /// The mode settings control.
        /// </param>
        /// <param name="itemFactory">
        /// The factory for creating item controls.
        /// </param>
        /// <param name="dungeons">
        /// The dungeons panel body control.
        /// </param>
        /// <param name="pinnedLocations">
        /// The pinned locations panel body control.
        /// </param>
        /// <param name="dropdownFactory">
        /// The factory for creating dropdown controls.
        /// </param>
        /// <param name="factory">
        /// An Autofac factory for creating UI panel controls.
        /// </param>
        /// <param name="largeFactory">
        /// The items panel body control.
        /// </param>
        public UIPanelFactory(
            IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements, IModeSettingsVM modeSettings,
            IItemVMFactory itemFactory, IDungeonPanelVM dungeons, IPinnedLocationsPanelVM pinnedLocations,
            IDropdownVMFactory dropdownFactory, IUIPanelVM.Factory factory, ILargeItemPanelVM.Factory largeFactory)
        {
            _modeSettings = modeSettings;
            _dungeons = dungeons;
            _pinnedLocations = pinnedLocations;
            _itemFactory = itemFactory;
            _dropdownFactory = dropdownFactory;
            
            _factory = factory;
            _largeFactory = largeFactory;
            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
        }

        /// <summary>
        /// Returns the requirement for the specified UI panel to be visible.
        /// </summary>
        /// <param name="type">
        /// The UI panel type.
        /// </param>
        /// <returns>
        /// The requirement to be visible.
        /// </returns>
        private IRequirement? GetUIPanelRequirement(UIPanelType type)
        {
            return type switch
            {
                UIPanelType.Item => null,
                UIPanelType.Dungeon => null,
                UIPanelType.Dropdown => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _entranceShuffleRequirements[EntranceShuffle.All],
                    _entranceShuffleRequirements[EntranceShuffle.Insanity]
                }],
                UIPanelType.Location => null,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        /// <summary>
        /// Returns the title of the specified UI panel.
        /// </summary>
        /// <param name="type">
        /// The UI panel type.
        /// </param>
        /// <returns>
        /// A string representing the title of the specified UI panel.
        /// </returns>
        private static string GetUIPanelTitle(UIPanelType type)
        {
            return type switch
            {
                UIPanelType.Item => "Items",
                UIPanelType.Dungeon => "Dungeons",
                UIPanelType.Dropdown => "Dropdowns",
                UIPanelType.Location => "Locations",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        /// <summary>
        /// Returns whether to use the alternate body color for the specified UI panel.
        /// </summary>
        /// <param name="type">
        /// The UI panel type.
        /// </param>
        /// <returns>
        /// A boolean representing whether to use the alternate body color.
        /// </returns>
        private static bool GetUIPanelAlternateBodyColor(UIPanelType type)
        {
            return type switch
            {
                UIPanelType.Item => false,
                UIPanelType.Dungeon => true,
                UIPanelType.Dropdown => false,
                UIPanelType.Location => true,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        /// <summary>
        /// Returns the body content control for the specified UI panel.
        /// </summary>
        /// <param name="type">
        /// The UI panel type.
        /// </param>
        /// <returns>
        /// The body content control.
        /// </returns>
        private IUIPanelBodyVMBase GetUIPanelBodyContent(UIPanelType type)
        {
            return type switch
            {
                UIPanelType.Item => _largeFactory(_itemFactory.GetLargeItemControlVMs()),
                UIPanelType.Dungeon => _dungeons,
                UIPanelType.Dropdown => _largeFactory(_dropdownFactory.GetDropdownVMs()),
                UIPanelType.Location => _pinnedLocations,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        /// <summary>
        /// Returns a new UI panel control of the specified type.
        /// </summary>
        /// <param name="type">
        /// The UI panel type.
        /// </param>
        /// <returns>
        /// A new UI panel control.
        /// </returns>
        public IUIPanelVM GetUIPanelVM(UIPanelType type)
        {
            return _factory(
                GetUIPanelRequirement(type), GetUIPanelTitle(type), type == UIPanelType.Item ? _modeSettings : null,
                GetUIPanelAlternateBodyColor(type), GetUIPanelBodyContent(type));
        }
    }
}