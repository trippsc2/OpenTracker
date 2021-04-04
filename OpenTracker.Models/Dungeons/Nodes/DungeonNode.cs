﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
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

        public IList<INodeConnection> Connections { get; } = new List<INodeConnection>();

        public event EventHandler? ChangePropagated;

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
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     The dungeon node factory for creating node connections.
        /// </param>
        /// <param name="dungeonData">
        ///     The mutable dungeon data parent class.
        /// </param>
        public DungeonNode(IDungeonNodeFactory factory, IMutableDungeon dungeonData)
        {
            _factory = factory;
            _dungeonData = dungeonData;

            dungeonData.Nodes.ItemCreated += OnNodeCreated;
        }

        /// <summary>
        ///     Subscribes to the ItemCreated event on the IRequirementNodeDictionary interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the ItemCreated event.
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
        ///     Subscribes to the PropertyChanged event on the INodeConnection interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(INodeConnection.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Updates the accessibility of the node.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetNodeAccessibility(new List<INode>());
        }

        public AccessibilityLevel GetNodeAccessibility(IList<INode> excludedNodes)
        {
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
