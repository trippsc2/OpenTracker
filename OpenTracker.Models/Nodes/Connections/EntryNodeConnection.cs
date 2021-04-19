using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Nodes.Connections
{
    /// <summary>
    ///     This class contains dungeon entry node connection data.
    /// </summary>
    public class EntryNodeConnection : ReactiveObject, IEntryNodeConnection
    {
        private readonly INode _fromNode;

        public IRequirement? Requirement { get; } = null;
        
        public AccessibilityLevel Accessibility => _fromNode.Accessibility;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="fromNode">
        ///     The node from which the connection originates.
        /// </param>
        public EntryNodeConnection(INode fromNode)
        {
            _fromNode = fromNode;

            _fromNode.PropertyChanged += OnNodeChanged;
        }

        public AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes)
        {
            return Accessibility;
        }
        
        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
            {
                this.RaisePropertyChanged(nameof(Accessibility));
            }
        }
    }
}
