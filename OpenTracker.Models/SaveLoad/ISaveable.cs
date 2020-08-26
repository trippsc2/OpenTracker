namespace OpenTracker.Models.SaveLoad
{
    public interface ISaveable<T>
    {
        T Save();
        void Load(T saveData);
    }
}
