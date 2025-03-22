using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.Nodes
{
    /// <summary>
    /// This class contains the dungeon requirement node data.
    /// </summary>
    public class DungeonNode : ReactiveObject, IDungeonNode
    {
        private readonly IDungeonNodeFactory _factory;
        private readonly IMutableDungeon _dungeonData;

        public IList<INodeConnection> Connections { get; } = new List<INodeConnection>();

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set =>this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     The <see cref="IDungeonNodeFactory"/>.
        /// </param>
        /// <param name="dungeonData">
        ///     The <see cref="IMutableDungeon"/> parent class.
        /// </param>
        public DungeonNode(IDungeonNodeFactory factory, IMutableDungeon dungeonData)
        {
            _factory = factory;
            _dungeonData = dungeonData;

            dungeonData.Nodes.ItemCreated += OnNodeCreated;
        }

        /// <summary>
        /// Subscribes to the <see cref="IDungeonNodeDictionary.ItemCreated"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="KeyValuePair{TKey,TValue}"/> of <see cref="DungeonNodeID"/> and
        ///     <see cref="IDungeonNode"/> objects that were created.
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
        /// Subscribes to the <see cref="INodeConnection.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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
