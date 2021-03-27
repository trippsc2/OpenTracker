using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.Requirements.Node
{
    /// <summary>
    /// This class containing requirement node requirement data.
    /// </summary>
    public class RequirementNodeRequirement : AccessibilityRequirement
    {
        private readonly IRequirementNode _node;

        public delegate RequirementNodeRequirement Factory(IRequirementNode node);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="node">
        /// The required requirement node.
        /// </param>
        public RequirementNodeRequirement(IRequirementNode node)
        {
            _node = node;

            _node.PropertyChanged += OnNodeChanged;

            UpdateValue();
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
                UpdateValue();
            }
        }

        protected override AccessibilityLevel GetAccessibility()
        {
            return _node.Accessibility;
        }
    }
}
