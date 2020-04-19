using OpenTracker.Models.Enums;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Item : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly int _starting;
        private readonly Action _autoTrack;
        private readonly bool _subscribeToRoomMemory;
        private readonly bool _subscribeToItemMemory;
        private readonly bool _subscribeToNPCItemMemory;

        public ItemType Type { get; }
        public int Maximum { get; }

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
            _game = game;
            Type = itemType;

            switch (Type)
            {
                case ItemType.Bow:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 78, 128);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.SilverArrows:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 78, 64);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Boomerang:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 128);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.RedBoomerang:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 64);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Powder:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 16);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Bombos:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 7);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Ether:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 8);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Quake:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 9);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Flute:

                    _autoTrack = () =>
                    {
                        if (_game.AutoTracker.ItemMemory.Count > 76)
                        {
                            bool? result1 = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 1);
                            bool? result2 = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 2);

                            if (result1.HasValue || result2.HasValue)
                            {
                                if ((result1.HasValue && result1.Value) ||
                                    (result2.HasValue && result2.Value))
                                    Current = 1;
                                else
                                    Current = 0;
                            }
                        }
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.BigBomb:

                    _autoTrack = () =>
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

                    _subscribeToRoomMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.MagicBat:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 128);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToNPCItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.HCSmallKey:
                case ItemType.GoMode:
                case ItemType.GreenPendant:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
                case ItemType.Aga:
                    Maximum = 1;
                    break;
                case ItemType.EPBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 32);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.DPBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 16);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.ToHBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 32);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.PoDBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 2);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.SPBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 4);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.SWBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 128);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.TTBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 16);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.IPBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 64);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.MMBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 39, 1);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.TRBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 8);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.GTBigKey:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 38, 4);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.FluteActivated:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 1);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Hookshot:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 2);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.FireRod:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 5);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.IceRod:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 6);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Shovel:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryFlag(MemorySegmentType.Item, 76, 4);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Lamp:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 10);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Hammer:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 11);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Net:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 13);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Book:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 14);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.MoonPearl:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 23);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.CaneOfSomaria:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 16);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.CaneOfByrna:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 17);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Cape:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 18);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Mirror:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 19);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Boots:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 21);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Flippers:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 22);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.HalfMagic:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 59);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Bomb:

                    _autoTrack = () =>
                    {
                        bool? result = _game.AutoTracker.CheckMemoryByte(MemorySegmentType.Item, 3);

                        if (result.HasValue)
                            Current = result.Value ? 1 : 0;
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 1;
                    break;
                case ItemType.Mushroom:

                    _autoTrack = () =>
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

                    _subscribeToItemMemory = true;
                    _subscribeToNPCItemMemory = true;
                    Maximum = 2;
                    break;
                case ItemType.Gloves:

                    _autoTrack = () =>
                    {
                        int? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 20);

                        if (result.HasValue)
                            Current = Math.Min(Maximum, result.Value);
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 2;
                    break;
                case ItemType.Mail:

                    _autoTrack = () =>
                    {
                        int? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 27);

                        if (result.HasValue)
                            Current = Math.Min(Maximum, result.Value);
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 2;
                    break;
                case ItemType.Pendant:
                case ItemType.RedCrystal:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    Maximum = 2;
                    break;
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    Maximum = 7;
                    break;
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    Maximum = 3;
                    break;
                case ItemType.Shield:

                    _autoTrack = () =>
                    {
                        int? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 26);

                        if (result.HasValue)
                            Current = Math.Min(Maximum, result.Value);
                    };
                    
                    _subscribeToItemMemory = true;
                    Maximum = 3;
                    break;
                case ItemType.Bottle:

                    _autoTrack = () =>
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

                    _subscribeToItemMemory = true;
                    Maximum = 4;
                    break;
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    Maximum = 4;
                    break;
                case ItemType.Sword:

                    _autoTrack = () =>
                    {
                        if (Current > 0)
                        {
                            byte? result = _game.AutoTracker.CheckMemoryByteValue(MemorySegmentType.Item, 25);

                            if (result.HasValue)
                                Current = Math.Min(Maximum, result.Value + 1);
                        }
                    };

                    _subscribeToItemMemory = true;
                    Maximum = 5;
                    _starting = 1;
                    Current = 1;
                    break;
                case ItemType.Crystal:
                    Maximum = 5;
                    break;
                case ItemType.PoDSmallKey:
                    Maximum = 6;
                    break;
            }

            SubscribeToAutoTracker();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnMemoryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_game.AutoTracker.IsInGame())
                _autoTrack();
        }

        private void SubscribeToAutoTracker()
        {
            if (_subscribeToRoomMemory)
                _game.AutoTracker.RoomMemory.CollectionChanged += OnMemoryCollectionChanged;

            if (_subscribeToItemMemory)
                _game.AutoTracker.ItemMemory.CollectionChanged += OnMemoryCollectionChanged;

            if (_subscribeToNPCItemMemory)
                _game.AutoTracker.NPCItemMemory.CollectionChanged += OnMemoryCollectionChanged;
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
