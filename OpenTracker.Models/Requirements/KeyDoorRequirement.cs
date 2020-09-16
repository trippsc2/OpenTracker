using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.KeyDoors;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for key door requirements.
    /// </summary>
    public class KeyDoorRequirement : IRequirement
    {
        private readonly IKeyDoor _keyDoor;

        public bool Met => 
            Accessibility != AccessibilityLevel.None;

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
        /// <param name="keyDoor">
        /// The key door for the requirement.
        /// </param>
        public KeyDoorRequirement(IKeyDoor keyDoor)
        {
            _keyDoor = keyDoor ?? throw new ArgumentNullException(nameof(keyDoor));

            _keyDoor.PropertyChanged += OnKeyDoorChanged;
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
        /// Subscribes to the PropertyChanged event on the IKeyDoor interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnKeyDoorChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IKeyDoor.Unlocked))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _keyDoor.Unlocked ? AccessibilityLevel.Normal :
                AccessibilityLevel.None;
        }
    }
}
