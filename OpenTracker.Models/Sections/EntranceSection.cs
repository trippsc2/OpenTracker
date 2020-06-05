using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    public class EntranceSection : ISection
    {
        private readonly Game _game;
        private readonly Item _itemProvided;
        private readonly List<RequirementNodeConnection> _connections;

        public bool HasMarking => true;
        public string Name { get; }
        public Mode RequiredMode { get; }
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

        public EntranceSection(Game game, LocationID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _connections = new List<RequirementNodeConnection>();

            RequiredMode = new Mode();
            Available = 1;

            switch (iD)
            {
                case LocationID.LumberjackHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.LumberjackCaveEntrance:
                    {
                        Name = "Dropdown";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LumberjackCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.DeathMountainEntryCave:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEntryAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DeathMountainExitCave:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainExitAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainExit,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.KakarikoFortuneTellerEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.WomanLeftDoor:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.WomanRightDoor:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.LeftSnitchHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.RightSnitchHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BlindsHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TheWellEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ChickenHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.GrassHouseEntrance:
                    {
                        Name = "Move your lawn";
                        _itemProvided = _game.Items[ItemType.GrassHouseAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GrassHouse,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TavernFront:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.KakarikoShop:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BombHutEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.BombHutAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombHut,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SickKidEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BlacksmithHouse:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MagicBatEntrance:
                    {
                        Name = "Dropdown";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MagicBatLedge,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.ChestGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.RaceHouseLeft:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.RaceGameLedgeAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RaceGameLedge,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.RaceHouseRight:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.LibraryEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ForestHideoutEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ForestHideout,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.ForestChestGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CastleSecretEntrance:
                    {
                        Name = "Dropdown";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretBack,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretFront,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.CastleMainEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CastleLeftEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.HyruleCastleTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CastleRightEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.HyruleCastleTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CastleTowerEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.HyruleCastleTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.AgahnimTowerEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DamEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CentralBonkRocksEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CentralBonkRocks,
                            RequirementType.LWDash, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.WitchsHutEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LWWitchAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWWitchArea,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.WaterfallFairyEntrance:
                    {
                        Name = "Waterfall Cave";
                        _itemProvided = _game.Items[ItemType.WaterfallFairyAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.WaterfallFairy,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SahasrahlasHutEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TreesFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.PegsFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.EasternPalaceEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HoulihanHole:
                    {
                        Name = "Dropdown";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.SanctuaryGrave:
                    {
                        Name = "Dropdown";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.EscapeGrave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.NorthBonkRocks:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.NorthBonkRocks,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.KingsTombEntrance:
                    {
                        Name = "The Crypt";
                        _itemProvided = _game.Items[ItemType.LWKingsTombAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.KingsTombGrave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.GraveyardLedgeEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LWGraveyardLedgeAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DesertLeftEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DesertLedgeAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DesertBackEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DesertPalaceBackEntranceAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceBackEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DesertRightEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];
                    }
                    break;
                case LocationID.DesertFrontEntrance:
                    {
                        Name = "Cave";

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceFrontEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.AginahsCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ThiefCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.RupeeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RupeeCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SkullWoodsBack:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SkullWoodsBackEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ThievesTownEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ThievesTownEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CShapedHouseEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HammerHouse:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.HammerHouseAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerHouse,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkVillageFortuneTellerEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkChapelEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ShieldShop:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkLumberjack:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TreasureGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BombableShackEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldWestAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombableShack,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HammerPegsEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.HammerPegsAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerPegs,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BumperCaveExit:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.BumperCaveTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCaveTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BumperCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.BumperCaveAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HypeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HypeCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SwampPalaceEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkCentralBonkRocksEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWCentralBonkRocks,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SouthOfGroveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SouthOfGroveLedge,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.BombShop:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ArrowGameEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkHyliaFortuneTeller:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkTreesFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkSahasrahlaEntrance:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.PalaceOfDarknessEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PalaceOfDarknessEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkWitchsHut:
                    {
                        Name = "House";
                        _itemProvided = _game.Items[ItemType.DWWitchAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkFluteSpotFiveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.FatFairyEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldEastAccess];

                        // Rework
                    }
                    break;
                case LocationID.GanonHole:
                    {
                        Name = "Dropdown";

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonHole,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonHoleBack,
                            RequirementType.None, new Mode(), AccessibilityLevel.Inspect));
                    }
                    break;
                case LocationID.DarkIceRodCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkFakeIceRodCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkIceRodRockEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkWorldSouthEastAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodRock,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HypeFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HypeFairyCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.FortuneTellerEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.LakeShop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.UpgradeFairy:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LakeHyliaFairyIslandAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LakeHyliaFairyIsland,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MiniMoldormCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MiniMoldormCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.IceRodCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceRodCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.IceBeeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.IceFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceFairyCave,
                            RequirementType.LWLift1, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.IcePalaceEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.IcePalaceAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IcePalaceEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MiseryMireEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.MireAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MiseryMireEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MireShackEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.MireAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MireRightShackEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.MireAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MireCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.MireAreaAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.CheckerboardCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.CheckerboardLedgeAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CheckerboardCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DeathMountainEntranceBack:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.OldManResidence:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.OldManBackResidence:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DeathMountainExitFront:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SpectacleRockLeft:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SpectacleRockRight:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SpectacleRockTop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SpikeCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DarkMountainFairyEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainWestBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TowerOfHeraEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.DeathMountainWestTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SpiralCaveBottom:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.EDMFairyCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ParadoxCaveMiddle:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ParadoxCaveBottom:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.EDMConnectorBottom:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottomConnector,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SpiralCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.SpiralCaveAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SpiralCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.MimicCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.MimicCaveAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MimicCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.EDMConnectorTop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastTopConnectorAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.ParadoxCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DeathMountainEastTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SuperBunnyCaveBottom:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.DeathMountainShop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainEastBottomAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.SuperBunnyCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HookshotCaveEntrance:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TurtleRockEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockFrontEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.GanonsTowerEntrance:
                    {
                        Name = "Dungeon";
                        _itemProvided = _game.Items[ItemType.DarkDeathMountainTopAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonsTowerEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TRLedgeLeft:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TRLedgeRight:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.TurtleRockTunnelAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.TRSafetyDoor:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.TurtleRockSafetyDoorAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockSafetyDoor,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.HookshotCaveTop:
                    {
                        Name = "Cave";
                        _itemProvided = _game.Items[ItemType.DWFloatingIslandAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWFloatingIsland,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
                case LocationID.LinksHouseEntrance:
                    {
                        Name = "Link's House";
                        _itemProvided = _game.Items[ItemType.LightWorldAccess];

                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.Normal));
                    }
                    break;
            }

            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<RequirementType> requirementSubscriptions = new List<RequirementType>();

            foreach (RequirementNodeConnection connection in _connections)
            {
                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    _game.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    nodeSubscriptions.Add(connection.FromNode);
                }

                if (!requirementSubscriptions.Contains(connection.Requirement))
                {
                    _game.Requirements[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(connection.Requirement);
                }
            }

            UpdateAccessibility();
        }

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
            {
                if (_itemProvided != null)
                {
                    if (IsAvailable())
                        _itemProvided.Change(-1, true);
                    else
                        _itemProvided.Change(1, true);
                }
            }
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void UpdateAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)_game.RequirementNodes[connection.FromNode].Accessibility);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                    continue;

                AccessibilityLevel requirementAccessibility =
                    _game.Requirements[connection.Requirement].Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min(Math.Min((byte)nodeAccessibility,
                    (byte)requirementAccessibility), (byte)connection.MaximumAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                {
                    finalAccessibility = AccessibilityLevel.Normal;
                    break;
                }

                if (finalConnectionAccessibility > finalAccessibility)
                    finalAccessibility = finalConnectionAccessibility;
            }

            Accessibility = finalAccessibility;
        }

        public void Clear(bool force)
        {
            Available = 0;
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }

        public void Reset()
        {
            Marking = null;
            Available = 1;
        }
    }
}
