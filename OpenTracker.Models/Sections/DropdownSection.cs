using System.ComponentModel;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains dropdown section data.
    /// </summary>
    public class DropdownSection : ReactiveObject, IEntranceSection
    {
        private readonly IMode _mode;

        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;

        private readonly IRequirementNode _exitNode;
        private readonly IRequirementNode _holeNode;

        public string Name { get; }
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }
        public IMarking Marking { get; }
        
        public AccessibilityLevel Accessibility =>
            _mode.EntranceShuffle == EntranceShuffle.Insanity ? _holeNode.Accessibility :
                AccessibilityLevelMethods.Max(
                    _holeNode.Accessibility, _exitNode.Accessibility > AccessibilityLevel.Inspect ?
                        AccessibilityLevel.Inspect : AccessibilityLevel.None);

        private int _available;

        public int Available
        {
            get => _available;
            set => this.RaiseAndSetIfChanged(ref _available, value);
        }

        public delegate DropdownSection Factory(
            string name, IRequirementNode? exitNode, IRequirementNode holeNode, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="collectSectionFactory">
        /// An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        /// An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="marking">
        /// The section marking.
        /// </param>
        /// <param name="name">
        /// A string representing the name of the section.
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
            IMode mode, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, IMarking marking, string name,
            IRequirementNode? exitNode, IRequirementNode holeNode, IRequirement requirement)
        {
            _mode = mode;

            _collectSectionFactory = collectSectionFactory;
            _uncollectSectionFactory = uncollectSectionFactory;
            
            _exitNode = exitNode!;
            _holeNode = holeNode;

            Marking = marking;
            Name = name;
            Available = 1;
            Requirement = requirement;

            _exitNode.PropertyChanged += OnNodeChanged;
            _holeNode.PropertyChanged += OnNodeChanged;
            _mode.PropertyChanged += OnModeChanged;
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
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                this.RaisePropertyChanged(nameof(Accessibility));
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
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
            {
                this.RaisePropertyChanged(nameof(Accessibility));
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
        public void Load(SectionSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
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
