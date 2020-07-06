using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing the entrance sections of locations.
    /// </summary>
    public class EntranceSection : ISection
    {
        private readonly IItem _itemProvided;
        private readonly List<RequirementNodeConnection> _connections;

        public bool HasMarking => true;
        public string Name { get; }
        public ModeRequirement ModeRequirement { get; }
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="iD">
        /// The location identity.
        /// </param>
        public EntranceSection(LocationID iD)
        {
            _connections = new List<RequirementNodeConnection>();

            ModeRequirement = new ModeRequirement();
            Available = 1;

            switch (iD)
            {
                case LocationID.LumberjackHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.LumberjackCaveEntrance:
                    {
                        Name = "Dropdown";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LumberjackCaveEntrance,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.DeathMountainEntryCave:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEntryAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DeathMountainExitCave:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainExitAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainExit,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.KakarikoFortuneTellerEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.WomanLeftDoor:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.WomanRightDoor:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.LeftSnitchHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.RightSnitchHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BlindsHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TheWellEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ChickenHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.GrassHouseEntrance:
                    {
                        Name = "Move your lawn";
                        _itemProvided = ItemDictionary.Instance[ItemType.GrassHouseAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GrassHouse,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TavernFront:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.KakarikoShop:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BombHutEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.BombHutAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombHut,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SickKidEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BlacksmithHouse:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MagicBatEntrance:
                    {
                        Name = "Dropdown";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MagicBatLedge,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.ChestGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.RaceHouseLeft:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.RaceGameLedgeAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RaceGameLedge,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.RaceHouseRight:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.LibraryEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ForestHideoutEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ForestHideout,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.ForestChestGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CastleSecretEntrance:
                    {
                        Name = "Dropdown";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretBack,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretFront,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.CastleMainEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CastleLeftEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.HyruleCastleTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CastleRightEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.HyruleCastleTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CastleTowerEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.HyruleCastleTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.AgahnimTowerEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DamEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CentralBonkRocksEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CentralBonkRocks,
                            RequirementType.LWDash, new ModeRequirement()));
                    }
                    break;
                case LocationID.WitchsHutEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LWWitchAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWWitchArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.WaterfallFairyEntrance:
                    {
                        Name = "Waterfall Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.WaterfallFairyAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.WaterfallFairy,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SahasrahlasHutEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TreesFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.PegsFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.EasternPalaceEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HoulihanHole:
                    {
                        Name = "Dropdown";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.NotBunnyLW, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.SanctuaryGrave:
                    {
                        Name = "Dropdown";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.EscapeGrave,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.NorthBonkRocks:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.NorthBonkRocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.KingsTombEntrance:
                    {
                        Name = "The Crypt";
                        _itemProvided = ItemDictionary.Instance[ItemType.LWKingsTombAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.KingsTombGrave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.GraveyardLedgeEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LWGraveyardLedgeAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DesertLeftEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DesertLedgeAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DesertBackEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DesertPalaceBackEntranceAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceBackEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DesertRightEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                    }
                    break;
                case LocationID.DesertFrontEntrance:
                    {
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceFrontEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.AginahsCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ThiefCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.RupeeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RupeeCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SkullWoodsBack:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SkullWoodsBackEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ThievesTownEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ThievesTownEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CShapedHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HammerHouse:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.HammerHouseAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerHouse,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkVillageFortuneTellerEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkChapelEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ShieldShop:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkLumberjack:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TreasureGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BombableShackEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombableShack,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HammerPegsEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.HammerPegsAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerPegs,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BumperCaveExit:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.BumperCaveTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCaveTop,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BumperCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.BumperCaveAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HypeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HypeCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SwampPalaceEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkCentralBonkRocksEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWCentralBonkRocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SouthOfGroveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SouthOfGroveLedge,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BombShop:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ArrowGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkHyliaFortuneTeller:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkTreesFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkSahasrahlaEntrance:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.PalaceOfDarknessEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PalaceOfDarknessEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkWitchsHut:
                    {
                        Name = "House";
                        _itemProvided = ItemDictionary.Instance[ItemType.DWWitchAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkFluteSpotFiveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.FatFairyEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BigBombToWall,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.GanonHole:
                    {
                        Name = "Dropdown";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonHole,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonHoleBack,
                            RequirementType.Inspect, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkIceRodCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkFakeIceRodCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkIceRodRockEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkWorldSouthEastAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodRock,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HypeFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HypeFairyCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.FortuneTellerEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.LakeShop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.UpgradeFairy:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LakeHyliaFairyIslandAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LakeHyliaFairyIsland,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MiniMoldormCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MiniMoldormCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.IceRodCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceRodCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.IceBeeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.IceFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceFairyCave,
                            RequirementType.LWLift1, new ModeRequirement()));
                    }
                    break;
                case LocationID.IcePalaceEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.IcePalaceAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IcePalaceEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MiseryMireEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.MireAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MiseryMireEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MireShackEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.MireAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MireRightShackEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.MireAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MireCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.MireAreaAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CheckerboardCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.CheckerboardLedgeAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CheckerboardCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DeathMountainEntranceBack:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.OldManResidence:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.OldManBackResidence:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DeathMountainExitFront:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SpectacleRockLeft:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SpectacleRockRight:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SpectacleRockTop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SpikeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkMountainFairyEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainWestBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TowerOfHeraEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainWestTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SpiralCaveBottom:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.EDMFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ParadoxCaveMiddle:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ParadoxCaveBottom:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.EDMConnectorBottom:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottomConnector,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SpiralCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.SpiralCaveAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SpiralCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MimicCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.MimicCaveAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MimicCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.EDMConnectorTop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastTopConnectorAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ParadoxCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DeathMountainEastTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SuperBunnyCaveBottom:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DeathMountainShop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainEastBottomAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.SuperBunnyCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HookshotCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TurtleRockEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockFrontEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.GanonsTowerEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = ItemDictionary.Instance[ItemType.DarkDeathMountainTopAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonsTowerEntrance,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TRLedgeLeft:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.TurtleRockTunnelAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TRLedgeRight:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.TurtleRockTunnelAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.TRSafetyDoor:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.TurtleRockSafetyDoorAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockSafetyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HookshotCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = ItemDictionary.Instance[ItemType.DWFloatingIslandAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWFloatingIsland,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.LinksHouseEntrance:
                    {
                        Name = "Link's House";
                        _itemProvided = ItemDictionary.Instance[ItemType.LightWorldAccess];
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
            }

            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<RequirementType> requirementSubscriptions = new List<RequirementType>();

            foreach (RequirementNodeConnection connection in _connections)
            {
                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    RequirementNodeDictionary.Instance[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    nodeSubscriptions.Add(connection.FromNode);
                }

                if (!requirementSubscriptions.Contains(connection.Requirement))
                {
                    RequirementDictionary.Instance[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(connection.Requirement);
                }
            }

            UpdateAccessibility();
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
                if (_itemProvided != null)
                {
                    if (IsAvailable())
                    {
                        _itemProvided.Change(-1, true);
                    }
                    else
                    {
                        _itemProvided.Change(1, true);
                    }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Requirement and RequirementNode
        /// classes that are requirements for dungeon items.
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
        /// Updates the accessibility of the entrance.
        /// </summary>
        private void UpdateAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)RequirementNodeDictionary.Instance[connection.FromNode].Accessibility);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                {
                    continue;
                }

                AccessibilityLevel requirementAccessibility =
                    RequirementDictionary.Instance[connection.Requirement].Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min(Math.Min((byte)nodeAccessibility,
                    (byte)requirementAccessibility), (byte)connection.MaximumAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                {
                    finalAccessibility = AccessibilityLevel.Normal;
                    break;
                }

                if (finalConnectionAccessibility > finalAccessibility)
                {
                    finalAccessibility = finalConnectionAccessibility;
                }
            }

            Accessibility = finalAccessibility;
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            Available = 0;
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
            Available = 1;
        }
    }
}
