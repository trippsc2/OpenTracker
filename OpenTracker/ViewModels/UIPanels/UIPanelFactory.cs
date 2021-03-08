using OpenTracker.ViewModels.Items;
using System;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Dropdowns;
using OpenTracker.ViewModels.PinnedLocations;

namespace OpenTracker.ViewModels.UIPanels
{
    /// <summary>
    /// This class contains the creation logic for UI panel controls.
    /// </summary>
    public class UIPanelFactory : IUIPanelFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IModeSettingsVM _modeSettings;
        private readonly IItemsPanelVM _items;
        private readonly IDropdownPanelVM _dropdowns;
        private readonly IPinnedLocationsPanelVM _pinnedLocations;
        
        private readonly IUIPanelVM.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="modeSettings">
        /// The mode settings control.
        /// </param>
        /// <param name="items">
        /// The items panel body control.
        /// </param>
        /// <param name="dropdowns">
        /// The dropdowns panel body control.
        /// </param>
        /// <param name="pinnedLocations">
        /// The pinned locations panel body control.
        /// </param>
        /// <param name="factory">
        /// An Autofac factory for creating UI panel controls.
        /// </param>
        public UIPanelFactory(
            IRequirementDictionary requirements, IModeSettingsVM modeSettings, IItemsPanelVM items,
            IDropdownPanelVM dropdowns, IPinnedLocationsPanelVM pinnedLocations, IUIPanelVM.Factory factory)
        {
            _requirements = requirements;
            _modeSettings = modeSettings;
            _items = items;
            _dropdowns = dropdowns;
            _pinnedLocations = pinnedLocations;
            
            _factory = factory;
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
                UIPanelType.Dropdown => true,
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
                UIPanelType.Item => _items,
                UIPanelType.Dropdown => _dropdowns,
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