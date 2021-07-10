using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.Requirements.Static;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    /// This class contains the creation logic for <see cref="IRequirement"/> objects for boss type requirements.
    /// </summary>
    public class BossTypeRequirementFactory : IBossTypeRequirementFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IBossTypeRequirementDictionary _bossTypeRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly IItemExactRequirementDictionary _itemExactRequirements;
        private readonly IItemPlacementRequirementDictionary _itemPlacementRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        private readonly IStaticRequirementDictionary _staticRequirements;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
        /// </param>
        /// <param name="alternativeRequirements">
        ///     The <see cref="IAlternativeRequirementDictionary"/>.
        /// </param>
        /// <param name="bossTypeRequirements">
        ///     The <see cref="IBossTypeRequirementDictionary"/>.
        /// </param>
        /// <param name="complexRequirements">
        ///     The <see cref="IComplexRequirementDictionary"/>.
        /// </param>
        /// <param name="itemRequirements">
        ///     The <see cref="IItemRequirementDictionary"/>.
        /// </param>
        /// <param name="itemExactRequirements">
        ///     The <see cref="IItemExactRequirementDictionary"/>.
        /// </param>
        /// <param name="itemPlacementRequirements">
        ///     The <see cref="IItemPlacementRequirementDictionary"/>.
        /// </param>
        /// <param name="sequenceBreakRequirements">
        ///     The <see cref="ISequenceBreakRequirementDictionary"/>.
        /// </param>
        /// <param name="staticRequirements">
        ///     The <see cref="IStaticRequirementDictionary"/>.
        /// </param>
        public BossTypeRequirementFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IAlternativeRequirementDictionary alternativeRequirements,
            IBossTypeRequirementDictionary bossTypeRequirements, IComplexRequirementDictionary complexRequirements,
            IItemRequirementDictionary itemRequirements, IItemExactRequirementDictionary itemExactRequirements,
            IItemPlacementRequirementDictionary itemPlacementRequirements,
            ISequenceBreakRequirementDictionary sequenceBreakRequirements,
            IStaticRequirementDictionary staticRequirements)
        {
            _aggregateRequirements = aggregateRequirements;
            _alternativeRequirements = alternativeRequirements;
            _bossTypeRequirements = bossTypeRequirements;
            _complexRequirements = complexRequirements;
            _itemRequirements = itemRequirements;
            _itemExactRequirements = itemExactRequirements;
            _itemPlacementRequirements = itemPlacementRequirements;
            _sequenceBreakRequirements = sequenceBreakRequirements;
            _staticRequirements = staticRequirements;
        }

        public IRequirement GetBossTypeRequirement(BossType? type)
        {
            return type switch
            {
                BossType.Test => _staticRequirements[AccessibilityLevel.Normal],
                BossType.Armos => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Sword, 2)],
                    _itemRequirements[(ItemType.Hammer, 1)],
                    _itemRequirements[(ItemType.Bow, 1)],
                    _itemRequirements[(ItemType.Boomerang, 1)],
                    _itemRequirements[(ItemType.RedBoomerang, 1)],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _complexRequirements[ComplexRequirementType.ExtendMagic2],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemRequirements[(ItemType.FireRod, 1)],
                            _itemRequirements[(ItemType.IceRod, 1)]
                        }]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _complexRequirements[ComplexRequirementType.ExtendMagic1],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemRequirements[(ItemType.CaneOfByrna, 1)],
                            _itemRequirements[(ItemType.CaneOfSomaria, 1)]
                        }]
                    }]
                }],
                BossType.Lanmolas => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Sword, 2)],
                    _itemRequirements[(ItemType.Hammer, 1)],
                    _itemRequirements[(ItemType.Bow, 1)],
                    _itemRequirements[(ItemType.FireRod, 1)],
                    _itemRequirements[(ItemType.IceRod, 1)],
                    _itemRequirements[(ItemType.CaneOfByrna, 1)],
                    _itemRequirements[(ItemType.CaneOfSomaria, 1)],
                    _sequenceBreakRequirements[SequenceBreakType.LanmolasBombs]
                }],
                BossType.Moldorm => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Sword, 2)],
                    _itemRequirements[(ItemType.Hammer, 1)]
                }],
                BossType.HelmasaurKing => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Sword, 3)],
                    _itemRequirements[(ItemType.Bow, 1)],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Sword, 2)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemPlacementRequirements[ItemPlacement.Advanced],
                            _sequenceBreakRequirements[SequenceBreakType.HelmasaurKingBasic]
                        }]
                    }],
                }],
                BossType.Arrghus => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemExactRequirements[(ItemType.Sword, 0)],
                        _itemRequirements[(ItemType.Sword, 3)],
                        _sequenceBreakRequirements[SequenceBreakType.ArrghusBasic]
                    }],
                    _itemRequirements[(ItemType.Hookshot, 1)],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Hammer, 1)],
                        _itemRequirements[(ItemType.Sword, 2)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _complexRequirements[ComplexRequirementType.ExtendMagic1],
                                _itemRequirements[(ItemType.Bow, 1)]
                            }],
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _itemRequirements[(ItemType.FireRod, 1)],
                                _itemRequirements[(ItemType.IceRod, 1)]
                            }]
                        }]
                    }]
                }],
                BossType.Mothula => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemRequirements[(ItemType.Sword, 3)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _complexRequirements[ComplexRequirementType.ExtendMagic1],
                            _itemRequirements[(ItemType.FireRod, 1)]
                        }],
                        _sequenceBreakRequirements[SequenceBreakType.MothulaBasic]
                    }],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Sword, 2)],
                        _itemRequirements[(ItemType.Hammer, 1)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _complexRequirements[ComplexRequirementType.ExtendMagic1],
                            _itemRequirements[(ItemType.FireRod, 1)]
                        }],
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)],
                        _itemRequirements[(ItemType.CaneOfByrna, 1)]
                    }]
                }],
                BossType.Blind => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemExactRequirements[(ItemType.Sword, 0)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _itemRequirements[(ItemType.Sword, 2)],
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _itemRequirements[(ItemType.Cape, 1)],
                                _itemRequirements[(ItemType.CaneOfByrna, 1)]
                            }]
                        }],
                        _sequenceBreakRequirements[SequenceBreakType.BlindBasic]
                    }],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Sword, 2)],
                        _itemRequirements[(ItemType.Hammer, 1)],
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)],
                        _itemRequirements[(ItemType.CaneOfByrna, 1)]
                    }]
                }],
                BossType.Kholdstare => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemRequirements[(ItemType.Sword, 3)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _complexRequirements[ComplexRequirementType.ExtendMagic2],
                            _itemRequirements[(ItemType.FireRod, 1)]
                        }],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _itemRequirements[(ItemType.Bombos, 1)],
                            _complexRequirements[ComplexRequirementType.UseMedallion],
                            _complexRequirements[ComplexRequirementType.ExtendMagic1],
                            _itemRequirements[(ItemType.FireRod, 1)]
                        }],
                        _sequenceBreakRequirements[SequenceBreakType.KholdstareBasic]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _complexRequirements[ComplexRequirementType.MeltThings],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemRequirements[(ItemType.Hammer, 1)],
                            _itemRequirements[(ItemType.Sword, 2)],
                            _aggregateRequirements[new HashSet<IRequirement>
                            {
                                _complexRequirements[ComplexRequirementType.ExtendMagic2],
                                _itemRequirements[(ItemType.FireRod, 1)]
                            }],
                            _aggregateRequirements[new HashSet<IRequirement>
                            {
                                _itemExactRequirements[(ItemType.Sword, 0)],
                                _complexRequirements[ComplexRequirementType.ExtendMagic1],
                                _itemRequirements[(ItemType.FireRod, 1)],
                                _itemRequirements[(ItemType.Bombos, 1)]
                            }]
                        }]
                    }]
                }],
                BossType.Vitreous => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemRequirements[(ItemType.Sword, 3)],
                        _itemRequirements[(ItemType.Bow, 1)],
                        _sequenceBreakRequirements[SequenceBreakType.VitreousBasic]
                    }],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Hammer, 1)],
                        _itemRequirements[(ItemType.Sword, 2)],
                        _itemRequirements[(ItemType.Bow, 1)]
                    }]
                }],
                BossType.Trinexx => _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemExactRequirements[(ItemType.Sword, 0)],
                        _itemRequirements[(ItemType.Sword, 4)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _complexRequirements[ComplexRequirementType.ExtendMagic1],
                            _itemRequirements[(ItemType.Sword, 3)]
                        }],
                        _sequenceBreakRequirements[SequenceBreakType.TrinexxBasic]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.FireRod, 1)],
                        _itemRequirements[(ItemType.IceRod, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemRequirements[(ItemType.Sword, 4)],
                            _itemRequirements[(ItemType.Hammer, 1)],
                            _aggregateRequirements[new HashSet<IRequirement>
                            {
                                _complexRequirements[ComplexRequirementType.ExtendMagic1],
                                _itemRequirements[(ItemType.Sword, 3)]
                            }],
                            _aggregateRequirements[new HashSet<IRequirement>
                            {
                                _complexRequirements[ComplexRequirementType.ExtendMagic2],
                                _itemRequirements[(ItemType.Sword, 2)]
                            }]
                        }]
                    }]
                }],
                BossType.Aga => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Sword, 2)],
                    _itemRequirements[(ItemType.Hammer, 1)],
                    _itemRequirements[(ItemType.Net, 1)]
                }],
                null => _alternativeRequirements[new HashSet<IRequirement>
                {
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bossTypeRequirements[BossType.Armos],
                        _bossTypeRequirements[BossType.Lanmolas],
                        _bossTypeRequirements[BossType.Moldorm],
                        _bossTypeRequirements[BossType.HelmasaurKing],
                        _bossTypeRequirements[BossType.Arrghus],
                        _bossTypeRequirements[BossType.Mothula],
                        _bossTypeRequirements[BossType.Blind],
                        _bossTypeRequirements[BossType.Kholdstare],
                        _bossTypeRequirements[BossType.Vitreous],
                        _bossTypeRequirements[BossType.Trinexx]
                    }],
                    _staticRequirements[AccessibilityLevel.SequenceBreak]
                }],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}