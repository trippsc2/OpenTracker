using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This is the interface for prize placements.
    /// </summary>
    public interface IPrizePlacement : INotifyPropertyChanging, INotifyPropertyChanged,
        ISaveable<PrizePlacementSaveData>
    {
        IItem Prize { get; set; }

        bool CanCycle();
        void Cycle();
        void Reset();
    }
}