using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing the item section of dungeons.
    /// </summary>
    public class DungeonItemSection : ISection
    {
        private readonly Game _game;
        private readonly Location _location;
        private readonly int _baseTotal;
        private readonly DungeonData _dungeonData;
        private readonly ConcurrentQueue<DungeonData> _dungeonDataQueue;

        public int MapCompass { get; }
        public int SmallKey { get; }
        public int BigKey { get; }
        public ItemType? SmallKeyType { get; }
        public ItemType? BigKeyType { get; }
        public Func<bool> CanComplete { get; }
        public List<KeyLayout> KeyLayouts { get; }
        public List<BigKeyPlacement> BigKeyPlacements { get; }

        public string Name => "Dungeon";
        public bool HasMarking { get; }
        public ModeRequirement ModeRequirement =>
            new ModeRequirement();
        public bool UserManipulated { get; set; }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        private MarkingType? _marking;
        public MarkingType? Marking
        {
            get => _marking;
            set
            {
                if (_marking != value)
                {
                    OnPropertyChanging(nameof(Marking));
                    _marking = value;
                    OnPropertyChanged(nameof(Marking));
                }
            }
        }

        private int _available;
        public int Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set
            {
                if (_accessible != value)
                {
                    _accessible = value;
                    OnPropertyChanged(nameof(Accessible));
                }
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="location">
        /// The data for the location to which this section belongs.
        /// </param>
        public DungeonItemSection(Game game, Location location)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _dungeonData = new DungeonData(_game, _location, this);
            _dungeonDataQueue = new ConcurrentQueue<DungeonData>();
            KeyLayouts = new List<KeyLayout>();
            BigKeyPlacements = new List<BigKeyPlacement>();

            for (int i = 0; i < Environment.ProcessorCount - 1; i++)
            {
                _dungeonDataQueue.Enqueue(new DungeonData(_game, _location, this));
            }

            switch (_location.ID)
            {
                case LocationID.HyruleCastle:
                    {
                        MapCompass = 1;
                        SmallKey = 1;
                        SmallKeyType = ItemType.HCSmallKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight
                        }, 1, new List<DungeonItemID>(), new ModeRequirement()));

                        CanComplete = () =>
                        {
                            if (_game.RequirementNodes[RequirementNodeID.HCFrontEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.RequirementNodes[RequirementNodeID.HCSanctuaryEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomHC].Accessibility == AccessibilityLevel.Normal)
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    return _game.RequirementNodes[RequirementNodeID.HCBackEntry].Accessibility == AccessibilityLevel.Normal ||
                                        _game.Items.Has(ItemType.HCSmallKey);
                                }
                                else
                                {
                                    return _game.RequirementNodes[RequirementNodeID.HCBackEntry].Accessibility == AccessibilityLevel.Normal;
                                }
                            }

                            return false;
                        };
                    }
                    break;
                case LocationID.AgahnimTower:
                    {
                        SmallKey = 2;
                        SmallKeyType = ItemType.ATSmallKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.ATRoom03,
                            DungeonItemID.ATDarkMaze
                        }, 2, new List<DungeonItemID>(), new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.ATEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomAT].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Curtains].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.ATBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.ATSmallKey, 2));
                        };
                    }
                    break;
                case LocationID.EasternPalace:
                    {
                        MapCompass = 2;
                        BigKey = 1;
                        BigKeyType = ItemType.EPBigKey;
                        BigKeyPlacements = new List<BigKeyPlacement>
                        {
                            new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.EPCannonballChest,
                                DungeonItemID.EPMapChest,
                                DungeonItemID.EPCompassChest,
                                DungeonItemID.EPBigKeyChest,
                            }, new ModeRequirement())
                        };

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.EPEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomEPRightWing].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomEPBack].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.RedEyegoreGoriya].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.EPBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.EPBigKey));
                        };
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        MapCompass = 2;
                        SmallKey = 1;
                        BigKey = 1;
                        SmallKeyType = ItemType.DPSmallKey;
                        BigKeyType = ItemType.DPBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest
                        }, 1, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPCompassChest,
                            DungeonItemID.DPBigKeyChest
                        }, new ModeRequirement()));
                        HasMarking = true;

                        CanComplete = () =>
                        {
                            return (_game.RequirementNodes[RequirementNodeID.DPFrontEntry].Accessibility == AccessibilityLevel.Normal ||
                                _game.RequirementNodes[RequirementNodeID.DPLeftEntry].Accessibility == AccessibilityLevel.Normal) &&
                                _game.RequirementNodes[RequirementNodeID.DPBackEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Torch].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.FireSource].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DPBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.DPSmallKey)) &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.DPBigKey));
                        };
                    }
                    break;
                case LocationID.TowerOfHera:
                    {
                        MapCompass = 2;
                        SmallKey = 1;
                        SmallKeyType = ItemType.ToHSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.ToHBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest,
                            DungeonItemID.ToHBoss
                        }, 1, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest
                        }, new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.ToHEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.FireSource].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.ToHBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.ToHSmallKey)) &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.ToHBigKey));
                        };
                    }
                    break;
                case LocationID.PalaceOfDarkness:
                    {
                        MapCompass = 2;
                        SmallKey = 6;
                        SmallKeyType = ItemType.PoDSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.PoDBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.PoDShooterRoom,
                            DungeonItemID.PoDMapChest,
                            DungeonItemID.PoDArenaLedge,
                            DungeonItemID.PoDBigKeyChest,
                            DungeonItemID.PoDStalfosBasement,
                            DungeonItemID.PoDArenaBridge
                        }, 4, new List<DungeonItemID>(), new ModeRequirement()));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.PoDShooterRoom,
                            DungeonItemID.PoDMapChest,
                            DungeonItemID.PoDArenaLedge,
                            DungeonItemID.PoDBigKeyChest,
                            DungeonItemID.PoDStalfosBasement,
                            DungeonItemID.PoDArenaBridge,
                            DungeonItemID.PoDCompassChest,
                            DungeonItemID.PoDDarkBasementLeft,
                            DungeonItemID.PoDDarkBasementRight,
                            DungeonItemID.PoDHarmlessHellway
                        }, 6, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.PoDShooterRoom,
                            DungeonItemID.PoDMapChest,
                            DungeonItemID.PoDArenaLedge,
                            DungeonItemID.PoDBigKeyChest,
                            DungeonItemID.PoDStalfosBasement,
                            DungeonItemID.PoDArenaBridge,
                            DungeonItemID.PoDCompassChest,
                            DungeonItemID.PoDDarkBasementLeft,
                            DungeonItemID.PoDDarkBasementRight,
                            DungeonItemID.PoDHarmlessHellway,
                            DungeonItemID.PoDDarkMazeTop,
                            DungeonItemID.PoDDarkMazeBottom
                        }, new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.PoDEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.RedEyegoreGoriya].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Bow].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hammer].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomPoDDarkBasement].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomPoDDarkMaze].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomPoDBossArea].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.PoDBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.PoDBigKey)) &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.PoDSmallKey, 5));
                        };
                    }
                    break;
                case LocationID.SwampPalace:
                    {
                        MapCompass = 2;
                        SmallKey = 1;
                        SmallKeyType = ItemType.SPSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.SPBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.SPEntrance
                        }, 1, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.SPEntrance,
                            DungeonItemID.SPMapChest,
                            DungeonItemID.SPBigChest,
                            DungeonItemID.SPCompassChest,
                            DungeonItemID.SPWestChest,
                            DungeonItemID.SPBigKeyChest,
                            DungeonItemID.SPFloodedRoomLeft,
                            DungeonItemID.SPFloodedRoomRight,
                            DungeonItemID.SPWaterfallRoom,
                            DungeonItemID.SPBoss
                        }, new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.SPEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Swim].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hammer].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hookshot].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.SPBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.SPBigKey)) &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.SPSmallKey));
                        };
                    }
                    break;
                case LocationID.SkullWoods:
                    {
                        MapCompass = 2;
                        SmallKey = 3;
                        SmallKeyType = ItemType.SWSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.SWBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.SWPinballRoom
                        }, 1, new List<DungeonItemID>(),
                            new ModeRequirement(itemPlacement: ItemPlacement.Basic)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom
                        }, 3, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                        }, new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.SWFrontEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.RequirementNodes[RequirementNodeID.SWFrontLeftDropEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.RequirementNodes[RequirementNodeID.SWPinballRoomEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.RequirementNodes[RequirementNodeID.SWFrontTopDropEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.RequirementNodes[RequirementNodeID.SWFrontBackConnectorEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.RequirementNodes[RequirementNodeID.SWBackEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.FireRod].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Curtains].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.SWBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.SWBigKey)) &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.SWSmallKey));
                        };
                    }
                    break;
                case LocationID.ThievesTown:
                    {
                        MapCompass = 2;
                        SmallKey = 1;
                        SmallKeyType = ItemType.TTSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.TTBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTAttic,
                            DungeonItemID.TTBlindsCell,
                            DungeonItemID.TTBigChest,
                            DungeonItemID.TTBoss
                        }, 1, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest
                        }, new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.TTEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hammer].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.TTBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.TTBigKey)) &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TTSmallKey));
                        };
                    }
                    break;
                case LocationID.IcePalace:
                    {
                        MapCompass = 2;
                        SmallKey = 2;
                        SmallKeyType = ItemType.IPSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.IPBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom
                        }, 1, new List<DungeonItemID>(),
                            new ModeRequirement(itemPlacement: ItemPlacement.Advanced)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom,
                            DungeonItemID.IPBoss
                        }, 2, new List<DungeonItemID>(),
                            new ModeRequirement(itemPlacement: ItemPlacement.Advanced)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom
                        }, 2, new List<DungeonItemID>(),
                            new ModeRequirement(itemPlacement: ItemPlacement.Basic)));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPIcedTRoom,
                        }, new ModeRequirement()));

                        CanComplete = () =>
                        {
                            if (_game.RequirementNodes[RequirementNodeID.IPEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.MeltThings].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hammer].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Lift1].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.IPBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.IPBigKey)))
                            {
                                if (_game.Mode.SmallKeyShuffle)
                                {
                                    if (_game.Items.Has(ItemType.IPSmallKey) && _game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        return true;
                                    }

                                    if (_game.Items.Has(ItemType.IPSmallKey))
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (_game.Items.Has(ItemType.CaneOfSomaria))
                                    {
                                        return true;
                                    }
                                }
                            }

                            return false;
                        };
                    }
                    break;
                case LocationID.MiseryMire:
                    {
                        MapCompass = 2;
                        SmallKey = 3;
                        SmallKeyType = ItemType.MMSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.MMBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        }, 3, new List<DungeonItemID>(),
                            new ModeRequirement(dungeonItemShuffle: DungeonItemShuffle.Keysanity)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        }, 3, new List<DungeonItemID>
                        {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMMainLobby,
                                DungeonItemID.MMMapChest,
                        },
                            new ModeRequirement()));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMCompassChest,
                            DungeonItemID.MMBigKeyChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        }, 3, new List<DungeonItemID>
                        {
                            DungeonItemID.MMCompassChest,
                            DungeonItemID.MMBigKeyChest
                        },
                            new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMMainLobby,
                                DungeonItemID.MMCompassChest,
                                DungeonItemID.MMBigKeyChest,
                                DungeonItemID.MMMapChest,
                            },
                            new ModeRequirement()));

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.MMEntry].Accessibility == AccessibilityLevel.Normal &&
                                (_game.Requirements[RequirementType.Bonk].Accessibility == AccessibilityLevel.Normal ||
                                _game.Requirements[RequirementType.Hookshot].Accessibility == AccessibilityLevel.Normal) &&
                                _game.Requirements[RequirementType.FireSource].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.CaneOfSomaria].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.DarkRoomMM].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.MMBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.MMBigKey));
                        };
                    }
                    break;
                case LocationID.TurtleRock:
                    {
                        MapCompass = 2;
                        SmallKey = 4;
                        SmallKeyType = ItemType.TRSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.TRBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight
                        }, 1, new List<DungeonItemID>(),
                            new ModeRequirement(worldState: WorldState.StandardOpen, entranceShuffle: false)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight,
                            DungeonItemID.TRChainChomps
                        }, 2, new List<DungeonItemID>(),
                            new ModeRequirement(worldState: WorldState.StandardOpen, entranceShuffle: false)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight,
                            DungeonItemID.TRChainChomps,
                            DungeonItemID.TRBigKeyChest,
                            DungeonItemID.TRBigChest,
                            DungeonItemID.TRCrystarollerRoom
                        }, 3, new List<DungeonItemID>(),
                            new ModeRequirement(worldState: WorldState.StandardOpen, entranceShuffle: false)));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight,
                            DungeonItemID.TRChainChomps,
                            DungeonItemID.TRBigKeyChest,
                            DungeonItemID.TRBigChest,
                            DungeonItemID.TRCrystarollerRoom,
                            DungeonItemID.TRLaserBridgeTopLeft,
                            DungeonItemID.TRLaserBridgeTopRight,
                            DungeonItemID.TRLaserBridgeBottomLeft,
                            DungeonItemID.TRLaserBrdigeBottomRight
                        }, 4, new List<DungeonItemID>(),
                            new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigKeyChest,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBrdigeBottomRight
                            },
                            new ModeRequirement(worldState: WorldState.StandardOpen, entranceShuffle: false)));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigKeyChest,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBrdigeBottomRight
                            },
                            new ModeRequirement(worldState: WorldState.StandardOpen, entranceShuffle: true)));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigKeyChest,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBrdigeBottomRight
                            },
                            new ModeRequirement(worldState: WorldState.Inverted)));

                        CanComplete = () =>
                        {
                            if (_game.Requirements[RequirementType.FireRod].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.CaneOfSomaria].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.TRBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.TRBigKey)))
                            {
                                if (_game.RequirementNodes[RequirementNodeID.TRBackEntry].Accessibility == AccessibilityLevel.Normal)
                                {
                                    if (_game.RequirementNodes[RequirementNodeID.TRMiddleEntry].Accessibility == AccessibilityLevel.Normal)
                                    {
                                        if (_game.RequirementNodes[RequirementNodeID.TRFrontEntry].Accessibility == AccessibilityLevel.Normal)
                                        {
                                            return !_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TRSmallKey);
                                        }

                                        return !_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TRSmallKey, 2);
                                    }

                                    return _game.Requirements[RequirementType.DarkRoomTR].Accessibility == AccessibilityLevel.Normal &&
                                        (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TRSmallKey, 2));
                                }

                                if (_game.RequirementNodes[RequirementNodeID.TRMiddleEntry].Accessibility == AccessibilityLevel.Normal)
                                {
                                    if (_game.RequirementNodes[RequirementNodeID.TRFrontEntry].Accessibility == AccessibilityLevel.Normal)
                                    {
                                        return !_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TRSmallKey, 2);
                                    }

                                    return !_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TRSmallKey, 3);
                                }

                                if (_game.RequirementNodes[RequirementNodeID.TRFrontEntry].Accessibility == AccessibilityLevel.Normal)
                                {
                                    return _game.Requirements[RequirementType.DarkRoomTR].Accessibility == AccessibilityLevel.Normal &&
                                        (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.TRSmallKey, 4));
                                }
                            }

                            return false;
                        };
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        MapCompass = 2;
                        SmallKey = 4;
                        SmallKeyType = ItemType.GTSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.GTBigKey;
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        }, 2, new List<DungeonItemID>(), new ModeRequirement()));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom
                        }, 3, new List<DungeonItemID>(), new ModeRequirement()));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTMapChest,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTRandomizerRoomTopLeft,
                            DungeonItemID.GTRandomizerRoomTopRight,
                            DungeonItemID.GTRandomizerRoomBottomLeft,
                            DungeonItemID.GTRandomizerRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest
                        }, 4, new List<DungeonItemID>
                        {
                            DungeonItemID.GTRandomizerRoomTopLeft,
                            DungeonItemID.GTRandomizerRoomTopRight,
                            DungeonItemID.GTRandomizerRoomBottomLeft,
                            DungeonItemID.GTRandomizerRoomBottomRight
                        }, new ModeRequirement()));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTMapChest,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTCompassRoomTopLeft,
                            DungeonItemID.GTCompassRoomTopRight,
                            DungeonItemID.GTCompassRoomBottomLeft,
                            DungeonItemID.GTCompassRoomBottomRight,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest
                        }, 4, new List<DungeonItemID>
                        {
                            DungeonItemID.GTCompassRoomTopLeft,
                            DungeonItemID.GTCompassRoomTopRight,
                            DungeonItemID.GTCompassRoomBottomLeft,
                            DungeonItemID.GTCompassRoomBottomRight
                        }, new ModeRequirement()));
                        KeyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTMapChest,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest
                        }, 4, new List<DungeonItemID>(), new ModeRequirement()));
                        BigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.GTHopeRoomLeft,
                                DungeonItemID.GTHopeRoomRight,
                                DungeonItemID.GTBobsTorch,
                                DungeonItemID.GTDMsRoomTopLeft,
                                DungeonItemID.GTDMsRoomTopRight,
                                DungeonItemID.GTDMsRoomBottomLeft,
                                DungeonItemID.GTDMsRoomBottomRight,
                                DungeonItemID.GTMapChest,
                                DungeonItemID.GTFiresnakeRoom,
                                DungeonItemID.GTRandomizerRoomTopLeft,
                                DungeonItemID.GTRandomizerRoomTopRight,
                                DungeonItemID.GTRandomizerRoomBottomLeft,
                                DungeonItemID.GTRandomizerRoomBottomRight,
                                DungeonItemID.GTTileRoom,
                                DungeonItemID.GTCompassRoomTopLeft,
                                DungeonItemID.GTCompassRoomTopRight,
                                DungeonItemID.GTCompassRoomBottomLeft,
                                DungeonItemID.GTCompassRoomBottomRight,
                                DungeonItemID.GTBobsChest,
                                DungeonItemID.GTBigKeyRoomTopLeft,
                                DungeonItemID.GTBigKeyRoomTopRight,
                                DungeonItemID.GTBigKeyChest
                            }, new ModeRequirement()));
                        HasMarking = true;

                        CanComplete = () =>
                        {
                            return _game.RequirementNodes[RequirementNodeID.GTEntry].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Torch].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hammer].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.Hookshot].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.FireRod].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.CaneOfSomaria].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.RedEyegoreGoriya].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.GTBoss1].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.GTBoss2].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.GTBoss3].Accessibility == AccessibilityLevel.Normal &&
                                _game.Requirements[RequirementType.GTFinalBoss].Accessibility == AccessibilityLevel.Normal &&
                                (!_game.Mode.BigKeyShuffle || _game.Items.Has(ItemType.GTBigKey)) &&
                                (!_game.Mode.SmallKeyShuffle || _game.Items.Has(ItemType.GTSmallKey, 2));
                        };
                    }
                    break;
            }

            _baseTotal = _dungeonData.DungeonItems.Count - MapCompass - SmallKey - BigKey;

            SetTotal(false);
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changing property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                SetTotal();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Item, Requirement,
        /// and RequirementNode classes that are requirements for dungeon items.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        /// <summary>
        /// Sets the total based on whether dungeon items are shuffled.
        /// </summary>
        /// <param name="updateAccessibility">
        /// A boolean representing whether to call the UpdateAccessibiliy method at the end.
        /// </param>
        private void SetTotal(bool updateAccessibility = true)
        {
            int newTotal = _baseTotal +
                (_game.Mode.MapCompassShuffle ? MapCompass : 0) +
                (_game.Mode.SmallKeyShuffle ? SmallKey : 0) +
                (_game.Mode.BigKeyShuffle ? BigKey : 0);

            int delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));

            if (updateAccessibility)
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Returns a list of numbers of small keys that could be collected based on
        /// the current game mode and number of small keys collected in small key shuffle.
        /// </summary>
        /// <returns>
        /// A list of 32-bit integers representing the possible numbers of small keys collected.
        /// </returns>
        private List<int> GetSmallKeyValues()
        {
            List<int> smallKeysValues = new List<int>();

            if (_game.Mode.SmallKeyShuffle)
            {
                int smallKeyValue;

                if (SmallKeyType.HasValue)
                {
                    smallKeyValue = Math.Min(SmallKey, _game.Items[SmallKeyType.Value].Current +
                        (_game.Mode.WorldState == WorldState.Retro ? _game.Items[ItemType.SmallKey].Current : 0));
                }
                else
                {
                    smallKeyValue = 0;
                }

                smallKeysValues.Add(smallKeyValue);
            }
            else
            {
                for (int i = 0; i <= SmallKey; i++)
                {
                    smallKeysValues.Add(i);
                }
            }

            return smallKeysValues;
        }

        /// <summary>
        /// Returns a list of possible states for big key collection based on game mode and
        /// whether the big key is collected in big key shuffle.
        /// </summary>
        /// <returns>
        /// A list of booleans representing the possible states of big key collection.
        /// </returns>
        private List<bool> GetBigKeyValues()
        {
            List<bool> bigKeyValues = new List<bool>();

            if (_game.Mode.SmallKeyShuffle)
            {
                bool bigKeyValue;

                if (BigKeyType.HasValue)
                {
                    bigKeyValue = _game.Items[BigKeyType.Value].Current > 0;
                }
                else
                {
                    bigKeyValue = false;
                }

                bigKeyValues.Add(bigKeyValue);
            }
            else
            {
                bigKeyValues.Add(false);

                if (BigKey > 0)
                {
                    bigKeyValues.Add(true);
                }
            }

            return bigKeyValues;
        }

        /// <summary>
        /// Returns the next available DungeonData class in the queue.
        /// </summary>
        /// <returns>
        /// The next available DungeonData class.
        /// </returns>
        private DungeonData GetDungeonData()
        {
            while (true)
            {
                if (_dungeonDataQueue.TryDequeue(out DungeonData result))
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items.
        /// </summary>
        private void UpdateAccessibility()
        {
            if (CanComplete())
            {
                Accessibility = AccessibilityLevel.Normal;
                Accessible = Available;

                for (int i = 0; i < _dungeonData.BossItems.Count; i++)
                {
                    _location.BossSections[i].Accessibility = AccessibilityLevel.Normal;
                }

                return;
            }

            List<BlockingCollection<(List<KeyDoorID>, int, bool)>> keyDoorPermutationQueue =
                new List<BlockingCollection<(List<KeyDoorID>, int, bool)>>();
            List<Task[]> keyDoorTasks = new List<Task[]>();
            BlockingCollection<(List<KeyDoorID>, int, bool)> finalKeyDoorPermutationQueue =
                new BlockingCollection<(List<KeyDoorID>, int, bool)>();
            BlockingCollection<(List<AccessibilityLevel>, AccessibilityLevel, int)> resultQueue =
                new BlockingCollection<(List<AccessibilityLevel>, AccessibilityLevel, int)>();

            for (int i = 0; i <= _dungeonData.SmallKeyDoors.Count; i++)
            {
                keyDoorPermutationQueue.Add(new BlockingCollection<(List<KeyDoorID>, int, bool)>());
            }

            List<int> smallKeyValues = GetSmallKeyValues();
            List<bool> bigKeyValues = GetBigKeyValues();

            foreach (int smallKeyValue in smallKeyValues)
            {
                foreach (bool bigKeyValue in bigKeyValues)
                {
                    keyDoorPermutationQueue[0].Add((new List<KeyDoorID>(), smallKeyValue, bigKeyValue));
                }
            }

            keyDoorPermutationQueue[0].CompleteAdding();

            for (int i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                int currentIteration = i;

                keyDoorTasks.Add(Enumerable.Range(1, Math.Max(1, Environment.ProcessorCount - 1))
                    .Select(_ => Task.Factory.StartNew(() =>
                    {
                        foreach (var item in keyDoorPermutationQueue[currentIteration].GetConsumingEnumerable())
                        {
                            DungeonData dungeonData = GetDungeonData();

                            dungeonData.SetSmallKeyDoorState(item.Item1);
                            dungeonData.SetBigKeyDoorState(item.Item3);

                            int availableKeys = dungeonData.GetFreeKeys() + item.Item2 - item.Item1.Count;

                            if (availableKeys == 0)
                            {
                                finalKeyDoorPermutationQueue.Add(item);
                                _dungeonDataQueue.Enqueue(dungeonData);
                                continue;
                            }

                            List<KeyDoorID> accessibleKeyDoors = dungeonData.GetAccessibleKeyDoors();

                            if (accessibleKeyDoors.Count == 0)
                            {
                                finalKeyDoorPermutationQueue.Add(item);
                                _dungeonDataQueue.Enqueue(dungeonData);
                                continue;
                            }

                            foreach (KeyDoorID keyDoor in accessibleKeyDoors)
                            {
                                List<KeyDoorID> newPermutation = item.Item1.GetRange(0, item.Item1.Count);
                                newPermutation.Add(keyDoor);
                                keyDoorPermutationQueue[currentIteration + 1].Add((newPermutation, item.Item2, item.Item3));
                            }

                            _dungeonDataQueue.Enqueue(dungeonData);
                        }
                    },
                    CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)).ToArray());
            }

            for (int i = 0; i < keyDoorTasks.Count; i++)
            {
                Task.WaitAll(keyDoorTasks[i]);
                keyDoorPermutationQueue[i].Dispose();

                if (i + 1 < keyDoorPermutationQueue.Count)
                {
                    keyDoorPermutationQueue[i + 1].CompleteAdding();
                }
                else
                {
                    finalKeyDoorPermutationQueue.CompleteAdding();
                }
            }

            Task[] finalKeyDoorTasks = Enumerable.Range(1, Math.Max(1, Environment.ProcessorCount - 1))
                .Select(_ => Task.Factory.StartNew(() =>
                {
                    foreach (var item in finalKeyDoorPermutationQueue.GetConsumingEnumerable())
                    {
                        DungeonData dungeonData = GetDungeonData();

                        dungeonData.SetSmallKeyDoorState(item.Item1);
                        dungeonData.SetBigKeyDoorState(item.Item3);

                        if (!dungeonData.ValidateSmallKeyLayout(item.Item2, item.Item3))
                        {
                            _dungeonDataQueue.Enqueue(dungeonData);
                            continue;
                        }

                        if (!dungeonData.ValidateBigKeyPlacement(item.Item3))
                        {
                            _dungeonDataQueue.Enqueue(dungeonData);
                            continue;
                        }

                        List<AccessibilityLevel> bossAccessibility = dungeonData.GetBossAccessibility();

                        (AccessibilityLevel, int) accessibility =
                            dungeonData.GetItemAccessibility(item.Item2, item.Item3);

                        resultQueue.Add((bossAccessibility, accessibility.Item1, accessibility.Item2));

                        _dungeonDataQueue.Enqueue(dungeonData);
                    }
                },
                CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)).ToArray();

            Task.WaitAll(finalKeyDoorTasks);
            finalKeyDoorPermutationQueue.Dispose();
            resultQueue.CompleteAdding();

            List<AccessibilityLevel> lowestBossAccessibilities = new List<AccessibilityLevel>();
            List<AccessibilityLevel> highestBossAccessibilities = new List<AccessibilityLevel>();

            for (int i = 0; i < _dungeonData.BossItems.Count; i++)
            {
                lowestBossAccessibilities.Add(AccessibilityLevel.Normal);
                highestBossAccessibilities.Add(AccessibilityLevel.None);
            }

            AccessibilityLevel lowestAccessibility = AccessibilityLevel.Normal;
            AccessibilityLevel highestAccessibility = AccessibilityLevel.None;
            int highestAccessible = 0;

            foreach (var item in resultQueue.GetConsumingEnumerable())
            {
                for (int i = 0; i < item.Item1.Count; i++)
                {
                    if (item.Item1[i] < lowestBossAccessibilities[i])
                    {
                        lowestBossAccessibilities[i] = item.Item1[i];
                    }

                    if (item.Item1[i] > highestBossAccessibilities[i])
                    {
                        highestBossAccessibilities[i] = item.Item1[i];
                    }
                }

                if (item.Item2 < lowestAccessibility)
                {
                    lowestAccessibility = item.Item2;
                }

                if (item.Item2 > highestAccessibility)
                {
                    highestAccessibility = item.Item2;
                }

                if (item.Item3 > highestAccessible)
                {
                    highestAccessible = item.Item3;
                }
            }

            resultQueue.Dispose();

            AccessibilityLevel finalAccessibility = highestAccessibility;

            if (finalAccessibility == AccessibilityLevel.Normal && lowestAccessibility != AccessibilityLevel.Normal)
            {
                finalAccessibility = AccessibilityLevel.SequenceBreak;
            }

            Accessibility = finalAccessibility;
            Accessible = highestAccessible;

            for (int i = 0; i < _dungeonData.BossItems.Count; i++)
            {
                if (highestBossAccessibilities[i] == AccessibilityLevel.Normal &&
                    lowestBossAccessibilities[i] != AccessibilityLevel.Normal)
                {
                    highestBossAccessibilities[i] = AccessibilityLevel.SequenceBreak;
                }

                _location.BossSections[i].Accessibility = highestBossAccessibilities[i];
            }
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            do
            {
                Available--;
            } while ((Accessibility > AccessibilityLevel.Inspect || force ||
                (Accessibility == AccessibilityLevel.Inspect && Marking == null)) && Available > 0);
        }

        /// <summary>
        /// Returns whether the location has not been fully collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section has been fully collected.
        /// </returns>
        public bool IsAvailable()
        {
            return Available > 0;
        }

        /// <summary>
        /// Resets the section to its starting values.
        /// </summary>
        public void Reset()
        {
            Marking = null;
            Available = Total;
            UserManipulated = false;
        }

        /// <summary>
        /// Initializes the section.
        /// </summary>
        public void Initialize()
        {
            _game.Mode.PropertyChanged += OnModeChanged;

            if (SmallKeyType.HasValue)
            {
                _game.Items[SmallKeyType.Value].PropertyChanged += OnRequirementChanged;
            }

            if (BigKeyType.HasValue)
            {
                _game.Items[BigKeyType.Value].PropertyChanged += OnRequirementChanged;
            }

            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<RequirementType> requirementSubscriptions = new List<RequirementType>();

            if (_location.ID == LocationID.IcePalace)
            {
                _game.Items[ItemType.CaneOfSomaria].PropertyChanged += OnRequirementChanged;
            }

            foreach (DungeonNode node in _dungeonData.RequirementNodes.Values)
            {
                foreach (RequirementNodeConnection connection in node.Connections)
                {
                    if (!nodeSubscriptions.Contains(connection.FromNode))
                    {
                        _game.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    }
                }

                foreach (RequirementNodeConnection dungeonConnection in node.DungeonConnections)
                {
                    if (!requirementSubscriptions.Contains(dungeonConnection.Requirement))
                    {
                        _game.Requirements[dungeonConnection.Requirement].PropertyChanged += OnRequirementChanged;
                    }
                }
            }

            foreach (DungeonItem item in _dungeonData.DungeonItems.Values)
            {
                foreach (RequirementNodeConnection connection in item.Connections)
                {
                    if (!requirementSubscriptions.Contains(connection.Requirement))
                    {
                        _game.Requirements[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    }
                }
            }

            UpdateAccessibility();
        }
    }
}
