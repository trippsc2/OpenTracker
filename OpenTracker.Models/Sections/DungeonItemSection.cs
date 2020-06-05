using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.Sections
{
    public class DungeonItemSection : ISection
    {
        private readonly Game _game;
        private readonly Location _location;
        private readonly int _mapCompass;
        private readonly int _baseTotal;
        private readonly Dictionary<DungeonItemID, DungeonItem> _dungeonItems;
        private readonly List<RequirementNode> _entryNodes;
        private readonly List<DungeonNode> _dungeonNodes;
        private readonly List<KeyDoor> _smallKeyDoors;
        private readonly List<KeyDoor> _bigKeyDoors;
        private readonly List<KeyLayout> _keyLayouts;
        private readonly List<BigKeyPlacement> _bigKeyPlacements;
        private readonly List<DungeonItem> _bossItems;

        public int SmallKey { get; }
        public int BigKey { get; }
        public ItemType? SmallKeyType { get; }
        public ItemType? BigKeyType { get; }

        public string Name => "Dungeon";
        public bool HasMarking { get; }
        public Mode RequiredMode => new Mode();
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

        public DungeonItemSection(Game game, Location location)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _dungeonItems = new Dictionary<DungeonItemID, DungeonItem>();
            _entryNodes = new List<RequirementNode>();
            _dungeonNodes = new List<DungeonNode>();
            _smallKeyDoors = new List<KeyDoor>();
            _bigKeyDoors = new List<KeyDoor>();
            _keyLayouts = new List<KeyLayout>();
            _bigKeyPlacements = new List<BigKeyPlacement>();
            _bossItems = new List<DungeonItem>();

            DungeonItemID firstItem = DungeonItemID.HCSanctuary;
            DungeonItemID lastItem = DungeonItemID.HCSanctuary;
            RequirementNodeID firstNode = RequirementNodeID.Start;
            RequirementNodeID lastNode = RequirementNodeID.Start;
            RequirementNodeID firstEntryNode = RequirementNodeID.HCSanctuaryEntry;
            RequirementNodeID lastEntryNode = RequirementNodeID.HCSanctuaryEntry;
            KeyDoorID? firstSmallKeyDoor = null;
            KeyDoorID? lastSmallKeyDoor = null;
            KeyDoorID? firstBigKeyDoor = null;
            KeyDoorID? lastBigKeyDoor = null;
            DungeonItemID? firstBossItem = null;
            DungeonItemID? lastBossItem = null;

            switch (_location.ID)
            {
                case LocationID.HyruleCastle:
                    {
                        _mapCompass = 1;
                        SmallKey = 1;
                        SmallKeyType = ItemType.HCSmallKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight
                        }, 1, new List<DungeonItemID>(), new Mode()));
                        firstItem = DungeonItemID.HCSanctuary;
                        lastItem = DungeonItemID.HCSecretRoomRight;
                        firstSmallKeyDoor = KeyDoorID.HCEscapeFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.HCSewerRatRoomKeyDoor;
                        firstNode = RequirementNodeID.HCSanctuary;
                        lastNode = RequirementNodeID.HCBack;
                        firstEntryNode = RequirementNodeID.HCSanctuaryEntry;
                        lastEntryNode = RequirementNodeID.HCBackEntry;
                    }
                    break;
                case LocationID.AgahnimTower:
                    {
                        SmallKey = 2;
                        SmallKeyType = ItemType.ATSmallKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.ATRoom03,
                            DungeonItemID.ATDarkMaze
                        }, 2, new List<DungeonItemID>(), new Mode()));
                        firstItem = DungeonItemID.ATRoom03;
                        lastItem = DungeonItemID.ATDarkMaze;
                        firstSmallKeyDoor = KeyDoorID.ATFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.ATFourthKeyDoor;
                        firstNode = RequirementNodeID.AT;
                        lastNode = RequirementNodeID.ATBoss;
                        firstEntryNode = RequirementNodeID.ATEntry;
                        lastEntryNode = RequirementNodeID.ATEntry;
                        firstBossItem = DungeonItemID.ATBoss;
                        lastBossItem = DungeonItemID.ATBoss;
                    }
                    break;
                case LocationID.EasternPalace:
                    {
                        _mapCompass = 2;
                        BigKey = 1;
                        BigKeyType = ItemType.EPBigKey;
                        _bigKeyPlacements = new List<BigKeyPlacement>
                        {
                            new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.EPCannonballChest,
                            DungeonItemID.EPMapChest,
                            DungeonItemID.EPCompassChest,
                            DungeonItemID.EPBigKeyChest,
                        }, new Mode())
                        };
                        firstItem = DungeonItemID.EPCannonballChest;
                        lastItem = DungeonItemID.EPBoss;
                        firstSmallKeyDoor = KeyDoorID.EPRightWingKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.EPBossKeyDoor;
                        firstBigKeyDoor = KeyDoorID.EPBigChest;
                        lastBigKeyDoor = KeyDoorID.EPBigKeyDoor;
                        firstNode = RequirementNodeID.EP;
                        lastNode = RequirementNodeID.EPBoss;
                        firstEntryNode = RequirementNodeID.EPEntry;
                        lastEntryNode = RequirementNodeID.EPEntry;
                        firstBossItem = DungeonItemID.EPBoss;
                        lastBossItem = DungeonItemID.EPBoss;
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        _mapCompass = 2;
                        SmallKey = 1;
                        BigKey = 1;
                        SmallKeyType = ItemType.DPSmallKey;
                        BigKeyType = ItemType.DPBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest
                        }, 1, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPCompassChest,
                            DungeonItemID.DPBigKeyChest
                        }, new Mode()));
                        firstItem = DungeonItemID.DPMapChest;
                        lastItem = DungeonItemID.DPBoss;
                        firstNode = RequirementNodeID.DPFront;
                        lastNode = RequirementNodeID.DPBoss;
                        firstEntryNode = RequirementNodeID.DPFrontEntry;
                        lastEntryNode = RequirementNodeID.DPBackEntry;
                        firstSmallKeyDoor = KeyDoorID.DPRightWingKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.DP2FSecondKeyDoor;
                        firstBigKeyDoor = KeyDoorID.DPBigChest;
                        lastBigKeyDoor = KeyDoorID.DPBigKeyDoor;
                        firstBossItem = DungeonItemID.DPBoss;
                        lastBossItem = DungeonItemID.DPBoss;
                    }
                    break;
                case LocationID.TowerOfHera:
                    {
                        _mapCompass = 2;
                        SmallKey = 1;
                        SmallKeyType = ItemType.ToHSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.ToHBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest,
                            DungeonItemID.ToHBoss
                        }, 1, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest
                        }, new Mode()));
                        firstItem = DungeonItemID.ToHBasementCage;
                        lastItem = DungeonItemID.ToHBoss;
                        firstNode = RequirementNodeID.ToH;
                        lastNode = RequirementNodeID.ToHBoss;
                        firstEntryNode = RequirementNodeID.ToHEntry;
                        lastEntryNode = RequirementNodeID.ToHEntry;
                        firstSmallKeyDoor = KeyDoorID.ToHKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.ToHKeyDoor;
                        firstBigKeyDoor = KeyDoorID.ToHBigKeyDoor;
                        lastBigKeyDoor = KeyDoorID.ToHBigChest;
                        firstBossItem = DungeonItemID.ToHBoss;
                        lastBossItem = DungeonItemID.ToHBoss;
                    }
                    break;
                case LocationID.PalaceOfDarkness:
                    {
                        _mapCompass = 2;
                        SmallKey = 6;
                        SmallKeyType = ItemType.PoDSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.PoDBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.PoDShooterRoom,
                            DungeonItemID.PoDMapChest,
                            DungeonItemID.PoDArenaLedge,
                            DungeonItemID.PoDBigKeyChest,
                            DungeonItemID.PoDStalfosBasement,
                            DungeonItemID.PoDArenaBridge
                        }, 4, new List<DungeonItemID>(), new Mode()));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, 6, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
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
                        }, new Mode()));
                        firstItem = DungeonItemID.PoDShooterRoom;
                        lastItem = DungeonItemID.PoDBoss;
                        firstNode = RequirementNodeID.PoD;
                        lastNode = RequirementNodeID.PoDBoss;
                        firstEntryNode = RequirementNodeID.PoDEntry;
                        lastEntryNode = RequirementNodeID.PoDEntry;
                        firstSmallKeyDoor = KeyDoorID.PoDFrontKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.PoDBossAreaKeyDoor;
                        firstBigKeyDoor = KeyDoorID.PoDBigChest;
                        lastBigKeyDoor = KeyDoorID.PoDBigKeyDoor;
                        firstBossItem = DungeonItemID.PoDBoss;
                        lastBossItem = DungeonItemID.PoDBoss;
                    }
                    break;
                case LocationID.SwampPalace:
                    {
                        _mapCompass = 2;
                        SmallKey = 1;
                        SmallKeyType = ItemType.SPSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.SPBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.SPEntrance
                        }, 1, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
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
                        }, new Mode()));
                        firstItem = DungeonItemID.SPEntrance;
                        lastItem = DungeonItemID.SPBoss;
                        firstNode = RequirementNodeID.SP;
                        lastNode = RequirementNodeID.SPBoss;
                        firstEntryNode = RequirementNodeID.SPEntry;
                        lastEntryNode = RequirementNodeID.SPEntry;
                        firstSmallKeyDoor = KeyDoorID.SP1FKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.SPBossRoomKeyDoor;
                        firstBigKeyDoor = KeyDoorID.SPBigChest;
                        lastBigKeyDoor = KeyDoorID.SPBigChest;
                        firstBossItem = DungeonItemID.SPBoss;
                        lastBossItem = DungeonItemID.SPBoss;
                    }
                    break;
                case LocationID.SkullWoods:
                    {
                        _mapCompass = 2;
                        SmallKey = 3;
                        SmallKeyType = ItemType.SWSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.SWBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.SWPinballRoom
                        }, 1, new List<DungeonItemID>(), new Mode()
                        {
                            ItemPlacement = ItemPlacement.Basic
                        }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom
                        }, 3, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                        }, new Mode()));
                        firstItem = DungeonItemID.SWBigKeyChest;
                        lastItem = DungeonItemID.SWBoss;
                        firstNode = RequirementNodeID.SWBigChestAreaBottom;
                        lastNode = RequirementNodeID.SWBoss;
                        firstEntryNode = RequirementNodeID.SWFrontEntry;
                        lastEntryNode = RequirementNodeID.SWBackEntry;
                        firstSmallKeyDoor = KeyDoorID.SWFrontLeftKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.SWBackSeceondKeyDoor;
                        firstBigKeyDoor = KeyDoorID.SWBigChest;
                        lastBigKeyDoor = KeyDoorID.SWBigChest;
                        firstBossItem = DungeonItemID.SWBoss;
                        lastBossItem = DungeonItemID.SWBoss;
                    }
                    break;
                case LocationID.ThievesTown:
                    {
                        _mapCompass = 2;
                        SmallKey = 1;
                        SmallKeyType = ItemType.TTSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.TTBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTAttic,
                            DungeonItemID.TTBlindsCell,
                            DungeonItemID.TTBigChest,
                            DungeonItemID.TTBoss
                        }, 1, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest
                        }, new Mode()));
                        firstItem = DungeonItemID.TTMapChest;
                        lastItem = DungeonItemID.TTBoss;
                        firstNode = RequirementNodeID.TT;
                        lastNode = RequirementNodeID.TTBoss;
                        firstEntryNode = RequirementNodeID.TTEntry;
                        lastEntryNode = RequirementNodeID.TTEntry;
                        firstSmallKeyDoor = KeyDoorID.TTFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.TTBigChestKeyDoor;
                        firstBigKeyDoor = KeyDoorID.TTBigKeyDoor;
                        lastBigKeyDoor = KeyDoorID.TTBigChest;
                        firstBossItem = DungeonItemID.TTBoss;
                        lastBossItem = DungeonItemID.TTBoss;
                    }
                    break;
                case LocationID.IcePalace:
                    {
                        _mapCompass = 2;
                        SmallKey = 2;
                        SmallKeyType = ItemType.IPSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.IPBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom
                        }, 1, new List<DungeonItemID>(), new Mode()
                        {
                            ItemPlacement = ItemPlacement.Advanced
                        }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom,
                            DungeonItemID.IPBoss
                        }, 2, new List<DungeonItemID>(), new Mode()
                        {
                            ItemPlacement = ItemPlacement.Advanced
                        }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom
                        }, 2, new List<DungeonItemID>(), new Mode()
                        {
                            ItemPlacement = ItemPlacement.Basic
                        }));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPIcedTRoom,
                        }, new Mode()));
                        firstItem = DungeonItemID.IPCompassChest;
                        lastItem = DungeonItemID.IPBoss;
                        firstNode = RequirementNodeID.IP;
                        lastNode = RequirementNodeID.IPBoss;
                        firstEntryNode = RequirementNodeID.IPEntry;
                        lastEntryNode = RequirementNodeID.IPEntry;
                        firstSmallKeyDoor = KeyDoorID.IP1FKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.IPB6KeyDoor;
                        firstBigKeyDoor = KeyDoorID.IPBigKeyDoor;
                        lastBigKeyDoor = KeyDoorID.IPBigChest;
                        firstBossItem = DungeonItemID.IPBoss;
                        lastBossItem = DungeonItemID.IPBoss;
                    }
                    break;
                case LocationID.MiseryMire:
                    {
                        _mapCompass = 2;
                        SmallKey = 3;
                        SmallKeyType = ItemType.MMSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.MMBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        }, 3, new List<DungeonItemID>(), new Mode() { DungeonItemShuffle = DungeonItemShuffle.Keysanity }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, new Mode()));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMMainLobby,
                                DungeonItemID.MMCompassChest,
                                DungeonItemID.MMBigKeyChest,
                                DungeonItemID.MMMapChest,
                            }, new Mode()));
                        firstItem = DungeonItemID.MMBridgeChest;
                        lastItem = DungeonItemID.MMBoss;
                        firstNode = RequirementNodeID.MM;
                        lastNode = RequirementNodeID.MMBoss;
                        firstEntryNode = RequirementNodeID.MMEntry;
                        lastEntryNode = RequirementNodeID.MMEntry;
                        firstSmallKeyDoor = KeyDoorID.MMB1TopRightKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.MMB2WorthlessKeyDoor;
                        firstBigKeyDoor = KeyDoorID.MMBigChest;
                        lastBigKeyDoor = KeyDoorID.MMBossRoomBigKeyDoor;
                        firstBossItem = DungeonItemID.MMBoss;
                        lastBossItem = DungeonItemID.MMBoss;
                    }
                    break;
                case LocationID.TurtleRock:
                    {
                        _mapCompass = 2;
                        SmallKey = 4;
                        SmallKeyType = ItemType.TRSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.TRBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight
                        }, 1, new List<DungeonItemID>(), new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight,
                            DungeonItemID.TRChainChomps
                        }, 2, new List<DungeonItemID>(), new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight,
                            DungeonItemID.TRChainChomps,
                            DungeonItemID.TRBigKeyChest,
                            DungeonItemID.TRBigChest,
                            DungeonItemID.TRCrystarollerRoom
                        }, 3, new List<DungeonItemID>(), new Mode()
                        {
                            WorldState = WorldState.StandardOpen,
                            EntranceShuffle = false
                        }));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, 4, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
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
                            }, new Mode()
                            {
                                WorldState = WorldState.StandardOpen,
                                EntranceShuffle = false
                            }));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
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
                            }, new Mode()
                            {
                                WorldState = WorldState.StandardOpen,
                                EntranceShuffle = true
                            }));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
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
                            }, new Mode()
                            {
                                WorldState = WorldState.Inverted
                            }));
                        firstItem = DungeonItemID.TRCompassChest;
                        lastItem = DungeonItemID.TRBoss;
                        firstNode = RequirementNodeID.TRFront;
                        lastNode = RequirementNodeID.TRBoss;
                        firstEntryNode = RequirementNodeID.TRFrontEntry;
                        lastEntryNode = RequirementNodeID.TRBackEntry;
                        firstSmallKeyDoor = KeyDoorID.TR1FFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.TRB2KeyDoor;
                        firstBigKeyDoor = KeyDoorID.TRBigChest;
                        lastBigKeyDoor = KeyDoorID.TRBossRoomBigKeyDoor;
                        firstBossItem = DungeonItemID.TRBoss;
                        lastBossItem = DungeonItemID.TRBoss;
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        _mapCompass = 2;
                        SmallKey = 4;
                        SmallKeyType = ItemType.GTSmallKey;
                        BigKey = 1;
                        BigKeyType = ItemType.GTBigKey;
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        }, 2, new List<DungeonItemID>(), new Mode()));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, 3, new List<DungeonItemID>(), new Mode()));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, new Mode()));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, new Mode()));
                        _keyLayouts.Add(new KeyLayout(new List<DungeonItemID>
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
                        }, 4, new List<DungeonItemID>(), new Mode()));
                        _bigKeyPlacements.Add(new BigKeyPlacement(new List<DungeonItemID>
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
                            }, new Mode()));
                        firstItem = DungeonItemID.GTHopeRoomLeft;
                        lastItem = DungeonItemID.GTMoldormChest;
                        firstNode = RequirementNodeID.GT;
                        lastNode = RequirementNodeID.GTFinalBoss;
                        firstEntryNode = RequirementNodeID.GTEntry;
                        lastEntryNode = RequirementNodeID.GTEntry;
                        firstSmallKeyDoor = KeyDoorID.GT1FLeftToRightKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.GT6FSecondKeyDoor;
                        firstBigKeyDoor = KeyDoorID.GTBigChest;
                        lastBigKeyDoor = KeyDoorID.GT7FBigKeyDoor;
                        firstBossItem = DungeonItemID.GTBoss1;
                        lastBossItem = DungeonItemID.GTFinalBoss;
                    }
                    break;
            }

            for (int i = (int)firstItem; i <= (int)lastItem; i++)
                _dungeonItems.Add((DungeonItemID)i, new DungeonItem(_game, (DungeonItemID)i));

            for (int i = (int)firstNode; i <= (int)lastNode; i++)
                _dungeonNodes.Add((DungeonNode)_game.RequirementNodes[(RequirementNodeID)i]);

            for (int i = (int)firstEntryNode; i <= (int)lastEntryNode; i++)
                _entryNodes.Add(_game.RequirementNodes[(RequirementNodeID)i]);

            if (firstSmallKeyDoor.HasValue && lastSmallKeyDoor.HasValue)
            {
                for (int i = (int)firstSmallKeyDoor.Value; i <= (int)lastSmallKeyDoor.Value; i++)
                    _smallKeyDoors.Add(_game.KeyDoors[(KeyDoorID)i]);
            }

            if (firstBigKeyDoor.HasValue && lastBigKeyDoor.HasValue)
            {
                for (int i = (int)firstBigKeyDoor.Value; i <= (int)lastBigKeyDoor.Value; i++)
                    _bigKeyDoors.Add(_game.KeyDoors[(KeyDoorID)i]);
            }

            if (firstBossItem.HasValue && lastBossItem.HasValue)
            {
                for (int i = (int)firstBossItem.Value; i <= (int)lastBossItem.Value; i++)
                {
                    if (_dungeonItems.ContainsKey((DungeonItemID)i))
                        _bossItems.Add(_dungeonItems[(DungeonItemID)i]);
                    else
                        _bossItems.Add(new DungeonItem(_game, (DungeonItemID)i));
                }
            }

            _baseTotal = _dungeonItems.Count - _mapCompass - SmallKey - BigKey;

            SetTotal(false);
        }

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
                UpdateAccessibility();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.DungeonItemShuffle))
                SetTotal();
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnNodeRequirementChanged(object sender, EventArgs e)
        {
            UpdateAccessibility();
        }

        private void SetTotal(bool updateAccessibility = true)
        {
            int newTotal = _baseTotal +
                (_game.Mode.MapCompassShuffle ? _mapCompass : 0) +
                (_game.Mode.SmallKeyShuffle ? SmallKey : 0) +
                (_game.Mode.BigKeyShuffle ? BigKey : 0);

            int delta = newTotal - Total;

            Total = newTotal;
            Available = Math.Max(0, Math.Min(Total, Available + delta));

            if (updateAccessibility)
                UpdateAccessibility();
        }

        private int GetFreeKeys()
        {
            int freeKeys = 0;

            foreach (DungeonNode node in _dungeonNodes)
            {
                if (node.Accessibility >= AccessibilityLevel.SequenceBreak)
                    freeKeys += node.FreeKeysProvided;
            }

            return freeKeys;
        }

        private void SetKeyDoorState(List<KeyDoorID> unlockedDoors)
        {
            foreach (KeyDoor smallKeyDoor in _smallKeyDoors)
            {
                if (unlockedDoors.Contains(smallKeyDoor.ID))
                    smallKeyDoor.Unlocked = true;
                else
                    smallKeyDoor.Unlocked = false;
            }
        }

        private void SetBigKeyDoorState(bool unlocked)
        {
            foreach (KeyDoor bigKeyDoor in _bigKeyDoors)
                bigKeyDoor.Unlocked = unlocked;
        }

        private List<KeyDoor> GetAccessibleKeyDoors()
        {
            List<KeyDoor> accessibleKeyDoors = new List<KeyDoor>();

            foreach (KeyDoor keyDoor in _smallKeyDoors)
            {
                if (keyDoor.Accessibility >= AccessibilityLevel.SequenceBreak && !keyDoor.Unlocked)
                    accessibleKeyDoors.Add(keyDoor);
            }

            return accessibleKeyDoors;
        }

        private (bool, KeyDoorID?) KeyDoorLoop(Dictionary<List<KeyDoorID>,
            (AccessibilityLevel, int)> testedPermutations, List<KeyDoorID> currentPermutation,
            List<AccessibilityLevel> bossAccessibility, int keysCollected, bool bigKeyCollected,
            KeyDoorID? startAt)
        {
            SetKeyDoorState(currentPermutation);
            SetBigKeyDoorState(bigKeyCollected);

            int availableKeys = GetFreeKeys() + keysCollected - currentPermutation.Count;
            KeyDoorID? newStartAt;

            List<KeyDoor> accessibleKeyDoors = GetAccessibleKeyDoors();

            if (availableKeys == 0 || accessibleKeyDoors.Count == 0)
            {
                if (!testedPermutations.Keys.Any(x => x.SequenceEqual(currentPermutation)))
                {
                    List<KeyDoorID> newPermutation = currentPermutation.GetRange(0, currentPermutation.Count);

                    if (!ValidateSmallKeyLayout(keysCollected, bigKeyCollected))
                        testedPermutations.Add(newPermutation, (AccessibilityLevel.None, 0));
                    else if (!ValidateBigKeyPlacement(bigKeyCollected))
                        testedPermutations.Add(newPermutation, (AccessibilityLevel.None, 0));
                    else
                    {
                        testedPermutations.Add(newPermutation, GetCurrentAccessibility(bossAccessibility,
                            keysCollected, bigKeyCollected));
                    }
                }

                if (currentPermutation.Count == 0)
                    return (false, null);
                
                newStartAt = currentPermutation[^1];
                currentPermutation.RemoveAt(currentPermutation.Count - 1);
                
                return (true, newStartAt);
            }

            bool startAtFound = !startAt.HasValue;

            foreach (KeyDoor door in accessibleKeyDoors)
            {
                if (!startAtFound)
                {
                    if (door.ID == startAt.Value)
                        startAtFound = true;

                    continue;
                }
                
                currentPermutation.Add(door.ID);
                
                return (true, null);
            }

            if (currentPermutation.Count > 0)
            {
                newStartAt = currentPermutation[^1];
                currentPermutation.RemoveAt(currentPermutation.Count - 1);

                return (true, newStartAt);
            }

            return (false, null);
        }

        private bool ValidateSmallKeyLayout(int keysCollected, bool bigKeyCollected)
        {
            if (SmallKey == 0)
                return true;

            if (_game.Mode.SmallKeyShuffle)
                return true;

            foreach (KeyLayout keyLayout in _keyLayouts)
            {
                if (!_game.Mode.Validate(keyLayout.RequiredMode))
                    continue;

                if (BigKey > 0)
                {
                    if (_game.Mode.BigKeyShuffle)
                    {
                        if (keyLayout.BigKeyLocations.Count > 0)
                            continue;
                    }
                    else if (bigKeyCollected)
                    {
                        bool anyBigKeyLocationsAccessible = false;

                        foreach (DungeonItemID iD in keyLayout.BigKeyLocations)
                        {
                            if (_dungeonItems[iD].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                anyBigKeyLocationsAccessible = true;
                                break;
                            }
                        }

                        if (!anyBigKeyLocationsAccessible)
                            continue;
                    }
                    else
                    {
                        bool allBigKeysAccessible = true;

                        foreach (DungeonItemID iD in keyLayout.BigKeyLocations)
                        {
                            if (_dungeonItems[iD].Accessibility < AccessibilityLevel.SequenceBreak)
                            {
                                allBigKeysAccessible = false;
                                break;
                            }
                        }

                        if (allBigKeysAccessible)
                            continue;
                    }
                }

                int inaccessibleItems = 0;

                foreach (DungeonItemID item in keyLayout.SmallKeyLocations)
                {
                    if (_dungeonItems[item].Accessibility < AccessibilityLevel.SequenceBreak)
                        inaccessibleItems++;
                }

                int inaccessibleKeys = Math.Max(0, inaccessibleItems -
                    (keyLayout.SmallKeyLocations.Count - keyLayout.SmallKeyCount));

                if (SmallKey - keysCollected < inaccessibleKeys)
                    return false;
            }

            return true;
        }

        private bool ValidateBigKeyPlacement(bool bigKeyCollected)
        {
            if (BigKey == 0)
                return true;

            if (_game.Mode.BigKeyShuffle)
                return true;

            foreach (BigKeyPlacement placement in _bigKeyPlacements)
            {
                if (!_game.Mode.Validate(placement.RequiredMode))
                    continue;

                if (bigKeyCollected)
                {
                    foreach (DungeonItemID item in placement.Placements)
                    {
                        if (_dungeonItems[item].Accessibility >= AccessibilityLevel.SequenceBreak)
                            return true;
                    }
                }
                else
                {
                    foreach (DungeonItemID item in placement.Placements)
                    {
                        if (_dungeonItems[item].Accessibility < AccessibilityLevel.SequenceBreak)
                            return true;
                    }
                }
            }

            return false;
        }

        private (AccessibilityLevel, int) GetCurrentAccessibility(List<AccessibilityLevel> bossAccessibility,
            int keysCollected, bool bigKeyCollected)
        {
            int inaccessibleItems = 0;
            bool sequenceBreak = false;

            for (int i = 0; i < _bossItems.Count; i++)
            {
                if (_bossItems[i].Accessibility > bossAccessibility[i])
                    bossAccessibility[i] = _bossItems[i].Accessibility;
            }

            foreach (DungeonItem item in _dungeonItems.Values)
            {
                switch (item.Accessibility)
                {
                    case AccessibilityLevel.None:
                        inaccessibleItems++;
                        break;
                    case AccessibilityLevel.Inspect:
                    case AccessibilityLevel.SequenceBreak:
                        sequenceBreak = true;
                        break;
                }
            }

            if (inaccessibleItems == 0)
            {
                if (sequenceBreak)
                    return (AccessibilityLevel.SequenceBreak, Available);
                else
                    return (AccessibilityLevel.Normal, Available);
            }

            if (!_game.Mode.MapCompassShuffle)
                inaccessibleItems = Math.Max(0, inaccessibleItems - _mapCompass);

            if (inaccessibleItems == 0)
                return (AccessibilityLevel.SequenceBreak, Available);

            if (!_game.Mode.BigKeyShuffle && !bigKeyCollected)
                inaccessibleItems -= BigKey;

            if (inaccessibleItems == 0)
                return (AccessibilityLevel.SequenceBreak, Available);

            if (!_game.Mode.SmallKeyShuffle)
                inaccessibleItems -= SmallKey - keysCollected;

            if (inaccessibleItems == 0)
                return (AccessibilityLevel.SequenceBreak, Available);

            if (Available - inaccessibleItems < 1)
                return (AccessibilityLevel.None, 0);

            return (AccessibilityLevel.Partial, Available - inaccessibleItems);
        }

        private void UpdateAccessibility()
        {
            List<int> smallKeysCollectedCounts = new List<int>();
            List<bool> bigKeyCollectedValues = new List<bool>();

            if (_game.Mode.SmallKeyShuffle)
            {
                int smallKeyCount;

                if (SmallKeyType.HasValue)
                {
                    smallKeyCount = Math.Min(SmallKey, _game.Items[SmallKeyType.Value].Current +
                        (_game.Mode.WorldState == WorldState.Retro ? _game.Items[ItemType.SmallKey].Current : 0));
                }
                else
                    smallKeyCount = 0;

                smallKeysCollectedCounts.Add(smallKeyCount);
            }
            else
            {
                for (int i = 0; i <= SmallKey; i++)
                    smallKeysCollectedCounts.Add(i);
            }

            if (_game.Mode.BigKeyShuffle)
            {
                bool bigKeyValue;

                if (BigKeyType.HasValue)
                    bigKeyValue = _game.Items[BigKeyType.Value].Current > 0;
                else
                    bigKeyValue = false;

                bigKeyCollectedValues.Add(bigKeyValue);
            }
            else
            {
                bigKeyCollectedValues.Add(false);

                if (BigKey > 0)
                    bigKeyCollectedValues.Add(true);
            }

            Dictionary<(int, bool), (AccessibilityLevel, int)> results =
                new Dictionary<(int, bool), (AccessibilityLevel, int)>();

            List<AccessibilityLevel> bossAccessibility = new List<AccessibilityLevel>();

            for (int i = 0; i < _bossItems.Count; i++)
                bossAccessibility.Add(AccessibilityLevel.None);

            foreach (int smallKeysCollected in smallKeysCollectedCounts)
            {
                foreach (bool bigKeyCollected in bigKeyCollectedValues)
                {
                    Dictionary<List<KeyDoorID>, (AccessibilityLevel, int)> testedPermutations =
                        new Dictionary<List<KeyDoorID>, (AccessibilityLevel, int)>();
                    List<KeyDoorID> currentPermutation = new List<KeyDoorID>();
                    KeyDoorID? startAt = null;

                    bool continueIterating;

                    do
                    {
                        (bool, KeyDoorID?) permutationResults =
                            KeyDoorLoop(testedPermutations, currentPermutation,
                            bossAccessibility, smallKeysCollected, bigKeyCollected,
                            startAt);
                        continueIterating = permutationResults.Item1;
                        startAt = permutationResults.Item2;
                    } while (continueIterating);

                    foreach ((AccessibilityLevel, int) result in testedPermutations.Values)
                    {
                        if (results.ContainsKey((smallKeysCollected, bigKeyCollected)))
                        {
                            if (result.Item1 > results[(smallKeysCollected, bigKeyCollected)].Item1)
                                results[(smallKeysCollected, bigKeyCollected)] = result;
                            else if (result.Item1 == results[(smallKeysCollected, bigKeyCollected)].Item1)
                            {
                                if (result.Item2 > results[(smallKeysCollected, bigKeyCollected)].Item2)
                                    results[(smallKeysCollected, bigKeyCollected)] = result;
                            }
                        }
                        else
                            results.Add((smallKeysCollected, bigKeyCollected), result);
                    }
                }
            }

            (AccessibilityLevel, int) finalResult = (AccessibilityLevel.None, 0);

            foreach ((AccessibilityLevel, int) result in results.Values)
            {
                if (finalResult.Item1 < result.Item1)
                    finalResult = result;
                else if (finalResult.Item1 == result.Item1)
                {
                    if (finalResult.Item2 < result.Item2)
                        finalResult = result;
                }
            }

            Accessibility = finalResult.Item1;
            Accessible = finalResult.Item2;

            for (int i = 0; i < _bossItems.Count; i++)
            {
                _location.BossSections[i].Accessibility = bossAccessibility[i];
            }
        }

        public void Clear(bool force)
        {
            do
            {
                Available--;
            } while ((Accessibility > AccessibilityLevel.Inspect || force ||
                (Accessibility == AccessibilityLevel.Inspect && Marking == null)) && Available > 0);
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }

        public void Reset()
        {
            Marking = null;
            Available = Total;
            UserManipulated = false;
        }

        public void Initialize()
        {
            _game.Mode.PropertyChanged += OnModeChanged;

            if (SmallKeyType.HasValue)
                _game.Items[SmallKeyType.Value].PropertyChanged += OnRequirementChanged;

            if (BigKeyType.HasValue)
                _game.Items[BigKeyType.Value].PropertyChanged += OnRequirementChanged;

            foreach (DungeonNode node in _dungeonNodes)
                node.RequirementChanged += OnNodeRequirementChanged;

            foreach (RequirementNode node in _entryNodes)
                node.PropertyChanged += OnRequirementChanged;

            UpdateAccessibility();
        }
    }
}
