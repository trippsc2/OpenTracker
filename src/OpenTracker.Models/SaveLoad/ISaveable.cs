namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This generic interface contains logic for saving and loading data.
    /// </summary>
    /// <typeparam name="T">
    ///     The save data type.
    /// </typeparam>
    public interface ISaveable<T>
    {
        /// <summary>
        /// Returns save data.
        /// </summary>
        /// <returns>
        ///     The <see cref="T"/> save data from the object.
        /// </returns>
        T Save();
        
        /// <summary>
        /// Loads the provided save data.
        /// </summary>
        /// <param name="saveData">
        ///     The <see cref="T"/> save data.
        /// </param>
        void Load(T? saveData);
    }
}
