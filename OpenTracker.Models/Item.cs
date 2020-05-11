using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Item : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly int _starting;

        public ItemType Type { get; }
        public int Maximum { get; }
        public Action AutoTrack { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _current;
        public int Current
        {
            get => _current;
            private set
            {
                if (_current != value)
                {
                    _current = value;
                    OnPropertyChanged(nameof(Current));
                }
            }
        }

        public Item(Game game, ItemType itemType)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            Type = itemType;

            switch (Type)
            {
                case ItemType.Sword:
                    {
                        _starting = 1;
                        Current = 1;
                        Maximum = 5;

                        AutoTrack = () =>
                        {
                            if (Current > 0)
                            {
                                byte? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 25);

                                if (result.HasValue)
                                {
                                    if (result.Value != 255)
                                        Current = Math.Min(Maximum, result.Value + 1);
                                }
                            }
                        };

                        _game.AutoTracker.ItemMemory[25].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Shield:
                    {
                        Maximum = 3;

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 26);

                            if (result.HasValue)
                                Current = Math.Min(Maximum, result.Value);
                        };

                        _game.AutoTracker.ItemMemory[26].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Mail:
                    {
                        Maximum = 2;

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 27);

                            if (result.HasValue)
                                Current = Math.Min(Maximum, result.Value);
                        };

                        _game.AutoTracker.ItemMemory[27].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Aga:
                case ItemType.GreenPendant:
                case ItemType.HCSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
                    {
                        Maximum = 1;
                        break;
                    }
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    {
                        Maximum = 7;
                        break;
                    }
                case ItemType.Bow:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 0);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[0].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Arrows:
                    {
                        Maximum = 2;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 78, 64);

                            if (result.HasValue && result.Value)
                                Current = 2;
                            else
                            {
                                result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 55);

                                if (result.HasValue)
                                    Current = result.Value ? 1 : 0;
                            }
                        };

                        _game.AutoTracker.ItemMemory[55].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.ItemMemory[78].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Boomerang:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 128);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.RedBoomerang:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 64);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Hookshot:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 2);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[2].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Bomb:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 3);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[3].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.BigBomb:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                            {
                                (MemorySegmentType.Room, 556, 16),
                                (MemorySegmentType.Room, 556, 32)
                            };

                            int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                            if (result.HasValue)
                                Current = result.Value > 0 ? 1 : 0;
                        };

                        _game.AutoTracker.RoomMemory[556].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Powder:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 16);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.MagicBat:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 128);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Mushroom:
                    {
                        Maximum = 2;

                        AutoTrack = () =>
                        {
                            bool? witchTurnIn = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 32);
                            bool? mushroom = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 32);

                            if (witchTurnIn.HasValue || mushroom.HasValue)
                            {
                                if (witchTurnIn.HasValue && witchTurnIn.Value)
                                    Current = 2;
                                else if (mushroom.HasValue && mushroom.Value)
                                    Current = 1;
                                else
                                    Current = 0;
                            }
                        };

                        _game.AutoTracker.NPCItemMemory[1].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Boots:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 21);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[21].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.FireRod:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 5);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[5].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.IceRod:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 6);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[6].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Bombos:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 7);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[7].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    {
                        Maximum = 3;
                        break;
                    }
                case ItemType.Ether:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 8);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[8].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Quake:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 9);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[9].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.SmallKey:
                    {
                        Maximum = 29;
                        break;
                    }
                case ItemType.Gloves:
                    {
                        Maximum = 2;

                        AutoTrack = () =>
                        {
                            int? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 20);

                            if (result.HasValue)
                                Current = Math.Min(Maximum, result.Value);
                        };

                        _game.AutoTracker.ItemMemory[20].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Lamp:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 10);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[10].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Hammer:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 11);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[11].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Flute:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            (MemorySegmentType, int, byte)[] addressFlags = new (MemorySegmentType, int, byte)[2]
                            {
                            (MemorySegmentType.Item, 76, 1),
                            (MemorySegmentType.Item, 76, 2)
                            };

                            int? result = _game.AutoTracker.CheckMemoryFlagArray(addressFlags);

                            if (result.HasValue)
                                Current = result.Value > 0 ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.FluteActivated:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 1);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Net:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 13);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[13].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Book:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 14);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[14].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Shovel:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 4);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[76].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Flippers:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 22);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[22].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Bottle:
                    {
                        Maximum = 4;

                        AutoTrack = () =>
                        {
                            (MemorySegmentType, int)[] addresses = new (MemorySegmentType, int)[4]
                            {
                                (MemorySegmentType.Item, 28),
                                (MemorySegmentType.Item, 29),
                                (MemorySegmentType.Item, 30),
                                (MemorySegmentType.Item, 31)
                            };

                            int? result = _game.AutoTracker.CheckMemoryByteArray(addresses);

                            if (result.HasValue)
                                Current = result.Value;
                        };

                        _game.AutoTracker.ItemMemory[28].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.ItemMemory[29].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.ItemMemory[30].PropertyChanged += OnMemoryChanged;
                        _game.AutoTracker.ItemMemory[31].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.CaneOfSomaria:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 16);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[16].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.CaneOfByrna:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 17);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[17].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Cape:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 18);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[18].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Mirror:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 19);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[19].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.HalfMagic:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 59);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[59].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.MoonPearl:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 23);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[23].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.Crystal:
                    {
                        Maximum = 5;
                        break;
                    }
                case ItemType.Pendant:
                case ItemType.RedCrystal:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    {
                        Maximum = 2;
                        break;
                    }
                case ItemType.PoDSmallKey:
                    {
                        Maximum = 6;
                        break;
                    }
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    {
                        Maximum = 4;
                        break;
                    }
                case ItemType.EPBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 32);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[39].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.DPBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 16);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[39].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.ToHBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 32);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[38].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.PoDBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 2);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[39].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.SPBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 4);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[39].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.SWBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 128);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[38].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.TTBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 16);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[38].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.IPBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 64);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[38].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.MMBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 1);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[39].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.TRBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 8);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[38].PropertyChanged += OnMemoryChanged;

                        break;
                    }
                case ItemType.GTBigKey:
                    {
                        Maximum = 1;

                        AutoTrack = () =>
                        {
                            bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 4);

                            if (result.HasValue)
                                Current = result.Value ? 1 : 0;
                        };

                        _game.AutoTracker.ItemMemory[38].PropertyChanged += OnMemoryChanged;

                        break;
                    }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            AutoTrack();
        }

        public void Change(int delta, bool ignoreMaximum = false)
        {
            if (ignoreMaximum)
                Current += delta;
            else
                Current = Math.Min(Maximum, Current + delta);
        }

        public void SetCurrent(int current = 0)
        {
            Current = current;
        }

        public void Reset()
        {
            Current = _starting;
        }
    }
}
