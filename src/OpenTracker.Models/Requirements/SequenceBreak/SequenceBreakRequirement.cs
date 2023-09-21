using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.SequenceBreak;

/// <summary>
/// This class contains <see cref="ISequenceBreak"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class SequenceBreakRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private ISequenceBreak SequenceBreak { get; }

    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="SequenceBreakRequirement"/> objects.
    /// </summary>
    public delegate SequenceBreakRequirement Factory(ISequenceBreak sequenceBreak);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sequenceBreak">
    ///     The <see cref="ISequenceBreak"/>.
    /// </param>
    public SequenceBreakRequirement(ISequenceBreak sequenceBreak)
    {
        SequenceBreak = sequenceBreak;

        this.WhenAnyValue(x => x.SequenceBreak.Enabled)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.SequenceBreak : AccessibilityLevel.None)
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