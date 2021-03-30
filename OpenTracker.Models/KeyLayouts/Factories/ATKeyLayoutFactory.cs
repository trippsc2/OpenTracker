using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Agahnim's Tower key layouts.
    /// </summary>
    public class ATKeyLayoutFactory : IATKeyLayoutFactory
    {
        private readonly IRequirementDictionary _requirements;

        private readonly EndKeyLayout.Factory _endFactory;
        private readonly SmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="endFactory">
        /// An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        /// An Autofac factory for creating small key layouts.
        /// </param>
        public ATKeyLayoutFactory(
            IRequirementDictionary requirements, EndKeyLayout.Factory endFactory,
            SmallKeyLayout.Factory smallKeyFactory)
        {
            _requirements = requirements;

            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }
        
        public List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new()
            {
                _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                _smallKeyFactory(2, new List<DungeonItemID> {DungeonItemID.ATRoom03, DungeonItemID.ATDarkMaze},
                    false, new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])},
                    dungeon, _requirements[RequirementType.KeyDropShuffleOffSmallKeyShuffleOff]),
                _smallKeyFactory(4,
                    new List<DungeonItemID>
                    {
                        DungeonItemID.ATRoom03,
                        DungeonItemID.ATDarkMaze,
                        DungeonItemID.ATDarkArcherDrop,
                        DungeonItemID.ATCircleOfPotsDrop
                    }, false,
                    new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                    _requirements[RequirementType.KeyDropShuffleOnSmallKeyShuffleOff])
            };
        }
    }
}