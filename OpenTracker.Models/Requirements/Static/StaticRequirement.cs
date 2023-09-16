using System;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements.Static;

/// <summary>
/// This class contains unchanging <see cref="IRequirement"/> data.
/// </summary>
public class StaticRequirement : ReactiveObject, IStaticRequirement
{
    public bool Met => true;
        
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="accessibility">
    ///     The <see cref="AccessibilityLevel"/>.
    /// </param>
    public StaticRequirement(AccessibilityLevel accessibility)
    {
        Accessibility = accessibility;
    }
}