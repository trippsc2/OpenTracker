using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing item sections of locations.
    /// </summary>
    public class ItemSection : IItemSection
    {
        private readonly IRequirementNode _node;

        private Func<int?> AutoTrackFunction { get; }
        public string Name { get; }
        public int Total { get; }
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AccessibilityLevel Accessibility =>
            _node.Accessibility;

        private int _available;
        public int Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set
            {
                if (_accessible != value)
                {
                    _accessible = value;
                    OnPropertyChanged(nameof(Accessible));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">
        /// A string representing the name of the section.
        /// </param>
        /// <param name="total">
        /// A 32-bit signed integer representing the total number of items in the section.
        /// </param>
        /// <param name="node">
        /// The requirement node to which this section belongs.
        /// </param>
        /// <param name="autoTrackFunction">
        /// The autotracking function.
        /// </param>
        /// <param name="memoryAddresses">
        /// The list of memory addresses to subscribe to for autotracking.
        /// </param>
        public ItemSection(
            string name, int total, IRequirementNode node, Func<int?> autoTrackFunction,
            List<(MemorySegmentType, int)> memoryAddresses, IRequirement requirement = null)
        {
            if (memoryAddresses == null)
            {
                throw new ArgumentNullException(nameof(memoryAddresses));
            }

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Total = total;
            _node = node ?? throw new ArgumentNullException(nameof(node));
            AutoTrackFunction = autoTrackFunction ??
                throw new ArgumentNullException(nameof(autoTrackFunction));
            Requirement = requirement ??
                RequirementDictionary.Instance[RequirementType.NoRequirement];
            Available = Total;

            foreach ((MemorySegmentType, int) address in memoryAddresses)
            {
                SubscribeToMemoryAddress(address.Item1, address.Item2);
            }

            _node.PropertyChanged += OnNodeChanged;
            UpdateAccessible();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
            {
                UpdateAccessible();
            }
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
        private void OnNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                OnPropertyChanged(nameof(Accessibility));
                UpdateAccessible();
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
        /// Updates the number of accessible items.
        /// </summary>
        private void UpdateAccessible()
        {
            if (Accessibility >= AccessibilityLevel.SequenceBreak)
            {
                Accessible = Available;
            }
            else
            {
                Accessible = 0;
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
            return Available < Total;
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            do
            {
                Available--;
            } while ((Accessibility > AccessibilityLevel.None || force) && Available > 0);
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
        /// Resets the section to its starting values.
        /// </summary>
        public void Reset()
        {
            Available = Total;
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
            return new SectionSaveData()
            {
                Available = Available
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
        }
    }
}
