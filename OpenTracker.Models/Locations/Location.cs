using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Locations;
using OpenTracker.Models.UndoRedo.Notes;
using ReactiveUI;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains location data.
    /// </summary>
    public class Location : ReactiveObject, ILocation
    {
        private readonly IMarking.Factory _markingFactory;
        private readonly IClearLocation.Factory _clearLocationFactory;
        private readonly IPinLocation.Factory _pinLocationFactory;
        private readonly IUnpinLocation.Factory _unpinLocationFactory;
        private readonly IAddNote.Factory _addNoteFactory;
        private readonly IRemoveNote.Factory _removeNoteFactory;

        public LocationID ID { get; }
        public string Name { get; }

        public List<IMapLocation> MapLocations { get; }
        public List<ISection> Sections { get; }
        public ILocationNoteCollection Notes { get; }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private int _available;
        public int Available
        {
            get => _available;
            private set => this.RaiseAndSetIfChanged(ref _available, value);
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set => this.RaiseAndSetIfChanged(ref _accessible, value);
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set => this.RaiseAndSetIfChanged(ref _total, value);
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            private set => this.RaiseAndSetIfChanged(ref _visible, value);
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
        /// <param name="addNoteFactory">
        /// An Autofac factory for creating undoable actions to add a note.
        /// </param>
        /// <param name="clearLocationFactory">
        /// An Autofac factory for creating undoable actions to clear the location.
        /// </param>
        /// <param name="pinLocationFactory">
        /// An Autofac factory for creating undoable actions to pin the location.
        /// </param>
        /// <param name="removeNoteFactory">
        /// An Autofac factory for creating undoable actions to remove a note.
        /// </param>
        /// <param name="unpinLocationFactory">
        /// An Autofac factory for creating undoable actions to unpin the location.
        /// </param>
        /// <param name="notes">
        /// A new collection of location notes.
        /// </param>
        /// <param name="id">
        /// The ID of the location.
        /// </param>
        public Location(
            ILocationFactory factory, IMapLocationFactory mapLocationFactory, ISectionFactory sectionFactory,
            IMarking.Factory markingFactory, IClearLocation.Factory clearLocationFactory,
            IPinLocation.Factory pinLocationFactory, IUnpinLocation.Factory unpinLocationFactory,
            IAddNote.Factory addNoteFactory, IRemoveNote.Factory removeNoteFactory, ILocationNoteCollection notes,
            LocationID id)
        {
            _markingFactory = markingFactory;
            _clearLocationFactory = clearLocationFactory;
            _pinLocationFactory = pinLocationFactory;
            _unpinLocationFactory = unpinLocationFactory;
            _addNoteFactory = addNoteFactory;
            _removeNoteFactory = removeNoteFactory;

            ID = id;
            Name = factory.GetLocationName(ID);
            MapLocations = mapLocationFactory.GetMapLocations(this);
            Sections = sectionFactory.GetSections(ID);
            Notes = notes;

            PropertyChanged += OnPropertyChanged;

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
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Accessibility))
            {
                UpdateVisible();
            }
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
            switch (e.PropertyName)
            {
                case nameof(ISection.Available):
                    UpdateAccessibility();
                    UpdateAvailable();
                    break;
                case nameof(ISection.Accessibility):
                    UpdateAccessibility();
                    break;
                case nameof(IItemSection.Accessible):
                    UpdateAccessible();
                    break;
                case nameof(IItemSection.Total):
                    UpdateTotal();
                    break;
            }
        }

        /// <summary>
        /// Returns whether the location can be cleared.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the location can be cleared.
        /// </returns>
        public bool CanBeCleared(bool force)
        {
            return Sections.Any(section => section.CanBeCleared(force));
        }

        /// <summary>
        /// Returns a new undoable action to add a note.
        /// </summary>
        public IUndoable CreateAddNoteAction()
        {
            return _addNoteFactory(this);
        }

        /// <summary>
        /// Returns a new undoable action to remove a note.
        /// </summary>
        /// <param name="note">
        ///     The note to be removed.
        /// </param>
        public IUndoable CreateRemoveNoteAction(IMarking note)
        {
            return _removeNoteFactory(note, this);
        }

        /// <summary>
        /// Returns a new undoable action to clear the location.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to ignore the logic.
        /// </param>
        /// <returns>
        /// A new undoable action to clear the location.
        /// </returns>
        public IUndoable CreateClearLocationAction(bool force = false)
        {
            return _clearLocationFactory(this, force);
        }

        /// <summary>
        /// Returns a new undoable action to pin the location.
        /// </summary>
        /// <returns>
        /// A new undoable action to pin the location.
        /// </returns>
        public IUndoable CreatePinLocationAction()
        {
            return _pinLocationFactory(this);
        }

        /// <summary>
        /// Returns a new undoable action to unpin the location.
        /// </summary>
        /// <returns>
        /// A new undoable action to unpin the location.
        /// </returns>
        public IUndoable CreateUnpinLocationAction()
        {
            return _unpinLocationFactory(this);
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

        /// <summary>
        /// Updates the accessibility of the location.
        /// </summary>
        private void UpdateAccessibility()
        {
            var leastAccessible = AccessibilityLevel.Normal;
            var mostAccessible = AccessibilityLevel.None;

            var available = false;

            foreach (ISection section in Sections)
            {
                if (!section.IsAvailable() || !section.Requirement.Met)
                {
                    continue;
                }
                
                available = true;
                var sectionAccessibility = section.Accessibility;

                if (leastAccessible > sectionAccessibility)
                {
                    leastAccessible = sectionAccessibility;
                }

                if (mostAccessible < sectionAccessibility)
                {
                    mostAccessible = sectionAccessibility;
                }
            }

            if (!available)
            {
                Accessibility = AccessibilityLevel.Cleared;
                return;
            }

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

        /// <summary>
        /// Updates the available count of the location.
        /// </summary>
        private void UpdateAvailable()
        {
            var available = 0;

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
            var accessible = 0;

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
            var total = 0;

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
    }
}
