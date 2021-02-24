using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the section class of items with a marking.
    /// </summary>
    public class VisibleItemSection : IMarkableSection, IItemSection
    {
        private readonly IRequirementNode? _visibleNode;
        private readonly IRequirementNode _node;
        private readonly IAutoTrackValue? _autoTrackValue;

        public string Name { get; }
        public int Total { get; }
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }
        public IMarking Marking { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AccessibilityLevel Accessibility
        {
            get
            {
                if (_visibleNode == null)
                {
                    return _node.Accessibility;
                }

                return AccessibilityLevelMethods.Max(_node.Accessibility,
                    _visibleNode.Accessibility > AccessibilityLevel.Inspect ?
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

        public delegate VisibleItemSection Factory(
            string name, int total, IRequirementNode node, IAutoTrackValue? autoTrackValue,
            IRequirement requirement, IRequirementNode? visibleNode = null);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">
        /// A string representing the name of the section.
        /// </param>
        /// <param name="total">
        /// A 32-bit signed integer representing the total number of items in the section.
        /// </param>
        /// <param name="node">
        /// The requirement node to which this section belongs.
        /// </param>
        /// <param name="autoTrackValue">
        /// The autotracking value for this section.
        /// </param>
        /// <param name="visibleNode">
        /// The node that provides Inspect accessibility for this section.
        /// </param>
        public VisibleItemSection(
            IMarking marking, string name, int total, IRequirementNode node,
            IAutoTrackValue? autoTrackValue, IRequirement requirement,
            IRequirementNode? visibleNode = null)
        {
            _node = node;
            _autoTrackValue = autoTrackValue;
            _visibleNode = visibleNode;

            Marking = marking;
            Name = name;
            Total = total;
            Requirement = requirement;
            Available = Total;

            _node.PropertyChanged += OnNodeChanged;
            UpdateAccessible();

            if (_visibleNode != null)
            {
                _visibleNode.PropertyChanged += OnNodeChanged;
            }

            if (_autoTrackValue != null)
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackChanged;
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

            if (propertyName == nameof(Available))
            {
                UpdateAccessible();
            }
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
                UpdateAccessible();
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
        private void OnAutoTrackChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        /// <summary>
        /// Updates the section available/accessible count from the autotracked value.
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
        /// Updates the number of accessible items.
        /// </summary>
        private void UpdateAccessible()
        {
            if (Accessibility >= AccessibilityLevel.SequenceBreak)
            {
                Accessible = Available;
            }
            else
            {
                Accessible = 0;
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
            return IsAvailable() && (force || Accessibility > AccessibilityLevel.Inspect
                || (Accessibility == AccessibilityLevel.Inspect &&
                Marking.Mark == MarkType.Unknown));
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
            do
            {
                Available--;
            } while ((Accessibility > AccessibilityLevel.None || force) && Available > 0);
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
                Available = Available
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
        }
    }
}
