namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This interface contains undoable action data.
    /// </summary>
    public interface IUndoable
    {
        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the action can be executed.
        /// </returns>
        bool CanExecute();
        
        /// <summary>
        /// Does the action.
        /// </summary>
        void ExecuteDo();
        
        /// <summary>
        /// Undoes the action.
        /// </summary>
        void ExecuteUndo();
    }
}
