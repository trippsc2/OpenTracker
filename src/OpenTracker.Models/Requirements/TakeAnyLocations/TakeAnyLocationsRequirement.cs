using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.TakeAnyLocations;

/// <summary>
/// This class contains the <see cref="IMode.TakeAnyLocations"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class TakeAnyLocationsRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IMode Mode { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="TakeAnyLocationsRequirement"/> objects.
    /// </summary>
    public delegate TakeAnyLocationsRequirement Factory(bool expectedValue);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> expected <see cref="IMode.TakeAnyLocations"/> value.
    /// </param>
    public TakeAnyLocationsRequirement(IMode mode, bool expectedValue)
    {
        Mode = mode;

        this.WhenAnyValue(x => x.Mode.TakeAnyLocations)
            .Select(x => x == expectedValue)
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