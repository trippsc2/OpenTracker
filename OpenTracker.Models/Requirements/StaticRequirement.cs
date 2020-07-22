using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for unchanging requirements, such as no requirement or
    /// inspect requirements.
    /// </summary>
    public class StaticRequirement : IRequirement
    {
        public bool Met =>
            true;

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

        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accessibility">
        /// The accessibility level of the requirement.
        /// </param>
        public StaticRequirement(AccessibilityLevel accessibility)
        {
            Accessibility = accessibility;
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
    }
}
