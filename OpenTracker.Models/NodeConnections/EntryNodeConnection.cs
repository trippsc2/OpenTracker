using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.NodeConnections
{
    public class EntryNodeConnection : INodeConnection
    {
        private readonly IRequirementNode _fromNode;

        public IRequirement Requirement { get; } =
            RequirementDictionary.Instance[RequirementType.NoRequirement];

        public event PropertyChangedEventHandler PropertyChanged;

        public AccessibilityLevel Accessibility =>
            _fromNode.Accessibility;

        public EntryNodeConnection(IRequirementNode fromNode)
        {
            _fromNode = fromNode ?? throw new ArgumentNullException(nameof(fromNode));

            _fromNode.PropertyChanged += OnNodeChanged;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                OnPropertyChanged(nameof(Accessibility));
            }
        }

        public AccessibilityLevel GetConnectionAccessibility(List<IRequirementNode> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            return Accessibility;
        }
    }
}
