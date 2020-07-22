using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the section class of dungeon items with a marking.
    /// </summary>
    public class MarkableDungeonItemSection : IMarkableSection, IDungeonItemSection
    {
        private readonly IDungeonItemSection _section;

        public string Name =>
            _section.Name;
        public IRequirement Requirement =>
            _section.Requirement;

        public bool UserManipulated
        {
            get => _section.UserManipulated;
            set => _section.UserManipulated = value;
        }
        public int Available
        {
            get => _section.Available;
            set => _section.Available = value;
        }
        public int Total =>
            _section.Total;

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public AccessibilityLevel Accessibility
        {
            get => _section.Accessibility;
            set => _section.Accessibility = value;
        }
        public int Accessible
        {
            get => _section.Accessible;
            set => _section.Accessible = value;
        }

        private MarkingType? _marking;
        public MarkingType? Marking
        {
            get => _marking;
            set
            {
                if (_marking != value)
                {
                    OnPropertyChanging(nameof(Marking));
                    _marking = value;
                    OnPropertyChanged(nameof(Marking));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The dungeon item section to be encapsulated.
        /// </param>
        public MarkableDungeonItemSection(IDungeonItemSection section)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));

            _section.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changing property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
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
        /// Subscribes to the PropertyChanged event on the encapsulated IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

            if (e.PropertyName == nameof(ISection.Available))
            {
                if (!IsAvailable())
                {
                    Marking = null;
                }
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
            if (_section.CanBeCleared(force))
            {
                return true;
            }

            return IsAvailable() && Accessibility == AccessibilityLevel.Inspect && !Marking.HasValue;
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
            _section.Clear(force);
        }

        /// <summary>
        /// Returns whether the location has not been fully collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section has been fully collected.
        /// </returns>
        public bool IsAvailable()
        {
            return _section.IsAvailable();
        }

        /// <summary>
        /// Resets the section to its starting values.
        /// </summary>
        public void Reset()
        {
            _section.Reset();
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
                UserManipulated = UserManipulated,
                Marking = Marking
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
            Marking = saveData.Marking;
        }
    }
}
