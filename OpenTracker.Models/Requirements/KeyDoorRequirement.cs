using OpenTracker.Models.KeyDoors;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for key door requirements.
    /// </summary>
    public class KeyDoorRequirement : BooleanRequirement
    {
        private readonly IKeyDoor _keyDoor;

        public delegate KeyDoorRequirement Factory(IKeyDoor keyDoor);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keyDoor">
        /// The key door for the requirement.
        /// </param>
        public KeyDoorRequirement(IKeyDoor keyDoor)
        {
            _keyDoor = keyDoor;

            _keyDoor.PropertyChanged += OnKeyDoorChanged;

            UpdateValue();
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
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _keyDoor.Unlocked;
        }
    }
}
