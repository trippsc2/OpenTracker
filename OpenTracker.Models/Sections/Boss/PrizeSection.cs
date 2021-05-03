using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.PrizePlacements;
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
        public PrizeSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, ITogglePrizeSection.Factory togglePrizeSectionFactory,
            IBossAccessibilityProvider accessibilityProvider, string name, IBossPlacement bossPlacement,
            IPrizePlacement prizePlacement, IAutoTrackValue? autoTrackValue = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, accessibilityProvider, name,
                bossPlacement, autoTrackValue)
        {
            _togglePrizeSectionFactory = togglePrizeSectionFactory;

            PrizePlacement = prizePlacement;
            
            Total = 1;
            _available = 1;
            
            UpdateShouldBeDisplayed();

            PrizePlacement.PropertyChanging += OnPrizePlacementChanging;
            PrizePlacement.PropertyChanged += OnPrizePlacementChanged;
        }

        public IUndoable CreateTogglePrizeSectionAction(bool force)
        {
            return _togglePrizeSectionFactory(this, force);
        }

        protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName != nameof(Available) || PrizePlacement.Prize is null)
            {
                return;
            }
            
            if (IsAvailable())
            {
                PrizePlacement.Prize.Current--;
                return;
            }

            PrizePlacement.Prize.Current++;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanging event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanging event.
        /// </param>
        private void OnPrizePlacementChanging(object? sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize) && PrizePlacement.Prize is not null && !IsAvailable())
            {
                PrizePlacement.Prize.Current--;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnPrizePlacementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize) && PrizePlacement.Prize is not null && !IsAvailable())
            {
                PrizePlacement.Prize.Current++;
            }
        }
    }
}