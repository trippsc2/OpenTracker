using System;
using System.Diagnostics.CodeAnalysis;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.UnitTests.Models.Requirements;

[ExcludeFromCodeCoverage]
public sealed class MockRequirement : ReactiveObject, IRequirement
{
    [Reactive]
    public bool Met { get; set; }
    [Reactive]
    public AccessibilityLevel Accessibility { get; set; }
    
    public event EventHandler? ChangePropagated;
    
    public void RaiseChangePropagated()
    {
        ChangePropagated?.Invoke(this, EventArgs.Empty);
    }
}