using System;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    ///     This is the interface for requirements.
    /// </summary>
    public interface IRequirement : IReactiveObject
    {
        /// <summary>
        ///     A boolean representing whether the requirement has been met.
        /// </summary>
        bool Met { get; }
        
        /// <summary>
        ///     The accessibility level of the requirement.
        /// </summary>
        AccessibilityLevel Accessibility { get; }

        /// <summary>
        ///     An event that indicates that the accessibility has changed and subscribing methods have been called.
        /// </summary>
        event EventHandler? ChangePropagated;
    }
}
