using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using ReactiveUI;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This class contains requirement node data.
    /// </summary>
    public class RequirementNode : ReactiveObject, IRequirementNode
    {
        private readonly IMode _mode;
        private readonly IRequirementNodeFactory _factory;
        
        private readonly List<INodeConnection> _connections = new List<INodeConnection>();

        public event EventHandler? ChangePropagated;

        private int _dungeonExitsAccessible;
        public int DungeonExitsAccessible
        {
            get => _dungeonExitsAccessible;
            set => this.RaiseAndSetIfChanged(ref _dungeonExitsAccessible, value);
        }

        private int _exitsAccessible;
        public int ExitsAccessible
        {
            get => _exitsAccessible;
            set => this.RaiseAndSetIfChanged(ref _exitsAccessible, value);
        }

        private int _insanityExitsAccessible;
        public int InsanityExitsAccessible
        {
            get => _insanityExitsAccessible;
            set => this.RaiseAndSetIfChanged(ref _insanityExitsAccessible, value);
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
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        /// <param name="factory">
        /// The requirement node factory.
        /// </param>
        public RequirementNode(
            IMode mode, IRequirementNodeDictionary requirementNodes, IRequirementNodeFactory factory)
        {
            _mode = mode;
            _factory = factory;

            PropertyChanged += OnPropertyChanged;
            requirementNodes.ItemCreated += OnNodeCreated;
            _mode.PropertyChanged += OnModeChanged;
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
                case nameof(ExitsAccessible) when _mode.EntranceShuffle >= EntranceShuffle.All:
                case nameof(DungeonExitsAccessible) when _mode.EntranceShuffle >= EntranceShuffle.Dungeon:
                case nameof(InsanityExitsAccessible) when _mode.EntranceShuffle == EntranceShuffle.Insanity:
                    UpdateAccessibility();
                    break;
            }
        }

        /// <summary>
        /// Subscribes to the ItemCreated event on the IRequirementNodeDictionary interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the ItemCreated event.
        /// </param>
        private void OnNodeCreated(object? sender, KeyValuePair<RequirementNodeID, IRequirementNode> e)
        {
            if (e.Value != this)
            {
                return;
            }
            
            var requirementNodes = (IRequirementNodeDictionary)sender!;
            requirementNodes.ItemCreated -= OnNodeCreated;
            _connections.AddRange(_factory.GetNodeConnections(e.Key, this));

            UpdateAccessibility();

            foreach (var connection in _connections)
            {
                connection.PropertyChanged += OnConnectionChanged;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IMode.EntranceShuffle))
            {
                return;
            }

            if (ExitsAccessible > 0 || InsanityExitsAccessible > 0 || DungeonExitsAccessible > 0)
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the INodeConnection interface.
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
        /// The list of node IDs from which to not check accessibility.
        /// </param>
        /// <returns>
        /// The accessibility of the node.
        /// </returns>
        public AccessibilityLevel GetNodeAccessibility(List<IRequirementNode> excludedNodes)
        {
            if (ExitsAccessible > 0 && _mode.EntranceShuffle >= EntranceShuffle.All)
            {
                return AccessibilityLevel.Normal;
            }

            if (DungeonExitsAccessible > 0 && _mode.EntranceShuffle >= EntranceShuffle.Dungeon)
            {
                return AccessibilityLevel.Normal;
            }

            if (InsanityExitsAccessible > 0 && _mode.EntranceShuffle >= EntranceShuffle.Insanity)
            {
                return AccessibilityLevel.Normal;
            }

            var finalAccessibility = AccessibilityLevel.None;

            foreach (var connection in _connections)
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
