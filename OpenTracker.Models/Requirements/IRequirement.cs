using System;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the interface for requirements.
    /// </summary>
    public interface IRequirement : IReactiveObject
    {
        bool Met { get; }
        AccessibilityLevel Accessibility { get; }

        event EventHandler? ChangePropagated;
    }
}
