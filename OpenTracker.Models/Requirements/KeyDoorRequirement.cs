using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.KeyDoors;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
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

        public KeyDoorRequirement(IKeyDoor keyDoor)
        {
            _keyDoor = keyDoor ?? throw new ArgumentNullException(nameof(keyDoor));

            _keyDoor.PropertyChanged += OnKeyDoorChanged;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
