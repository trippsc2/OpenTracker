using OpenTracker.Models.AccessibilityLevels;
using System;
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
        bool Testing { get; set; }

        event EventHandler? ChangePropagated;
    }
}
