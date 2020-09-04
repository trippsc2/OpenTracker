using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the class for requirement nodes.
    /// </summary>
    public class RequirementNode : IRequirementNode
    {
        private readonly RequirementNodeID _id;
        private readonly bool _start;
        private readonly List<INodeConnection> _connections =
            new List<INodeConnection>();

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

        private int _exitsAccessible;
        public int ExitsAccessible
        {
            get => _exitsAccessible;
            set
            {
                if (_exitsAccessible != value)
                {
                    _exitsAccessible = value;
                    UpdateAccessibility();
                }
            }
        }

        private int _dungeonExitsAccessible;
        public int DungeonExitsAccessible
        {
            get => _dungeonExitsAccessible;
            set
            {
                if (_dungeonExitsAccessible != value)
                {
                    _dungeonExitsAccessible = value;
                    OnPropertyChanged(nameof(DungeonExitsAccessible));
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
        /// The requirement node identity.
        /// </param>
        /// <param name="connections">
        /// A list of connections to this node.
        /// </param>
        public RequirementNode(RequirementNodeID id, bool start)
        {
            _id = id;
            _start = start;
            AlwaysAccessible = _start;

            RequirementNodeDictionary.Instance.NodeCreated += OnNodeCreated;
            Mode.Instance.PropertyChanged += OnModeChanged;
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

            if (propertyName == nameof(ExitsAccessible) ||
                propertyName == nameof(DungeonExitsAccessible) ||
                propertyName == nameof(AlwaysAccessible))
            {
                UpdateAccessibility();
            }
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                UpdateAccessibility();
            }    
        }

        /// <summary>
        /// Subscribes to the NodeCreated event on the RequirementNodeDictionary class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeCreated(object sender, KeyValuePair<RequirementNodeID, IRequirementNode> e)
        {
            if (e.Value == this)
            {
                RequirementNodeDictionary.Instance.NodeCreated -= OnNodeCreated;
                RequirementNodeFactory.PopulateNodeConnections(e.Key, this, _connections);
                UpdateAccessibility();

                foreach (var connection in _connections)
                {
                    connection.PropertyChanged += OnConnectionChanged;
                }
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
        private void OnConnectionChanged(object sender, PropertyChangedEventArgs e)
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
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            if (AlwaysAccessible ||
                (ExitsAccessible > 0 && Mode.Instance.EntranceShuffle == EntranceShuffle.All) ||
                (DungeonExitsAccessible > 0 &&
                Mode.Instance.EntranceShuffle > EntranceShuffle.None))
            {
                return AccessibilityLevel.Normal;
            }

            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            if (excludedNodes.Count == 0)
            {
                foreach (var connection in _connections)
                {
                    finalAccessibility = AccessibilityLevelMethods.Max(
                        finalAccessibility, connection.Accessibility);

                    if (finalAccessibility == AccessibilityLevel.Normal)
                    {
                        break;
                    }
                }

                return finalAccessibility;
            }

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
