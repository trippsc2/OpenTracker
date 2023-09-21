using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.EnemyShuffle;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Item.SmallKey;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Requirements.Static;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Complex;

/// <summary>
/// This class contains the creation logic for complex <see cref="IRequirement"/> objects.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ComplexRequirementFactory : IComplexRequirementFactory
{
    private readonly IAggregateRequirementDictionary _aggregateRequirements;
    private readonly IAlternativeRequirementDictionary _alternativeRequirements;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IEnemyShuffleRequirementDictionary _enemyShuffleRequirements;
    private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly IItemExactRequirementDictionary _itemExactRequirements;
    private readonly IItemPlacementRequirementDictionary _itemPlacementRequirements;
    private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;
    private readonly IPrizeRequirementDictionary _prizeRequirements;
    private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
    private readonly ISmallKeyRequirementDictionary _smallKeyRequirements;
    private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
    private readonly IStaticRequirementDictionary _staticRequirements;
    private readonly IWorldStateRequirementDictionary _worldStateRequirements;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="aggregateRequirements">
    ///     The <see cref="IAggregateRequirementDictionary"/>.
    /// </param>
    /// <param name="alternativeRequirements">
    ///     The <see cref="IAlternativeRequirementDictionary"/>.
    /// </param>
    /// <param name="complexRequirements">
    ///     The <see cref="IComplexRequirementDictionary"/>.
    /// </param>
    /// <param name="enemyShuffleRequirements">
    ///     The <see cref="IEnemyShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="entranceShuffleRequirements">
    ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
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
    /// <param name="keyDropShuffleRequirements">
    ///     The <see cref="IKeyDropShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="prizeRequirements">
    ///     The <see cref="IPrizeRequirementDictionary"/>.
    /// </param>
    /// <param name="sequenceBreakRequirements">
    ///     The <see cref="ISequenceBreakRequirementDictionary"/>.
    /// </param>
    /// <param name="smallKeyRequirements">
    ///     The <see cref="ISmallKeyRequirementDictionary"/>.
    /// </param>
    /// <param name="smallKeyShuffleRequirements">
    ///     The <see cref="ISmallKeyShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="staticRequirements">
    ///     The <see cref="IStaticRequirementDictionary"/>.
    /// </param>
    /// <param name="worldStateRequirements">
    ///     The <see cref="IWorldStateRequirementDictionary"/>.
    /// </param>
    public ComplexRequirementFactory(
        IAggregateRequirementDictionary aggregateRequirements,
        IAlternativeRequirementDictionary alternativeRequirements,
        IComplexRequirementDictionary complexRequirements,
        IEnemyShuffleRequirementDictionary enemyShuffleRequirements,
        IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
        IItemRequirementDictionary itemRequirements, IItemExactRequirementDictionary itemExactRequirements,
        IItemPlacementRequirementDictionary itemPlacementRequirements,
        IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
        IPrizeRequirementDictionary prizeRequirements,
        ISequenceBreakRequirementDictionary sequenceBreakRequirements,
        ISmallKeyRequirementDictionary smallKeyRequirements,
        ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements,
        IStaticRequirementDictionary staticRequirements, IWorldStateRequirementDictionary worldStateRequirements)
    {
        _aggregateRequirements = aggregateRequirements;
        _alternativeRequirements = alternativeRequirements;
        _complexRequirements = complexRequirements;
        _enemyShuffleRequirements = enemyShuffleRequirements;
        _entranceShuffleRequirements = entranceShuffleRequirements;
        _itemRequirements = itemRequirements;
        _itemExactRequirements = itemExactRequirements;
        _itemPlacementRequirements = itemPlacementRequirements;
        _keyDropShuffleRequirements = keyDropShuffleRequirements;
        _prizeRequirements = prizeRequirements;
        _sequenceBreakRequirements = sequenceBreakRequirements;
        _smallKeyRequirements = smallKeyRequirements;
        _smallKeyShuffleRequirements = smallKeyShuffleRequirements;
        _staticRequirements = staticRequirements;
        _worldStateRequirements = worldStateRequirements;
    }

    public IRequirement GetComplexRequirement(ComplexRequirementType type)
    {
        return type switch
        {
            ComplexRequirementType.AllMedallions => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Bombos, 1)],
                _itemRequirements[(ItemType.Ether, 1)],
                _itemRequirements[(ItemType.Quake, 1)]
            }],
            ComplexRequirementType.ExtendMagic1 => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Bottle, 1)],
                _itemRequirements[(ItemType.HalfMagic, 1)]
            }],
            ComplexRequirementType.ExtendMagic2 => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Bottle, 1)],
                _itemRequirements[(ItemType.HalfMagic, 1)]
            }],
            ComplexRequirementType.FireRodDarkRoom => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.FireRod, 1)],
                _itemPlacementRequirements[ItemPlacement.Advanced]
            }],
            ComplexRequirementType.UseMedallion => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemExactRequirements[(ItemType.Sword, 0)],
                _itemRequirements[(ItemType.Sword, 2)]
            }],
            ComplexRequirementType.MeltThings => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.FireRod, 1)],
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Bombos, 1)],
                    _complexRequirements[ComplexRequirementType.UseMedallion]
                }]
            }],
            ComplexRequirementType.NotBunnyLW => _alternativeRequirements[new HashSet<IRequirement>
            {
                _worldStateRequirements[WorldState.StandardOpen],
                _itemRequirements[(ItemType.MoonPearl, 1)]
            }],
            ComplexRequirementType.NotBunnyDW => _alternativeRequirements[new HashSet<IRequirement>
            {
                _worldStateRequirements[WorldState.Inverted],
                _itemRequirements[(ItemType.MoonPearl, 1)]
            }],
            ComplexRequirementType.ATBarrier => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Cape, 1)],
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _itemExactRequirements[(ItemType.Sword, 0)],
                    _itemRequirements[(ItemType.Hammer, 1)]
                }],
                _itemRequirements[(ItemType.Sword, 3)]
            }],
            ComplexRequirementType.BombDuplicationAncillaOverload =>
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _sequenceBreakRequirements[SequenceBreakType.BombDuplicationAncillaOverload],
                    _itemRequirements[(ItemType.Bow, 1)],
                    _itemRequirements[(ItemType.CaneOfSomaria, 1)],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Boomerang, 1)],
                        _itemRequirements[(ItemType.RedBoomerang, 1)]
                    }]
                }],
            ComplexRequirementType.BombDuplicationMirror => _aggregateRequirements[new HashSet<IRequirement>
            {
                _sequenceBreakRequirements[SequenceBreakType.BombDuplicationMirror],
                _itemRequirements[(ItemType.Flippers, 1)],
                _itemRequirements[(ItemType.Mirror, 1)]
            }],
            ComplexRequirementType.BonkOverLedge => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Boots, 1)],
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemPlacementRequirements[ItemPlacement.Advanced],
                    _sequenceBreakRequirements[SequenceBreakType.BonkOverLedge]
                }]
            }],
            ComplexRequirementType.BumperCaveGap => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Hookshot, 1)],
                _itemPlacementRequirements[ItemPlacement.Advanced],
                _sequenceBreakRequirements[SequenceBreakType.BumperCaveHookshot]
            }],
            ComplexRequirementType.CameraUnlock => _aggregateRequirements[new HashSet<IRequirement>
            {
                _sequenceBreakRequirements[SequenceBreakType.CameraUnlock],
                _itemRequirements[(ItemType.Bottle, 1)]
            }],
            ComplexRequirementType.Curtains => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemExactRequirements[(ItemType.Sword, 0)],
                _itemRequirements[(ItemType.Sword, 2)]
            }],
            ComplexRequirementType.DarkRoomDeathMountainEntry => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomDeathMountainEntry]
            }],
            ComplexRequirementType.DarkRoomDeathMountainExit => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomDeathMountainExit]
            }],
            ComplexRequirementType.DarkRoomHC => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _complexRequirements[ComplexRequirementType.FireRodDarkRoom],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomHC]
            }],
            ComplexRequirementType.DarkRoomAT => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomAT]
            }],
            ComplexRequirementType.DarkRoomEPRight => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomEPRight]
            }],
            ComplexRequirementType.DarkRoomEPBack => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _complexRequirements[ComplexRequirementType.FireRodDarkRoom],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomEPBack]
            }],
            ComplexRequirementType.DarkRoomPoDDarkBasement => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _complexRequirements[ComplexRequirementType.FireRodDarkRoom],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomPoDDarkBasement]
            }],
            ComplexRequirementType.DarkRoomPoDDarkMaze => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomPoDDarkMaze]
            }],
            ComplexRequirementType.DarkRoomPoDBossArea => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomPoDBossArea]
            }],
            ComplexRequirementType.DarkRoomMM => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomMM]
            }],
            ComplexRequirementType.DarkRoomTR => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _sequenceBreakRequirements[SequenceBreakType.DarkRoomTR]
            }],
            ComplexRequirementType.FakeFlippersFairyRevival => _aggregateRequirements[new HashSet<IRequirement>
            {
                _sequenceBreakRequirements[SequenceBreakType.FakeFlippersFairyRevival],
                _itemRequirements[(ItemType.Bottle, 1)]
            }],
            ComplexRequirementType.FakeFlippersSplashDeletion => _aggregateRequirements[new HashSet<IRequirement>
            {
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Bow, 1)],
                    _itemRequirements[(ItemType.RedBoomerang, 1)],
                    _itemRequirements[(ItemType.CaneOfSomaria, 1)],
                    _itemRequirements[(ItemType.IceRod, 1)]
                }],
                _sequenceBreakRequirements[SequenceBreakType.FakeFlippersFairyRevival]
            }],
            ComplexRequirementType.FireSource => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Lamp, 1)],
                _itemRequirements[(ItemType.FireRod, 1)]
            }],
            ComplexRequirementType.Hover => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Boots, 1)],
                _sequenceBreakRequirements[SequenceBreakType.Hover]
            }],
            ComplexRequirementType.LaserBridge => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemPlacementRequirements[ItemPlacement.Advanced],
                _itemRequirements[(ItemType.Cape, 1)],
                _itemRequirements[(ItemType.CaneOfByrna, 1)],
                _itemRequirements[(ItemType.Shield, 3)],
                _sequenceBreakRequirements[SequenceBreakType.TRLaserSkip]
            }],
            ComplexRequirementType.MagicBat => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Powder, 1)],
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _sequenceBreakRequirements[SequenceBreakType.FakePowder],
                    _itemExactRequirements[(ItemType.Mushroom, 1)],
                    _itemRequirements[(ItemType.CaneOfSomaria, 1)]
                }]
            }],
            ComplexRequirementType.Pedestal => _alternativeRequirements[new HashSet<IRequirement>
            {
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _prizeRequirements[(PrizeType.Pendant, 2)],
                    _prizeRequirements[(PrizeType.GreenPendant, 1)],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _itemRequirements[(ItemType.Book, 1)],
                        _sequenceBreakRequirements[SequenceBreakType.BlindPedestal]
                    }]
                }]
            }],
            ComplexRequirementType.RedEyegoreGoriya => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Bow, 1)],
                _enemyShuffleRequirements[true]
            }],
            ComplexRequirementType.SpikeCave => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.CaneOfByrna, 1)],
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Cape, 1)],
                    _complexRequirements[ComplexRequirementType.ExtendMagic1]
                }]
            }],
            ComplexRequirementType.SuperBunnyMirror => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Mirror, 1)],
                _sequenceBreakRequirements[SequenceBreakType.SuperBunnyMirror]
            }],
            ComplexRequirementType.Tablet => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Book, 1)],
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.Sword, 3)],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemExactRequirements[(ItemType.Sword, 0)],
                        _itemRequirements[(ItemType.Hammer, 1)]
                    }]
                }]
            }],
            ComplexRequirementType.Torch => _alternativeRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Boots, 1)],
                _staticRequirements[AccessibilityLevel.Inspect]
            }],
            ComplexRequirementType.ToHHerapot => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Hookshot, 1)],
                _sequenceBreakRequirements[SequenceBreakType.ToHHerapot]
            }],
            ComplexRequirementType.IPIceBreaker => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.CaneOfSomaria, 1)],
                _sequenceBreakRequirements[SequenceBreakType.IPIceBreaker]
            }],
            ComplexRequirementType.MMMedallion => _aggregateRequirements[new HashSet<IRequirement>
            {
                _complexRequirements[ComplexRequirementType.UseMedallion],
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _complexRequirements[ComplexRequirementType.AllMedallions],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Bombos, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemExactRequirements[(ItemType.BombosDungeons, 1)],
                            _itemExactRequirements[(ItemType.BombosDungeons, 3)]
                        }]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Ether, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemExactRequirements[(ItemType.EtherDungeons, 1)],
                            _itemExactRequirements[(ItemType.EtherDungeons, 3)]
                        }]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Quake, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemExactRequirements[(ItemType.QuakeDungeons, 1)],
                            _itemExactRequirements[(ItemType.QuakeDungeons, 3)]
                        }]
                    }],
                }]
            }],
            ComplexRequirementType.TRMedallion => _aggregateRequirements[new HashSet<IRequirement>
            {
                _complexRequirements[ComplexRequirementType.UseMedallion],
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _complexRequirements[ComplexRequirementType.AllMedallions],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Bombos, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemExactRequirements[(ItemType.BombosDungeons, 2)],
                            _itemExactRequirements[(ItemType.BombosDungeons, 3)]
                        }]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Ether, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemExactRequirements[(ItemType.EtherDungeons, 2)],
                            _itemExactRequirements[(ItemType.EtherDungeons, 3)]
                        }]
                    }],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.Quake, 1)],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _itemExactRequirements[(ItemType.QuakeDungeons, 2)],
                            _itemExactRequirements[(ItemType.QuakeDungeons, 3)]
                        }]
                    }],
                }]
            }],
            ComplexRequirementType.TRKeyDoorsToMiddleExit => _alternativeRequirements[new HashSet<IRequirement>
            {
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _smallKeyShuffleRequirements[true],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _smallKeyRequirements[(DungeonID.TurtleRock, 3)],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _keyDropShuffleRequirements[false],
                            _smallKeyRequirements[(DungeonID.TurtleRock, 2)]
                        }]
                    }]
                }],
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _smallKeyShuffleRequirements[false],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _itemRequirements[(ItemType.FireRod, 1)],
                        _staticRequirements[AccessibilityLevel.SequenceBreak]
                    }]
                }]
            }],
            ComplexRequirementType.WaterWalk => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Boots, 1)],
                _sequenceBreakRequirements[SequenceBreakType.WaterWalk]
            }],
            ComplexRequirementType.WaterWalkFromWaterfallCave => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemExactRequirements[(ItemType.Flippers, 0)],
                _sequenceBreakRequirements[SequenceBreakType.WaterWalkFromWaterfallCave],
                _alternativeRequirements[new HashSet<IRequirement>
                {
                    _itemRequirements[(ItemType.MoonPearl, 1)],
                    _entranceShuffleRequirements[EntranceShuffle.All],
                    _entranceShuffleRequirements[EntranceShuffle.Insanity]
                }]
            }],
            ComplexRequirementType.LWMirror => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Mirror, 1)],
                _worldStateRequirements[WorldState.Inverted]
            }],
            ComplexRequirementType.DWMirror => _aggregateRequirements[new HashSet<IRequirement>
            {
                _itemRequirements[(ItemType.Mirror, 1)],
                _worldStateRequirements[WorldState.StandardOpen]
            }],
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}