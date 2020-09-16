namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the generic interface for assigning save data.
    /// </summary>
    /// <typeparam name="T">
    /// The class for save data.
    /// </typeparam>
    public interface ISaveable<T>
    {
        T Save();
        void Load(T saveData);
    }
}
