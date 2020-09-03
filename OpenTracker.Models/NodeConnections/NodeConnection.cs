using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.NodeConnections
{
    public class NodeConnection : INodeConnection
    {
        private readonly IRequirementNode _fromNode;
        private readonly IRequirementNode _toNode;

        public IRequirement Requirement { get; }

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

        public NodeConnection(
            IRequirementNode fromNode, IRequirementNode toNode, IRequirement requirement = null)
        {
            _fromNode = fromNode ?? throw new ArgumentNullException(nameof(fromNode));
            _toNode = toNode ?? throw new ArgumentNullException(nameof(toNode));
            Requirement = requirement ?? RequirementDictionary.Instance[RequirementType.NoRequirement];

            _fromNode.PropertyChanged += OnNodeChanged;
            Requirement.PropertyChanged += OnRequirementChanged;
            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        private void UpdateAccessibility()
        {
            Accessibility = GetConnectionAccessibility(new List<IRequirementNode>());
        }

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
