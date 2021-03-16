using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This interface contains prize placement data.
    /// </summary>
    public interface IPrizePlacement : IReactiveObject, ISaveable<PrizePlacementSaveData>
    {
        IItem? Prize { get; set; }

        delegate IPrizePlacement Factory(IItem? startingPrize = null);

        bool CanCycle();
        void Cycle();
        void Reset();
    }
}