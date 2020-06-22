using OpenTracker.Models.AutotrackerConnectors;
using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using WebSocketSharp;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class containing autotracking data and methods
    /// </summary>
    public class AutoTracker : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private USB2SNESConnector _connector;
        public USB2SNESConnector Connector
        {
            get => _connector;
            set
            {
                if (_connector != value)
                {
                    OnPropertyChanging(nameof(Connector));
                    _connector = value;
                    OnPropertyChanged(nameof(Connector));
                }
            }
        }

        private byte? _inGameStatus;
        public byte? InGameStatus
        {
            get => _inGameStatus;
            private set
            {
                if (_inGameStatus != value)
                {
                    _inGameStatus = value;
                    OnPropertyChanged(nameof(InGameStatus));
                }
            }
        }

        public List<MemoryAddress> RoomMemory { get; }
        public List<MemoryAddress> OverworldEventMemory { get; }
        public List<MemoryAddress> ItemMemory { get; }
        public List<MemoryAddress> NPCItemMemory { get; }

        public Action<string, LogLevel> MessageHandler { get; set; }
        
        /// <summary>
        /// Basic constructor
        /// </summary>
        public AutoTracker()
        {
            RoomMemory = new List<MemoryAddress>(592);
            OverworldEventMemory = new List<MemoryAddress>(130);
            ItemMemory = new List<MemoryAddress>(144);
            NPCItemMemory = new List<MemoryAddress>(2);

            for (int i = 0; i < 592; i++)
            {
                RoomMemory.Add(new MemoryAddress());

                if (i < 130)
                {
                    OverworldEventMemory.Add(new MemoryAddress());
                }

                if (i < 144)
                {
                    ItemMemory.Add(new MemoryAddress());
                }

                if (i < 2)
                {
                    NPCItemMemory.Add(new MemoryAddress());
                }
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

            if (propertyName == nameof(InGameStatus))
            {
                return;
            }
        }

        /// <summary>
        /// Creates a connection with the USB2SNES websocket at the specified URI
        ///  with the specified retry time and number of retries.
        /// </summary>
        /// <param name="uriString">
        /// The URI string for the USB2SNES websocket.
        /// </param>
        /// <param name="retryTimeInMS">
        /// The number of ms to wait between trying to confirm connection to the websocket.
        /// </param>
        /// <param name="retries">
        /// The number of retries to perform before ending.
        /// </param>
        public void Start(string uriString, int retryTimeInMS = 1000, int retries = 3)
        {
            Connector = new USB2SNESConnector(uriString, MessageHandler);

            Connector.ConnectIfNecessary();

            int i = 0;

            while (Connector != null && !Connector.Connected && i < retries)
            {
                Thread.Sleep(retryTimeInMS);
                i++;
            }
        }

        /// <summary>
        /// Disconnects from the USB2SNES websocket, disposes of the connection, and
        ///  resets all memory addresses to null or 0.
        /// </summary>
        public void Stop()
        {
            if (Connector != null)
            {
                Connector.Dispose();
                Connector = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            InGameStatus = null;

            foreach (MemoryAddress address in RoomMemory)
            {
                address.Reset();
            }

            foreach (MemoryAddress address in OverworldEventMemory)
            {
                address.Reset();
            }

            foreach (MemoryAddress address in ItemMemory)
            {
                address.Reset();
            }

            foreach (MemoryAddress address in NPCItemMemory)
            {
                address.Reset();
            }
        }

        /// <summary>
        /// Returns whether the cached value of SNES memory address that provides game status
        /// and determines whether the player is in-game.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the player is in-game.
        /// </returns>
        public bool IsInGame()
        {
            return InGameStatus.HasValue && InGameStatus.Value > 0x05 &&
                InGameStatus.Value != 0x14;
        }

        /// <summary>
        /// Updates cached value of the SNES memory address that provides game status.
        /// </summary>
        public void InGameCheck()
        {
            if (Connector != null && Connector.Connected)
            {
                try
                {
                    _connector.ReadByte(0x7e0010, out byte inGameStatus);
                    InGameStatus = inGameStatus;
                }
                catch (Exception ex)
                {
                    MessageHandler?.Invoke(ex.Message, LogLevel.Warn);
                }
            }
        }

        /// <summary>
        /// Updates cached values of all SNES memory address ranges being tracked.
        /// </summary>
        public void MemoryCheck()
        {
            if (Connector != null && Connector.Connected)
            {
                try
                {
                    if (IsInGame())
                    {
                        byte[] buffer = new byte[592];
                        _connector.Read(0x7ef000, buffer);

                        for (int i = 0; i < buffer.Length; i++)
                        {
                            RoomMemory[i].Value = buffer[i];
                        }

                        buffer = new byte[130];
                        _connector.Read(0x7ef280, buffer);

                        for (int i = 0; i < buffer.Length; i++)
                        {
                            OverworldEventMemory[i].Value = buffer[i];
                        }

                        buffer = new byte[144];
                        _connector.Read(0x7ef340, buffer);

                        for (int i = 0; i < buffer.Length; i++)
                        {
                            ItemMemory[i].Value = buffer[i];
                        }

                        buffer = new byte[2];
                        _connector.Read(0x7ef410, buffer);

                        for (int i = 0; i < buffer.Length; i++)
                        {
                            NPCItemMemory[i].Value = buffer[i];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageHandler?.Invoke(ex.Message, LogLevel.Warn);
                }
            }
        }

        /// <summary>
        /// Returns whether the memory address, specified by the memory segment and list index,
        /// has the specified bitwise flag is set.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to be checked.
        /// </param>
        /// <param name="index">
        /// The index within the memory segment list to be checked.
        /// </param>
        /// <param name="flag">
        /// The bitwise flag to be checked.
        /// </param>
        /// <returns>
        /// A nullable boolean representing whether the bitwise flag is set.
        /// </returns>
        public bool? CheckMemoryFlag(MemorySegmentType segment, int index, byte flag)
        {
            List<MemoryAddress> memory = segment switch
            {
                MemorySegmentType.Room => RoomMemory,
                MemorySegmentType.OverworldEvent => OverworldEventMemory,
                MemorySegmentType.Item => ItemMemory,
                MemorySegmentType.NPCItem => NPCItemMemory,
                _ => null
            };

            if (memory == null)
            {
                throw new ArgumentOutOfRangeException(nameof(segment));
            }

            if (memory.Count > index)
            {
                return (memory[index].Value & flag) != 0;
            }

            return null;
        }

        /// <summary>
        /// Returns the number of memory addresses, specified by the memory segment and list index,
        /// that have the specified bitwise flag set.
        /// </summary>
        /// <param name="addressFlags">
        /// An array of memory addresses, specified by memory segment and list index, and bitwise flags.
        /// </param>
        /// <returns>
        /// A nullable 32-bit integer representing the number of memory addresses that have their
        /// specified bitwise flag set.
        /// </returns>
        public int? CheckMemoryFlagArray((MemorySegmentType, int, byte)[] addressFlags)
        {
            if (addressFlags == null)
            {
                throw new ArgumentNullException(nameof(addressFlags));
            }
            
            int? result = null;

            foreach ((MemorySegmentType, int, byte) address in addressFlags)
            {
                bool? addressResult = CheckMemoryFlag(address.Item1, address.Item2, address.Item3);

                if (addressResult.HasValue)
                {
                    if (addressResult.Value)
                    {
                        if (result.HasValue)
                        {
                            result++;
                        }
                        else
                        {
                            result = 1;
                        }
                    }
                    else if (!result.HasValue)
                    {
                        result = 0;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the memory address, specified by the memory segment and list index,
        /// is greater than the specified minimum value.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to be checked.
        /// </param>
        /// <param name="index">
        /// The index within the memory segment list to be checked.
        /// </param>
        /// <param name="minValue">
        /// The minimum value to compare the memory address value against.
        /// </param>
        /// <returns>
        /// A nullable boolean representing whether the value is greater than the minimum value.
        /// </returns>
        public bool? CheckMemoryByte(MemorySegmentType segment, int index, byte minValue = 0)
        {
            List<MemoryAddress> memory = segment switch
            {
                MemorySegmentType.Room => RoomMemory,
                MemorySegmentType.OverworldEvent => OverworldEventMemory,
                MemorySegmentType.Item => ItemMemory,
                MemorySegmentType.NPCItem => NPCItemMemory,
                _ => null
            };

            if (memory == null)
            {
                throw new ArgumentOutOfRangeException(nameof(segment));
            }

            if (memory.Count > index)
            {
                return memory[index].Value > minValue;
            }

            return null;
        }

        /// <summary>
        /// Returns value the memory address, specified by the memory segment and list index.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to be checked.
        /// </param>
        /// <param name="index">
        /// The index within the memory segment list to be checked.
        /// </param>
        /// <returns>
        /// A nullable 8-bit integer representing the value of the memory address.
        /// </returns>
        public byte? CheckMemoryByteValue(MemorySegmentType segment, int index)
        {
            List<MemoryAddress> memory = segment switch
            {
                MemorySegmentType.Room => RoomMemory,
                MemorySegmentType.OverworldEvent => OverworldEventMemory,
                MemorySegmentType.Item => ItemMemory,
                MemorySegmentType.NPCItem => NPCItemMemory,
                _ => null
            };

            if (memory == null)
            {
                throw new ArgumentOutOfRangeException(nameof(segment));
            }

            if (memory.Count > index)
            {
                return memory[index].Value;
            }

            return null;
        }

        /// <summary>
        /// Returns the number of memory addresses, specified by the memory segment and list index,
        /// that have a value greater than 0.
        /// </summary>
        /// <param name="addressFlags">
        /// An array of memory addresses, specified by memory segment and list index.
        /// </param>
        /// <returns>
        /// A nullable 32-bit integer representing the number of memory addresses that have a value
        /// greater than 0.
        /// </returns>
        public int? CheckMemoryByteArray((MemorySegmentType, int)[] addresses)
        {
            if (addresses == null)
            {
                throw new ArgumentNullException(nameof(addresses));
            }

            int? result = null;

            foreach ((MemorySegmentType, int) address in addresses)
            {
                bool? addressResult = CheckMemoryByte(address.Item1, address.Item2);

                if (addressResult.HasValue)
                {
                    if (addressResult.Value)
                    {
                        if (result.HasValue)
                        {
                            result++;
                        }
                        else
                        {
                            result = 1;
                        }
                    }
                    else if (!result.HasValue)
                    {
                        result = 0;
                    }
                }
            }

            return result;
        }
    }
}
