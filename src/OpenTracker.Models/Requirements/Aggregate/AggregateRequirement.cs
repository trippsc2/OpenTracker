using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Aggregate;

/// <summary>
/// This class contains logic aggregating a set of <see cref="IRequirement"/>.
/// </summary>
[DependencyInjection]
public sealed class AggregateRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    [ObservableAsProperty]
    public bool Met { get; }
    
    public event EventHandler? ChangePropagated;
    
    /// <summary>
    /// A factory method for creating new <see cref="AggregateRequirement"/> objects.
    /// </summary>
    public delegate AggregateRequirement Factory(IEnumerable<IRequirement> requirements);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="requirements">
    ///     A <see cref="IEnumerable{T}"/> of <see cref="IRequirement"/> to be aggregated.
    /// </param>
    public AggregateRequirement(IEnumerable<IRequirement> requirements)
    {
        var sourceList = new SourceList<IRequirement>();
            sourceList.AddRange(requirements);
            
        sourceList.Connect()
            .AutoRefresh(x => x.Accessibility)
            .Sort(SortExpressionComparer<IRequirement>.Ascending(x => x.Accessibility))
            .ToCollection()
            .Select(x => x.First().Accessibility)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Accessibility)
            .Select(x => x > AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Subscribe(_ => ChangePropagated?.Invoke(this, EventArgs.Empty))
            .DisposeWith(_disposables);
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
    }
}