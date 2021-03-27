using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains take any section data.
    /// </summary>
    public class TakeAnySection : ReactiveObject, ITakeAnySection
    {
        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;
        
        private readonly IRequirementNode _node;

        public string Name { get; } = "Take Any";
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }

        public AccessibilityLevel Accessibility => _node.Accessibility;

        private int _available;
        public int Available
        {
            get => _available;
            set => this.RaiseAndSetIfChanged(ref _available, value);
        }

        public delegate TakeAnySection Factory(IRequirementNode node, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collectSectionFactory">
        /// An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        /// An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="node">
        /// The list of connections to this section.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public TakeAnySection(
            ICollectSection.Factory collectSectionFactory, IUncollectSection.Factory uncollectSectionFactory,
             IRequirementNode node, IRequirement requirement)
        {
            _collectSectionFactory = collectSectionFactory;
            _uncollectSectionFactory = uncollectSectionFactory;

            _node = node;
            
            Requirement = requirement;
            Available = 1;

            _node.PropertyChanged += OnRequirementChanged;
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
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
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
            return new()
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
