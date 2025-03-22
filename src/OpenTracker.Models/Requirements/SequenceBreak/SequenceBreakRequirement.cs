using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements.SequenceBreak
{
    /// <summary>
    /// This class contains <see cref="ISequenceBreak"/> <see cref="IRequirement"/> data.
    /// </summary>
    public class SequenceBreakRequirement : BooleanRequirement, ISequenceBreakRequirement
    {
        private readonly ISequenceBreak _sequenceBreak;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sequenceBreak">
        ///     The <see cref="ISequenceBreak"/>.
        /// </param>
        public SequenceBreakRequirement(ISequenceBreak sequenceBreak) : base(AccessibilityLevel.SequenceBreak)
        {
            _sequenceBreak = sequenceBreak;

            _sequenceBreak.PropertyChanged += OnSequenceBreakChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the <see cref="ISequenceBreak.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnSequenceBreakChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISequenceBreak.Enabled))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _sequenceBreak.Enabled;
        }
    }
}
