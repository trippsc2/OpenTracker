using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains location data.
    /// </summary>
    public class Location : ILocation
    {
        private readonly IMarking.Factory _markingFactory;

        public LocationID ID { get; }
        public string Name { get; }

        public List<IMapLocation> MapLocations { get; }
        public List<ISection> Sections { get; }
        public ILocationNoteCollection Notes { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility == value)
                {
                    return;
                }
                
                _accessibility = value;
                OnPropertyChanged(nameof(Accessibility));
                UpdateVisible();
            }
        }

        private int _available;
        public int Available
        {
            get => _available;
            private set
            {
                if (_available == value)
                {
                    return;
                }
                
                _available = value;
                OnPropertyChanged(nameof(Available));
            }
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set
            {
                if (_accessible == value)
                {
                    return;
                }
                
                _accessible = value;
                OnPropertyChanged(nameof(Accessible));
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set
            {
                if (_total == value)
                {
                    return;
                }
                
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            private set
            {
                if (_visible == value)
                {
                    return;
                }

                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The location factory.
        /// </param>
        /// <param name="mapLocationFactory">
        /// The map location factory.
        /// </param>
        /// <param name="sectionFactory">
        /// The section factory.
        /// </param>
        /// <param name="markingFactory">
        /// The marking factory.
        /// </param>
        /// <param name="notes">
        /// A new collection of location notes.
        /// </param>
        /// <param name="id">
        /// The ID of the location.
        /// </param>
        public Location(ILocationFactory factory, IMapLocationFactory mapLocationFactory,
            ISectionFactory sectionFactory, IMarking.Factory markingFactory, ILocationNoteCollection notes,
            LocationID id)
        {
            _markingFactory = markingFactory;

            ID = id;
            Name = factory.GetLocationName(ID);
            MapLocations = mapLocationFactory.GetMapLocations(this);
            Sections = sectionFactory.GetSections(ID);
            Notes = notes;

            foreach (ISection section in Sections)
            {
                section.PropertyChanged += OnSectionChanged;
            }

            UpdateAccessibility();
            UpdateAccessible();
            UpdateAvailable();
            UpdateTotal();
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
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
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
            AccessibilityLevel leastAccessible = AccessibilityLevel.Normal;
            AccessibilityLevel mostAccessible = AccessibilityLevel.None;

            bool available = false;

            foreach (ISection section in Sections)
            {
                if (section.IsAvailable() && section.Requirement.Met)
                {
                    available = true;
                    AccessibilityLevel sectionAccessibility = section.Accessibility;

                    if (leastAccessible > sectionAccessibility)
                    {
                        leastAccessible = sectionAccessibility;
                    }

                    if (mostAccessible < sectionAccessibility)
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
                Accessibility = mostAccessible switch
                {
                    AccessibilityLevel.None => AccessibilityLevel.None,
                    AccessibilityLevel.Inspect => AccessibilityLevel.Inspect,
                    AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.SequenceBreak when leastAccessible <= AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal when leastAccessible <= AccessibilityLevel.Partial => AccessibilityLevel.Partial,
                    AccessibilityLevel.Normal when leastAccessible == AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
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
        /// Updates whether the location is currently visible.
        /// </summary>
        private void UpdateVisible()
        {
            Visible = Accessibility switch
            {
                AccessibilityLevel.None => Sections[0] is IEntranceSection,
                AccessibilityLevel.Cleared => false,
                _ => true
            };
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
            List<MarkType?> markings = new List<MarkType?>();

            foreach (var section in Sections)
            {
                sections.Add(section.Save());
            }

            foreach (var marking in Notes)
            {
                markings.Add(marking.Mark);
            }

            return new LocationSaveData()
            {
                Sections = sections,
                Markings = markings
            };
        }

        /// <summary>
        /// Loads location save data.
        /// </summary>
        public void Load(LocationSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            Notes.Clear();

            for (var i = 0; i < saveData.Sections!.Count; i++)
            {
                Sections[i].Load(saveData.Sections[i]);
            }

            foreach (var marking in saveData.Markings!)
            {
                var newMarking = _markingFactory();

                newMarking.Mark = marking ?? MarkType.Unknown;

                Notes.Add(newMarking);
            }
        }
    }
}
