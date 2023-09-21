using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Node;

/// <summary>
/// This class containing <see cref="INode"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class NodeRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private INode Node { get; }
    
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    [ObservableAsProperty]
    public bool Met { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="NodeRequirement"/> objects.
    /// </summary>
    public delegate NodeRequirement Factory(INode node);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="node">
    ///     The <see cref="INode"/>.
    /// </param>
    public NodeRequirement(INode node)
    {
        Node = node;

        this.WhenAnyValue(x => x.Node.Accessibility)
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