using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.SequenceBreaks;

/// <summary>
/// This is the ViewModel of the sequence break control.
/// </summary>
[DependencyInjection]
public sealed class SequenceBreakControlVM : ViewModel, ISequenceBreakControlVM
{
    private ISequenceBreak SequenceBreak { get; }

    public string Text { get; }
    public string ToolTipText { get; }

    [ObservableAsProperty]
    public bool Enabled { get; }

    public ReactiveCommand<Unit, Unit> ToggleEnabledCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sequenceBreak">
    /// The sequence break to be represented.
    /// </param>
    /// <param name="text">
    /// A string representing the name of the sequence break.
    /// </param>
    /// <param name="toolTipText">
    /// A string representing the tooltip text of the sequence break.
    /// </param>
    public SequenceBreakControlVM(ISequenceBreak sequenceBreak, string text, string toolTipText)
    {
        SequenceBreak = sequenceBreak;
        Text = text;
        ToolTipText = toolTipText;
        
        ToggleEnabledCommand = ReactiveCommand.Create(ToggleEnabled);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.SequenceBreak.Enabled)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Enabled)
                .DisposeWith(disposables);
        });
    }

    private void ToggleEnabled()
    {
        SequenceBreak.Enabled = !SequenceBreak.Enabled;
    }
}