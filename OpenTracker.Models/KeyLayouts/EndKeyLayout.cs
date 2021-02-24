using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the class containing the end key layout.
    /// </summary>
    public class EndKeyLayout : IKeyLayout
    {
        private readonly IRequirement _requirement;

        public delegate EndKeyLayout Factory(IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirement">
        /// The requirement for this key layout to be valid.
        /// </param>
        public EndKeyLayout(IRequirement requirement)
        {
            _requirement = requirement;
        }

        /// <summary>
        /// Returns whether the key layout is possible in the current game state.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon mutable data.
        /// </param>
        /// <param name="smallKeys">
        /// A 32-bit signed integer representing the number of small keys collected.
        /// </param>
        /// <param name="bigKey">
        /// A boolean representing whether the big key was collected.
        /// </param>
        /// <returns>
        /// A boolean representing whether the key layout is possible.
        /// </returns>
        public bool CanBeTrue(IMutableDungeon dungeonData, IDungeonState state)
        {
            return _requirement.Met;
        }
    }
}
