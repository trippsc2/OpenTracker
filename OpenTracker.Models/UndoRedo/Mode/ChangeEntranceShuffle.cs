﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains undoable action data to change the entrance shuffle setting.
    /// </summary>
    public class ChangeEntranceShuffle : IChangeEntranceShuffle
    {
        private readonly IMode _mode;
        private readonly EntranceShuffle _newValue;

        private EntranceShuffle _previousValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new entrance shuffle setting.
        /// </param>
        public ChangeEntranceShuffle(IMode mode, EntranceShuffle newValue)
        {
            _mode = mode;
            _newValue = newValue;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _previousValue = _mode.EntranceShuffle;
            _mode.EntranceShuffle = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.EntranceShuffle = _previousValue;
        }
    }
}
