using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using OpenTracker.Models.SNESConnectors;
using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class containing autotracking data and methods
    /// </summary>
    public class AutoTracker
    {
        private byte? _inGameStatus;

        private bool InGame =>
            _inGameStatus.HasValue && _inGameStatus.Value > 0x05 && _inGameStatus.Value != 0x14;

        public ISNESConnector SNESConnector { get; }

        public List<MemoryAddress> RoomMemory { get; }
        public List<MemoryAddress> OverworldEventMemory { get; }
        public List<MemoryAddress> ItemMemory { get; }
        public List<MemoryAddress> NPCItemMemory { get; }

        public Action<LogLevel, string> LogHandler { get; set; }
        
        /// <summary>
        /// Basic constructor
        /// </summary>
        public AutoTracker()
        {
            SNESConnector = SNESConnectorFactory.GetSNESConnector(HandleLog);

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
        /// Calls the LogHandler property and is passed to the connector to allow for log handling.
        /// </summary>
        /// <param name="logLevel">
        /// The log level of the event to be logged.
        /// </param>
        /// <param name="message">
        /// A string representing the log message.
        /// </param>
        private void HandleLog(LogLevel logLevel, string message)
        {
            LogHandler?.Invoke(logLevel, message);
        }

        /// <summary>
        /// Updates cached value of the SNES memory address that provides game status.
        /// </summary>
        public void InGameCheck()
        {
            if (SNESConnector != null && SNESConnector.Socket != null &&
                SNESConnector.Status != ConnectionStatus.Error &&
                SNESConnector.Read(0x7e0010, out byte inGameStatus))
            {
                _inGameStatus = inGameStatus;
            }
        }

        /// <summary>
        /// Updates cached values of a memory segment.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to updated.
        /// </param>
        public void MemoryCheck(MemorySegmentType segment)
        {
            if (SNESConnector != null && SNESConnector.Socket != null &&
                SNESConnector.Status != ConnectionStatus.Error && InGame)
            {
                List<MemoryAddress> memory;
                ulong startAddress;

                switch (segment)
                {
                    case MemorySegmentType.Room:
                        {
                            startAddress = 0x7ef000;
                            memory = RoomMemory;
                        }
                        break;
                    case MemorySegmentType.OverworldEvent:
                        {
                            startAddress = 0x7ef280;
                            memory = OverworldEventMemory;
                        }
                        break;
                    case MemorySegmentType.Item:
                        {
                            startAddress = 0x7ef340;
                            memory = ItemMemory;
                        }
                        break;
                    case MemorySegmentType.NPCItem:
                        {
                            startAddress = 0x7ef410;
                            memory = NPCItemMemory;
                        }
                        break;
                    default:
                        {
                            throw new ArgumentOutOfRangeException(nameof(segment));
                        }
                }

                byte[] buffer = new byte[memory.Count];

                if (SNESConnector.Read(startAddress, buffer))
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        memory[i].Value = buffer[i];
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator of devices to which can be connected.
        /// </summary>
        /// <returns>
        /// An enumerator of devices to which can be connected.
        /// </returns>
        public IEnumerable<string> GetDevices()
        {
            return SNESConnector.GetDevices();
        }

        /// <summary>
        /// Disconnects from the USB2SNES websocket, disposes of the connection, and
        ///  resets all memory addresses to null or 0.
        /// </summary>
        public void Stop()
        {
            SNESConnector.Disconnect();
            _inGameStatus = null;

            foreach (var address in RoomMemory)
            {
                address.Reset();
            }

            foreach (var address in OverworldEventMemory)
            {
                address.Reset();
            }

            foreach (var address in ItemMemory)
            {
                address.Reset();
            }

            foreach (var address in NPCItemMemory)
            {
                address.Reset();
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
                _ => throw new ArgumentOutOfRangeException(nameof(segment))
            };
            
            return (memory[index].Value & flag) != 0;
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
                _ => throw new ArgumentOutOfRangeException(nameof(segment))
            };
            
            return memory[index].Value > minValue;
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
                _ => throw new ArgumentOutOfRangeException(nameof(segment))
            };
            
            return memory[index].Value;
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
