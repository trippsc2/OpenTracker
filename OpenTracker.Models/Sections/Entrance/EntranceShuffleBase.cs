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
    ///     This base class contains the entrance section data.
    /// </summary>
    public abstract class EntranceSectionBase : SectionBase, IEntranceSection
    {
        private readonly EntranceShuffle _entranceShuffleLevel;
        private readonly IOverworldNode? _exitProvided;

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
        /// <param name="markingFactory">
        ///     An Autofac factory for creating new markings.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="entranceShuffleLevel">
        ///     The minimum entrance shuffle level.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        /// <param name="exitProvided">
        ///     The overworld node exit that this section provides.
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
            Available = 1;
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
    }
}