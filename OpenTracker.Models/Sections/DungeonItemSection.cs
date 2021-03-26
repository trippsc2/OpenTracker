using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
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
        private readonly IMode _mode;
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;
        
        private readonly LocationID _locationID;
        private IDungeon? _dungeon;
        private readonly IAutoTrackValue? _autoTrackValue;

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
            LocationID locationID, IAutoTrackValue? autoTrackValue, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="locations">
        /// The location dictionary.
        /// </param>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="collectSectionFactory">
        /// An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        /// An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="locationID">
        /// The ID of the dungeon to which this section belongs.
        /// </param>
        /// <param name="autoTrackValue">
        /// The section auto track value.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public DungeonItemSection(
            ILocationDictionary locations, IMode mode, ISaveLoadManager saveLoadManager,
            ICollectSection.Factory collectSectionFactory, IUncollectSection.Factory uncollectSectionFactory,
            LocationID locationID, IAutoTrackValue? autoTrackValue, IRequirement requirement)
        {
            _mode = mode;
            _saveLoadManager = saveLoadManager;

            _collectSectionFactory = collectSectionFactory;
            _uncollectSectionFactory = uncollectSectionFactory;
            
            _locationID = locationID;
            _autoTrackValue = autoTrackValue;
            Requirement = requirement;

            _mode.PropertyChanged += OnModeChanged;
            locations.ItemCreated += OnLocationCreated;

            if (_autoTrackValue != null)
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackValueChanged;
            }
        }
        
        /// <summary>
        /// Subscribes to the ItemCreated event on the ILocationDictionary interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the ItemCreated event.
        /// </param>
        private void OnLocationCreated(object? sender, KeyValuePair<LocationID, ILocation> e)
        {
            if (e.Key != _locationID)
            {
                return;
            }
            
            ((ILocationDictionary)sender!).ItemCreated -= OnLocationCreated;

            _dungeon = (IDungeon)e.Value;

            SetTotal();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.WorldState) || e.PropertyName == nameof(IMode.MapShuffle) ||
                e.PropertyName == nameof(IMode.CompassShuffle) || e.PropertyName == nameof(IMode.SmallKeyShuffle) ||
                e.PropertyName == nameof(IMode.BigKeyShuffle) || e.PropertyName == nameof(IMode.KeyDropShuffle))
            {
                SetTotal();
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
            return new SectionSaveData()
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
        /// Sets the total based on whether dungeon items are shuffled.
        /// </summary>
        private void SetTotal()
        {
            if (_dungeon == null)
            {
                return;
            }

            int baseTotal = _dungeon.DungeonItems.Count;

            if (_mode.KeyDropShuffle)
            {
                baseTotal += _dungeon.SmallKeyDrops.Count + _dungeon.BigKeyDrops.Count;
            }

            int dungeonItems = 0;

            if (!_mode.MapShuffle)
            {
                dungeonItems += _dungeon.Map;
            }

            if (!_mode.CompassShuffle)
            {
                dungeonItems += _dungeon.Compass;
            }

            if (!_mode.SmallKeyShuffle)
            {
                dungeonItems += _dungeon.SmallKeys;
                
                if (_mode.KeyDropShuffle)
                {
                    dungeonItems += _dungeon.SmallKeyDrops.Count;
                }
            }

            if (!_mode.BigKeyShuffle)
            {
                dungeonItems += _dungeon.BigKey;

                if (_mode.KeyDropShuffle)
                {
                    dungeonItems += _dungeon.BigKeyDrops.Count;
                }
            }

            int newTotal = baseTotal - dungeonItems;
            int delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));
        }
    }
}

