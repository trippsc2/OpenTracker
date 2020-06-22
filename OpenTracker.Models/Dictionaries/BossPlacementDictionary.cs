using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dictionaries
{
    /// <summary>
    /// This is the dictionary container for boss placements.
    /// </summary>
    public class BossPlacementDictionary : Dictionary<BossPlacementID, BossPlacement>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public BossPlacementDictionary(Game game) : base()
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            foreach (BossPlacementID iD in Enum.GetValues(typeof(BossPlacementID)))
            {
                Add(iD, new BossPlacement(game, iD));
            }
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
    }
}
