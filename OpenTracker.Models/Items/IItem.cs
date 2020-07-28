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

        bool CanAdd();
        bool CanRemove();
        void Reset();
        ItemSaveData Save();
        void Load(ItemSaveData saveData);
    }
}