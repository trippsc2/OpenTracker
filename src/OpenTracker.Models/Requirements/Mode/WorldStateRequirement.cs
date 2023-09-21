using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains <see cref="IMode.WorldState"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class WorldStateRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IMode Mode { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="WorldStateRequirement"/> objects.
    /// </summary>
    public delegate WorldStateRequirement Factory(WorldState expectedValue);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="WorldState"/> representing the expected <see cref="IMode.WorldState"/> value.
    /// </param>
    public WorldStateRequirement(IMode mode, WorldState expectedValue)
    {
        Mode = mode;

        this.WhenAnyValue(x => x.Mode.WorldState)
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