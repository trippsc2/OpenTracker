using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems;

/// <summary>
/// This class contains <see cref="IMode.GuaranteedBossItems"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class GuaranteedBossItemsRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IMode Mode { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="GuaranteedBossItemsRequirement"/> objects.
    /// </summary>
    public delegate GuaranteedBossItemsRequirement Factory(bool expectedValue);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.GuaranteedBossItems"/> value.
    /// </param>
    public GuaranteedBossItemsRequirement(IMode mode, bool expectedValue)
    {
        Mode = mode;

        this.WhenAnyValue(x => x.Mode.GuaranteedBossItems)
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