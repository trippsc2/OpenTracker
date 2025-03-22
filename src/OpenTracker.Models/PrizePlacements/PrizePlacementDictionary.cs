using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IPrizePlacement"/>
    /// objects indexed by <see cref="PrizePlacementID"/>.
    /// </summary>
    public class PrizePlacementDictionary : LazyDictionary<PrizePlacementID, IPrizePlacement>,
        IPrizePlacementDictionary
    {
        private readonly Lazy<IPrizePlacementFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating the <see cref="IPrizePlacementFactory"/> object.
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

        public IDictionary<PrizePlacementID, PrizePlacementSaveData> Save()
        {
            return Keys.ToDictionary(
                prizePlacement => prizePlacement,
                prizePlacement => this[prizePlacement].Save());
        }

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
