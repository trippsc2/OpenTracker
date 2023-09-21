using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Item.Exact;

/// <summary>
/// This class contains <see cref="IItem"/> exact value <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class ItemExactRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IItem Item { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;
    
    /// <summary>
    /// A factory method for creating new <see cref="ItemExactRequirement"/> objects.
    /// </summary>
    public delegate ItemExactRequirement Factory(IItem item, int count);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    ///     The <see cref="IItem"/>.
    /// </param>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of the item required.
    /// </param>
    public ItemExactRequirement(IItem item, int count)
    {
        Item = item;

        this.WhenAnyValue(x => x.Item.Current)
            .Select(x => x == count)
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