using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Nodes;

/// <summary>
/// This interface contains the node data.
/// </summary>
public interface INode : INotifyPropertyChanged
{
    /// <summary>
    /// The <see cref="AccessibilityLevel"/>.
    /// </summary>
    AccessibilityLevel Accessibility { get; }

    /// <summary>
    /// Returns the node <see cref="AccessibilityLevel"/>.
    /// </summary>
    /// <param name="excludedNodes">
    ///     The <see cref="IList{T}"/> of <see cref="INode"/> from which to skip connections and prevent loops.
    /// </param>
    /// <returns>
    ///     The <see cref="AccessibilityLevel"/> of the node.
    /// </returns>
    AccessibilityLevel GetNodeAccessibility(IList<INode> excludedNodes);
}