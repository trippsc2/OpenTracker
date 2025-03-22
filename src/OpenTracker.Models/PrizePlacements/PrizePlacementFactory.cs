using OpenTracker.Models.Prizes;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This class contains the creation logic for <see cref="IPrizePlacement"/> objects.
    /// </summary>
    public class PrizePlacementFactory : IPrizePlacementFactory
    {
        private readonly IPrizeDictionary _prizes;
        private readonly IPrizePlacement.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizes">
        ///     The <see cref="IPrizeDictionary"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IPrizePlacement"/> objects.
        /// </param>
        public PrizePlacementFactory(IPrizeDictionary prizes, IPrizePlacement.Factory factory)
        {
            _prizes = prizes;
            _factory = factory;
        }

        /// <summary>
        /// Returns a new <see cref="IPrizePlacement"/> object for the specified <see cref="PrizePlacementID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="PrizePlacementID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IPrizePlacement"/> object.
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
