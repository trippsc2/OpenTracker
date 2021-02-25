using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This interface contains prize placement data.
    /// </summary>
    public interface IPrizePlacement : INotifyPropertyChanging, INotifyPropertyChanged,
        ISaveable<PrizePlacementSaveData>
    {
        IItem? Prize { get; set; }

        delegate IPrizePlacement Factory(IItem? startingPrize = null);

        bool CanCycle();
        void Cycle();
        void Reset();
    }
}