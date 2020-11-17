using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    public class DropdownSection : IEntranceSection
    {
        private readonly IRequirementNode _exitNode;
        private readonly IRequirementNode _holeNode;
        private readonly IRequirementNode _exitProvided;
        public string Name { get; }
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }
        public IMarking Marking { get; } =
            MarkingFactory.GetMarking();

        public event PropertyChangedEventHandler PropertyChanged;

        public AccessibilityLevel Accessibility
        {
            get
            {
                if (Mode.Instance.EntranceShuffle == EntranceShuffle.Insanity)
                {
                    return _holeNode.Accessibility;
                }

                return AccessibilityLevelMethods.Max(_holeNode.Accessibility,
                    _exitNode.Accessibility > AccessibilityLevel.Inspect ?
                    AccessibilityLevel.Inspect : AccessibilityLevel.None);
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">
        /// A string representing the name of the section.
        /// </param>
        /// <param name="exitProvided">
        /// The exit provided.
        /// </param>
        /// <param name="exitNode">
        /// The requirement node of the exit of the dropdown.
        /// </param>
        /// <param name="holeNode">
        /// The requirement node of the hole of the dropdown.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public DropdownSection(
            string name, IRequirementNode exitNode, IRequirementNode holeNode,
            IRequirement requirement = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Available = 1;
            _exitNode = exitNode ?? throw new ArgumentNullException(nameof(exitNode));
            _holeNode = holeNode ?? throw new ArgumentNullException(nameof(holeNode));
            Requirement = requirement ??
                RequirementDictionary.Instance[RequirementType.NoRequirement];

            _exitNode.PropertyChanged += OnNodeChanged;
            _holeNode.PropertyChanged += OnNodeChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
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
        /// Subscribes to the PropertyChanged event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                OnPropertyChanged(nameof(Accessibility));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                OnPropertyChanged(nameof(Accessibility));
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
            return IsAvailable();
        }

        /// <summary>
        /// Returns whether the section can be uncollected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be uncollected.
        /// </returns>
        public bool CanBeUncleared()
        {
            return Available == 0;
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            Available = 0;
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
            Marking.Mark = MarkType.Unknown;
            Available = 1;
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
                Marking = Marking.Mark
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

            if (saveData.Marking.HasValue)
            {
                Marking.Mark = saveData.Marking.Value;
            }
        }
    }
}
