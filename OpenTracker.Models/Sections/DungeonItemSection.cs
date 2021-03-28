using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains dungeon item section data.
    /// </summary>
    public class DungeonItemSection : ReactiveObject, IDungeonItemSection
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;

        private readonly IAutoTrackValue? _autoTrackValue;
        private readonly IDungeon _dungeon;
        private readonly IDungeonAccessibilityProvider _accessibilityProvider;

        public string Name => "Dungeon";

        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private int _available;
        public int Available
        {
            get => _available;
            set => this.RaiseAndSetIfChanged(ref _available, value);
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            set => this.RaiseAndSetIfChanged(ref _accessible, value);
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set => this.RaiseAndSetIfChanged(ref _total, value);
        }

        public delegate DungeonItemSection Factory(
            IAutoTrackValue? autoTrackValue, IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider, 
            IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="collectSectionFactory">
        /// An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        /// An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon data.
        /// </param>
        /// <param name="accessibilityProvider">
        /// The dungeon accessibility provider.
        /// </param>
        /// <param name="autoTrackValue">
        /// The section auto track value.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public DungeonItemSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, IAutoTrackValue? autoTrackValue, IDungeon dungeon,
            IDungeonAccessibilityProvider accessibilityProvider, IRequirement requirement)
        {
            _saveLoadManager = saveLoadManager;

            _collectSectionFactory = collectSectionFactory;
            _uncollectSectionFactory = uncollectSectionFactory;
            
            _autoTrackValue = autoTrackValue;
            _accessibilityProvider = accessibilityProvider;
            _dungeon = dungeon;
            
            Requirement = requirement;

            PropertyChanged += OnPropertyChanged;
            _accessibilityProvider.PropertyChanged += OnAccessibilityProviderChanged;
            _dungeon.PropertyChanged += OnDungeonChanged;

            if (_autoTrackValue is null)
            {
                return;
            }
            
            _autoTrackValue.PropertyChanged += OnAutoTrackValueChanged;
            
            UpdateTotal();
            UpdateAccessibility();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Available))
            {
                UpdateAccessibility();
            }
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on the IDungeonAccessibilityProvider interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
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
        /// Subscribes to the PropertyChanged event on the IDungeon interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnDungeonChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IDungeon.Total))
            {
                UpdateTotal();
            }
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackValueChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        /// <summary>
        /// Returns whether the section can be cleared or collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be cleared or collected.
        /// </returns>
        public bool CanBeCleared(bool force)
        {
            return IsAvailable() && (force || Accessibility > AccessibilityLevel.Inspect);
        }

        /// <summary>
        /// Returns whether the section can be uncollected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be uncollected.
        /// </returns>
        public bool CanBeUncleared()
        {
            return Available < Total;
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            if (force)
            {
                Available = 0;
                return;
            }

            Available -= Accessible;
        }

        /// <summary>
        /// Returns whether the location has not been fully collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section has been fully collected.
        /// </returns>
        public bool IsAvailable()
        {
            return Available > 0;
        }

        /// <summary>
        /// Creates an undoable action to collect the section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether to override the logic while collecting the section.
        /// </param>
        public IUndoable CreateCollectSectionAction(bool force)
        {
            return _collectSectionFactory(this, force);
        }

        /// <summary>
        /// Creates an undoable action to uncollect the section and sends it to the undo/redo manager.
        /// </summary>
        public IUndoable CreateUncollectSectionAction()
        {
            return _uncollectSectionFactory(this);
        }

        /// <summary>
        /// Resets the section to its starting values.
        /// </summary>
        public void Reset()
        {
            Available = Total;
            UserManipulated = false;
        }

        /// <summary>
        /// Returns a new section save data instance for this section.
        /// </summary>
        /// <returns>
        /// A new section save data instance.
        /// </returns>
        public SectionSaveData Save()
        {
            return new()
            {
                Available = Available,
                UserManipulated = UserManipulated
            };
        }

        /// <summary>
        /// Loads section save data.
        /// </summary>
        public void Load(SectionSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            Available = saveData.Available;
            UserManipulated = saveData.UserManipulated;
        }

        /// <summary>
        /// Updates the value of the section from auto-tracking.
        /// </summary>
        private void AutoTrackUpdate()
        {
            if (_autoTrackValue!.CurrentValue.HasValue)
            {
                if (Available != Total - _autoTrackValue.CurrentValue.Value)
                {
                    Available = Total - _autoTrackValue.CurrentValue.Value;
                    _saveLoadManager.Unsaved = true;
                }
            }
        }

        /// <summary>
        /// Updates the Total property.
        /// </summary>
        private void UpdateTotal()
        {
            var newTotal = _dungeon.Total;
            var delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));
        }

        /// <summary>
        /// Updates the Accessible and Accessibility properties.
        /// </summary>
        private void UpdateAccessibility()
        {
            var unavailable = Total - Available;
            
            Accessible = _accessibilityProvider.Accessible - unavailable;

            if (Accessible >= Available)
            {
                Accessibility = _accessibilityProvider.SequenceBreak
                    ? AccessibilityLevel.SequenceBreak
                    : AccessibilityLevel.Normal;
                return;
            }

            if (Accessible > 0)
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

