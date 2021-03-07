using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.NodeConnections
{
    /// <summary>
    /// This class contains node connection data.
    /// </summary>
    public class NodeConnection : INodeConnection
    {
        private readonly IRequirementNode _fromNode;
        private readonly IRequirementNode _toNode;

        public IRequirement Requirement { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public delegate NodeConnection Factory(
            IRequirementNode fromNode, IRequirementNode toNode, IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromNode">
        /// The node from which the connection originates.
        /// </param>
        /// <param name="toNode">
        /// The node to which the connection belongs.
        /// </param>
        /// <param name="requirement">
        /// The requirement for the connection to be accessible.
        /// </param>
        public NodeConnection(
            IRequirementNode fromNode, IRequirementNode toNode, IRequirement requirement)
        {
            _fromNode = fromNode;
            _toNode = toNode;

            Requirement = requirement;

            _fromNode.PropertyChanged += OnNodeChanged;
            Requirement.PropertyChanged += OnRequirementChanged;

            UpdateAccessibility();
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
        /// Subscribes to the PropertyChanged event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the Accessibility property.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetConnectionAccessibility(new List<IRequirementNode>());
        }

        /// <summary>
        /// Returns the availability of the connection, excluding loops from the specified nodes.
        /// </summary>
        /// <param name="excludedNodes">
        /// A list of nodes to exclude to prevent loops.
        /// </param>
        /// <returns>
        /// The availability of the connection.
        /// </returns>
        public AccessibilityLevel GetConnectionAccessibility(List<IRequirementNode> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            if (Requirement.Accessibility == AccessibilityLevel.None ||
                _fromNode.Accessibility == AccessibilityLevel.None ||
                excludedNodes.Contains(_fromNode))
            {
                return AccessibilityLevel.None;
            }

            List<IRequirementNode> newExcludedNodes = excludedNodes.GetRange(
                0, excludedNodes.Count);
            newExcludedNodes.Add(_toNode);

            return AccessibilityLevelMethods.Min(Requirement.Accessibility,
                _fromNode.GetNodeAccessibility(newExcludedNodes));
        }
    }
}
