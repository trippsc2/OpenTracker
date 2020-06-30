using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing boss/prize sections of dungeons.
    /// </summary>
    public class BossSection : ISection
    {
        private readonly Game _game;

        public string Name { get; } = "Boss";
        public bool HasMarking { get => false; }
        public bool PrizeVisible { get; } = true;
        public ModeRequirement ModeRequirement { get; }
        public bool UserManipulated { get; set; }
        public MarkingType? Marking { get => null; set { } }
        public BossPlacement BossPlacement { get; }

        public Action AutoTrack { get; }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set
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

        private IItem _prize;
        public IItem Prize
        {
            get => _prize;
            set
            {
                if (_prize != value)
                {
                    OnPropertyChanging(nameof(Prize));
                    _prize = value;
                    OnPropertyChanged(nameof(Prize));
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
        public BossSection(Game game, LocationID iD, int index = 0)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));

            ModeRequirement = new ModeRequirement();
            Available = 1;

            switch (iD)
            {
                case LocationID.AgahnimTower:
                    {
                        Prize = _game.Items[ItemType.Aga];
                        BossPlacement = _game.BossPlacements[BossPlacementID.ATBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 133, 2);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.ItemMemory[133].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.EasternPalace:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.EPBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 401, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[401].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.DPBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 103, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[103].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.TowerOfHera:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.ToHBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 15, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[15].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.PalaceOfDarkness:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.PoDBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 181, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[181].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SwampPalace:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.SPBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 13, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[13].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.SkullWoods:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.SWBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 83, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[83].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.ThievesTown:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.TTBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 345, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[345].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.IcePalace:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.IPBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 445, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[445].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.MiseryMire:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.MMBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 289, 8);

                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[289].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.TurtleRock:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.TRBoss];

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Room, 329, 8);
                            
                            if (result.HasValue)
                            {
                                if (result.Value)
                                {
                                    Available = 0;
                                }
                                else
                                {
                                    Available = 1;
                                }
                            }
                        };

                        _game.AutoTracker.RoomMemory[329].PropertyChanged += OnMemoryChanged;
                    }
                    break;
                case LocationID.GanonsTower when index == 0:
                    {
                        PrizeVisible = false;
                        BossPlacement = _game.BossPlacements[BossPlacementID.GTBoss1];

                        Name = "Boss 1";
                    }
                    break;
                case LocationID.GanonsTower when index == 1:
                    {
                        PrizeVisible = false;
                        BossPlacement = _game.BossPlacements[BossPlacementID.GTBoss2];

                        Name = "Boss 2";
                    }
                    break;
                case LocationID.GanonsTower when index == 2:
                    {
                        PrizeVisible = false;
                        BossPlacement = _game.BossPlacements[BossPlacementID.GTBoss3];

                        Name = "Boss 3";
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        BossPlacement = _game.BossPlacements[BossPlacementID.GTFinalBoss];
                        Prize = _game.Items[ItemType.Aga2];

                        Name = "Aga";
                    }
                    break;
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changing property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            if (propertyName == nameof(Prize) && Prize != null && !IsAvailable())
            {
                Prize.Change(-1, true);
            }
            
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
            if (propertyName == nameof(Available) && Prize != null)
            {
                if (Prize.Type == ItemType.Aga)
                {
                    if (IsAvailable())
                    {
                        Prize.Change(-1, false);
                    }
                    else
                    {
                        Prize.Change(1, false);
                    }
                }
                else
                {
                    if (IsAvailable())
                    {
                        Prize.Change(-1, true);
                    }
                    else
                    {
                        Prize.Change(1, true);
                    }
                }
            }

            if (propertyName == nameof(Prize) && Prize != null && !IsAvailable())
            {
                Prize.Change(1, true);
            }
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            Available = 1;
            BossPlacement.Reset();

            if (Prize != _game.Items[ItemType.Aga] || Prize != _game.Items[ItemType.Aga2])
            {
                Prize = null;
            }
        }
    }
}
