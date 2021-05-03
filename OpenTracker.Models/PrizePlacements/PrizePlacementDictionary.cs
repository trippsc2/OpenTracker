using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    ///     This class contains the dictionary container for prize placement data.
    /// </summary>
    public class PrizePlacementDictionary : LazyDictionary<PrizePlacementID, IPrizePlacement>,
        IPrizePlacementDictionary
    {
        private readonly Lazy<IPrizePlacementFactory> _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     The prize placement factory.
        /// </param>
        public PrizePlacementDictionary(IPrizePlacementFactory.Factory factory)
            : base(new Dictionary<PrizePlacementID, IPrizePlacement>())
        {
            _factory = new Lazy<IPrizePlacementFactory>(() => factory());
        }

        public void Reset()
        {
            foreach (var placement in Values)
            {
                placement.Reset();
            }
        }

        /// <summary>
        ///     Returns a dictionary of prize placement save data.
        /// </summary>
        /// <returns>
        ///     A dictionary of prize placement save data.
        /// </returns>
        public IDictionary<PrizePlacementID, PrizePlacementSaveData> Save()
        {
            return Keys.ToDictionary(
                prizePlacement => prizePlacement,
                prizePlacement => this[prizePlacement].Save());
        }

        /// <summary>
        ///     Loads a dictionary of prize placement save data.
        /// </summary>
        public void Load(IDictionary<PrizePlacementID, PrizePlacementSaveData>? saveData)
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

        protected override IPrizePlacement Create(PrizePlacementID key)
        {
            return _factory.Value.GetPrizePlacement(key);
        }
    }
}
