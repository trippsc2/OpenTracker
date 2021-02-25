namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This generic interface contains methods saving and loading data.
    /// </summary>
    /// <typeparam name="T">
    /// The save data class.
    /// </typeparam>
    public interface ISaveable<T>
    {
        T Save();
        void Load(T saveData);
    }
}
