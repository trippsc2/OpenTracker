using System.ComponentModel;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Entrance
{
    /// <summary>
    /// This base class contains the entrance section data.
    /// </summary>
    public abstract class EntranceSectionBase : SectionBase, IEntranceSection
    {
        private readonly EntranceShuffle _entranceShuffleLevel;
        private readonly IOverworldNode? _exitProvided;

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
        /// <param name="markingFactory">
        ///     An Autofac factory for creating new <see cref="IMarking"/> objects.
        /// </param>
        /// <param name="name">
        ///     A <see cref="string"/> representing the section name.
        /// </param>
        /// <param name="entranceShuffleLevel">
        ///     The minimum <see cref="EntranceShuffle"/> level.
        /// </param>
        /// <param name="requirement">
        ///     The <see cref="IRequirement"/> for the section to be active.
        /// </param>
        /// <param name="exitProvided">
        ///     The nullable <see cref="IOverworldNode"/> exit that this section provides.
        /// </param>
        protected EntranceSectionBase(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, IMarking.Factory markingFactory, string name,
            EntranceShuffle entranceShuffleLevel, IRequirement requirement, IOverworldNode? exitProvided = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, name, null,
                markingFactory(), requirement)
        {
            _entranceShuffleLevel = entranceShuffleLevel;
            _exitProvided = exitProvided;

            Total = 1;
            _available = 1;

            Marking!.PropertyChanged += OnMarkingChanged;

            UpdateShouldBeDisplayed();
        }

        public override bool CanBeCleared(bool force = false)
        {
            return IsAvailable();
        }

        public override void Clear(bool force)
        {
            Available = 0;
        }

        protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);
            
            if (e.PropertyName != nameof(Available) || _exitProvided is null)
            {
                return;
            }
            
            if (IsAvailable())
            {
                switch (_entranceShuffleLevel)
                {
                    case EntranceShuffle.Dungeon:
                        _exitProvided.DungeonExitsAccessible--;
                        break;
                    case EntranceShuffle.All:
                        _exitProvided.AllExitsAccessible--;
                        break;
                    case EntranceShuffle.Insanity:
                        _exitProvided.InsanityExitsAccessible--;
                        break;
                }
                return;
            }

            switch (_entranceShuffleLevel)
            {
                case EntranceShuffle.Dungeon:
                    _exitProvided.DungeonExitsAccessible++;
                    break;
                case EntranceShuffle.All:
                    _exitProvided.AllExitsAccessible++;
                    break;
                case EntranceShuffle.Insanity:
                    _exitProvided.InsanityExitsAccessible++;
                    break;
            }
        }

        /// <summary>
        /// Subscribes to the <see cref="IMarking.PropertyChanged"/> event on <see cref="Marking"/>.
        /// </summary>
        /// <param name="sender">
        ///     The sending <see cref="IMarking"/>.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnMarkingChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                UpdateShouldBeDisplayed();
            }
        }

        protected override void UpdateShouldBeDisplayed()
        {
            if (Marking!.Mark is not MarkType.Unknown)
            {
                ShouldBeDisplayed = true;
                return;
            }
            
            base.UpdateShouldBeDisplayed();
        }
    }
}