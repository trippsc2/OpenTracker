using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the dictionary container for boss placements.
    /// </summary>
    public class BossPlacementDictionary : LazyDictionary<BossPlacementID, IBossPlacement>,
        IBossPlacementDictionary
    {
        private readonly Lazy<IBossPlacementFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// A factory for creating boss placements.
        /// </param>
        public BossPlacementDictionary(IBossPlacementFactory.Factory factory)
            : base(new Dictionary<BossPlacementID, IBossPlacement>())
        {
            _factory = new Lazy<IBossPlacementFactory>(() => factory());
        }

        protected override IBossPlacement Create(BossPlacementID key)
        {
            return _factory.Value.GetBossPlacement(key);
        }

        /// <summary>
        /// Resets the contained boss placements to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (var placement in Values)
            {
                placement.Reset();
            }
        }

        /// <summary>
        /// Returns a dictionary of boss placement save data.
        /// </summary>
        /// <returns>
        /// A dictionary of boss placement save data.
        /// </returns>
        public Dictionary<BossPlacementID, BossPlacementSaveData> Save()
        {
            Dictionary<BossPlacementID, BossPlacementSaveData> bossPlacements =
                new Dictionary<BossPlacementID, BossPlacementSaveData>();

            foreach (var bossPlacement in Keys)
            {
                bossPlacements.Add(bossPlacement, this[bossPlacement].Save());
            }

            return bossPlacements;
        }

        /// <summary>
        /// Loads a dictionary of boss placement save data.
        /// </summary>
        public void Load(Dictionary<BossPlacementID, BossPlacementSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var bossPlacement in saveData.Keys)
            {
                this[bossPlacement].Load(saveData[bossPlacement]);
            }
        }
    }
}
