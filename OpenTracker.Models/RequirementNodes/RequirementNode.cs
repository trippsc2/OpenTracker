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

#if DEBUG        
        private readonly RequirementNodeID _id;
#endif
        private readonly bool _start;

        private readonly List<INodeConnection> _connections =
            new List<INodeConnection>();

        public event EventHandler? ChangePropagated;

        private bool _alwaysAccessible;
        public bool AlwaysAccessible
        {
            get => _alwaysAccessible;
            set => this.RaiseAndSetIfChanged(ref _alwaysAccessible, value);
        }

        private int _exitsAccessible;
        public int ExitsAccessible
        {
            get => _exitsAccessible;
            set => this.RaiseAndSetIfChanged(ref _exitsAccessible, value);
        }

        private int _dungeonExitsAccessible;
        public int DungeonExitsAccessible
        {
            get => _dungeonExitsAccessible;
            set => this.RaiseAndSetIfChanged(ref _dungeonExitsAccessible, value);
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

        public delegate IRequirementNode Factory(RequirementNodeID id, bool start);

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
        /// <param name="id">
        /// The requirement node identity.
        /// </param>
        /// <param name="start">
        /// A boolean representing whether the node is the start node.
        /// </param>
        public RequirementNode(
            IMode mode, IRequirementNodeDictionary requirementNodes,
            IRequirementNodeFactory factory, RequirementNodeID id, bool start)
        {
            _mode = mode;
            _factory = factory;
#if DEBUG
            _id = id;
#endif
            _start = start;
            AlwaysAccessible = _start;

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
                case nameof(ExitsAccessible):
                case nameof(DungeonExitsAccessible):
                case nameof(AlwaysAccessible):
                case nameof(InsanityExitsAccessible):
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
            if (e.Value == this)
            {
                var requirementNodes = ((IRequirementNodeDictionary)sender!);

                requirementNodes.ItemCreated -= OnNodeCreated;
                _factory.PopulateNodeConnections(e.Key, this, _connections);

                UpdateAccessibility();

                foreach (var connection in _connections)
                {
                    connection.PropertyChanged += OnConnectionChanged;
                }
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
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
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
            if (AlwaysAccessible ||
                (InsanityExitsAccessible > 0 &&
                _mode.EntranceShuffle >= EntranceShuffle.Insanity) ||
                (ExitsAccessible > 0 && _mode.EntranceShuffle >= EntranceShuffle.All) ||
                (DungeonExitsAccessible > 0 &&
                _mode.EntranceShuffle >= EntranceShuffle.Dungeon))
            {
                return AccessibilityLevel.Normal;
            }

            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

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

        /// <summary>
        /// Resets AlwaysAccessible property for testing purposes.
        /// </summary>
        public void Reset()
        {
            AlwaysAccessible = _start;
        }
    }
}
