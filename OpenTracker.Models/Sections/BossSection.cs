using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This class contains boss section data (GT LW boss re-fights)
    /// </summary>
    public class BossSection : ReactiveObject, IBossSection
    {
        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;

        public string Name { get; }
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }
        public IBossPlacement BossPlacement { get; }
        
        private AccessibilityLevel _accessibility;

        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private int _available;
        public int Available
        {
            get => _available;
            set => this.RaiseAndSetIfChanged(ref _available, value);
        }

        public delegate BossSection Factory(string name, IBossPlacement bossPlacement, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collectSectionFactory">
        /// An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        /// An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="name">
        /// A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement for this boss section.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public BossSection(
            ICollectSection.Factory collectSectionFactory, IUncollectSection.Factory uncollectSectionFactory,
            string name, IBossPlacement bossPlacement, IRequirement requirement)
        {
            _collectSectionFactory = collectSectionFactory;
            _uncollectSectionFactory = uncollectSectionFactory;
            
            Name = name;
            BossPlacement = bossPlacement;
            Requirement = requirement;
            Available = 1;
        }

        /// <summary>
        /// Returns whether the section can be cleared or collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be cleared or collected.
        /// </returns>
        public virtual bool CanBeCleared(bool force)
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
        /// Returns a new undoable action to collect the section.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether to override the logic while collecting the section.
        /// </param>
        /// <returns>
        /// An undoable action to collect the section.
        /// </returns>
        public IUndoable CreateCollectSectionAction(bool force)
        {
            return _collectSectionFactory(this, force);
        }

        /// <summary>
        /// Returns a new undoable action to uncollect the section.
        /// </summary>
        /// <returns>
        /// An undoable action to uncollect the section.
        /// </returns>
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
            return new()
            {
                Available = Available,
                UserManipulated = UserManipulated
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
        }
    }
}
