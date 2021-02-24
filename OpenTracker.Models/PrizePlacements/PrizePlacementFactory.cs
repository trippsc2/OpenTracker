using OpenTracker.Models.Items;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This is the class for creating prize placement classes.
    /// </summary>
    public class PrizePlacementFactory : IPrizePlacementFactory
    {
        private readonly IPrizeDictionary _prizes;
        private readonly IPrizePlacement.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// A factory that creates prize placements.
        /// </param>
        public PrizePlacementFactory(IPrizeDictionary prizes, IPrizePlacement.Factory factory)
        {
            _prizes = prizes;
            _factory = factory;
        }

        /// <summary>
        /// Returns a new prize placement instance for the specified prize placement ID.
        /// </summary>
        /// <param name="id">
        /// The prize placement ID.
        /// </param>
        /// <returns>
        /// A new prize placement instance.
        /// </returns>
        public IPrizePlacement GetPrizePlacement(PrizePlacementID id)
        {
            return id switch
            {
                PrizePlacementID.ATPrize => _factory(_prizes[PrizeType.Aga1]),
                PrizePlacementID.GTPrize => _factory(_prizes[PrizeType.Aga2]),
                _ => _factory()
            };
        }
    }
}
