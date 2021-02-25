using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains dungeon item section data.
    /// </summary>
    public class DungeonItemSection : IDungeonItemSection
    {
        private readonly IMode _mode;
        private readonly LocationID _locationID;
        private IDungeon? _dungeon;
        private readonly IAutoTrackValue? _autoTrackValue;

        public string Name =>
            "Dungeon";

        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        private int _available;
        public int Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            set
            {
                if (_accessible != value)
                {
                    _accessible = value;
                    OnPropertyChanged(nameof(Accessible));
                }
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
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
            ILocationDictionary locations, IMode mode, LocationID locationID,
            IAutoTrackValue? autoTrackValue, IRequirement requirement)
        {
            _mode = mode;
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
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            if (e.Key == _locationID)
            {
                ((ILocationDictionary)sender!).ItemCreated -= OnLocationCreated;

                _dungeon = (IDungeon)e.Value;

                SetTotal();
            }
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
            if (e.PropertyName == nameof(IMode.WorldState) ||
                e.PropertyName == nameof(IMode.MapShuffle) ||
                e.PropertyName == nameof(IMode.CompassShuffle) ||
                e.PropertyName == nameof(IMode.SmallKeyShuffle) ||
                e.PropertyName == nameof(IMode.BigKeyShuffle) ||
                e.PropertyName == nameof(IMode.KeyDropShuffle))
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
        /// Updates the value of the section from autotracking.
        /// </summary>
        private void AutoTrackUpdate()
        {
            if (_autoTrackValue!.CurrentValue.HasValue)
            {
                if (Available != Total - _autoTrackValue.CurrentValue.Value)
                {
                    Available = Total - _autoTrackValue.CurrentValue.Value;
                    //SaveLoadManager.Instance.Unsaved = true;
                }
            }
        }

        /// <summary>
        /// Sets the total based on whether dungeon items are shuffled.
        /// </summary>
        /// <param name="updateAccessibility">
        /// A boolean representing whether to call the UpdateAccessibiliy method at the end.
        /// </param>
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
            }
            else
            {
                Available -= Accessible;
            }
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
        public void Load(SectionSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Available = saveData.Available;
            UserManipulated = saveData.UserManipulated;
        }
    }
}

