using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.UnitTests.Models.Requirements;

[ExcludeFromCodeCoverage]
public sealed class MockBooleanRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    [Reactive]
    public bool Met { get; set; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;

    public MockBooleanRequirement()
    {
        this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.Normal : AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}