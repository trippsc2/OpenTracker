using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This class contains the dictionary container for prize placement data.
    /// </summary>
    public class PrizePlacementDictionary : LazyDictionary<PrizePlacementID, IPrizePlacement>,
        IPrizePlacementDictionary
    {
        private readonly Lazy<IPrizePlacementFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The prize placement factory.
        /// </param>
        public PrizePlacementDictionary(IPrizePlacementFactory.Factory factory)
            : base(new Dictionary<PrizePlacementID, IPrizePlacement>())
        {
            _factory = new Lazy<IPrizePlacementFactory>(() => factory());
        }

        protected override IPrizePlacement Create(PrizePlacementID key)
        {
            return _factory.Value.GetPrizePlacement(key);
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
        /// Returns a dictionary of prize placement save data.
        /// </summary>
        /// <returns>
        /// A dictionary of prize placement save data.
        /// </returns>
        public Dictionary<PrizePlacementID, PrizePlacementSaveData> Save()
        {
            Dictionary<PrizePlacementID, PrizePlacementSaveData> prizePlacements =
                new Dictionary<PrizePlacementID, PrizePlacementSaveData>();

            foreach (var prizePlacement in Keys)
            {
                prizePlacements.Add(prizePlacement, this[prizePlacement].Save());
            }

            return prizePlacements;
        }

        /// <summary>
        /// Loads a dictionary of prize placement save data.
        /// </summary>
        public void Load(Dictionary<PrizePlacementID, PrizePlacementSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var prizePlacement in saveData.Keys)
            {
                this[prizePlacement].Load(saveData[prizePlacement]);
            }
        }
    }
}
