using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.SequenceBreaks;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for sequence break requirements.
    /// </summary>
    internal class SequenceBreakRequirement : IRequirement
    {
        private readonly SequenceBreak _sequenceBreak;

        public bool Met =>
            Accessibility != AccessibilityLevel.None;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ChangePropagated;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sequenceBreak">
        /// The sequence break required.
        /// </param>
        public SequenceBreakRequirement(SequenceBreak sequenceBreak)
        {
            _sequenceBreak = sequenceBreak ?? throw new ArgumentNullException(nameof(sequenceBreak));

            _sequenceBreak.PropertyChanged += OnSequenceBreakChanged;

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ChangePropagated?.Invoke(this, new EventArgs());
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
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _sequenceBreak.Enabled ?
                AccessibilityLevel.SequenceBreak : AccessibilityLevel.None;
        }
    }
}
