using System.ComponentModel;
using OpenTracker.Models.Dungeons.KeyDoors;

namespace OpenTracker.Models.Requirements.KeyDoor
{
    /// <summary>
    /// This class contains <see cref="IKeyDoor"/> <see cref="IRequirement"/> data.
    /// </summary>
    public class KeyDoorRequirement : BooleanRequirement, IKeyDoorRequirement
    {
        private readonly IKeyDoor _keyDoor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keyDoor">
        ///     The <see cref="IKeyDoor"/>.
        /// </param>
        public KeyDoorRequirement(IKeyDoor keyDoor)
        {
            _keyDoor = keyDoor;

            _keyDoor.PropertyChanged += OnKeyDoorChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the <see cref="IKeyDoor.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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
