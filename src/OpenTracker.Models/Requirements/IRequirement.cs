using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements;

/// <summary>
/// This interface contains the requirement data.
/// </summary>
public interface IRequirement : IDisposable, INotifyPropertyChanged
{
    /// <summary>
    /// A <see cref="bool"/> representing whether the condition has been met.
    /// </summary>
    bool Met { get; }
        
    /// <summary>
    /// The <see cref="AccessibilityLevel"/>.
    /// </summary>
    AccessibilityLevel Accessibility { get; }

    /// <summary>
    /// An event that indicates that the <see cref="Accessibility"/> or <see cref="Met"/> property has changed and
    /// all subscribing methods have been executed.
    /// </summary>
    event EventHandler? ChangePropagated;
}