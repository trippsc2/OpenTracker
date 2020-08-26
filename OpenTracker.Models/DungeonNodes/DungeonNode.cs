using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the class for dungeon requirement node.
    /// </summary>
    public class DungeonNode : IDungeonNode
    {
        private readonly List<KeyDoorID> _pendingKeyDoors;
        private readonly IMutableDungeon _dungeonData;
        private readonly List<RequirementNodeConnection> _entryConnections;
        private readonly List<IKeyDoor> _keyDoorConnections = new List<IKeyDoor>();
        private readonly List<DungeonNodeConnection> _dungeonConnections;

        public DungeonNodeID ID { get; }
        public int FreeKeysProvided { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _alwaysAccessible;
        public bool AlwaysAccessible
        {
            get => _alwaysAccessible;
            set
            {
                if (_alwaysAccessible != value)
                {
                    _alwaysAccessible = value;
                    UpdateAccessibility();
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
        /// <param name="id">
        /// The node identity.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <param name="freeKeysProvided">
        /// A 32-bit signed integer representing the number of free keys provided by accessing the
        /// node.
        /// </param>
        /// <param name="entryConnections">
        /// A list of connections to the node from outside the dungeon.
        /// </param>
        /// <param name="keyDoorConnections">
        /// A list of key doors to which this node is connected.
        /// </param>
        /// <param name="dungeonConnections">
        /// A list of non-key door connections to this node from within the dungeon.
        /// </param>
        public DungeonNode(
            DungeonNodeID id, IMutableDungeon dungeonData, int freeKeysProvided,
            List<RequirementNodeConnection> entryConnections, List<KeyDoorID> keyDoorConnections,
            List<DungeonNodeConnection> dungeonConnections)
        {
            ID = id;
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
            FreeKeysProvided = freeKeysProvided;
            _entryConnections = entryConnections ??
                throw new ArgumentNullException(nameof(entryConnections));
            _pendingKeyDoors = keyDoorConnections ??
                throw new ArgumentNullException(nameof(keyDoorConnections));
            _dungeonConnections = dungeonConnections ??
                throw new ArgumentNullException(nameof(dungeonConnections));

            _dungeonData.RequirementNodes.NodeCreated += OnNodeCreated;
            _dungeonData.KeyDoorDictionary.DoorCreated += OnDoorCreated;
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
        }

        /// <summary>
        /// Subscribes to the NodeCreated event on the RequirementNodeDictionary class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the NodeCreated event.
        /// </param>
        private void OnNodeCreated(object sender, DungeonNodeID id)
        {
            if (id == ID)
            {
                SubscribeToNodesAndRequirements();
                UpdateAccessibility();
                _dungeonData.RequirementNodes.NodeCreated -= OnNodeCreated;
            }
        }

        /// <summary>
        /// Subscribes to the DoorCreated event on the KeyDoorDictionary class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the DoorCreated event.
        /// </param>
        private void OnDoorCreated(object sender, KeyDoorID id)
        {
            if (_pendingKeyDoors.Contains(id))
            {
                IKeyDoor keyDoor = _dungeonData.KeyDoorDictionary[id];
                _keyDoorConnections.Add(keyDoor);
                keyDoor.PropertyChanged += OnRequirementChanged;
                _pendingKeyDoors.Remove(id);
                UpdateAccessibility();
            }

            if (_pendingKeyDoors.Count == 0)
            {
                _dungeonData.KeyDoorDictionary.DoorCreated -= OnDoorCreated;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the RequirementNode, DungeonNode,
        /// IKeyDoor, and IRequirement classes/interfaces.
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
        /// Subscribes to the key doors, nodes, and requirements.
        /// </summary>
        private void SubscribeToNodesAndRequirements()
        {
            List<RequirementNodeID> entryNodeSubscriptions = new List<RequirementNodeID>();
            List<DungeonNodeID> dungeonNodeSubscriptions = new List<DungeonNodeID>();
            List<IRequirement> requirementSubscriptions = new List<IRequirement>();

            foreach (RequirementNodeConnection entryConnection in _entryConnections)
            {
                if (!entryNodeSubscriptions.Contains(entryConnection.FromNode))
                {
                    RequirementNodeDictionary.Instance[entryConnection.FromNode].PropertyChanged +=
                        OnRequirementChanged;
                    entryNodeSubscriptions.Add(entryConnection.FromNode);
                }

                if (!requirementSubscriptions.Contains(entryConnection.Requirement))
                {
                    entryConnection.Requirement.PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(entryConnection.Requirement);
                }
            }

            foreach (DungeonNodeConnection dungeonConnection in _dungeonConnections)
            {
                if (!dungeonNodeSubscriptions.Contains(dungeonConnection.FromNode))
                {
                    _dungeonData.RequirementNodes[dungeonConnection.FromNode].PropertyChanged +=
                        OnRequirementChanged;
                    dungeonNodeSubscriptions.Add(dungeonConnection.FromNode);
                }

                if (!requirementSubscriptions.Contains(dungeonConnection.Requirement))
                {
                    dungeonConnection.Requirement.PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(dungeonConnection.Requirement);
                }
            }

            foreach (KeyDoor keyDoor in _keyDoorConnections)
            {
                keyDoor.PropertyChanged += OnRequirementChanged;
            }
        }

        /// <summary>
        /// Updates the accessibility of the node.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetNodeAccessibility(new List<DungeonNodeID>());
        }

        /// <summary>
        /// Returns the accessibility of the provided entry connection.
        /// </summary>
        /// <param name="connection">
        /// The entry connection to be evaluated.
        /// </param>
        /// <returns>
        /// The accessibility level of the entry connection.
        /// </returns>
        private static AccessibilityLevel GetEntryConnectionAccessibility(
            RequirementNodeConnection connection)
        {
            if (!connection.Requirement.Met)
            {
                return AccessibilityLevel.None;
            }

            return AccessibilityLevelMethods.Min(
                RequirementNodeDictionary.Instance[connection.FromNode].Accessibility,
                connection.Requirement.Accessibility);
        }

        /// <summary>
        /// Returns the accessibility of the provided dungeon connection.
        /// </summary>
        /// <param name="connection">
        /// The dungeon connection to be evaluated.
        /// </param>
        /// <param name="excludedNodes">
        /// The list of excluded nodes to be passed to prevent loops.
        /// </param>
        /// <returns>
        /// The accessibility level of the entry connection.
        /// </returns>
        private AccessibilityLevel GetDungeonConnectionAccessibility(
            DungeonNodeConnection connection, List<DungeonNodeID> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            if (!connection.Requirement.Met)
            {
                return AccessibilityLevel.None;
            }

            if (excludedNodes.Contains(connection.FromNode))
            {
                return AccessibilityLevel.None;
            }

            return AccessibilityLevelMethods.Min(
                _dungeonData.RequirementNodes[connection.FromNode]
                .GetNodeAccessibility(excludedNodes),
                connection.Requirement.Accessibility);
        }

        /// <summary>
        /// Returns the accessibility of the provided entry connection.
        /// </summary>
        /// <param name="connection">
        /// The entry connection to be evaluated.
        /// </param>
        /// <param name="excludedNodes">
        /// The list of excluded nodes to be passed to prevent loops.
        /// </param>
        /// <returns>
        /// The accessibility level of the entry connection.
        /// </returns>
        private static AccessibilityLevel GetKeyDoorAccessibility(
            KeyDoor keyDoor, List<DungeonNodeID> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            if (keyDoor == null)
            {
                throw new ArgumentNullException(nameof(keyDoor));
            }

            if (!keyDoor.Unlocked)
            {
                return AccessibilityLevel.None;
            }

            return keyDoor.GetDoorAccessibility(excludedNodes);
        }

        /// <summary>
        /// Returns the node accessibility.
        /// </summary>
        /// <param name="excludedNodes">
        /// The list of node IDs from which to not check accessibility.
        /// </param>
        /// <returns>
        /// The accessibility of the node.
        /// </returns>
        public AccessibilityLevel GetNodeAccessibility(List<DungeonNodeID> excludedNodes)
        {
            if (AlwaysAccessible)
            {
                return AccessibilityLevel.Normal;
            }

            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            List<DungeonNodeID> newExcludedNodes =
                excludedNodes.GetRange(0, excludedNodes.Count);
            newExcludedNodes.Add(ID);

            AccessibilityLevel accessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection entryConnection in _entryConnections)
            {
                accessibility = AccessibilityLevelMethods.Max(
                    accessibility, GetEntryConnectionAccessibility(entryConnection));

                if (accessibility == AccessibilityLevel.Normal)
                {
                    return AccessibilityLevel.Normal;
                }
            }

            foreach (DungeonNodeConnection dungeonConnection in _dungeonConnections)
            {
                accessibility = AccessibilityLevelMethods.Max(
                    accessibility, GetDungeonConnectionAccessibility(
                        dungeonConnection, newExcludedNodes));

                if (accessibility == AccessibilityLevel.Normal)
                {
                    return AccessibilityLevel.Normal;
                }
            }

            foreach (KeyDoor keyDoor in _keyDoorConnections)
            {
                accessibility = AccessibilityLevelMethods.Max(
                    accessibility, GetKeyDoorAccessibility(
                        keyDoor, newExcludedNodes));

                if (accessibility == AccessibilityLevel.Normal)
                {
                    return AccessibilityLevel.Normal;
                }
            }

            return accessibility;
        }
    }
}
