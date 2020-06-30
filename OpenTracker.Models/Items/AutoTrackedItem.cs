using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    public class AutoTrackedItem : IItem
    {
        private readonly IItem _item;

        private Action AutoTrack { get; }

        public ItemType Type =>
            _item.Type;
        public int Maximum =>
            _item.Maximum;
        public int Current => 
            _item.Current;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The base item to be wrapped in auto tracking.
        /// </param>
        /// <param name="autoTrack">
        /// The action delegate to perform autotracking.
        /// </param>
        /// <param name="memoryAddresses">
        /// The list of memory addresses to which to subscribe to indicate a change in autotracking.
        /// </param>
        internal AutoTrackedItem(IItem item, List<(MemorySegmentType, int)> memoryAddresses)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            AutoTrack = GetAutoTrackAction();

            foreach ((MemorySegmentType, int) address in memoryAddresses)
            {
                SubscribeToMemoryAddress(address.Item1, address.Item2);
            }

            _item.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
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
            AutoTrack();
        }

        /// <summary>
        /// Returns the action to take when autotracking this item.
        /// </summary>
        /// <returns>
        /// The action to take when autotracking this item.
        /// </returns>
        private Action GetAutoTrackAction()
        {
            return Type switch
            {
                ItemType.Sword => () =>
                {
                    if (Current > 0)
                    {
                        byte? result = Game.Instance.AutoTracker
                            .CheckMemoryByteValue(MemorySegmentType.Item, 25);

                        if (result.HasValue)
                        {
                            if (result.Value != 255)
                            {
                                SetCurrent(Math.Min(Maximum, result.Value + 1));
                            }
                        }
                    }
                },
                ItemType.Shield => () =>
                {
                    int? result = Game.Instance.AutoTracker
                        .CheckMemoryByteValue(MemorySegmentType.Item, 26);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(Math.Min(Maximum, result.Value));
                    }
                },
                ItemType.Mail => () =>
                {
                    int? result = Game.Instance.AutoTracker
                        .CheckMemoryByteValue(MemorySegmentType.Item, 27);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(Math.Min(Maximum, result.Value));
                    }
                },
                ItemType.Bow => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 0);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Arrows => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 78, 64);

                    if (result.HasValue && result.Value)
                    {
                        _item.SetCurrent(2);
                    }
                    else
                    {
                        result = Game.Instance.AutoTracker
                            .CheckMemoryByte(MemorySegmentType.Item, 55);

                        if (result.HasValue)
                        {
                            _item.SetCurrent(result.Value ? 1 : 0);
                        }
                    }
                },
                ItemType.Boomerang => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 128);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.RedBoomerang => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 64);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Hookshot => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 2);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Bomb => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 3);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.BigBomb => () =>
                {
                    int? result = Game.Instance.AutoTracker.CheckMemoryFlagArray(
                        new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 556, 16),
                            (MemorySegmentType.Room, 556, 32)
                        });

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value > 0 ? 1 : 0);
                    }
                },
                ItemType.Powder => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 16);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.MagicBat => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 128);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Mushroom => () =>
                {
                    bool? witchTurnIn = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.NPCItem, 1, 32);
                    bool? mushroom = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 32);

                    if (witchTurnIn.HasValue || mushroom.HasValue)
                    {
                        if (witchTurnIn.HasValue && witchTurnIn.Value)
                        {
                            _item.SetCurrent(2);
                        }
                        else if (mushroom.HasValue && mushroom.Value)
                        {
                            _item.SetCurrent(1);
                        }
                        else
                        {
                            _item.SetCurrent(0);
                        }
                    }
                },
                ItemType.Boots => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 21);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.FireRod => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 5);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.IceRod => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 6);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Bombos => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 7);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Ether => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 8);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Quake => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 9);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Gloves => () =>
                {
                    int? result = Game.Instance.AutoTracker
                        .CheckMemoryByteValue(MemorySegmentType.Item, 20);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(Math.Min(Maximum, result.Value));
                    }
                },
                ItemType.Lamp => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 10);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Hammer => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 11);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Flute => () =>
                {
                    int? result = Game.Instance.AutoTracker.CheckMemoryFlagArray(
                        new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Item, 76, 1),
                            (MemorySegmentType.Item, 76, 2)
                        });

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value > 0 ? 1 : 0);
                    }
                },
                ItemType.FluteActivated => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 1);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Net => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 13);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Book => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 14);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Shovel => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 4);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Flippers => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 22);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Bottle => () =>
                {
                    int? result = Game.Instance.AutoTracker.CheckMemoryByteArray(
                        new (MemorySegmentType, int)[4]
                        {
                            (MemorySegmentType.Item, 28),
                            (MemorySegmentType.Item, 29),
                            (MemorySegmentType.Item, 30),
                            (MemorySegmentType.Item, 31)
                        });

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value);
                    }
                },
                ItemType.CaneOfSomaria => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 16);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.CaneOfByrna => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 17);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Cape => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 18);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.Mirror => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 19);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.HalfMagic => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 59);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.MoonPearl => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryByte(MemorySegmentType.Item, 23);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.EPBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 32);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.DPBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 16);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.ToHBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 32);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.PoDBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 2);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.SPBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 4);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.SWBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 128);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.TTBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 16);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.IPBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 64);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.MMBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 1);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.TRBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 8);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                ItemType.GTBigKey => () =>
                {
                    bool? result = Game.Instance.AutoTracker
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 4);

                    if (result.HasValue)
                    {
                        _item.SetCurrent(result.Value ? 1 : 0);
                    }
                },
                _ => null
            };
        }

        /// <summary>
        /// Creates subscription to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to which to subscribe.
        /// </param>
        /// <param name="index">
        /// The index within the memory address list to which to subscribe.
        /// </param>
        internal void SubscribeToMemoryAddress(MemorySegmentType segment, int index)
        {
            List<MemoryAddress> memory = segment switch
            {
                MemorySegmentType.Room => Game.Instance.AutoTracker.RoomMemory,
                MemorySegmentType.OverworldEvent => Game.Instance.AutoTracker.OverworldEventMemory,
                MemorySegmentType.Item => Game.Instance.AutoTracker.ItemMemory,
                MemorySegmentType.NPCItem => Game.Instance.AutoTracker.NPCItemMemory,
                _ => throw new ArgumentOutOfRangeException(nameof(segment))
            };

            if (index >= memory.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            memory[index].PropertyChanged += OnMemoryChanged;
        }

        /// <summary>
        /// Changes the current value by the specified delta and specifying whether the obey
        /// or ignore the maximum value.
        /// </summary>
        /// <param name="delta">
        /// A 32-bit integer representing the delta value of the change.
        /// </param>
        /// <param name="ignoreMaximum">
        /// A boolean representing whether the maximum is ignored.
        /// </param>
        public void Change(int delta, bool ignoreMaximum = false)
        {
            _item.Change(delta, ignoreMaximum);
        }

        /// <summary>
        /// Sets the current value of the item.
        /// </summary>
        /// <param name="current">
        /// A 32-bit integer representing the new current value.
        /// </param>
        public void Reset()
        {
            _item.Reset();
        }

        /// <summary>
        /// Resets the item to its starting values.
        /// </summary>
        public void SetCurrent(int current = 0)
        {
            _item.SetCurrent(current);
        }
    }
}
