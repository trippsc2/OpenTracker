using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the key door data class.
    /// </summary>
    public class KeyDoor : IKeyDoor
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly List<IDungeonNode> _connectedNodes;

        public KeyDoorID ID { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _unlocked;
        public bool Unlocked
        {
            get => _unlocked;
            set
            {
                if (_unlocked != value)
                {
                    _unlocked = value;
                    OnPropertyChanged(nameof(Unlocked));
                }
            }
        }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <param name="id">
        /// The key door identity.
        /// </param>
        public KeyDoor(KeyDoorID id, IMutableDungeon dungeonData, List<IDungeonNode> connectedNodes)
        {
            ID = id;
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
            _connectedNodes = connectedNodes ?? throw new ArgumentNullException(nameof(connectedNodes));

            _dungeonData.KeyDoorDictionary.DoorCreated += OnDoorCreated;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the DoorCreated event on the KeyDoorDictionary class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="id">
        /// The arguments of the DoorCreated event.
        /// </param>
        private void OnDoorCreated(object sender, KeyDoorID id)
        {
            if (id == ID)
            {
                foreach (DungeonNode node in _connectedNodes)
                {
                    node.PropertyChanged += OnRequirementChanged;
                }

                UpdateAccessibility();
                _dungeonData.KeyDoorDictionary.DoorCreated -= OnDoorCreated;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface and
        /// RequirementNode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        /// <summary>
        /// Updates the accessibility of the key door.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetDoorAccessibility(new List<DungeonNodeID>(0));
        }

        /// <summary>
        /// Returns the current accessibility of the key door.
        /// </summary>
        /// <param name="excludedNodes">
        /// The list of requirement node IDs from which accessibility should not be checked.</param>
        /// <returns>
        /// The accessibility level of the key door.
        /// </returns>
        public AccessibilityLevel GetDoorAccessibility(List<DungeonNodeID> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            AccessibilityLevel accessibility = AccessibilityLevel.None;

            foreach (DungeonNode node in _connectedNodes)
            {
                if (excludedNodes.Contains(node.ID))
                {
                    continue;
                }

                if (node.Accessibility > accessibility)
                {
                    accessibility = node.GetNodeAccessibility(excludedNodes);
                }

                if (accessibility == AccessibilityLevel.Normal)
                {
                    break;
                }
            }

            return accessibility;
        }
    }
}
