using System;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.UnitTests.Models.Requirements;

public sealed class MockAccessibilityRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private readonly ObservableAsPropertyHelper<bool> _met;

    private AccessibilityLevel _accessibility;
    public AccessibilityLevel Accessibility
    {
        get => _accessibility;
        set => this.RaiseAndSetIfChanged(ref _accessibility, value);
    }
    public bool Met => _met.Value;
    
    public event EventHandler? ChangePropagated;

    public MockAccessibilityRequirement()
    {
        _met = this.WhenAnyValue(x => x.Accessibility)
            .Select(x => x > AccessibilityLevel.None)
            .ToProperty(this, x => x.Met)
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}