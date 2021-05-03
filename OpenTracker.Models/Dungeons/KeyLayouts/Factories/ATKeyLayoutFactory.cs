using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Agahnim's Tower key layouts.
    /// </summary>
    public class ATKeyLayoutFactory : IATKeyLayoutFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;

        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The aggregate requirement dictionary.
        /// </param>
        /// <param name="keyDropShuffleRequirements">
        ///     The key drop shuffle requirement dictionary.
        /// </param>
        /// <param name="smallKeyShuffleRequirements">
        ///     The small key shuffle requirement dictionary.
        /// </param>
        /// <param name="endFactory">
        ///     An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating small key layouts.
        /// </param>
        public ATKeyLayoutFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IEndKeyLayout.Factory endFactory,
            ISmallKeyLayout.Factory smallKeyFactory)
        {
            _aggregateRequirements = aggregateRequirements;
            _keyDropShuffleRequirements = keyDropShuffleRequirements;
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;

            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }
        
        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new List<IKeyLayout>
            {
                _endFactory(_smallKeyShuffleRequirements[true]),
                _smallKeyFactory(
                    2, new List<DungeonItemID> {DungeonItemID.ATRoom03, DungeonItemID.ATDarkMaze},
                    false, new List<IKeyLayout> {_endFactory()},
                    dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _keyDropShuffleRequirements[false],
                        _smallKeyShuffleRequirements[false]
                    }]),
                _smallKeyFactory(
                    4, new List<DungeonItemID>
                    {
                        DungeonItemID.ATRoom03,
                        DungeonItemID.ATDarkMaze,
                        DungeonItemID.ATDarkArcherDrop,
                        DungeonItemID.ATCircleOfPotsDrop
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _keyDropShuffleRequirements[true],
                        _smallKeyShuffleRequirements[false]
                    }])
            };
        }
    }
}