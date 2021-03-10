using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Dropdowns;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.PinnedLocations;
using System;
using OpenTracker.ViewModels.Dungeons;

namespace OpenTracker.ViewModels.UIPanels
{
    /// <summary>
    /// This class contains the creation logic for UI panel controls.
    /// </summary>
    public class UIPanelFactory : IUIPanelFactory
    {
        private readonly IRequirementDictionary _requirements;
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
        /// <param name="requirements">
        /// The requirement dictionary.
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
            IRequirementDictionary requirements, IModeSettingsVM modeSettings, IItemVMFactory itemFactory,
            IDungeonPanelVM dungeons, IPinnedLocationsPanelVM pinnedLocations, IDropdownVMFactory dropdownFactory,
            IUIPanelVM.Factory factory, ILargeItemPanelVM.Factory largeFactory)
        {
            _requirements = requirements;
            _modeSettings = modeSettings;
            _dungeons = dungeons;
            _pinnedLocations = pinnedLocations;
            _itemFactory = itemFactory;
            _dropdownFactory = dropdownFactory;
            
            _factory = factory;
            _largeFactory = largeFactory;
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
        private IRequirement GetUIPanelRequirement(UIPanelType type)
        {
            return type switch
            {
                UIPanelType.Item => _requirements[RequirementType.NoRequirement],
                UIPanelType.Dungeon => _requirements[RequirementType.NoRequirement],
                UIPanelType.Dropdown => _requirements[RequirementType.EntranceShuffleAllInsanity],
                UIPanelType.Location => _requirements[RequirementType.NoRequirement],
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