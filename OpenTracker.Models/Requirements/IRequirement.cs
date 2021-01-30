using OpenTracker.Models.AccessibilityLevels;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the interface for requirements.
    /// </summary>
    public interface IRequirement : INotifyPropertyChanged
    {
        bool Met { get; }
        AccessibilityLevel Accessibility { get; }

        event EventHandler ChangePropagated;
    }
}
