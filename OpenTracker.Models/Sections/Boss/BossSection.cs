using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boss
{
    /// <summary>
    ///     This class contains boss section data.  It will be used directly for GT LW boss re-fights and as a base
    ///         class for final bosses that provide a prize.
    /// </summary>
    public class BossSection : SectionBase, IBossSection
    {
        private readonly IBossAccessibilityProvider _accessibilityProvider;

        public IBossPlacement BossPlacement { get; }

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
        /// <param name="accessibilityProvider">
        ///     The boss accessibility provider for this section.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        ///     The boss placement for the section.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The section auto-track value.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        public BossSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, IBossAccessibilityProvider accessibilityProvider,
            string name, IBossPlacement bossPlacement, IAutoTrackValue? autoTrackValue = null,
            IRequirement? requirement = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, name, autoTrackValue, null,
                requirement)
        {
            _accessibilityProvider = accessibilityProvider;

            BossPlacement = bossPlacement;

            Total = 1;
            Available = 1;

            _accessibilityProvider.PropertyChanged += OnAccessibilityProviderChanged;
        }

        public override bool CanBeCleared(bool force = false)
        {
            return IsAvailable() && (force || Accessibility > AccessibilityLevel.Inspect);
        }

        public override void Clear(bool force)
        {
            Available = 0;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IBossAccessibilityProvider interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnAccessibilityProviderChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossAccessibilityProvider.Accessibility))
            {
                Accessibility = _accessibilityProvider.Accessibility;
            }
        }
    }
}