using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This is the interface for prize placements.
    /// </summary>
    public interface IPrizePlacement : INotifyPropertyChanging, INotifyPropertyChanged
    {
        IItem Prize { get; set; }

        void Load(PrizePlacementSaveData saveData);
        void Reset();
        PrizePlacementSaveData Save();
    }
}