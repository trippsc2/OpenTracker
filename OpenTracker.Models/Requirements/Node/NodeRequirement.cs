using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Requirements.Node
{
    /// <summary>
    /// This class containing <see cref="INode"/> <see cref="IRequirement"/> data.
    /// </summary>
    public class NodeRequirement : AccessibilityRequirement, INodeRequirement
    {
        private readonly INode _node;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="node">
        ///     The <see cref="INode"/>.
        /// </param>
        public NodeRequirement(INode node)
        {
            _node = node;

            _node.PropertyChanged += OnNodeChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the <see cref="INode.PropertyChanged"/> event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
            {
                UpdateValue();
            }
        }

        protected override AccessibilityLevel GetAccessibility()
        {
            return _node.Accessibility;
        }
    }
}
