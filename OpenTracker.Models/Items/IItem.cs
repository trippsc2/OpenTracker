using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the interface for item data.
    /// </summary>
    public interface IItem : INotifyPropertyChanged
    {
        ItemType Type { get; }
        int Maximum { get; }
        int Current { get; set; }

        void Change(int delta, bool ignoreMaximum = false);
        void Reset();
        void SetCurrent(int current = 0);
        ItemSaveData Save();
        void Load(ItemSaveData saveData);
    }
}