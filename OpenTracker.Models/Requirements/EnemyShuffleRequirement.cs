using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for enemy shuffle requirements.
    /// </summary>
    internal class EnemyShuffleRequirement : IRequirement
    {
        private readonly bool _enemyShuffle;

        public bool Met =>
            Accessibility != AccessibilityLevel.None;

        public event PropertyChangedEventHandler PropertyChanged;
        
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
        /// <param name="enemyShuffle">
        /// The required enemy shuffle value.
        /// </param>
        public EnemyShuffleRequirement(bool enemyShuffle)
        {
            _enemyShuffle = enemyShuffle;
            
            Mode.Instance.PropertyChanged += OnModeChanged;
            
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
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EnemyShuffle))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = Mode.Instance.EnemyShuffle == _enemyShuffle ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
