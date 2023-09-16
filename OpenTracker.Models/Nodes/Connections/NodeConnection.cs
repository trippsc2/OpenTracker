using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Nodes.Connections;

/// <summary>
/// This class contains node connection data.
/// </summary>
public class NodeConnection : ReactiveObject, INodeConnection
{
    private readonly INode _fromNode;
    private readonly INode _toNode;

    public IRequirement? Requirement { get; }
        
    private AccessibilityLevel _accessibility;
    public AccessibilityLevel Accessibility
    {
        get => _accessibility;
        private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fromNode">
    ///     The <see cref="INode"/> from which the connection originates.
    /// </param>
    /// <param name="toNode">
    ///     The <see cref="INode"/> to which the connection belongs.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the connection to be accessible.
    /// </param>
    public NodeConnection(INode fromNode, INode toNode, IRequirement? requirement = null)
    {
        _fromNode = fromNode;
        _toNode = toNode;

        Requirement = requirement;

        _fromNode.PropertyChanged += OnNodeChanged;

        UpdateAccessibility();

        if (Requirement is null)
        {
            return;
        }
            
        Requirement.PropertyChanged += OnRequirementChanged;
    }

    public AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes)
    {
        var requirement = Requirement?.Accessibility ?? AccessibilityLevel.Normal;

        if (requirement == AccessibilityLevel.None || _fromNode.Accessibility == AccessibilityLevel.None ||
            excludedNodes.Contains(_fromNode))
        {
            return AccessibilityLevel.None;
        }

        var newExcludedNodes = new List<INode>(excludedNodes) {_toNode};

        return AccessibilityLevelMethods.Min(requirement, _fromNode.GetNodeAccessibility(newExcludedNodes));
    }
        
    /// <summary>
    /// Subscribes to the <see cref="INode.PropertyChanged"/> event.
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
            UpdateAccessibility();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IRequirement.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Accessibility))
        {
            UpdateAccessibility();
        }
    }

    /// <summary>
    /// Updates the <see cref="Accessibility"/> property.
    /// </summary>
    private void UpdateAccessibility()
    {
        Accessibility = GetConnectionAccessibility(new List<INode>());
    }
}