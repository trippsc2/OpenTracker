using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Reset
{
    /// <summary>
    /// This is the class for resetting the tracker.
    /// </summary>
    public class ResetManager : IResetManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResetManager()
        {
        }

        /// <summary>
        /// Executes resetting the tracker.
        /// </summary>
        public void Reset()
        {
            UndoRedoManager.Instance.Reset();
            PinnedLocationCollection.Instance.Clear();
            AutoTracker.Instance.Stop();
            BossPlacementDictionary.Instance.Reset();
            LocationDictionary.Instance.Reset();
            PrizePlacementDictionary.Instance.Reset();
            ItemDictionary.Instance.Reset();
            ConnectionCollection.Instance.Clear();
        }
    }
}
