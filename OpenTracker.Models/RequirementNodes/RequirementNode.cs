using OpenTracker.Models.Requirements;
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
        private readonly List<RequirementNodeConnection> _connections;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public RequirementNode(
            RequirementNodeID id, List<RequirementNodeConnection> connections = null)
        {
            _id = id;
            _connections = connections ?? new List<RequirementNodeConnection>(0);

            RequirementNodeDictionary.Instance.NodeCreated += OnNodeCreated;
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
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeCreated(object sender, RequirementNodeID id)
        {
            if (id == _id)
            {
                SubscribeToNodesAndRequirements();
                UpdateAccessibility();
                RequirementNodeDictionary.Instance.NodeCreated -= OnNodeCreated;
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
        /// Updates the accessibility of the node.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetNodeAccessibility(new List<RequirementNodeID>());
        }

        /// <summary>
        /// Creates subscriptions to connected nodes and requirements.
        /// </summary>
        private void SubscribeToNodesAndRequirements()
        {
            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<IRequirement> requirementSubscriptions = new List<IRequirement>();

            foreach (RequirementNodeConnection connection in _connections)
            {
                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    RequirementNodeDictionary.Instance[connection.FromNode].PropertyChanged +=
                        OnRequirementChanged;
                    nodeSubscriptions.Add(connection.FromNode);
                }

                if (!requirementSubscriptions.Contains(connection.Requirement))
                {
                    connection.Requirement.PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(connection.Requirement);
                }
            }
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
        public AccessibilityLevel GetNodeAccessibility(List<RequirementNodeID> excludedNodes)
        {
            if (_id == RequirementNodeID.Start)
            {
                return AccessibilityLevel.Normal;
            }

            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            List<RequirementNodeID> newExcludedNodes =
                excludedNodes.GetRange(0, excludedNodes.Count);
            newExcludedNodes.Add(_id);
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                if (!connection.Requirement.Met)
                {
                    continue;
                }

                if (newExcludedNodes.Contains(connection.FromNode))
                {
                    continue;
                }

                AccessibilityLevel nodeAccessibility =
                    RequirementNodeDictionary.Instance[connection.FromNode]
                    .GetNodeAccessibility(newExcludedNodes);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                {
                    continue;
                }

                AccessibilityLevel requirementAccessibility = connection.Requirement.Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)requirementAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                {
                    return AccessibilityLevel.Normal;
                }

                if (finalConnectionAccessibility > finalAccessibility)
                {
                    finalAccessibility = finalConnectionAccessibility;
                }
            }

            return finalAccessibility;
        }
    }
}
