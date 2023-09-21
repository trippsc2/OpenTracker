using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.AutoTracking;

/// <summary>
/// This class contains <see cref="IAutoTracker.RaceIllegalTracking"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class RaceIllegalTrackingRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IAutoTracker AutoTracker { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="autoTracker">
    ///     The <see cref="IAutoTracker"/>.
    /// </param>
    public RaceIllegalTrackingRequirement(IAutoTracker autoTracker)
    {
        AutoTracker = autoTracker;

        this.WhenAnyValue(x => x.AutoTracker.RaceIllegalTracking)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.Normal : AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Accessibility)
            .Subscribe(_ => ChangePropagated?.Invoke(this, EventArgs.Empty))
            .DisposeWith(_disposables);
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
    }
}