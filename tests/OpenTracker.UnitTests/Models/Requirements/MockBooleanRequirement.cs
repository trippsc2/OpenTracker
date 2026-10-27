using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.UnitTests.Models.Requirements;

[ExcludeFromCodeCoverage]
public sealed class MockBooleanRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private readonly ObservableAsPropertyHelper<AccessibilityLevel> _accessibility;

    private bool _met;
    public bool Met
    {
        get => _met;
        set => this.RaiseAndSetIfChanged(ref _met, value);
    }
    public AccessibilityLevel Accessibility => _accessibility.Value;
    
    public event EventHandler? ChangePropagated;

    public MockBooleanRequirement()
    {
        _accessibility = this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.Normal : AccessibilityLevel.None)
            .ToProperty(this, x => x.Accessibility)
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}