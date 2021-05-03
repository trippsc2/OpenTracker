using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections.Item
{
    /// <summary>
    ///     This base class contains the item section data.
    /// </summary>
    public abstract class ItemSectionBase : SectionBase, IItemSection
    {
        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            protected set => this.RaiseAndSetIfChanged(ref _accessible, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The save/load manager.
        /// </param>
        /// <param name="collectSectionFactory">
        ///     An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        ///     An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-tracking value for this section.
        /// </param>
        /// <param name="marking">
        ///     The section marking.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        protected ItemSectionBase(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, string name, IAutoTrackValue? autoTrackValue = null,
            IMarking? marking = null, IRequirement? requirement = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, name, autoTrackValue, marking,
                requirement)
        {
        }

        public override bool CanBeCleared(bool force = false)
        {
            if (!IsAvailable())
            {
                return false;
            }

            if (force)
            {
                return true;
            }

            if (Accessibility > AccessibilityLevel.Inspect)
            {
                return true;
            }
            
            return Accessibility == AccessibilityLevel.Inspect && Marking is not null &&
                Marking.Mark == MarkType.Unknown;
        }
    }
}