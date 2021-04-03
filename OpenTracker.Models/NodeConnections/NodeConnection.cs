using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.NodeConnections
{
    /// <summary>
    /// This class contains node connection data.
    /// </summary>
    public class NodeConnection : ReactiveObject, INodeConnection
    {
        private readonly INode _fromNode;
        private readonly INode _toNode;

        public IRequirement Requirement { get; }
        
        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        public delegate NodeConnection Factory(INode fromNode, INode toNode, IRequirement requirement);

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
        public NodeConnection(INode fromNode, INode toNode, IRequirement requirement)
        {
            _fromNode = fromNode;
            _toNode = toNode;

            Requirement = requirement;

            _fromNode.PropertyChanged += OnNodeChanged;
            Requirement.PropertyChanged += OnRequirementChanged;

            UpdateAccessibility();
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
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
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
            Accessibility = GetConnectionAccessibility(new List<INode>());
        }

        /// <summary>
        /// Returns the availability of the connection, excluding loops from the specified nodes.
        /// </summary>
        /// <param name="excludedNodes">
        ///     A list of nodes to exclude to prevent loops.
        /// </param>
        /// <returns>
        /// The availability of the connection.
        /// </returns>
        public AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            if (Requirement.Accessibility == AccessibilityLevel.None ||
                _fromNode.Accessibility == AccessibilityLevel.None || excludedNodes.Contains(_fromNode))
            {
                return AccessibilityLevel.None;
            }

            IList<INode> newExcludedNodes = new List<INode>(excludedNodes);
            newExcludedNodes.Add(_toNode);

            return AccessibilityLevelMethods.Min(Requirement.Accessibility,
                _fromNode.GetNodeAccessibility(newExcludedNodes));
        }
    }
}
