using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Tower of Hera key layouts.
    /// </summary>
    public class ToHKeyLayoutFactory : IToHKeyLayoutFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
        private readonly IGuaranteedBossItemsRequirementDictionary _guaranteedBossItemsRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        
        private readonly IBigKeyLayout.Factory _bigKeyFactory;
        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
        /// </param>
        /// <param name="bigKeyShuffleRequirements">
        ///     The <see cref="IBigKeyShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="guaranteedBossItemsRequirements">
        ///     The <see cref="IGuaranteedBossItemsRequirementDictionary"/>.
        /// </param>
        /// <param name="smallKeyShuffleRequirements">
        ///     The <see cref="ISmallKeyShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="bigKeyFactory">
        ///     An Autofac factory for creating new <see cref="IBigKeyLayout"/> objects.
        /// </param>
        /// <param name="endFactory">
        ///     An Autofac factory for creating new <see cref="IEndKeyLayout"/> objects.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating new <see cref="ISmallKeyLayout"/> objects.
        /// </param>
        public ToHKeyLayoutFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
            IGuaranteedBossItemsRequirementDictionary guaranteedBossItemsRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IBigKeyLayout.Factory bigKeyFactory,
            IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
        {
            _aggregateRequirements = aggregateRequirements;
            _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
            _guaranteedBossItemsRequirements = guaranteedBossItemsRequirements;
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;

            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }

        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new List<IKeyLayout>
            {
                _endFactory(_aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _smallKeyShuffleRequirements[true]
                }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID>
                    {
                        DungeonItemID.ToHBasementCage,
                        DungeonItemID.ToHMapChest,
                        DungeonItemID.ToHBigKeyChest,
                        DungeonItemID.ToHCompassChest,
                        DungeonItemID.ToHBigChest,
                        DungeonItemID.ToHBoss
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _guaranteedBossItemsRequirements[false]
                    }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID>
                    {
                        DungeonItemID.ToHBasementCage,
                        DungeonItemID.ToHMapChest,
                        DungeonItemID.ToHBigKeyChest,
                        DungeonItemID.ToHCompassChest,
                        DungeonItemID.ToHBigChest
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _guaranteedBossItemsRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.ToHBasementCage, DungeonItemID.ToHMapChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.ToHBasementCage,
                                DungeonItemID.ToHMapChest,
                                DungeonItemID.ToHBigKeyChest,
                                DungeonItemID.ToHCompassChest,
                                DungeonItemID.ToHBigChest,
                                DungeonItemID.ToHBoss
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _guaranteedBossItemsRequirements[false]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.ToHBasementCage,
                                DungeonItemID.ToHMapChest,
                                DungeonItemID.ToHBigKeyChest,
                                DungeonItemID.ToHCompassChest,
                                DungeonItemID.ToHBigChest
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _guaranteedBossItemsRequirements[true])
                    },
                    _bigKeyShuffleRequirements[false]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.ToHBigKeyChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.ToHBasementCage,
                                DungeonItemID.ToHMapChest
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _bigKeyShuffleRequirements[false])
            };
        }
    }
}