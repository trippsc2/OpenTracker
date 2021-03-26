using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This base class contains non-boolean requirement data.
    /// </summary>
    public abstract class AccessibilityRequirement : ReactiveObject, IRequirement
    {
        public event EventHandler? ChangePropagated;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private bool _testing;
        public bool Testing
        {
            get => _testing;
            set => this.RaiseAndSetIfChanged(ref _testing, value);
        }

        public bool Met => Accessibility > AccessibilityLevel.None;
        
        /// <summary>
        /// Constructor
        /// </summary>
        protected AccessibilityRequirement()
        {
            PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Raises the PropertyChanged and ChangePropagated events for all properties.
        /// </summary>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Accessibility):
                    this.RaisePropertyChanged(nameof(Met));
                    break;
                case nameof(Met):
                    ChangePropagated?.Invoke(this, EventArgs.Empty);
                    break;
                case nameof(Testing):
                    UpdateValue();
                    break;
            }
        }

        /// <summary>
        /// Returns the current accessibility of the requirement.
        /// </summary>
        /// <returns>
        /// The accessibility level.
        /// </returns>
        protected abstract AccessibilityLevel GetAccessibility();

        /// <summary>
        /// Updates the value of the Accessibility property.
        /// </summary>
        protected void UpdateValue()
        {
            Accessibility = Testing ? AccessibilityLevel.Normal : GetAccessibility();
        }
    }
}
