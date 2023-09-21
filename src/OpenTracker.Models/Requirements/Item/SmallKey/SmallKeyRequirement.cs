using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Item.SmallKey;

/// <summary>
/// This class contains small key <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class SmallKeyRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private ISmallKeyItem Item { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="SmallKeyRequirement"/> objects.
    /// </summary>
    public delegate SmallKeyRequirement Factory(ISmallKeyItem item, int count = 1);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    ///     The <see cref="ISmallKeyItem"/>.
    /// </param>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of the item required.
    /// </param>
    public SmallKeyRequirement(ISmallKeyItem item, int count = 1)
    {
        Item = item;

        this.WhenAnyValue(x => x.Item.EffectiveCurrent)
            .Select(x => x >= count)
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