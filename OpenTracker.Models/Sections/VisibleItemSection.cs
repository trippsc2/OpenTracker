using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains item sections with marking data.
    /// </summary>
    public class VisibleItemSection : ReactiveObject, IMarkableSection, IItemSection
    {
        private readonly ISaveLoadManager _saveLoadManager;
        
        private readonly IRequirementNode? _visibleNode;
        private readonly IRequirementNode _node;
        private readonly IAutoTrackValue? _autoTrackValue;

        public string Name { get; }
        public int Total { get; }
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }
        public IMarking Marking { get; }

        public AccessibilityLevel Accessibility =>
            _visibleNode == null ? _node.Accessibility :
                AccessibilityLevelMethods.Max(
                    _node.Accessibility, _visibleNode.Accessibility > AccessibilityLevel.Inspect ?
                        AccessibilityLevel.Inspect : AccessibilityLevel.None);

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
            private set => this.RaiseAndSetIfChanged(ref _accessible, value);
        }

        public delegate VisibleItemSection Factory(
            string name, int total, IRequirementNode node, IAutoTrackValue? autoTrackValue, IRequirement requirement,
            IRequirementNode? visibleNode = null);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="marking">
        /// The section marking.
        /// </param>
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
        /// The auto-tracking value for this section.
        /// </param>
        /// <param name="requirement">
        /// The requirement for the section to be visible.
        /// </param>
        /// <param name="visibleNode">
        /// The node that provides Inspect accessibility for this section.
        /// </param>
        public VisibleItemSection(
            ISaveLoadManager saveLoadManager, IMarking marking, string name, int total, IRequirementNode node,
            IAutoTrackValue? autoTrackValue, IRequirement requirement, IRequirementNode? visibleNode = null)
        {
            _saveLoadManager = saveLoadManager;
            
            _node = node;
            _autoTrackValue = autoTrackValue;
            _visibleNode = visibleNode;

            Marking = marking;
            Name = name;
            Total = total;
            Requirement = requirement;
            Available = Total;

            PropertyChanged += OnPropertyChanged;
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
            if (e.PropertyName == nameof(Available))
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
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IRequirementNode.Accessibility))
            {
                return;
            }
            
            this.RaisePropertyChanged(nameof(Accessibility));
            UpdateAccessible();
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
        private void OnAutoTrackChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        /// <summary>
        /// Updates the section available/accessible count from the auto-tracked value.
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
        /// Updates the number of accessible items.
        /// </summary>
        private void UpdateAccessible()
        {
            if (Accessibility >= AccessibilityLevel.SequenceBreak)
            {
                Accessible = Available;
                return;
            }

            Accessible = 0;
        }

        /// <summary>
        /// Returns whether the section can be cleared or collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be cleared or collected.
        /// </returns>
        public bool CanBeCleared(bool force)
        {
            return IsAvailable() && (force || Accessibility > AccessibilityLevel.Inspect ||
                Accessibility == AccessibilityLevel.Inspect && Marking.Mark == MarkType.Unknown);
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
