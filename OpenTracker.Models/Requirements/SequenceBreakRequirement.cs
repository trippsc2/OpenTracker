using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains sequence break requirement data.
    /// </summary>
    public class SequenceBreakRequirement : BooleanRequirement
    {
        private readonly ISequenceBreak _sequenceBreak;

        public delegate SequenceBreakRequirement Factory(ISequenceBreak sequenceBreak);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sequenceBreak">
        /// The sequence break required.
        /// </param>
        public SequenceBreakRequirement(ISequenceBreak sequenceBreak) : base(AccessibilityLevel.SequenceBreak)
        {
            _sequenceBreak = sequenceBreak;

            _sequenceBreak.PropertyChanged += OnSequenceBreakChanged;

            UpdateValue();
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
