using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.SequenceBreaks
{
    /// <summary>
    /// This is the ViewModel of the sequence break control.
    /// </summary>
    public class SequenceBreakControlVM : ViewModelBase
    {
        private readonly SequenceBreak _sequenceBreak;

        public bool Enabled =>
            _sequenceBreak.Enabled;

        public string Text { get; }
        public string ToolTipText { get; }

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
        public SequenceBreakControlVM(
            SequenceBreak sequenceBreak, string text, string toolTipText)
        {
            _sequenceBreak = sequenceBreak ?? throw new ArgumentNullException(nameof(sequenceBreak));
            Text = text ?? throw new ArgumentNullException(nameof(text));
            ToolTipText = toolTipText ?? throw new ArgumentNullException(nameof(toolTipText));
            ToggleEnabledCommand = ReactiveCommand.Create(ToggleEnabled);

            _sequenceBreak.PropertyChanged += OnSequenceBreakChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the SequenceBreak class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSequenceBreakChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SequenceBreak.Enabled))
            {
                this.RaisePropertyChanged(nameof(Enabled));
            }
        }

        /// <summary>
        /// Toggles whether the sequence break is enabled.
        /// </summary>
        private void ToggleEnabled()
        {
            _sequenceBreak.Enabled = !_sequenceBreak.Enabled;
        }
    }
}
