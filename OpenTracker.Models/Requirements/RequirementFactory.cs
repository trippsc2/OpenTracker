using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.Multiple;
using OpenTracker.Models.Requirements.Node;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains creation logic for requirement data.
    /// </summary>
    public class RequirementFactory : IRequirementFactory
    {
        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IItemDictionary _items;
        private readonly IPrizeDictionary _prizes;
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _requirementNodes;
        private readonly ISequenceBreakDictionary _sequenceBreaks;
        private readonly AggregateRequirement.Factory _aggregateFactory;
        private readonly AlternativeRequirement.Factory _alternativeFactory;
        private readonly BigKeyShuffleRequirement.Factory _bigKeyShuffleFactory;
        private readonly IBossRequirement.Factory _bossFactory;
        private readonly BossShuffleRequirement.Factory _bossShuffleFactory;
        private readonly CompassShuffleRequirement.Factory _compassShuffleFactory;
        private readonly ICrystalRequirement.Factory _crystalFactory;
        private readonly EnemyShuffleRequirement.Factory _enemyShuffleFactory;
        private readonly EntranceShuffleRequirement.Factory _entranceShuffleFactory;
        private readonly GenericKeysRequirement.Factory _genericKeysFactory;
        private readonly GuaranteedBossItemsRequirement.Factory _guaranteedBossItemsFactory;
        private readonly IItemExactRequirement.Factory _itemExactFactory;
        private readonly ItemPlacementRequirement.Factory _itemPlacementFactory;
        private readonly IItemRequirement.Factory _itemFactory;
        private readonly KeyDropShuffleRequirement.Factory _keyDropShuffleFactory;
        private readonly MapShuffleRequirement.Factory _mapShuffleFactory;
        private readonly RaceIllegalTrackingRequirement.Factory _raceIllegalTrackingFactory;
        private readonly NodeRequirement.Factory _requirementNodeFactory;
        private readonly SequenceBreakRequirement.Factory _sequenceBreakFactory;
        private readonly ShopShuffleRequirement.Factory _shopShuffleFactory;
        private readonly ISmallKeyRequirement.Factory _smallKeyFactory;
        private readonly SmallKeyShuffleRequirement.Factory _smallKeyShuffleFactory;
        private readonly StaticRequirement.Factory _staticFactory;
        private readonly TakeAnyLocationsRequirement.Factory _takeAnyLocationsFactory;
        private readonly WorldStateRequirement.Factory _worldStateFactory;

        public delegate IRequirementFactory Factory(IRequirementDictionary requirements);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacements">
        /// The boss placement dictionary.
        /// </param>
        /// <param name="items">
        /// The item dictionary.
        /// </param>
        /// <param name="prizes">
        /// The prize dictionary.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        /// <param name="sequenceBreaks">
        /// The sequence break dictionary.
        /// </param>
        /// <param name="aggregateFactory">
        /// An Autofac factory for creating aggregate requirements.
        /// </param>
        /// <param name="alternativeFactory">
        /// An Autofac factory for creating alternative requirements.
        /// </param>
        /// <param name="bigKeyShuffleFactory">
        /// An Autofac factory for creating big key shuffle requirements.
        /// </param>
        /// <param name="bossFactory">
        /// An Autofac factory for creating boss placement requirements.
        /// </param>
        /// <param name="bossShuffleFactory">
        /// An Autofac factory for creating boss shuffle requirements.
        /// </param>
        /// <param name="compassShuffleFactory">
        /// An Autofac factory for creating compass shuffle requirements.
        /// </param>
        /// <param name="crystalFactory">
        /// An Autofac factory for creating crystal requirements.
        /// </param>
        /// <param name="enemyShuffleFactory">
        /// An Autofac factory for creating enemy shuffle requirements.
        /// </param>
        /// <param name="entranceShuffleFactory">
        /// An Autofac factory for creating entrance shuffle requirements.
        /// </param>
        /// <param name="genericKeysFactory">
        /// An Autofac factory for creating generic keys requirements.
        /// </param>
        /// <param name="guaranteedBossItemsFactory">
        /// An Autofac factory for creating guaranteed boss items requirements.
        /// </param>
        /// <param name="itemExactFactory">
        /// An Autofac factory for creating item exact amount requirements.
        /// </param>
        /// <param name="itemPlacementFactory">
        /// An Autofac factory for creating item placement requirements.
        /// </param>
        /// <param name="itemFactory">
        /// An Autofac factory for creating item requirements.
        /// </param>
        /// <param name="keyDropShuffleFactory">
        /// An Autofac factory for creating key drop shuffle requirements.
        /// </param>
        /// <param name="mapShuffleFactory">
        /// An Autofac factory for creating map shuffle requirements.
        /// </param>
        /// <param name="raceIllegalTrackingFactory">
        /// An Autofac factory for creating race illegal tracking requirements.
        /// </param>
        /// <param name="requirementNodeFactory">
        /// An Autofac factory for creating requirement node requirements.
        /// </param>
        /// <param name="sequenceBreakFactory">
        /// An Autofac factory for creating sequence break requirements.
        /// </param>
        /// <param name="shopShuffleFactory">
        /// An Autofac factory for creating shop shuffle requirements.
        /// </param>
        /// <param name="smallKeyFactory">
        /// An Autofac factory for creating small key requirements.
        /// </param>
        /// <param name="smallKeyShuffleFactory">
        /// An Autofac factory for creating small key shuffle requirements.
        /// </param>
        /// <param name="staticFactory">
        /// An Autofac factory for creating static requirements.
        /// </param>
        /// <param name="takeAnyLocationsFactory">
        /// An Autofac factory for creating take any locations requirements.
        /// </param>
        /// <param name="worldStateFactory">
        /// An Autofac factory for creating world state requirements.
        /// </param>
        /// <param name="requirements">
        /// The requirements dictionary.
        /// </param>
        public RequirementFactory(
            IBossPlacementDictionary bossPlacements, IItemDictionary items,
            IPrizeDictionary prizes,  IOverworldNodeDictionary requirementNodes,
            ISequenceBreakDictionary sequenceBreaks, AggregateRequirement.Factory aggregateFactory,
            AlternativeRequirement.Factory alternativeFactory,
            BigKeyShuffleRequirement.Factory bigKeyShuffleFactory,
            IBossRequirement.Factory bossFactory, BossShuffleRequirement.Factory bossShuffleFactory,
            CompassShuffleRequirement.Factory compassShuffleFactory,
            ICrystalRequirement.Factory crystalFactory,
            EnemyShuffleRequirement.Factory enemyShuffleFactory,
            EntranceShuffleRequirement.Factory entranceShuffleFactory,
            GenericKeysRequirement.Factory genericKeysFactory,
            GuaranteedBossItemsRequirement.Factory guaranteedBossItemsFactory,
            IItemExactRequirement.Factory itemExactFactory,
            ItemPlacementRequirement.Factory itemPlacementFactory,
            IItemRequirement.Factory itemFactory,
            KeyDropShuffleRequirement.Factory keyDropShuffleFactory,
            MapShuffleRequirement.Factory mapShuffleFactory,
            RaceIllegalTrackingRequirement.Factory raceIllegalTrackingFactory,
            NodeRequirement.Factory requirementNodeFactory,
            SequenceBreakRequirement.Factory sequenceBreakFactory,
            ShopShuffleRequirement.Factory shopShuffleFactory,
            ISmallKeyRequirement.Factory smallKeyFactory,
            SmallKeyShuffleRequirement.Factory smallKeyShuffleFactory,
            StaticRequirement.Factory staticFactory,
            TakeAnyLocationsRequirement.Factory takeAnyLocationsFactory,
            WorldStateRequirement.Factory worldStateFactory,
            IRequirementDictionary requirements)
        {
            _bossPlacements = bossPlacements;
            _items = items;
            _prizes = prizes;
            _requirements = requirements;
            _requirementNodes = requirementNodes;
            _sequenceBreaks = sequenceBreaks;
            _aggregateFactory = aggregateFactory;
            _alternativeFactory = alternativeFactory;
            _bigKeyShuffleFactory = bigKeyShuffleFactory;
            _bossFactory = bossFactory;
            _bossShuffleFactory = bossShuffleFactory;
            _compassShuffleFactory = compassShuffleFactory;
            _crystalFactory = crystalFactory;
            _enemyShuffleFactory = enemyShuffleFactory;
            _entranceShuffleFactory = entranceShuffleFactory;
            _genericKeysFactory = genericKeysFactory;
            _guaranteedBossItemsFactory = guaranteedBossItemsFactory;
            _itemExactFactory = itemExactFactory;
            _itemPlacementFactory = itemPlacementFactory;
            _itemFactory = itemFactory;
            _keyDropShuffleFactory = keyDropShuffleFactory;
            _mapShuffleFactory = mapShuffleFactory;
            _raceIllegalTrackingFactory = raceIllegalTrackingFactory;
            _requirementNodeFactory = requirementNodeFactory;
            _sequenceBreakFactory = sequenceBreakFactory;
            _shopShuffleFactory = shopShuffleFactory;
            _smallKeyFactory = smallKeyFactory;
            _smallKeyShuffleFactory = smallKeyShuffleFactory;
            _staticFactory = staticFactory;
            _takeAnyLocationsFactory = takeAnyLocationsFactory;
            _worldStateFactory = worldStateFactory;
        }

        /// <summary>
        /// Returns a static requirement accessibility.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A static accessibility level.
        /// </returns>
        private static AccessibilityLevel GetStaticAccessibility(RequirementType type)
        {
            return type switch
            {
                RequirementType.NoRequirement => AccessibilityLevel.Normal,
                RequirementType.Inspect => AccessibilityLevel.Inspect,
                RequirementType.SequenceBreak => AccessibilityLevel.SequenceBreak,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns the expected value of an item placement requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A item placement expected value.
        /// </returns>
        private static ItemPlacement GetItemPlacementValue(RequirementType type)
        {
            return type switch
            {
                RequirementType.ItemPlacementBasic => ItemPlacement.Basic,
                RequirementType.ItemPlacementAdvanced => ItemPlacement.Advanced,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns the boolean expected value of a requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A boolean expected value.
        /// </returns>
        private static bool GetBooleanValue(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.MapShuffleOff:
                case RequirementType.CompassShuffleOff:
                case RequirementType.SmallKeyShuffleOff:
                case RequirementType.BigKeyShuffleOff:
                case RequirementType.BossShuffleOff:
                case RequirementType.EnemyShuffleOff:
                case RequirementType.GuaranteedBossItemsOff:
                case RequirementType.KeyDropShuffleOff:
                    {
                        return false;
                    }
                case RequirementType.MapShuffleOn:
                case RequirementType.CompassShuffleOn:
                case RequirementType.SmallKeyShuffleOn:
                case RequirementType.BigKeyShuffleOn:
                case RequirementType.BossShuffleOn:
                case RequirementType.EnemyShuffleOn:
                case RequirementType.GuaranteedBossItemsOn:
                case RequirementType.GenericKeys:
                case RequirementType.TakeAnyLocations:
                case RequirementType.KeyDropShuffleOn:
                case RequirementType.ShopShuffle:
                case RequirementType.RaceIllegalTracking:
                    {
                        return true;
                    }
            }

         throw new ArgumentOutOfRangeException(nameof(type));
        }

        /// <summary>
        /// Returns the expected value of a world state requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A world state expected value.
        /// </returns>
        private static WorldState GetWorldStateValue(RequirementType type)
        {
            return type switch
            {
                RequirementType.WorldStateStandardOpen => WorldState.StandardOpen,
                RequirementType.WorldStateInverted => WorldState.Inverted,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a entrance shuffle requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A entrance shuffle requirement.
        /// </returns>
        private static EntranceShuffle GetEntranceShuffleValue(RequirementType type)
        {
            return type switch
            {
                RequirementType.EntranceShuffleNone => EntranceShuffle.None,
                RequirementType.EntranceShuffleDungeon => EntranceShuffle.Dungeon,
                RequirementType.EntranceShuffleAll => EntranceShuffle.All,
                RequirementType.EntranceShuffleInsanity => EntranceShuffle.Insanity,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns the item associated with the specified requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// The item.
        /// </returns>
        private IItem GetItem(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.Swordless:
                case RequirementType.Sword1:
                case RequirementType.Sword2:
                case RequirementType.Sword3:
                    {
                        return _items[ItemType.Sword];
                    }
                case RequirementType.Shield3:
                    {
                        return _items[ItemType.Shield];
                    }
                case RequirementType.Bow:
                    {
                        return _items[ItemType.Bow];
                    }
                case RequirementType.Boomerang:
                    {
                        return _items[ItemType.Boomerang];
                    }
                case RequirementType.RedBoomerang:
                    {
                        return _items[ItemType.RedBoomerang];
                    }
                case RequirementType.Hookshot:
                    {
                        return _items[ItemType.Hookshot];
                    }
                case RequirementType.Powder:
                    {
                        return _items[ItemType.Powder];
                    }
                case RequirementType.Mushroom:
                    {
                        return _items[ItemType.Mushroom];
                    }
                case RequirementType.Boots:
                    {
                        return _items[ItemType.Boots];
                    }
                case RequirementType.FireRod:
                    {
                        return _items[ItemType.FireRod];
                    }
                case RequirementType.IceRod:
                    {
                        return _items[ItemType.IceRod];
                    }
                case RequirementType.Bombos:
                    {
                        return _items[ItemType.Bombos];
                    }
                case RequirementType.BombosMM:
                case RequirementType.BombosTR:
                case RequirementType.BombosBoth:
                    {
                        return _items[ItemType.BombosDungeons];
                    }
                case RequirementType.Ether:
                    {
                        return _items[ItemType.Ether];
                    }
                case RequirementType.EtherMM:
                case RequirementType.EtherTR:
                case RequirementType.EtherBoth:
                    {
                        return _items[ItemType.EtherDungeons];
                    }
                case RequirementType.Quake:
                    {
                        return _items[ItemType.Quake];
                    }
                case RequirementType.QuakeMM:
                case RequirementType.QuakeTR:
                case RequirementType.QuakeBoth:
                    {
                        return _items[ItemType.QuakeDungeons];
                    }
                case RequirementType.Gloves1:
                case RequirementType.Gloves2:
                    {
                        return _items[ItemType.Gloves];
                    }
                case RequirementType.Lamp:
                    {
                        return _items[ItemType.Lamp];
                    }
                case RequirementType.Hammer:
                    {
                        return _items[ItemType.Hammer];
                    }
                case RequirementType.Flute:
                    {
                        return _items[ItemType.Flute];
                    }
                case RequirementType.FluteActivated:
                    {
                        return _items[ItemType.FluteActivated];
                    }
                case RequirementType.Net:
                    {
                        return _items[ItemType.Net];
                    }
                case RequirementType.Book:
                    {
                        return _items[ItemType.Book];
                    }
                case RequirementType.Shovel:
                    {
                        return _items[ItemType.Shovel];
                    }
                case RequirementType.NoFlippers:
                case RequirementType.Flippers:
                    {
                        return _items[ItemType.Flippers];
                    }
                case RequirementType.Bottle:
                    {
                        return _items[ItemType.Bottle];
                    }
                case RequirementType.CaneOfSomaria:
                    {
                        return _items[ItemType.CaneOfSomaria];
                    }
                case RequirementType.CaneOfByrna:
                    {
                        return _items[ItemType.CaneOfByrna];
                    }
                case RequirementType.Cape:
                    {
                        return _items[ItemType.Cape];
                    }
                case RequirementType.Mirror:
                    {
                        return _items[ItemType.Mirror];
                    }
                case RequirementType.HalfMagic:
                    {
                        return _items[ItemType.HalfMagic];
                    }
                case RequirementType.MoonPearl:
                    {
                        return _items[ItemType.MoonPearl];
                    }
                case RequirementType.Aga1:
                    {
                        return _prizes[PrizeType.Aga1];
                    }
                case RequirementType.Aga2:
                    {
                        return _prizes[PrizeType.Aga2];
                    }
                case RequirementType.RedCrystal:
                    {
                        return _prizes[PrizeType.RedCrystal];
                    }
                case RequirementType.Pendant:
                    {
                        return _prizes[PrizeType.Pendant];
                    }
                case RequirementType.GreenPendant:
                    {
                        return _prizes[PrizeType.GreenPendant];
                    }
                case RequirementType.TRSmallKey2:
                case RequirementType.TRSmallKey3:
                    {
                        return _items[ItemType.TRSmallKey];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }

        /// <summary>
        /// Returns the item count to be checked for the item requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the item count.
        /// </returns>
        private static int GetItemCount(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.Swordless:
                case RequirementType.NoFlippers:
                    {
                        return 0;
                    }
                case RequirementType.Sword1:
                case RequirementType.BombosTR:
                case RequirementType.EtherTR:
                case RequirementType.QuakeTR:
                case RequirementType.Gloves2:
                case RequirementType.RedCrystal:
                case RequirementType.Pendant:
                case RequirementType.TRSmallKey2:
                    {
                        return 2;
                    }
                case RequirementType.Sword2:
                case RequirementType.Shield3:
                case RequirementType.BombosBoth:
                case RequirementType.EtherBoth:
                case RequirementType.QuakeBoth:
                case RequirementType.TRSmallKey3:
                    {
                        return 3;
                    }
                case RequirementType.Sword3:
                    {
                        return 4;
                    }
                case RequirementType.Bow:
                case RequirementType.Boomerang:
                case RequirementType.RedBoomerang:
                case RequirementType.Hookshot:
                case RequirementType.Powder:
                case RequirementType.Mushroom:
                case RequirementType.Boots:
                case RequirementType.FireRod:
                case RequirementType.IceRod:
                case RequirementType.Bombos:
                case RequirementType.BombosMM:
                case RequirementType.Ether:
                case RequirementType.EtherMM:
                case RequirementType.Quake:
                case RequirementType.QuakeMM:
                case RequirementType.Gloves1:
                case RequirementType.Lamp:
                case RequirementType.Hammer:
                case RequirementType.Flute:
                case RequirementType.FluteActivated:
                case RequirementType.Net:
                case RequirementType.Book:
                case RequirementType.Shovel:
                case RequirementType.Flippers:
                case RequirementType.Bottle:
                case RequirementType.CaneOfSomaria:
                case RequirementType.CaneOfByrna:
                case RequirementType.Cape:
                case RequirementType.Mirror:
                case RequirementType.HalfMagic:
                case RequirementType.MoonPearl:
                case RequirementType.Aga1:
                case RequirementType.Aga2:
                case RequirementType.GreenPendant:
                    {
                        return 1;
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }

        /// <summary>
        /// Returns the associated sequence break for a requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A sequence break.
        /// </returns>
        private ISequenceBreak GetSequenceBreak(RequirementType type)
        {
            return type switch
            {
                RequirementType.SBBlindPedestal => _sequenceBreaks[SequenceBreakType.BlindPedestal],
                RequirementType.SBBonkOverLedge => _sequenceBreaks[SequenceBreakType.BonkOverLedge],
                RequirementType.SBBumperCaveHookshot => _sequenceBreaks[SequenceBreakType.BumperCaveHookshot],
                RequirementType.SBTRLaserSkip => _sequenceBreaks[SequenceBreakType.TRLaserSkip],
                RequirementType.SBHelmasaurKingBasic => _sequenceBreaks[SequenceBreakType.HelmasaurKingBasic],
                RequirementType.SBLanmolasBombs => _sequenceBreaks[SequenceBreakType.LanmolasBombs],
                RequirementType.SBArrghusBasic => _sequenceBreaks[SequenceBreakType.ArrghusBasic],
                RequirementType.SBMothulaBasic => _sequenceBreaks[SequenceBreakType.MothulaBasic],
                RequirementType.SBBlindBasic => _sequenceBreaks[SequenceBreakType.BlindBasic],
                RequirementType.SBKholdstareBasic => _sequenceBreaks[SequenceBreakType.KholdstareBasic],
                RequirementType.SBVitreousBasic => _sequenceBreaks[SequenceBreakType.VitreousBasic],
                RequirementType.SBTrinexxBasic => _sequenceBreaks[SequenceBreakType.TrinexxBasic],
                RequirementType.SBBombDuplicationAncillaOverload => _sequenceBreaks[SequenceBreakType.BombDuplicationAncillaOverload],
                RequirementType.SBBombDuplicationMirror => _sequenceBreaks[SequenceBreakType.BombDuplicationMirror],
                RequirementType.SBBombJumpPoDHammerJump => _sequenceBreaks[SequenceBreakType.BombJumpPoDHammerJump],
                RequirementType.SBBombJumpSWBigChest => _sequenceBreaks[SequenceBreakType.BombJumpSWBigChest],
                RequirementType.SBBombJumpIPBJ => _sequenceBreaks[SequenceBreakType.BombJumpIPBJ],
                RequirementType.SBBombJumpIPHookshotGap => _sequenceBreaks[SequenceBreakType.BombJumpIPHookshotGap],
                RequirementType.SBBombJumpIPFreezorRoomGap => _sequenceBreaks[SequenceBreakType.BombJumpIPFreezorRoomGap],
                RequirementType.SBDarkRoomDeathMountainEntry => _sequenceBreaks[SequenceBreakType.DarkRoomDeathMountainEntry],
                RequirementType.SBDarkRoomDeathMountainExit => _sequenceBreaks[SequenceBreakType.DarkRoomDeathMountainExit],
                RequirementType.SBDarkRoomHC => _sequenceBreaks[SequenceBreakType.DarkRoomHC],
                RequirementType.SBDarkRoomAT => _sequenceBreaks[SequenceBreakType.DarkRoomAT],
                RequirementType.SBDarkRoomEPRight => _sequenceBreaks[SequenceBreakType.DarkRoomEPRight],
                RequirementType.SBDarkRoomEPBack => _sequenceBreaks[SequenceBreakType.DarkRoomEPBack],
                RequirementType.SBDarkRoomPoDDarkBasement => _sequenceBreaks[SequenceBreakType.DarkRoomPoDDarkBasement],
                RequirementType.SBDarkRoomPoDDarkMaze => _sequenceBreaks[SequenceBreakType.DarkRoomPoDDarkMaze],
                RequirementType.SBDarkRoomPoDBossArea => _sequenceBreaks[SequenceBreakType.DarkRoomPoDBossArea],
                RequirementType.SBDarkRoomMM => _sequenceBreaks[SequenceBreakType.DarkRoomMM],
                RequirementType.SBDarkRoomTR => _sequenceBreaks[SequenceBreakType.DarkRoomTR],
                RequirementType.SBFakeFlippersFairyRevival => _sequenceBreaks[SequenceBreakType.FakeFlippersFairyRevival],
                RequirementType.SBFakeFlippersQirnJump => _sequenceBreaks[SequenceBreakType.FakeFlippersQirnJump],
                RequirementType.SBFakeFlippersScreenTransition => _sequenceBreaks[SequenceBreakType.FakeFlippersScreenTransition],
                RequirementType.SBFakeFlippersSplashDeletion => _sequenceBreaks[SequenceBreakType.FakeFlippersSplashDeletion],
                RequirementType.SBWaterWalk => _sequenceBreaks[SequenceBreakType.WaterWalk],
                RequirementType.SBWaterWalkFromWaterfallCave => _sequenceBreaks[SequenceBreakType.WaterWalkFromWaterfallCave],
                RequirementType.SBSuperBunnyFallInHole => _sequenceBreaks[SequenceBreakType.SuperBunnyFallInHole],
                RequirementType.SBSuperBunnyMirror => _sequenceBreaks[SequenceBreakType.SuperBunnyMirror],
                RequirementType.SBCameraUnlock => _sequenceBreaks[SequenceBreakType.CameraUnlock],
                RequirementType.SBDungeonRevive => _sequenceBreaks[SequenceBreakType.DungeonRevive],
                RequirementType.SBFakePowder => _sequenceBreaks[SequenceBreakType.FakePowder],
                RequirementType.SBHover => _sequenceBreaks[SequenceBreakType.Hover],
                RequirementType.SBMimicClip => _sequenceBreaks[SequenceBreakType.MimicClip],
                RequirementType.SBSpikeCave => _sequenceBreaks[SequenceBreakType.SpikeCave],
                RequirementType.SBToHHerapot => _sequenceBreaks[SequenceBreakType.ToHHerapot],
                RequirementType.SBIPIceBreaker => _sequenceBreaks[SequenceBreakType.IPIceBreaker],
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a requirement node for the specified requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A requirement node.
        /// </returns>
        private INode GetOverworldNode(RequirementType type)
        {
            return type switch
            {
                RequirementType.LightWorld => _requirementNodes[OverworldNodeID.LightWorld],
                RequirementType.HammerPegsArea => _requirementNodes[OverworldNodeID.HammerPegsArea],
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a boss placement for the specified requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A boss placement.
        /// </returns>
        private IBossPlacement GetBossPlacement(RequirementType type)
        {
            return type switch
            {
                RequirementType.ATBoss => _bossPlacements[BossPlacementID.ATBoss],
                RequirementType.EPBoss => _bossPlacements[BossPlacementID.EPBoss],
                RequirementType.DPBoss => _bossPlacements[BossPlacementID.DPBoss],
                RequirementType.ToHBoss => _bossPlacements[BossPlacementID.ToHBoss],
                RequirementType.PoDBoss => _bossPlacements[BossPlacementID.PoDBoss],
                RequirementType.SPBoss => _bossPlacements[BossPlacementID.SPBoss],
                RequirementType.SWBoss => _bossPlacements[BossPlacementID.SWBoss],
                RequirementType.TTBoss => _bossPlacements[BossPlacementID.TTBoss],
                RequirementType.IPBoss => _bossPlacements[BossPlacementID.IPBoss],
                RequirementType.MMBoss => _bossPlacements[BossPlacementID.MMBoss],
                RequirementType.TRBoss => _bossPlacements[BossPlacementID.TRBoss],
                RequirementType.GTBoss1 => _bossPlacements[BossPlacementID.GTBoss1],
                RequirementType.GTBoss2 => _bossPlacements[BossPlacementID.GTBoss2],
                RequirementType.GTBoss3 => _bossPlacements[BossPlacementID.GTBoss3],
                RequirementType.GTFinalBoss => _bossPlacements[BossPlacementID.GTFinalBoss],
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a new requirement of the proper type.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A requirement of the proper type.
        /// </returns>
        public IRequirement GetRequirement(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.NoRequirement:
                case RequirementType.Inspect:
                case RequirementType.SequenceBreak:
                    {
                        return _staticFactory(GetStaticAccessibility(type));
                    }
                case RequirementType.ItemPlacementBasic:
                case RequirementType.ItemPlacementAdvanced:
                    {
                        return _itemPlacementFactory(GetItemPlacementValue(type));
                    }
                case RequirementType.MapShuffleOff:
                case RequirementType.MapShuffleOn:
                    {
                        return _mapShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.CompassShuffleOff:
                case RequirementType.CompassShuffleOn:
                    {
                        return _compassShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.SmallKeyShuffleOff:
                case RequirementType.SmallKeyShuffleOn:
                    {
                        return _smallKeyShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.BigKeyShuffleOff:
                case RequirementType.BigKeyShuffleOn:
                    {
                        return _bigKeyShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.WorldStateStandardOpen:
                case RequirementType.WorldStateInverted:
                    {
                        return _worldStateFactory(GetWorldStateValue(type));
                    }
                case RequirementType.EntranceShuffleNone:
                case RequirementType.EntranceShuffleDungeon:
                case RequirementType.EntranceShuffleAll:
                case RequirementType.EntranceShuffleInsanity:
                    {
                        return _entranceShuffleFactory(GetEntranceShuffleValue(type));
                    }
                case RequirementType.BossShuffleOff:
                case RequirementType.BossShuffleOn:
                    {
                        return _bossShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.EnemyShuffleOff:
                case RequirementType.EnemyShuffleOn:
                    {
                        return _enemyShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.GuaranteedBossItemsOff:
                case RequirementType.GuaranteedBossItemsOn:
                    {
                        return _guaranteedBossItemsFactory(GetBooleanValue(type));
                    }
                case RequirementType.GenericKeys:
                    {
                        return _genericKeysFactory(GetBooleanValue(type));
                    }
                case RequirementType.TakeAnyLocations:
                    {
                        return _takeAnyLocationsFactory(GetBooleanValue(type));
                    }
                case RequirementType.KeyDropShuffleOff:
                case RequirementType.KeyDropShuffleOn:
                    {
                        return _keyDropShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.ShopShuffle:
                    {
                        return _shopShuffleFactory(GetBooleanValue(type));
                    }
                case RequirementType.NoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SmallKeyShuffleOff],
                            _requirements[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.SmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SmallKeyShuffleOn],
                            _requirements[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.BigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SmallKeyShuffleOff],
                            _requirements[RequirementType.BigKeyShuffleOn]
                        });
                    }
                case RequirementType.AllKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SmallKeyShuffleOn],
                            _requirements[RequirementType.BigKeyShuffleOn]
                        });
                    }
                case RequirementType.EntranceShuffleNoneDungeon:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.EntranceShuffleNone],
                            _requirements[RequirementType.EntranceShuffleDungeon]
                        });
                    }
                case RequirementType.EntranceShuffleNoneDungeonAll:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.EntranceShuffleNone],
                            _requirements[RequirementType.EntranceShuffleDungeon],
                            _requirements[RequirementType.EntranceShuffleAll]
                        });
                    }
                case RequirementType.EntranceShuffleDungeonAllInsanity:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.EntranceShuffleDungeon],
                            _requirements[RequirementType.EntranceShuffleAll],
                            _requirements[RequirementType.EntranceShuffleInsanity]
                        });
                    }
                case RequirementType.EntranceShuffleAllInsanity:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.EntranceShuffleAll],
                            _requirements[RequirementType.EntranceShuffleInsanity]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleNone:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpen],
                            _requirements[RequirementType.EntranceShuffleNone]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                            _requirements[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                            _requirements[RequirementType.BigKeyShuffleOnly]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpen],
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleAllInsanity:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpen],
                            _requirements[RequirementType.EntranceShuffleAllInsanity]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleNone:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.EntranceShuffleNone]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleAllInsanity:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.EntranceShuffleAllInsanity]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleInsanity:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.EntranceShuffleInsanity]
                        });
                    }
                case RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanity:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.EntranceShuffleDungeonAllInsanity]
                        });
                    }
                case RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanity],
                            _requirements[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanity],
                            _requirements[RequirementType.BigKeyShuffleOnly]
                        });
                    }
                case RequirementType.SmallKeyShuffleOffItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SmallKeyShuffleOff],
                            _requirements[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.SmallKeyShuffleOffItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SmallKeyShuffleOff],
                            _requirements[RequirementType.ItemPlacementBasic]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOnBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOn],
                            _requirements[RequirementType.BigKeyShuffleOnly]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOnSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOn],
                            _requirements[RequirementType.SmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOnSmallKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOn],
                            _requirements[RequirementType.SmallKeyShuffleOff]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOnNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOn],
                            _requirements[RequirementType.NoKeyShuffle]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOn],
                            _requirements[RequirementType.ItemPlacementBasic]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOnOrItemPlacementBasicSmallKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic],
                            _requirements[RequirementType.SmallKeyShuffleOff]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOffBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOff],
                            _requirements[RequirementType.BigKeyShuffleOnly]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOff],
                            _requirements[RequirementType.SmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOff],
                            _requirements[RequirementType.SmallKeyShuffleOff]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOffItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOff]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOffNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOff],
                            _requirements[RequirementType.NoKeyShuffle]
                        });
                    }
                case RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.GuaranteedBossItemsOff],
                            _requirements[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.ShopShuffleEntranceShuffleNoneDungeon:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ShopShuffle],
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]
                        });
                    }
                case RequirementType.TakeAnyLocationsOrShopShuffle:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.TakeAnyLocations],
                            _requirements[RequirementType.ShopShuffle]
                        });
                    }
                case RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.TakeAnyLocations],
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]
                        });
                    }
                case RequirementType.TakeAnyLocationsOrShopShuffleEntranceShuffleNoneDungeon:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.TakeAnyLocationsOrShopShuffle],
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]
                        });
                    }
                case RequirementType.KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnBigKeyShuffleOffItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnBigKeyShuffleOffItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementBasic],
                            _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.BigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementBasic],
                            _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnSmallKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.SmallKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.SmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.NoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOnNoKeyShuffleItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.KeyDropShuffleOnNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOnNoKeyShuffleItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementBasic],
                            _requirements[RequirementType.KeyDropShuffleOnNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOnAllKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.AllKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOnGuaranteedBossItemsOnBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.GuaranteedBossItemsOnBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnGuaranteedBossItemsOnSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.GuaranteedBossItemsOnSmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnGuaranteedBossItemsOnNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.GuaranteedBossItemsOnNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOn],
                            _requirements[RequirementType.GuaranteedBossItemsOffNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.WorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.WorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOn:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.BigKeyShuffleOn]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOffItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff],
                            _requirements[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOffItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff],
                            _requirements[RequirementType.ItemPlacementBasic]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.BigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyGuaranteedBossItemsOrItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly],
                            _requirements[RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementBasic],
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffSmallKeyShuffleOn:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.SmallKeyShuffleOn]
                        });
                    }
                case RequirementType.KeyDropShuffleOffSmallKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.SmallKeyShuffleOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOffSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.SmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.NoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOffNoKeyShuffleItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.KeyDropShuffleOffNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOffNoKeyShuffleItemPlacementBasic:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ItemPlacementBasic],
                            _requirements[RequirementType.KeyDropShuffleOffNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOnBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOnBigKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOnSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOnSmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOnNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOnNoKeyShuffle]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOff:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly],
                            _requirements[RequirementType.GuaranteedBossItemsOff]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnlyItemPlacementAdvanced:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnly],
                            _requirements[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffSmallKeyShuffleOnly:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOnly]
                        });
                    }
                case RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffNoKeyShuffle:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KeyDropShuffleOff],
                            _requirements[RequirementType.GuaranteedBossItemsOffNoKeyShuffle]
                        });
                    }
                case RequirementType.RaceIllegalTracking:
                    {
                        return _raceIllegalTrackingFactory(GetBooleanValue(type));
                    }
                case RequirementType.Swordless:
                case RequirementType.Mushroom:
                case RequirementType.BombosMM:
                case RequirementType.BombosTR:
                case RequirementType.BombosBoth:
                case RequirementType.EtherMM:
                case RequirementType.EtherTR:
                case RequirementType.EtherBoth:
                case RequirementType.QuakeMM:
                case RequirementType.QuakeTR:
                case RequirementType.QuakeBoth:
                case RequirementType.NoFlippers:
                    {
                        return _itemExactFactory(GetItem(type), GetItemCount(type));
                    }
                case RequirementType.Sword1:
                case RequirementType.Sword2:
                case RequirementType.Sword3:
                case RequirementType.Shield3:
                case RequirementType.Aga1:
                case RequirementType.Bow:
                case RequirementType.Boomerang:
                case RequirementType.RedBoomerang:
                case RequirementType.Hookshot:
                case RequirementType.Powder:
                case RequirementType.Boots:
                case RequirementType.FireRod:
                case RequirementType.IceRod:
                case RequirementType.Bombos:
                case RequirementType.Ether:
                case RequirementType.Quake:
                case RequirementType.Gloves1:
                case RequirementType.Gloves2:
                case RequirementType.Lamp:
                case RequirementType.Hammer:
                case RequirementType.Flute:
                case RequirementType.FluteActivated:
                case RequirementType.Net:
                case RequirementType.Book:
                case RequirementType.Shovel:
                case RequirementType.Flippers:
                case RequirementType.Bottle:
                case RequirementType.CaneOfSomaria:
                case RequirementType.CaneOfByrna:
                case RequirementType.Cape:
                case RequirementType.Mirror:
                case RequirementType.HalfMagic:
                case RequirementType.MoonPearl:
                case RequirementType.Aga2:
                case RequirementType.RedCrystal:
                case RequirementType.Pendant:
                case RequirementType.GreenPendant:
                    {
                        return _itemFactory(GetItem(type), GetItemCount(type));
                    }
                case RequirementType.TRSmallKey2:
                case RequirementType.TRSmallKey3:
                    {
                        return _smallKeyFactory((ISmallKeyItem)GetItem(type), GetItemCount(type));
                    }
                case RequirementType.SBBlindPedestal:
                case RequirementType.SBBonkOverLedge:
                case RequirementType.SBBumperCaveHookshot:
                case RequirementType.SBTRLaserSkip:
                case RequirementType.SBLanmolasBombs:
                case RequirementType.SBHelmasaurKingBasic:
                case RequirementType.SBArrghusBasic:
                case RequirementType.SBMothulaBasic:
                case RequirementType.SBBlindBasic:
                case RequirementType.SBKholdstareBasic:
                case RequirementType.SBVitreousBasic:
                case RequirementType.SBTrinexxBasic:
                case RequirementType.SBBombDuplicationAncillaOverload:
                case RequirementType.SBBombDuplicationMirror:
                case RequirementType.SBBombJumpPoDHammerJump:
                case RequirementType.SBBombJumpSWBigChest:
                case RequirementType.SBBombJumpIPBJ:
                case RequirementType.SBBombJumpIPHookshotGap:
                case RequirementType.SBBombJumpIPFreezorRoomGap:
                case RequirementType.SBDarkRoomDeathMountainEntry:
                case RequirementType.SBDarkRoomDeathMountainExit:
                case RequirementType.SBDarkRoomHC:
                case RequirementType.SBDarkRoomAT:
                case RequirementType.SBDarkRoomEPRight:
                case RequirementType.SBDarkRoomEPBack:
                case RequirementType.SBDarkRoomPoDDarkBasement:
                case RequirementType.SBDarkRoomPoDDarkMaze:
                case RequirementType.SBDarkRoomPoDBossArea:
                case RequirementType.SBDarkRoomMM:
                case RequirementType.SBDarkRoomTR:
                case RequirementType.SBFakeFlippersFairyRevival:
                case RequirementType.SBFakeFlippersQirnJump:
                case RequirementType.SBFakeFlippersScreenTransition:
                case RequirementType.SBFakeFlippersSplashDeletion:
                case RequirementType.SBWaterWalk:
                case RequirementType.SBWaterWalkFromWaterfallCave:
                case RequirementType.SBSuperBunnyFallInHole:
                case RequirementType.SBSuperBunnyMirror:
                case RequirementType.SBCameraUnlock:
                case RequirementType.SBDungeonRevive:
                case RequirementType.SBFakePowder:
                case RequirementType.SBHover:
                case RequirementType.SBMimicClip:
                case RequirementType.SBSpikeCave:
                case RequirementType.SBToHHerapot:
                case RequirementType.SBIPIceBreaker:
                    {
                        return _sequenceBreakFactory(GetSequenceBreak(type));
                    }
                case RequirementType.GTCrystal:
                    {
                        return _crystalFactory();
                    }
                case RequirementType.LightWorld:
                case RequirementType.HammerPegsArea:
                    {
                        return _requirementNodeFactory(GetOverworldNode(type));
                    }
                case RequirementType.AllMedallions:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Bombos],
                            _requirements[RequirementType.Ether],
                            _requirements[RequirementType.Quake]
                        });
                    }
                case RequirementType.ExtendMagic1:
                    {
                        return _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Bottle],
                                _requirements[RequirementType.HalfMagic]
                            });
                    }
                case RequirementType.ExtendMagic2:
                    {
                        return _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Bottle],
                                _requirements[RequirementType.HalfMagic]
                            });
                    }
                case RequirementType.FireRodDarkRoom:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.FireRod],
                            _requirements[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.UseMedallion:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Swordless],
                            _requirements[RequirementType.Sword1]
                        });
                    }
                case RequirementType.MeltThings:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.FireRod],
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Bombos],
                                _requirements[RequirementType.UseMedallion]
                            })
                        });
                    }
                case RequirementType.NotBunnyLW:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpen],
                            _requirements[RequirementType.MoonPearl]
                        });
                    }
                case RequirementType.NotBunnyDW:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.MoonPearl]
                        });
                    }
                case RequirementType.ATBarrier:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Cape],
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Swordless],
                                _requirements[RequirementType.Hammer]
                            }),
                            _requirements[RequirementType.Sword2]
                        });
                    }
                case RequirementType.BombDuplicationAncillaOverload:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBBombDuplicationAncillaOverload],
                            _requirements[RequirementType.Bow],
                            _requirements[RequirementType.CaneOfSomaria],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Boomerang],
                                _requirements[RequirementType.RedBoomerang]
                            })
                        });
                    }
                case RequirementType.BombDuplicationMirror:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBBombDuplicationMirror],
                            _requirements[RequirementType.Flippers],
                            _requirements[RequirementType.Mirror]
                        });
                    }
                case RequirementType.BombJumpPoDHammerJump:
                    {
                        return _requirements[RequirementType.SBBombJumpPoDHammerJump];
                    }
                case RequirementType.BombJumpSWBigChest:
                    {
                        return _requirements[RequirementType.SBBombJumpSWBigChest];
                    }
                case RequirementType.BombJumpIPBJ:
                    {
                        return _requirements[RequirementType.SBBombJumpIPBJ];
                    }
                case RequirementType.BombJumpIPHookshotGap:
                    {
                        return _requirements[RequirementType.SBBombJumpIPHookshotGap];
                    }
                case RequirementType.BombJumpIPFreezorRoomGap:
                    {
                        return _requirements[RequirementType.SBBombJumpIPFreezorRoomGap];
                    }
                case RequirementType.BonkOverLedge:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Boots],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBBonkOverLedge],
                                _requirements[RequirementType.ItemPlacementAdvanced]
                            })
                        });
                    }
                case RequirementType.BumperCaveGap:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Hookshot],
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.SBBumperCaveHookshot]
                        });
                    }
                case RequirementType.CameraUnlock:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBCameraUnlock],
                            _requirements[RequirementType.Bottle]
                        });
                    }
                case RequirementType.Curtains:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Swordless],
                            _requirements[RequirementType.Sword1]
                        });
                    }
                case RequirementType.DarkRoomDeathMountainEntry:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomDeathMountainEntry],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomDeathMountainExit:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomDeathMountainExit],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomHC:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomHC],
                            _requirements[RequirementType.Lamp],
                            _requirements[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomAT:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomAT],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomEPRight:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomEPRight],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomEPBack:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomEPBack],
                            _requirements[RequirementType.Lamp],
                            _requirements[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomPoDDarkBasement:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomPoDDarkBasement],
                            _requirements[RequirementType.Lamp],
                            _requirements[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomPoDDarkMaze:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomPoDDarkMaze],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomPoDBossArea:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomPoDBossArea],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomMM:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomMM],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomTR:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBDarkRoomTR],
                            _requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DungeonRevive:
                    {
                        return _requirements[RequirementType.SBDungeonRevive];
                    }
                case RequirementType.FakeFlippersFairyRevival:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBFakeFlippersFairyRevival],
                            _requirements[RequirementType.Bottle]
                        });
                    }
                case RequirementType.FakeFlippersQirnJump:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBFakeFlippersQirnJump]
                        });
                    }
                case RequirementType.FakeFlippersScreenTransition:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBFakeFlippersScreenTransition]
                        });
                    }
                case RequirementType.FakeFlippersSplashDeletion:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Bow],
                                _requirements[RequirementType.RedBoomerang],
                                _requirements[RequirementType.CaneOfSomaria],
                                _requirements[RequirementType.IceRod]
                            }),
                            _requirements[RequirementType.SBFakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.FireSource:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Lamp],
                            _requirements[RequirementType.FireRod]
                        });
                    }
                case RequirementType.Hover:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBHover],
                            _requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.LaserBridge:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBTRLaserSkip],
                            _requirements[RequirementType.ItemPlacementAdvanced],
                            _requirements[RequirementType.Cape],
                            _requirements[RequirementType.CaneOfByrna],
                            _requirements[RequirementType.Shield3]
                        });
                    }
                case RequirementType.MagicBat:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBFakePowder],
                                _requirements[RequirementType.Mushroom],
                                _requirements[RequirementType.CaneOfSomaria]
                            }),
                            _requirements[RequirementType.Powder]
                        });
                    }
                case RequirementType.MimicClip:
                    {
                        return _requirements[RequirementType.SBMimicClip];
                    }
                case RequirementType.Pedestal:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Pendant],
                                _requirements[RequirementType.GreenPendant],
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.SBBlindPedestal],
                                    _requirements[RequirementType.ItemPlacementAdvanced],
                                    _requirements[RequirementType.Book]
                                })
                            }),
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Book],
                                _requirements[RequirementType.Inspect]
                            })
                        });
                    }
                case RequirementType.RedEyegoreGoriya:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.EnemyShuffleOn],
                            _requirements[RequirementType.Bow]
                        });
                    }
                case RequirementType.SpikeCave:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SequenceBreak],
                            _requirements[RequirementType.CaneOfByrna],
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Cape],
                                _requirements[RequirementType.ExtendMagic1]
                            })
                        });
                    }
                case RequirementType.SuperBunnyFallInHole:
                    {
                        return _requirements[RequirementType.SBSuperBunnyFallInHole];
                    }
                case RequirementType.SuperBunnyMirror:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBSuperBunnyMirror],
                            _requirements[RequirementType.Mirror]
                        });
                    }
                case RequirementType.Tablet:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Book],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Inspect],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Swordless],
                                    _requirements[RequirementType.Hammer]
                                }),
                                _requirements[RequirementType.Sword2]
                            })
                        });
                    }
                case RequirementType.Torch:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Inspect],
                            _requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.ToHHerapot:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBToHHerapot],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Hookshot],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Boots],
                                    _requirements[RequirementType.Sword1]
                                })
                            })
                        });
                    }
                case RequirementType.IPIceBreaker:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBIPIceBreaker],
                            _requirements[RequirementType.CaneOfSomaria]
                        });
                    }
                case RequirementType.MMMedallion:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.UseMedallion],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.AllMedallions],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Bombos],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.BombosMM],
                                        _requirements[RequirementType.BombosBoth]
                                    })
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Ether],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.EtherMM],
                                        _requirements[RequirementType.EtherBoth]
                                    })
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Quake],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.QuakeMM],
                                        _requirements[RequirementType.QuakeBoth]
                                    })
                                }),
                            })
                        });
                    }
                case RequirementType.TRMedallion:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.UseMedallion],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.AllMedallions],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Bombos],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.BombosTR],
                                        _requirements[RequirementType.BombosBoth]
                                    })
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Ether],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.EtherTR],
                                        _requirements[RequirementType.EtherBoth]
                                    })
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Quake],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.QuakeTR],
                                        _requirements[RequirementType.QuakeBoth]
                                    })
                                }),
                            })
                        });
                    }
                case RequirementType.TRKeyDoorsToMiddleExit:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SmallKeyShuffleOn],
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.TRSmallKey3],
                                    _aggregateFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.KeyDropShuffleOff],
                                        _requirements[RequirementType.TRSmallKey2]
                                    })
                                }),
                            }),
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SmallKeyShuffleOff],
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.FireRod],
                                    _requirements[RequirementType.SequenceBreak]
                                })
                            })
                        });
                    }
                case RequirementType.WaterWalk:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBWaterWalk],
                            _requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.WaterWalkFromWaterfallCave:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SBWaterWalkFromWaterfallCave],
                            _requirements[RequirementType.NoFlippers],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.MoonPearl],
                                _requirements[RequirementType.EntranceShuffleAll]
                            })
                        });
                    }
                case RequirementType.LWMirror:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateInverted],
                            _requirements[RequirementType.Mirror]
                        });
                    }
                case RequirementType.DWMirror:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.WorldStateStandardOpen],
                            _requirements[RequirementType.Mirror]
                        });
                    }
                case RequirementType.Armos:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Hammer],
                            _requirements[RequirementType.Bow],
                            _requirements[RequirementType.Boomerang],
                            _requirements[RequirementType.RedBoomerang],
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.ExtendMagic2],
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.FireRod],
                                    _requirements[RequirementType.IceRod]
                                })
                            }),
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.ExtendMagic1],
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.CaneOfByrna],
                                    _requirements[RequirementType.CaneOfSomaria]
                                })
                            })
                        });
                    }
                case RequirementType.Lanmolas:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Hammer],
                            _requirements[RequirementType.Bow],
                            _requirements[RequirementType.FireRod],
                            _requirements[RequirementType.IceRod],
                            _requirements[RequirementType.CaneOfByrna],
                            _requirements[RequirementType.CaneOfSomaria],
                            _requirements[RequirementType.SBLanmolasBombs]
                        });
                    }
                case RequirementType.Moldorm:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Hammer]
                        });
                    }
                case RequirementType.HelmasaurKingSB:
                    {
                        return _requirements[RequirementType.Sword1];
                    }
                case RequirementType.HelmasaurKing:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.HelmasaurKingSB],
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.SBHelmasaurKingBasic],
                                    _requirements[RequirementType.ItemPlacementAdvanced]
                                })
                            }),
                            _requirements[RequirementType.Bow],
                            _requirements[RequirementType.Sword2]
                        });
                    }
                case RequirementType.ArrghusSB:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Hookshot],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Hammer],
                                _requirements[RequirementType.Sword1],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.ExtendMagic1],
                                        _requirements[RequirementType.Bow]
                                    }),
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.FireRod],
                                        _requirements[RequirementType.IceRod]
                                    })
                                })
                            }),
                        });
                    }
                case RequirementType.Arrghus:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.ArrghusSB],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBArrghusBasic],
                                _requirements[RequirementType.ItemPlacementAdvanced],
                                _requirements[RequirementType.Swordless],
                                _requirements[RequirementType.Sword2]
                            })
                        });
                    }
                case RequirementType.MothulaSB:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Hammer],
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.ExtendMagic1],
                                _requirements[RequirementType.FireRod]
                            }),
                            _requirements[RequirementType.CaneOfSomaria],
                            _requirements[RequirementType.CaneOfByrna]
                        });
                    }
                case RequirementType.Mothula:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.MothulaSB],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBMothulaBasic],
                                _requirements[RequirementType.ItemPlacementAdvanced],
                                _requirements[RequirementType.Sword2],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic1],
                                    _requirements[RequirementType.FireRod]
                                })
                            })
                        });
                    }
                case RequirementType.BlindSB:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Hammer],
                            _requirements[RequirementType.CaneOfSomaria],
                            _requirements[RequirementType.CaneOfByrna]
                        });
                    }
                case RequirementType.Blind:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.BlindSB],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBBlindBasic],
                                _requirements[RequirementType.ItemPlacementAdvanced],
                                _requirements[RequirementType.Swordless],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Sword1],
                                    _alternativeFactory(new List<IRequirement>
                                    {
                                        _requirements[RequirementType.Cape],
                                        _requirements[RequirementType.CaneOfByrna]
                                    })
                                })
                            })
                        });
                    }
                case RequirementType.KholdstareSB:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.MeltThings],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Hammer],
                                _requirements[RequirementType.Sword1],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic2],
                                    _requirements[RequirementType.FireRod]
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic1],
                                    _requirements[RequirementType.FireRod],
                                    _requirements[RequirementType.Bombos],
                                    _requirements[RequirementType.Swordless]
                                })
                            })
                        });
                    }
                case RequirementType.Kholdstare:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.KholdstareSB],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBKholdstareBasic],
                                _requirements[RequirementType.ItemPlacementAdvanced],
                                _requirements[RequirementType.Sword2],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic2],
                                    _requirements[RequirementType.FireRod]
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.Bombos],
                                    _requirements[RequirementType.UseMedallion],
                                    _requirements[RequirementType.ExtendMagic1],
                                    _requirements[RequirementType.FireRod]
                                })
                            })
                        });
                    }
                case RequirementType.VitreousSB:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Hammer],
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Bow]
                        });
                    }
                case RequirementType.Vitreous:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.VitreousSB],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBVitreousBasic],
                                _requirements[RequirementType.ItemPlacementAdvanced],
                                _requirements[RequirementType.Sword2],
                                _requirements[RequirementType.Bow]
                            })
                        });
                    }
                case RequirementType.TrinexxSB:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.FireRod],
                            _requirements[RequirementType.IceRod],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Sword3],
                                _requirements[RequirementType.Hammer],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic1],
                                    _requirements[RequirementType.Sword2]
                                }),
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic2],
                                    _requirements[RequirementType.Sword1]
                                })
                            })
                        });
                    }
                case RequirementType.Trinexx:
                    {
                        return _aggregateFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.TrinexxSB],
                            _alternativeFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.SBTrinexxBasic],
                                _requirements[RequirementType.ItemPlacementAdvanced],
                                _requirements[RequirementType.Swordless],
                                _requirements[RequirementType.Sword3],
                                _aggregateFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.ExtendMagic1],
                                    _requirements[RequirementType.Sword2]
                                })
                            })
                        });
                    }
                case RequirementType.AgaBoss:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.Sword1],
                            _requirements[RequirementType.Hammer],
                            _requirements[RequirementType.Net]
                        });
                    }
                case RequirementType.UnknownBoss:
                    {
                        return _alternativeFactory(new List<IRequirement>
                        {
                            _requirements[RequirementType.SequenceBreak],
                            _aggregateFactory(new List<IRequirement>
                            {
                                _requirements[RequirementType.Armos],
                                _requirements[RequirementType.Lanmolas],
                                _requirements[RequirementType.Moldorm],
                                _requirements[RequirementType.Arrghus],
                                _requirements[RequirementType.Mothula],
                                _requirements[RequirementType.Blind],
                                _requirements[RequirementType.Kholdstare],
                                _requirements[RequirementType.Vitreous],
                                _requirements[RequirementType.Trinexx]
                            })
                        });
                    }
                case RequirementType.ATBoss:
                case RequirementType.EPBoss:
                case RequirementType.DPBoss:
                case RequirementType.ToHBoss:
                case RequirementType.PoDBoss:
                case RequirementType.SPBoss:
                case RequirementType.SWBoss:
                case RequirementType.TTBoss:
                case RequirementType.IPBoss:
                case RequirementType.MMBoss:
                case RequirementType.TRBoss:
                case RequirementType.GTBoss1:
                case RequirementType.GTBoss2:
                case RequirementType.GTBoss3:
                case RequirementType.GTFinalBoss:
                    {
                        return _bossFactory(GetBossPlacement(type));
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
