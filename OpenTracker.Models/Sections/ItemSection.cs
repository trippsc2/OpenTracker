using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing item sections of locations.
    /// </summary>
    public class ItemSection : ISection
    {
        private readonly Game _game;
        private readonly Location _location;
        private readonly List<RequirementNodeConnection> _connections;

        public string Name { get; }
        public bool HasMarking { get; }
        public ModeRequirement ModeRequirement { get; }
        public Action AutoTrack { get; }
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

        public int Total { get; }

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
        /// <param name="index">
        /// The index of the location.
        /// </param>
        public ItemSection(Game game, Location location, int index = 0)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _connections = new List<RequirementNodeConnection>();

            switch (_location.ID)
            {
                case LocationID.Pedestal:
                    {
                        Total = 1;
                        Name = "Pedestal";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Pedestal, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 128, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[128].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.LumberjackCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LumberjackCaveEntrance,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 453, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[453].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BlindsHouse when index == 0:
                    {
                        Total = 4;
                        Name = "Main";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[4]
                                {
                                    (MemorySegmentType.Room, 570, 32),
                                    (MemorySegmentType.Room, 570, 64),
                                    (MemorySegmentType.Room, 570, 128),
                                    (MemorySegmentType.Room, 571, 1)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[570].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.RoomMemory[571].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BlindsHouse:
                    {
                        Total = 1;
                        Name = "Bomb";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 570, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[570].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.TheWell when index == 0:
                    {
                        Total = 4;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.SuperBunnyFallInHole, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[4]
                                {
                                    (MemorySegmentType.Room, 94, 32),
                                    (MemorySegmentType.Room, 94, 64),
                                    (MemorySegmentType.Room, 94, 128),
                                    (MemorySegmentType.Room, 95, 1)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[94].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.RoomMemory[95].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.TheWell:
                    {
                        Total = 1;
                        Name = "Bomb";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 94, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[94].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BottleVendor:
                    {
                        Total = 1;
                        Name = "Man";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.ItemMemory[137].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ChickenHouse:
                    {
                        Total = 1;
                        Name = "Bombable Wall";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 528, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[528].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Tavern:
                    {
                        Total = 1;
                        Name = "Back Room";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 518, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[518].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SickKid:
                    {
                        Total = 1;
                        Name = "By The Bed";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Bottle, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 4);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.MagicBat:
                    {
                        Total = 1;
                        Name = "Magic Bowl";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MagicBatLedge,
                            RequirementType.LWPowder, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 128);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.RaceGame:
                    {
                        Total = 1;
                        Name = "Take This Trash";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RaceGameLedge,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 40, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[40].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Library:
                    {
                        Total = 1;
                        Name = "On The Shelf";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWDash, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 128);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.MushroomSpot:
                    {
                        Total = 1;
                        Name = "Shroom";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ForestHideout:
                    {
                        Total = 1;
                        Name = "Hideout";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ForestHideout,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 451, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[451].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.CastleSecret when index == 0:
                    {
                        Total = 1;
                        Name = "Uncle";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretFront,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretBack,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 134, 1);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.ItemMemory[134].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.CastleSecret:
                    {
                        Total = 1;
                        Name = "Hallway";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretFront,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CastleSecretBack,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 170, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[170].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.LinksHouse:
                    {
                        Total = 1;
                        Name = "By The Door";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 1, 4),
                                    (MemorySegmentType.Room, 520, 16)
                                });

                            if (result.HasValue)
                            {
                                Available = result.Value > 0 ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[1].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.RoomMemory[520].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.GroveDiggingSpot:
                    {
                        Total = 1;
                        Name = "Hidden Treasure";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWShovel, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 42, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[42].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.PyramidLedge:
                    {
                        Total = 1;
                        Name = "Ledge";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 91, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[91].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.FatFairy:
                    {
                        Total = 2;
                        Name = "Big Bomb Spot";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.FatFairy,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 556, 16),
                                    (MemorySegmentType.Room, 556, 32)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[556].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.HauntedGrove:
                    {
                        Total = 1;
                        Name = "Stumpy";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 8);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.HypeCave:
                    {
                        Total = 5;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[5]
                                {
                                    (MemorySegmentType.Room, 572, 16),
                                    (MemorySegmentType.Room, 572, 32),
                                    (MemorySegmentType.Room, 572, 64),
                                    (MemorySegmentType.Room, 572, 128),
                                    (MemorySegmentType.Room, 573, 4)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[572].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.RoomMemory[573].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BombosTablet:
                    {
                        Total = 1;
                        Name = "Tablet";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombosTabletLedge,
                            RequirementType.Tablet, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SouthOfGrove:
                    {
                        Total = 1;
                        Name = "Circle of Bushes";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SouthOfGroveLedge,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 567, 4);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[567].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.DiggingGame:
                    {
                        Total = 1;
                        Name = "Dig For Treasure";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 104, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[104].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.WitchsHut:
                    {
                        Total = 1;
                        Name = "Assistant";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWWitchArea,
                            RequirementType.Mushroom, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 32);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.WaterfallFairy:
                    {
                        Total = 2;
                        Name = "Waterfall Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.WaterfallFairy,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 552, 16),
                                    (MemorySegmentType.Room, 552, 32)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[552].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ZoraArea when index == 0:
                    {
                        Total = 1;
                        Name = "Ledge";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.LWSwim, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.LWFakeFlippersFairyRevival, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.LWFakeFlippersSplashDeletion, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.LWWaterWalk, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 129, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[129].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ZoraArea:
                    {
                        Total = 1;
                        Name = "King Zora";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Catfish:
                    {
                        Total = 1;
                        Name = "Ring of Stones";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.Catfish,
                            RequirementType.DWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 32);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SahasrahlasHut when index == 0:
                    {
                        Total = 3;
                        Name = "Back Room";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[3]
                                {
                                    (MemorySegmentType.Room, 522, 16),
                                    (MemorySegmentType.Room, 522, 32),
                                    (MemorySegmentType.Room, 522, 64)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[522].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SahasrahlasHut:
                    {
                        Total = 1;
                        Name = "Saha";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.GreenPendant, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BonkRocks:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.NorthBonkRocks,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 584, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[584].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.KingsTomb:
                    {
                        Total = 1;
                        Name = "The Crypt";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.KingsTombGrave,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 550, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[550].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.GraveyardLedge:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 567, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[567].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.DesertLedge:
                    {
                        Total = 1;
                        Name = "Ledge";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 48, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[48].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.AginahsCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 532, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[532].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.CShapedHouse:
                    {
                        Total = 1;
                        Name = "House";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 568, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[568].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.TreasureGame:
                    {
                        Total = 1;
                        Name = "Prize";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 525, 4);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[525].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BombableShack:
                    {
                        Total = 1;
                        Name = "Downstairs";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombableShack,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 524, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[524].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Blacksmith:
                    {
                        Total = 1;
                        Name = "Bring Frog Home";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BlacksmithPrison,
                            RequirementType.LightWorld, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 4);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.PurpleChest:
                    {
                        Total = 1;
                        Name = "Gary";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerPegsArea,
                            RequirementType.LightWorld, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.ItemMemory[137].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.HammerPegs:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerPegs,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 591, 4);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[591].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.BumperCave:
                    {
                        Total = 1;
                        Name = "Ledge";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCaveTop,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 74, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[74].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Dam when index == 0:
                    {
                        Total = 1;
                        Name = "Inside";
                        ModeRequirement = new ModeRequirement(entranceShuffle: false);
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 534, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[534].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Dam:
                    {
                        Total = 1;
                        Name = "Outside";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 59, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[59].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.MiniMoldormCave:
                    {
                        Total = 5;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MiniMoldormCave,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[5]
                                {
                                    (MemorySegmentType.Room, 582, 16),
                                    (MemorySegmentType.Room, 582, 32),
                                    (MemorySegmentType.Room, 582, 64),
                                    (MemorySegmentType.Room, 582, 128),
                                    (MemorySegmentType.Room, 583, 4)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[582].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.RoomMemory[583].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.IceRodCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceRodCave,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 576, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[576].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.LakeHyliaIsland:
                    {
                        Total = 1;
                        Name = "Island";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LakeHyliaIsland,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 53, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[53].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.Hobo:
                    {
                        Total = 1;
                        Name = "Under The Bridge";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWLakeHylia,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 137, 1);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.ItemMemory[137].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.MireShack:
                    {
                        Total = 2;
                        Name = "Shack";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.DWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 538, 16),
                                    (MemorySegmentType.Room, 538, 32)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[538].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.CheckerboardCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CheckerboardCave,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 589, 2);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[589].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.OldMan:
                    {
                        Total = 1;
                        Name = "Old Man";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.DarkRoomDeathMountainEntry, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 0, 1);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[0].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SpectacleRock when index == 0:
                    {
                        Total = 1;
                        Name = "Top";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 3, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[3].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SpectacleRock:
                    {
                        Total = 1;
                        Name = "Cave";
                        ModeRequirement = new ModeRequirement(entranceShuffle: false);
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 469, 4);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[469].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.EtherTablet:
                    {
                        Total = 1;
                        Name = "Tablet";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.Tablet, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 1);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SpikeCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.DWSpikeCave, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 558, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[558].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SpiralCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.SuperBunnyFallInHole, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 508, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[508].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ParadoxCave when index == 0:
                    {
                        Total = 2;
                        Name = "Bottom";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.LWNotBunny, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 510, 16),
                                    (MemorySegmentType.Room, 510, 32)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[510].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ParadoxCave:
                    {
                        Total = 5;
                        Name = "Top";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.LWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.SuperBunnyFallInHole, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.SuperBunnyFallInHole, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[5]
                                {
                                    (MemorySegmentType.Room, 478, 16),
                                    (MemorySegmentType.Room, 478, 32),
                                    (MemorySegmentType.Room, 478, 64),
                                    (MemorySegmentType.Room, 478, 128),
                                    (MemorySegmentType.Room, 479, 1)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[478].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.RoomMemory[479].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SuperBunnyCave:
                    {
                        Total = 2;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.DWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.DWNotBunny, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.SuperBunnyMirror, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.SuperBunnyFallInHole, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 496, 16),
                                    (MemorySegmentType.Room, 496, 32)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[496].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.HookshotCave when index == 0:
                    {
                        Total = 1;
                        Name = "Bonkable Chest";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.DWHookshot, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.DWDash, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 120, 128);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[120].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.HookshotCave:
                    {
                        Total = 3;
                        Name = "Back";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.DWHookshot, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.DWHover, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[3]
                                {
                                    (MemorySegmentType.Room, 120, 16),
                                    (MemorySegmentType.Room, 120, 32),
                                    (MemorySegmentType.Room, 120, 64)
                                });

                            if (result.HasValue)
                            {
                                Available = Total - result.Value;
                            }
                        };

                        _game.AutoTracker.RoomMemory[120].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.FloatingIsland:
                    {
                        Total = 1;
                        Name = "Island";
                        HasMarking = true;
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LWFloatingIsland,
                            RequirementType.None, new ModeRequirement()));
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.Inspect, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.OverworldEvent, 5, 64);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.OverworldEventMemory[5].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.MimicCave:
                    {
                        Total = 1;
                        Name = "Cave";
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MimicCave,
                            RequirementType.LWHammer, new ModeRequirement()));

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 536, 16);

                            if (result.HasValue)
                            {
                                Available = result.Value ? 0 : 1;
                            }
                        };

                        _game.AutoTracker.RoomMemory[536].PropertyChanged += OnMemoryChanged;
                    }
                    break;
            }

            Available = Total;

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

            if (ModeRequirement == null)
            {
                ModeRequirement = new ModeRequirement();
            }

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
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
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available))
            {
                UpdateAccessibility();

                if (!IsAvailable())
                {
                    CollectMarkingItem();
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
        /// Subscribes to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!UserManipulated)
            {
                AutoTrack();
            }
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items.
        /// </summary>
        private void UpdateAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)_game.RequirementNodes[connection.FromNode].Accessibility);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                {
                    continue;
                }

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
                {
                    finalAccessibility = finalConnectionAccessibility;
                }
            }

            Accessibility = finalAccessibility;

            if (Accessibility >= AccessibilityLevel.SequenceBreak)
            {
                Accessible = Available;
            }
            else
            {
                Accessible = 0;
            }
        }

        /// <summary>
        /// Collects the item represented by the marking.
        /// </summary>
        private void CollectMarkingItem()
        {
            if (Marking.HasValue)
            {
                if (Enum.TryParse(Marking.Value.ToString(), out ItemType itemType))
                {
                    _game.Items[itemType].Change(1);
                }

                Marking = null;
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
    }
}
