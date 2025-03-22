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
    /// This class contains end of dungeon boss section with prize data.
    /// </summary>
    public class PrizeSection : BossSection, IPrizeSection
    {
        private readonly ITogglePrizeSection.Factory _togglePrizeSectionFactory;

        public IPrizePlacement PrizePlacement { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The <see cref="ISaveLoadManager"/>.
        /// </param>
        /// <param name="collectSectionFactory">
        ///     An Autofac factory for creating new <see cref="ICollectSection"/> objects.
        /// </param>
        /// <param name="uncollectSectionFactory">
        ///     An Autofac factory for creating new <see cref="IUncollectSection"/> objects.
        /// </param>
        /// <param name="togglePrizeSectionFactory">
        ///     An Autofac factory for creating new <see cref="ITogglePrizeSection"/> objects.
        /// </param>
        /// <param name="accessibilityProvider">
        ///     The <see cref="IBossAccessibilityProvider"/>.
        /// </param>
        /// <param name="name">
        ///     A <see cref="string"/> representing the section name.
        /// </param>
        /// <param name="bossPlacement">
        ///     The <see cref="IBossPlacement"/>.
        /// </param>
        /// <param name="prizePlacement">
        ///     The <see cref="IPrizePlacement"/>.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The nullable <see cref="IAutoTrackValue"/>.
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
        /// Subscribes to the <see cref="IPrizePlacement.PropertyChanging"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangingEventArgs"/>.
        /// </param>
        private void OnPrizePlacementChanging(object? sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize) && PrizePlacement.Prize is not null && !IsAvailable())
            {
                PrizePlacement.Prize.Current--;
            }
        }

        /// <summary>
        /// Subscribes to the <see cref="IPrizePlacement.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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