using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Nodes;

/// <summary>
/// This class contains the starting node data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class StartNode : ReactiveObject, IStartNode
{
    public AccessibilityLevel Accessibility => AccessibilityLevel.Normal;

    public AccessibilityLevel GetNodeAccessibility(IList<INode> excludedNodes)
    {
        return AccessibilityLevel.Normal;
    }
}