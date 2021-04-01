using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    ///     This class contains the dungeon requirement node data.
    /// </summary>
    public class DungeonNode : ReactiveObject, IDungeonNode
    {
        private readonly IDungeonNodeFactory _factory;
        private readonly IMutableDungeon _dungeonData;

        public int ExitsAccessible { get; set; }
        public int DungeonExitsAccessible { get; set; }
        public int InsanityExitsAccessible { get; set; }

        public IList<INodeConnection> Connections { get; } = new List<INodeConnection>();

        public event EventHandler? ChangePropagated;

        private bool _alwaysAccessible;
        public bool AlwaysAccessible
        {
            get => _alwaysAccessible;
            set => this.RaiseAndSetIfChanged(ref _alwaysAccessible, value);
        }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility == value)
                {
                    return;
                }
                
                this.RaiseAndSetIfChanged(ref _accessibility, value);
                ChangePropagated?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The dungeon node factory for creating node connections.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public DungeonNode(IDungeonNodeFactory factory, IMutableDungeon dungeonData)
        {
            _factory = factory;
            _dungeonData = dungeonData;

            PropertyChanged += OnPropertyChanged;
            dungeonData.Nodes.ItemCreated += OnNodeCreated;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AlwaysAccessible):
                    UpdateAccessibility();
                    break;
            }
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
        private void OnNodeCreated(object? sender, KeyValuePair<DungeonNodeID, IDungeonNode> e)
        {
            if (e.Value != this)
            {
                return;
            }
            
            var nodes = (IDungeonNodeDictionary)sender!;
            nodes.ItemCreated -= OnNodeCreated;

            _factory.PopulateNodeConnections(_dungeonData, e.Key, this, Connections);

            UpdateAccessibility();

            foreach (var connection in Connections)
            {
                connection.PropertyChanged += OnConnectionChanged;
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
        private void OnConnectionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(INodeConnection.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of the node.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetNodeAccessibility(new List<IRequirementNode>());
        }

        /// <summary>
        /// Returns the node accessibility.
        /// </summary>
        /// <param name="excludedNodes">
        ///     The list of node IDs from which to not check accessibility.
        /// </param>
        /// <returns>
        /// The accessibility of the node.
        /// </returns>
        public AccessibilityLevel GetNodeAccessibility(IList<IRequirementNode> excludedNodes)
        {
            if (AlwaysAccessible)
            {
                return AccessibilityLevel.Normal;
            }

            var finalAccessibility = AccessibilityLevel.None;

            foreach (var connection in Connections)
            {
                finalAccessibility = AccessibilityLevelMethods.Max(
                    finalAccessibility, connection.GetConnectionAccessibility(excludedNodes));

                if (finalAccessibility == AccessibilityLevel.Normal)
                {
                    break;
                }
            }

            return finalAccessibility;
        }
    }
}
