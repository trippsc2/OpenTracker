using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This base class contains boolean requirement data.
    /// </summary>
    public abstract class BooleanRequirement : ReactiveObject, IRequirement
    {
        private readonly AccessibilityLevel _metAccessibility;

        public event EventHandler? ChangePropagated;

        private bool _met;
        public bool Met
        {
            get => _met;
            private set
            {
                if (_met == value)
                {
                    return;
                }
                
                this.RaiseAndSetIfChanged(ref _met, value);
                ChangePropagated?.Invoke(this, EventArgs.Empty);
            }
        }

        public AccessibilityLevel Accessibility => Met ? _metAccessibility : AccessibilityLevel.None;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metAccessibility">
        /// The accessibility level when the condition is met.
        /// </param>
        protected BooleanRequirement(AccessibilityLevel metAccessibility = AccessibilityLevel.Normal)
        {
            _metAccessibility = metAccessibility;

            PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Raises the PropertyChanged and ChangePropagated events for all properties.
        /// </summary>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Met))
            {
                this.RaisePropertyChanged(nameof(Accessibility));
            }
        }

        /// <summary>
        /// Returns whether the condition is met.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the condition is met.
        /// </returns>
        protected abstract bool ConditionMet();

        /// <summary>
        /// Updates the value of the Met property.
        /// </summary>
        protected void UpdateValue()
        {
            Met = ConditionMet();
        }
    }
}
