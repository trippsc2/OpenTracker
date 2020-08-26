using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing boss/prize sections of dungeons.
    /// </summary>
    public class PrizeSection : BossSection, IPrizeSection
    {
        private readonly bool _alwaysClearable;

        private Func<int?> AutoTrackFunction { get; }
        public IPrizePlacement PrizePlacement { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">
        /// A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement of this section.
        /// </param>
        /// <param name="prizePlacement">
        /// The prize placement of this section.
        /// </param>
        /// <param name="autoTrackFunction">
        /// The autotracking function.
        /// </param>
        /// <param name="memoryAddresses">
        /// The list of memory addresses to subscribe to for autotracking.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public PrizeSection(
            string name, IBossPlacement bossPlacement, IPrizePlacement prizePlacement,
            Func<int?> autoTrackFunction, List<(MemorySegmentType, int)> memoryAddresses,
            bool alwaysClearable = false, IRequirement requirement = null) : base(name, bossPlacement, requirement)
        {
            if (memoryAddresses == null)
            {
                throw new ArgumentNullException(nameof(memoryAddresses));
            }

            _alwaysClearable = alwaysClearable;

            AutoTrackFunction = autoTrackFunction ??
                throw new ArgumentNullException(nameof(autoTrackFunction));
            PrizePlacement = prizePlacement ??
                throw new ArgumentNullException(nameof(prizePlacement));

            PropertyChanged += OnPropertyChanged;
            PrizePlacement.PropertyChanging += OnPrizeChanging;
            PrizePlacement.PropertyChanged += OnPrizeChanged;

            foreach ((MemorySegmentType, int) address in memoryAddresses)
            {
                SubscribeToMemoryAddress(address.Item1, address.Item2);
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Available))
            {
                if (PrizePlacement.Prize != null)
                {
                    if (IsAvailable())
                    {
                        PrizePlacement.Prize.Current--;
                    }
                    else
                    {
                        PrizePlacement.Prize.Current++;
                    }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnPrizeChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                if (!IsAvailable() && PrizePlacement != null &&
                PrizePlacement.Prize != null)
                {
                    PrizePlacement.Prize.Current--;
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPrizeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                if (!IsAvailable() && PrizePlacement != null &&
                PrizePlacement.Prize != null)
                {
                    PrizePlacement.Prize.Current++;
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            AutoTrack();
        }

        /// <summary>
        /// Autotrack the item.
        /// </summary>
        private void AutoTrack()
        {
            if (UserManipulated)
            {
                return;
            }

            int? result = AutoTrackFunction();

            if (result.HasValue)
            {
                Available = result.Value;
            }
        }

        /// <summary>
        /// Creates subscription to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to which to subscribe.
        /// </param>
        /// <param name="index">
        /// The index within the memory address list to which to subscribe.
        /// </param>
        private void SubscribeToMemoryAddress(MemorySegmentType segment, int index)
        {
            List<MemoryAddress> memory = segment switch
            {
                MemorySegmentType.Room => AutoTracker.Instance.RoomMemory,
                MemorySegmentType.OverworldEvent => AutoTracker.Instance.OverworldEventMemory,
                MemorySegmentType.Item => AutoTracker.Instance.ItemMemory,
                MemorySegmentType.NPCItem => AutoTracker.Instance.NPCItemMemory,
                _ => throw new ArgumentOutOfRangeException(nameof(segment))
            };

            if (index >= memory.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            memory[index].PropertyChanged += OnMemoryChanged;
        }

        /// <summary>
        /// Returns whether the section can be cleared or collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be cleared or collected.
        /// </returns>
        public override bool CanBeCleared(bool force)
        {
            if (IsAvailable() && _alwaysClearable)
            {
                return true;
            }

            return base.CanBeCleared(force);
        }
    }
}
