using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    public class BossSection : ISection
    {
        private readonly Game _game;

        public string Name { get; } = "Boss";
        public bool HasMarking { get => false; }
        public bool PrizeVisible { get; } = true;
        public Mode RequiredMode { get; }
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

        private Item _prize;
        public Item Prize
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

        public BossSection(Game game, LocationID iD, int index = 0)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));

            RequiredMode = new Mode();
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                    Available = 0;
                                else
                                    Available = 1;
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
                                Available = 0;
                            else
                                Available = 1;
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

        private void OnPropertyChanging(string propertyName)
        {
            if (propertyName == nameof(Prize) && Prize != null && !IsAvailable())
                Prize.Change(-1, true);

            if (PropertyChanging != null)
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Available) && Prize != null)
            {
                if (Prize.Type == ItemType.Aga)
                {
                    if (IsAvailable())
                        Prize.Change(-1, false);
                    else
                        Prize.Change(1, false);
                }
                else
                {
                    if (IsAvailable())
                        Prize.Change(-1, true);
                    else
                        Prize.Change(1, true);
                }
            }

            if (propertyName == nameof(Prize) && Prize != null && !IsAvailable())
                Prize.Change(1, true);
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!UserManipulated)
                AutoTrack();
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
            Available = 1;
            BossPlacement.Reset();

            if (Prize != _game.Items[ItemType.Aga] || Prize != _game.Items[ItemType.Aga2])
                Prize = null;
        }
    }
}
