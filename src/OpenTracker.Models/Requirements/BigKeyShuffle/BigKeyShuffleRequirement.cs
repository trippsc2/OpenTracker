using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.BigKeyShuffle;

/// <summary>
/// This class contains <see cref="IMode.BigKeyShuffle"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class BigKeyShuffleRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IMode Mode { get; }

    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="BigKeyShuffleRequirement"/> objects.
    /// </summary>
    public delegate BigKeyShuffleRequirement Factory(bool expectedValue);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected value.
    /// </param>
    public BigKeyShuffleRequirement(IMode mode, bool expectedValue)
    {
        Mode = mode;

        this.WhenAnyValue(x => x.Mode.BigKeyShuffle)
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