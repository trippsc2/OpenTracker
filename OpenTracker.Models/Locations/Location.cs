using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This is the class containing location data.
    /// </summary>
    public class Location : ILocation
    {
        public LocationID ID { get; }
        public string Name { get; }

        public List<MapLocation> MapLocations { get; }
        public List<ISection> Sections { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
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
            private set
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
            private set
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">
        /// The ID of the location.
        /// </param>
        /// <param name="name">
        /// A string representing the name of the location.
        /// </param>
        /// <param name="mapLocations">
        /// The list of map locations for the location.
        /// </param>
        public Location(LocationID id, string name, List<MapLocation> mapLocations)
        {
            ID = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MapLocations = mapLocations ?? throw new ArgumentNullException(nameof(mapLocations));
            Sections = SectionFactory.GetSections(id, this);

            LocationDictionary.Instance.LocationCreated += OnLocationCreated;
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
        /// Subscribes to the PropertyChanged event on the ILocation class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLocationCreated(object sender, LocationID id)
        {
            if (id == ID)
            {
                foreach (ISection section in Sections)
                {
                    section.PropertyChanged += OnSectionChanged;
                }

                UpdateAccessibility();
                UpdateAccessible();
                UpdateAvailable();
                UpdateTotal();

                LocationDictionary.Instance.LocationCreated -= OnLocationCreated;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                UpdateAccessibility();
                UpdateAvailable();
            }

            if (e.PropertyName == nameof(ISection.Accessibility))
                UpdateAccessibility();

            if (e.PropertyName == nameof(ItemSection.Accessible) ||
                e.PropertyName == nameof(DungeonItemSection.Accessible))
                UpdateAccessible();

            if (e.PropertyName == nameof(ItemSection.Total) ||
                e.PropertyName == nameof(DungeonItemSection.Total))
                UpdateTotal();
        }

        /// <summary>
        /// Updates the accessibility of the location.
        /// </summary>
        private void UpdateAccessibility()
        {
            AccessibilityLevel? leastAccessible = null;
            AccessibilityLevel? mostAccessible = null;

            bool available = false;

            foreach (ISection section in Sections)
            {
                if (section.IsAvailable() && section.Requirement.Met)
                {
                    available = true;
                    AccessibilityLevel sectionAccessibility = section.Accessibility;

                    if (leastAccessible == null || leastAccessible > sectionAccessibility)
                    {
                        leastAccessible = sectionAccessibility;
                    }

                    if (mostAccessible == null || mostAccessible < sectionAccessibility)
                    {
                        mostAccessible = sectionAccessibility;
                    }
                }
            }

            if (!available)
            {
                Accessibility = AccessibilityLevel.Cleared;
            }
            else
            {
                Accessibility = mostAccessible.Value switch
                {
                    AccessibilityLevel.None => AccessibilityLevel.None,
                    AccessibilityLevel.Inspect => AccessibilityLevel.Inspect,
                    AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.SequenceBreak when leastAccessible.Value <= AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal when leastAccessible.Value <= AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.Normal when leastAccessible.Value == AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal => AccessibilityLevel.Normal,
                    _ => throw new Exception(string.Format(CultureInfo.InvariantCulture, "Unknown availability state for location {0}", ID.ToString())),
                };
            }
        }

        /// <summary>
        /// Updates the available count of the location.
        /// </summary>
        private void UpdateAvailable()
        {
            int available = 0;

            foreach (ISection section in Sections)
            {
                if (section is IItemSection itemSection)
                {
                    available += itemSection.Available;
                }
            }

            Available = available;
        }

        /// <summary>
        /// Updates the accessible count of the location.
        /// </summary>
        private void UpdateAccessible()
        {
            int accessible = 0;

            foreach (ISection section in Sections)
            {
                if (section is IItemSection itemSection)
                {
                    accessible += itemSection.Accessible;
                }
            }

            Accessible = accessible;
        }

        /// <summary>
        /// Updates the total count of the location.
        /// </summary>
        private void UpdateTotal()
        {
            int total = 0;

            foreach (ISection section in Sections)
            {
                if (section is IItemSection itemSection)
                {
                    total += itemSection.Total;
                }
            }

            Total = total;
        }

        /// <summary>
        /// Returns whether the location can be cleared.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the location can be cleared.
        /// </returns>
        public bool CanBeCleared(bool force)
        {
            foreach (var section in Sections)
            {
                if (section.CanBeCleared(force))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Resets the location to its starting values.
        /// </summary>
        public void Reset()
        {
            foreach (ISection section in Sections)
            {
                section.Reset();
            }
        }

        /// <summary>
        /// Returns a new location save data instance for this location.
        /// </summary>
        /// <returns>
        /// A new location save data instance.
        /// </returns>
        public LocationSaveData Save()
        {
            List<SectionSaveData> sections = new List<SectionSaveData>();

            foreach (var section in Sections)
            {
                sections.Add(section.Save());
            }

            return new LocationSaveData()
            {
                Sections = sections
            };
        }

        /// <summary>
        /// Loads location save data.
        /// </summary>
        public void Load(LocationSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            for (int i = 0; i < saveData.Sections.Count; i++)
            {
                Sections[i].Load(saveData.Sections[i]);
            }
        }
    }
}
