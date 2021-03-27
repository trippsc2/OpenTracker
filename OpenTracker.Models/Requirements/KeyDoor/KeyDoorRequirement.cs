using System.ComponentModel;
using OpenTracker.Models.KeyDoors;

namespace OpenTracker.Models.Requirements.KeyDoor
{
    /// <summary>
    /// This class contains key door requirement data.
    /// </summary>
    public class KeyDoorRequirement : BooleanRequirement, IKeyDoorRequirement
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
        private void OnKeyDoorChanged(object? sender, PropertyChangedEventArgs e)
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
