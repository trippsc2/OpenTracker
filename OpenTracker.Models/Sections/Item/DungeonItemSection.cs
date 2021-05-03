using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Item
{
    /// <summary>
    ///     This class contains dungeon item section data.
    /// </summary>
    public class DungeonItemSection : ItemSectionBase, IDungeonItemSection
    {
        private readonly IDungeon _dungeon;
        private readonly IDungeonAccessibilityProvider _accessibilityProvider;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The save/load manager.
        /// </param>
        /// <param name="collectSectionFactory">
        ///     An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        ///     An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The dungeon accessibility provider.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The section auto track value.
        /// </param>
        /// <param name="marking">
        ///     The section marking.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this section to be visible.
        /// </param>
        public DungeonItemSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, IDungeon dungeon,
            IDungeonAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue = null,
            IMarking? marking = null, IRequirement? requirement = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, "Dungeon", autoTrackValue,
                marking, requirement)
        {
            _dungeon = dungeon;
            _accessibilityProvider = accessibilityProvider;

            _dungeon.PropertyChanged += OnDungeonChanged;
            _accessibilityProvider.PropertyChanged += OnAccessibilityProviderChanged;
            
            UpdateTotal();
            UpdateAccessibility();
        }

        public override void Clear(bool force)
        {
            if (force)
            {
                Available = 0;
                return;
            }

            Available -= Accessible;
        }

        protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Available))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IDungeon interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnDungeonChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IDungeon.Total))
            {
                UpdateTotal();
            }
        }
        
        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IDungeonAccessibilityProvider interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnAccessibilityProviderChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IDungeonAccessibilityProvider.Accessible):
                case nameof(IDungeonAccessibilityProvider.SequenceBreak):
                case nameof(IDungeonAccessibilityProvider.Visible):
                    UpdateAccessibility();
                    break;
            }
        }

        /// <summary>
        ///     Updates value of the Total property.
        /// </summary>
        private void UpdateTotal()
        {
            var newTotal = _dungeon.Total;
            var delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));
        }

        /// <summary>
        ///     Updates values of the Accessible and Accessibility properties.
        /// </summary>
        private void UpdateAccessibility()
        {
            var unavailable = Total - Available;
            var accessible = _accessibilityProvider.Accessible - unavailable;
            
            Accessible = Math.Max(0, accessible);

            if (accessible >= Available)
            {
                Accessibility = _accessibilityProvider.SequenceBreak
                    ? AccessibilityLevel.SequenceBreak
                    : AccessibilityLevel.Normal;
                return;
            }

            if (accessible > 0)
            {
                Accessibility = AccessibilityLevel.Partial;
                return;
            }
            
            if (unavailable == _accessibilityProvider.Accessible && _accessibilityProvider.Visible)
            {
                Accessibility = AccessibilityLevel.Inspect;
                return;
            }

            Accessibility = AccessibilityLevel.None;
        }
    }
}