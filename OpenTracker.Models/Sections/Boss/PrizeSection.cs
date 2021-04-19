using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boss
{
    /// <summary>
    ///     This class contains end of boss section with prize data.
    /// </summary>
    public class PrizeSection : BossSection, IPrizeSection
    {
        private readonly ITogglePrizeSection.Factory _togglePrizeSectionFactory;

        public IPrizePlacement PrizePlacement { get; }

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
        /// <param name="togglePrizeSectionFactory">
        ///     An Autofac factory for creating toggle prize section sections.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The boss accessibility provider for this section.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        ///     The boss placement for the section.
        /// </param>
        /// <param name="prizePlacement">
        ///     The prize placement for the section.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The section auto-track value.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        public PrizeSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory, IUncollectSection.Factory uncollectSectionFactory,
            ITogglePrizeSection.Factory togglePrizeSectionFactory, IBossAccessibilityProvider accessibilityProvider,
            string name, IBossPlacement bossPlacement, IPrizePlacement prizePlacement, IAutoTrackValue autoTrackValue,
            IRequirement? requirement = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, accessibilityProvider, name,
                bossPlacement, autoTrackValue, requirement)
        {
            _togglePrizeSectionFactory = togglePrizeSectionFactory;

            PrizePlacement = prizePlacement;
        }

        public IUndoable CreateTogglePrizeSectionAction(bool force)
        {
            return _togglePrizeSectionFactory(this, force);
        }
    }
}