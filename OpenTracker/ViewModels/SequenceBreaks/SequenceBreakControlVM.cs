using System.ComponentModel;
using System.Reactive;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.SequenceBreaks;

/// <summary>
/// This is the ViewModel of the sequence break control.
/// </summary>
[DependencyInjection]
public sealed class SequenceBreakControlVM : ViewModel, ISequenceBreakControlVM
{
    private readonly ISequenceBreak _sequenceBreak;

    public bool Enabled => _sequenceBreak.Enabled;

    public string Text { get; }
    public string ToolTipText { get; }

    public ReactiveCommand<Unit, Unit> ToggleEnabled { get; }

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
        _sequenceBreak = sequenceBreak;
            
        Text = text;
        ToolTipText = toolTipText;

        ToggleEnabled = ReactiveCommand.Create(ToggleEnabledImpl);

        _sequenceBreak.PropertyChanged += OnSequenceBreakChanged;
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the ISequenceBreak interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnSequenceBreakChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISequenceBreak.Enabled))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Enabled)));
        }
    }

    /// <summary>
    /// Toggles whether the sequence break is enabled.
    /// </summary>
    private void ToggleEnabledImpl()
    {
        _sequenceBreak.Enabled = !_sequenceBreak.Enabled;
    }
}