using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.UnitTests.Models.Requirements;

public sealed class MockAccessibilityRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    [Reactive]
    public AccessibilityLevel Accessibility { get; set; }
    [ObservableAsProperty]
    public bool Met { get; }
    
    public event EventHandler? ChangePropagated;

    public MockAccessibilityRequirement()
    {
        this.WhenAnyValue(x => x.Accessibility)
            .Select(x => x > AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}